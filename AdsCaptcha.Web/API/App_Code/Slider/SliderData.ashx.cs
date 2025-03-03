using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using API.Handlers;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.Workflows;

namespace Inqwise.AdsCaptcha.API.Slider
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SliderData_ashx : BaseHandler<JsonObject>, IRequiresSessionState
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        protected override JsonObject Process(HttpContext context)
        {
            return GetSliderData(context);
        }

        private JsonObject GetSliderData(HttpContext context)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var output = new JsonObject();
            HttpRequest request = context.Request;
            long? captchaId = null;
            string publicKey = null;
            string clientIp = null;
            var errors = new AdsCaptchaOperationResults();
            string imageId = null;
            string adExternalId = null;
            
            var imageType = ImageType.Undefined;
            int imageTypeId;
            if (int.TryParse(request["imageTypeId"], out imageTypeId))
            {
                imageType = (ImageType)imageTypeId;
            }

            string visitorUid = request["datr"];

            switch (imageType)
            {
                case ImageType.Demo:
                case ImageType.Random:
                    break;
                case ImageType.Temp:
                    imageId = request["imageId"];
                    if (string.IsNullOrEmpty(imageId) || string.IsNullOrEmpty(imageId = HttpUtility.UrlDecode(imageId)))
                    {
                        errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NoResults));
                    }
                    break;
                case ImageType.Ad:
                    adExternalId = request["adId"];
                    if (string.IsNullOrEmpty(adExternalId))
                    {
                        errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.AdidNotSet));
                    }
                    break;
                //case ImageType.Random:
                //    break;
                default:
                    {
                        string rawCaptchaId;
                        if (string.IsNullOrEmpty(rawCaptchaId = request.QueryString["CaptchaId"]))
                        {
                            errors.Add(AdsCaptchaErrors.CaptchaidNotSet);
                        }
                        else
                        {
                            int tmpCaptchaId;
                            if (int.TryParse(HttpUtility.HtmlEncode(rawCaptchaId), out tmpCaptchaId))
                            {
                                captchaId = tmpCaptchaId;
                            }
                            else
                            {
                                errors.Add(AdsCaptchaErrors.CaptchaidInvalid);
                            }
                        }

                        // Check PublicKey.
                        if (string.IsNullOrEmpty(publicKey = request.QueryString["PublicKey"]))
                        {
                            errors.Add(AdsCaptchaErrors.PublickeyNotSet);
                        }
                        else if (errors.IsEmpty)
                        {
                            publicKey = HttpUtility.HtmlEncode(publicKey).Trim();
                        } 
                    }
                    break;
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 100)
            {
                Log.Warn("SliderData: 1: {0}", sw.ElapsedMilliseconds);
            }


            // Check challenge id.
            string cid = HttpUtility.HtmlEncode(request.QueryString["cid"]);

            if (errors.IsEmpty)
            {
                clientIp = request.ClientIpFromRequest(true);
            }

            // Get Request
            IRequestBase captchaRequest = null;
            if (errors.IsEmpty && null != cid)
            {
                cid = cid.Trim();

                using (CacheBuilder.GetLocker().AcquireLock(cid, TimeSpan.FromSeconds(10))){} // Check if not in process
                // Get request
                if (imageType == ImageType.Temp || imageType == ImageType.Ad)
                {
                    captchaRequest = GetTempRequest(cid, errors);
                }
                else
                {
                    captchaRequest = GetCaptchaRequest(cid, imageType, errors);
                }

                if (null != captchaRequest)
                {
                    if (null == captchaRequest.SpriteUrl || (imageType != ImageType.Temp && captchaRequest.CountOfTouches > 0)) // Image not found or request already used
                    {
                        captchaRequest = null;
                    }
                }
            }

            ICaptcha captcha = null;
            NewRequestArgs requestArgs = null;
            if (errors.IsEmpty && null == captchaRequest)
            {
                requestArgs = new NewRequestArgs();
                requestArgs.ImageType = imageType;
                requestArgs.CaptchaId = captchaId;

                if (imageType == ImageType.Demo || imageType == ImageType.Temp || imageType == ImageType.Random)
                {
                    int width;
                    int height;

                    if (int.TryParse(context.Request.QueryString["w"], out width))
                    {
                        requestArgs.Width = width;
                    }

                    if (int.TryParse(context.Request.QueryString["h"], out height))
                    {
                        requestArgs.Height = height;
                    } 
                }

                switch (imageType)
                {
                    case ImageType.Temp:
                        requestArgs.ImagePath = imageId;
                        break;
                    case ImageType.Ad:
                        
                        if (errors.IsEmpty)
                        {
                            var adResult = AdsManager.Get(adExternalId);
                            if (adResult.HasError)
                            {
                                errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.AdidInvalid));
                            }
                            else
                            {
                                requestArgs.SetAd(adResult.Value);
                            }
                        }

                        break;
                }

                if (requestArgs.CaptchaId.HasValue)
                {
                    var captchaResult = CaptchasManager.Get(requestArgs.CaptchaId.Value, publicKey: publicKey, fromCache: true,
                                                                    defaultNotExist: true);
                    if (captchaResult.HasError)
                    {
                        errors.Add(captchaResult);
                    }
                    else
                    {
                        captcha = captchaResult.Value;
                    }
                }
            }

            if (errors.IsEmpty)
            {
                if (null == captchaRequest)
                {
                    context.Response.AddHeader("Ads-Request", "Sync");
                    requestArgs.PreviousRequestGuid = cid;
                    if (null != captcha)
                    {
                        requestArgs.SetCaptcha(captcha);
                        output.Put("feedbackId", captcha.FeedbackId ?? ApplicationConfiguration.DefaultFeedbackId.Value);
                    }
                    requestArgs.SessionId = context.Session.SessionID;
                    requestArgs.ClientIp = clientIp;
                    requestArgs.ReferrerUrl = request.GetUrlReferrer();
                    requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                    requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                    requestArgs.EffectType = RequestsManager.GetEffectType(null == captcha ? CaptchaSecurityLevel.Low : captcha.SecurityLevel);
                    requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                         requestArgs.SecurityLevel);
                    requestArgs.VisitorUid = visitorUid;

                    var insertResult = NewRequestFlow.Instance(requestArgs).Process();
                    if (null != insertResult)
                    {
                        errors.Add(insertResult);
                    }
                    else
                    {
                        captchaRequest = requestArgs.Request;
                        if (null != captcha) // temp
                        {
                            output.Put(CAPTCHA_ID_JO_KEY, captcha.Id);
                            output.Put(PUBLIC_KEY_JO_KEY, captcha.PublicKey);
                            output.Put("feedbackId", captcha.FeedbackId ?? ApplicationConfiguration.DefaultFeedbackId.Value);
                        }
                        output.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                    }
                }
            }

            if (errors.IsEmpty)
            {
                if (null == captchaRequest.VisitorUid)
                {
                    captchaRequest.VisitorUid = visitorUid;
                }

                if (imageType != ImageType.Temp)
                {
                    RequestsManager.Touch(captchaRequest, null, null, clientIp); 
                }

                output.Put(COUNT_OF_FRAMES_JO_KEY, captchaRequest.CountOfFrames);
                output.Put("cid", captchaRequest.RequestGuid);

                if (null != captchaRequest.ClickUrl)
                {
                    output.Put("clickUrl", captchaRequest.ClickUrl); 
                }

                if (null != captchaRequest.LikeUrl)
                {
                    output.Put("likeUrl", captchaRequest.LikeUrl);  
                }
                
                output.Put("spriteUrl", captchaRequest.SpriteUrl);
                if (null != captchaRequest.SpriteBase64LowQuality)
                {
                    output.Put("spriteBase64", captchaRequest.SpriteBase64LowQuality);
                }

                if (null != captchaRequest.PhotoBy)
                {
                    output.Put("photoBy", captchaRequest.PhotoBy);
                }

                if (null != captchaRequest.PhotoByUrl)
                {
                    output.Put("photoByUrl", captchaRequest.PhotoByUrl);
                }

                //output.Put("showAd", string.IsNullOrEmpty(captchaRequest.ClickUrl) && imageType == ImageType.Undefined && (null == captcha || captcha.SourceType == CaptchaSourceType.Commercial));
            }
            else
            {
                output = errors.ToJson();
            }

            return output;
        }

        private static IRequestBase GetCaptchaRequest(string cid, ImageType imageType, AdsCaptchaOperationResults errors)
        {
            IRequestBase captchaRequest = null;
            var actualRequestResult = RequestsManager.Get(cid, isDemo: imageType == ImageType.Demo);
            if (actualRequestResult.HasError)
            {
                // Cache timeout or not found
                if (actualRequestResult.Error != AdsCaptchaErrors.NoResults)
                {
                    errors.Add(actualRequestResult);
                }
                //Log.Warn("GetSliderData: No request #'{0}' found", cid);
            }
            else
            {
                captchaRequest = actualRequestResult.Value;
            }
            return captchaRequest;
        }

        private static IRequestBase GetTempRequest(string cid, AdsCaptchaOperationResults errors)
        {
            IRequestBase captchaRequest = null;
            var actualRequestResult = RequestsManager.GetTemp(cid);
            if (actualRequestResult.HasError)
            {
                // Cache timeout or not found
                if (actualRequestResult.Error != AdsCaptchaErrors.NoResults)
                {
                    errors.Add(actualRequestResult);
                }
            }
            else
            {
                captchaRequest = actualRequestResult.Value;
            }

            return captchaRequest;
        }
    }
}