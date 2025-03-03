using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web.Services;
using API.Handlers;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.Workflows;
using Inqwise.AdsCaptcha.Dal.ElasticSearch;


namespace Inqwise.AdsCaptcha.API.Slider
{
    /// <summary>
    /// Summary description for Get_New
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Get_ashx : BaseHandler<string>, System.Web.SessionState.IRequiresSessionState
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private string IMAGE_TYPE_ID_JO_KEY = "imageTypeId";
        private const string AD_ID_PARAM = "adId";
        private static readonly Lazy<string> StaticContentUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["StaticContentUrl"]);
        private const string LANGUAGE_JO_KEY = "language";
        private const string BASE_URL_KEY = "baseUrl";
        private const string DEMO_KEY = "demo";
        private const string STATIC_CONTENT_URL_JO_KEY = "imagesUrl";
        private static readonly Lazy<string> JsLoaderUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["JsLoaderUrl"]);
        private static readonly Lazy<bool> RequestSync = new Lazy<bool>(() => "true" == ConfigurationManager.AppSettings["Request.Sync"]);
        private const string IMAGE_ID_PARAM = "imageId";
        private static readonly long[] INJECT_BANNER_CAPTCHA_IDS = new long[] {};

        protected override string Process(HttpContext context)
        {
            string result;
            var sb = new StringBuilder();

            var responseType = (context.Request["responseType"] ?? "js").ToLower();

            bool injectBanner;
            JsonObject content = Get(context, out injectBanner);

            switch (responseType)
            {
                case "json":
                    ContextType = "application/json";
                    result = content.ToString();
                break;

                case "js":
                    ContextType = "application/javascript";
                    sb.AppendFormat("var Inqwise={0}", content);
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendFormat("document.write('<' + 'scr' + 'ipt type=\"text/javascript\" s' + 'rc=\"{0}\" charset=\"utf-8\"><' + '/scr' + 'ipt' + '>');", JsLoaderUrl.Value);
                    if (injectBanner)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("document.write('<' + 'scr' + 'ipt type=\"text/javascript\">clicksor_enable_pop = false;clicksor_enable_adhere = false;clicksor_enable_text_link =false;durl = \"\";clicksor_layer_border_color = \"\";clicksor_layer_ad_bg =\"\";clicksor_layer_ad_link_color = \"\";clicksor_layer_ad_text_color = \"\";clicksor_text_link_bg = \"\";clicksor_text_link_color = \"\";clicksor_enable_layer_pop = false;clicksor_banner_image_banner = true; clicksor_banner_text_banner = true;<' + '/scr' + 'ipt' + '>');");
                        sb.AppendFormat("document.write('<' + 'scr' + 'ipt type=\"text/javascript\" s' + 'rc=\"http://ads.clicksor.com/newServing/showAd.php?nid=1&amp;pid=315433&amp;sid=520017\"><' + '/scr' + 'ipt' + '>');");
                    }
                    sb.AppendLine();
                    result = sb.ToString();
                break;

                default:
                    throw new NotImplementedException();
            }


            return result;
        }

        private JsonObject Get(HttpContext context, out bool injectBanner)
        {
            HttpRequest request = context.Request;
            injectBanner = false;
            var imageType = ImageType.Undefined;
            int imageTypeId;
            if (int.TryParse(request[IMAGE_TYPE_ID_JO_KEY], out imageTypeId))
            {
                imageType = (ImageType)imageTypeId;
            }

            JsonObject jo;
            switch (imageType)
            {
                case ImageType.Temp:
                    jo = ProcessTemp(context, request);
                    break;
                case ImageType.Demo:
                    jo = ProcessDemo(context, request);
                    break;
                case ImageType.Random:
                    jo = ProcessRandom(context, request);
                    break;
                case ImageType.Ad:
                    jo = ProcessAd(context, request);
                    break;
                default:
                    jo = Process(context, request, out injectBanner);
                    break;
            }
            
            

            return jo;
        }

        private JsonObject ProcessAd(HttpContext context, HttpRequest request)
        {
            var server = context.Server;
            var errors = new AdsCaptchaOperationResults();
            var requestArgs = new NewRequestArgs();

            string adExternalId = request[AD_ID_PARAM];
            if (string.IsNullOrEmpty(adExternalId))
            {
                errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.AdidNotSet));
            }

            int width = 0;
            int height = 0;
            int securityLevelId = (int) CaptchaSecurityLevel.Undefined;

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["w"], out width))
            {
                requestArgs.Width = width;
            }

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["h"], out height))
            {
                requestArgs.Height = height;
            }

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["securityLevelId"], out securityLevelId))
            {
                requestArgs.SecurityLevel = (CaptchaSecurityLevel)securityLevelId;
            }

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

            JsonObject jo;
            if (errors.IsEmpty)
            {
                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.ImageType = ImageType.Ad;
                requestArgs.EffectType = RequestsManager.GetEffectType(requestArgs.SecurityLevel);
                requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                     requestArgs.SecurityLevel);

                var apiUrl = API_URL;
                jo = new JsonObject();
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, width);
                jo.Put(HEIGHT_JO_KEY, height);
                jo.Put(LANGUAGE_JO_KEY, "en");
                jo.Put(BASE_URL_KEY, apiUrl);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(IMAGE_TYPE_ID_JO_KEY, (int)ImageType.Ad);
                jo.Put("adId", adExternalId);
                jo.Put(STATIC_CONTENT_URL_JO_KEY, StaticContentUrl.Value);
                jo.Put("feedbackId", ApplicationConfiguration.DefaultFeedbackId.Value);

                var t = Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
                if (RequestSync.Value) t.Wait();
            }
            else
            {
                jo = errors.ToJson();
            }

            return jo;
        }

        private JsonObject ProcessRandom(HttpContext context, HttpRequest request)
        {
            var errors = new AdsCaptchaOperationResults();
            var requestArgs = new NewRequestArgs();

            int width;
            int height;
            int securityLevelId = (int)CaptchaSecurityLevel.Undefined;

            if (int.TryParse(context.Request.QueryString["w"], out width))
            {
                requestArgs.Width = width;
            }

            if (int.TryParse(context.Request.QueryString["h"], out height))
            {
                requestArgs.Height = height;
            }

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["securityLevelId"], out securityLevelId))
            {
                requestArgs.SecurityLevel = (CaptchaSecurityLevel)securityLevelId;
            }

            JsonObject jo;
            if (errors.IsEmpty)
            {

                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.ImageType = ImageType.Random;

                requestArgs.EffectType = RequestsManager.GetEffectType(requestArgs.SecurityLevel);
                requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                     requestArgs.SecurityLevel);

                var apiUrl = API_URL;
                jo = new JsonObject();
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, width);
                jo.Put(HEIGHT_JO_KEY, height);
                jo.Put(LANGUAGE_JO_KEY, "en");
                jo.Put(BASE_URL_KEY, apiUrl);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(STATIC_CONTENT_URL_JO_KEY, StaticContentUrl.Value);
                jo.Put(IMAGE_TYPE_ID_JO_KEY, (int)ImageType.Random);
                jo.Put("feedbackId", ApplicationConfiguration.DefaultFeedbackId.Value);

                var t = Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
                if (RequestSync.Value) t.Wait();
            }
            else
            {
                jo = errors.ToJson();
            }

            return jo;
        }

        private JsonObject ProcessDemo(HttpContext context, HttpRequest request)
        {
            var errors = new AdsCaptchaOperationResults();
            var requestArgs = new NewRequestArgs();

            int width;
            int height;

            int securityLevelId = (int)CaptchaSecurityLevel.Undefined;

            if (int.TryParse(context.Request.QueryString["w"], out width))
            {
                requestArgs.Width = width;
            }

            if (int.TryParse(context.Request.QueryString["h"], out height))
            {
                requestArgs.Height = height;
            }

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["securityLevelId"], out securityLevelId))
            {
                requestArgs.SecurityLevel = (CaptchaSecurityLevel)securityLevelId;
            }

            JsonObject jo;
            if (errors.IsEmpty)
            {

                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.ImageType = ImageType.Demo;

                requestArgs.EffectType = RequestsManager.GetEffectType(requestArgs.SecurityLevel);
                requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                     requestArgs.SecurityLevel);

                var apiUrl = API_URL;
                jo = new JsonObject();
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, width);
                jo.Put(HEIGHT_JO_KEY, height);
                jo.Put(LANGUAGE_JO_KEY, "en");
                jo.Put(BASE_URL_KEY, apiUrl);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(STATIC_CONTENT_URL_JO_KEY, StaticContentUrl.Value);
                jo.Put(IMAGE_TYPE_ID_JO_KEY, (int)ImageType.Demo);
                jo.Put("feedbackId", ApplicationConfiguration.DefaultFeedbackId.Value);

                var t = Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
                if (RequestSync.Value) t.Wait();
            }
            else
            {
                jo = errors.ToJson();
            }

            return jo;
        }

        private JsonObject ProcessTemp(HttpContext context, HttpRequest request)
        {
            var server = context.Server;
            var errors = new AdsCaptchaOperationResults();
            var requestArgs = new NewRequestArgs();
            string imageId = request[IMAGE_ID_PARAM];
            if (string.IsNullOrEmpty(imageId) || string.IsNullOrEmpty(imageId = server.UrlDecode(imageId)))
            {
                errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NoResults));
            }

            int width = 0;
            int height = 0;
            int securityLevelId = (int)CaptchaSecurityLevel.Undefined;

            if (int.TryParse(context.Request.QueryString["w"], out width))
            {
                requestArgs.Width = width;
            }

            if (int.TryParse(context.Request.QueryString["h"], out height))
            {
                requestArgs.Height = height;
            }

            if (errors.IsEmpty && int.TryParse(context.Request.QueryString["securityLevelId"], out securityLevelId))
            {
                requestArgs.SecurityLevel = (CaptchaSecurityLevel)securityLevelId;
            }

            JsonObject jo;
            if (errors.IsEmpty)
            {
                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.ImageType = ImageType.Temp;
                requestArgs.ImagePath = imageId;

                requestArgs.EffectType = RequestsManager.GetEffectType(requestArgs.SecurityLevel);
                requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                     requestArgs.SecurityLevel);

                var apiUrl = API_URL;
                jo = new JsonObject();
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, width);
                jo.Put(HEIGHT_JO_KEY, height);
                jo.Put(LANGUAGE_JO_KEY, "en");
                jo.Put(BASE_URL_KEY, apiUrl);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(IMAGE_TYPE_ID_JO_KEY, (int)ImageType.Temp);
                jo.Put(IMAGE_ID_PARAM, HttpUtility.UrlEncode(imageId));
                jo.Put(STATIC_CONTENT_URL_JO_KEY, StaticContentUrl.Value);
                jo.Put("feedbackId", ApplicationConfiguration.DefaultFeedbackId.Value);

                var t = Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
                if (RequestSync.Value) t.Wait();
            }
            else
            {
                jo = errors.ToJson();
            }

            return jo;
        }

        private static JsonObject Process(HttpContext context, HttpRequest request, out bool injectBanner)
        {
            Stopwatch sw = Stopwatch.StartNew();
            injectBanner = false;
            var errors = new AdsCaptchaOperationResults();
            var requestArgs = new NewRequestArgs();

            string rawCaptchaId;
            if (string.IsNullOrEmpty(rawCaptchaId = request.QueryString[CAPTCHA_ID_QS_KEY]))
            {
                errors.Add(AdsCaptchaErrors.CaptchaidNotSet);
            }
            else
            {
                int tmpCaptchaId;
                if (int.TryParse(HttpUtility.HtmlEncode(rawCaptchaId).Trim(), out tmpCaptchaId))
                {
                    requestArgs.CaptchaId = tmpCaptchaId;
                }
                else
                {
                    errors.Add(AdsCaptchaErrors.CaptchaidInvalid);
                }
            }

            // Check PublicKey.
            if (string.IsNullOrEmpty(requestArgs.PublicKey = request.QueryString[PUBLIC_KEY_QS_KEY]))
            {
                errors.Add(AdsCaptchaErrors.PublickeyNotSet);
            }
            else
            {
                requestArgs.PublicKey = HttpUtility.HtmlEncode(requestArgs.PublicKey).Trim();
            }
            
            ICaptcha captcha = null;
            if (errors.IsEmpty)
            {
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                var captchaResult = CaptchasManager.Get(requestArgs.CaptchaId.Value, publicKey: requestArgs.PublicKey,
                                                        fromCache: true, defaultNotExist: true);
                if (captchaResult.HasError)
                {
                    errors.Add(captchaResult);
                }
                else
                {
                    captcha = captchaResult.Value;
                    requestArgs.SetCaptcha(captcha);
                }
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 500)
            {
                Log.Warn("Get: 1: {0}", sw.ElapsedMilliseconds);
            }
            sw.Restart();

            if (errors.IsEmpty && null != captcha)
            {
                injectBanner = (INJECT_BANNER_CAPTCHA_IDS.Contains(captcha.Id));
            }

            JsonObject jo;
            
            if (errors.IsEmpty)
            {
                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.EffectType = RequestsManager.GetEffectType(captcha.SecurityLevel);
                requestArgs.DifficultyLevelId = RequestsManager.GetDifficultyLevelId(requestArgs.EffectType,
                                                                                     requestArgs.SecurityLevel);

                sw.Stop();
                if (sw.ElapsedMilliseconds > 100)
                {
                    Log.Warn("Get: 2: {0}", sw.ElapsedMilliseconds);
                }
                sw.Restart();

                jo = new JsonObject();
                jo.Put(CAPTCHA_ID_JO_KEY, captcha.Id);
                jo.Put(PUBLIC_KEY_JO_KEY, captcha.PublicKey);
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, captcha.MaxWidth);
                jo.Put(HEIGHT_JO_KEY, captcha.MaxHeight);
                jo.Put(LANGUAGE_JO_KEY, captcha.Language);
                jo.Put(BASE_URL_KEY, API_URL);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(STATIC_CONTENT_URL_JO_KEY, StaticContentUrl.Value);
                jo.Put("feedbackId", captcha.FeedbackId ?? ApplicationConfiguration.DefaultFeedbackId.Value);
                
                var t = Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
                if (RequestSync.Value) t.Wait();

                sw.Stop();
                if (sw.ElapsedMilliseconds > 100)
                {
                    Log.Warn("Get: 3: {0}", sw.ElapsedMilliseconds);
                }
            }
            else
            {
                jo = errors.ToJson();
            }
            return jo;
        }
    }

}