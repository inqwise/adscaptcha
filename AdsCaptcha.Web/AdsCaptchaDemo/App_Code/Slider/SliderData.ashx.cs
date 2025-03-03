using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using Inqwise.AdsCaptcha.API.Slider;
using Inqwise.AdsCaptchaDemo.Handlers;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.Workflows;

namespace Inqwise.AdsCaptchaDemo.Slider
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SliderData_ashx : BaseHandler<JsonObject>, IRequiresSessionState
    {
        protected override JsonObject Process(HttpContext context)
        {
            return GetSliderData(context);
        }

        private JsonObject GetSliderData(HttpContext context)
        {
            var output = new JsonObject();
            HttpRequest request = context.Request;
            string clientIp = null;
            var errors = new AdsCaptchaOperationResults();

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
                // Get request
                var actualRequestResult = RequestsManager.Get(cid, isDemo:true);
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
            }

            if (errors.IsEmpty)
            {
                if (null == captchaRequest)
                {
                    context.Response.AddHeader("Ads-Request", "Sync");
                    var requestArgs = new NewRequestArgs();
                    requestArgs.PreviousRequestGuid = cid;
                    //requestArgs.SetCaptcha(captcha);
                    requestArgs.SessionId = context.Session.SessionID;
                    requestArgs.ClientIp = clientIp;
                    requestArgs.ReferrerUrl = request.GetUrlReferrer();
                    requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                    requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                    requestArgs.ImageType = ImageType.Demo;

                    // Width and Height
                    int sliderWidth;
                    int sliderHeight;

                    if (int.TryParse(request.QueryString["w"], out sliderWidth))
                    {
                        requestArgs.Width = sliderWidth;
                    }

                    if (int.TryParse(request.QueryString["h"], out sliderHeight))
                    {
                        requestArgs.Height = sliderHeight;
                    }

                    // AdId
                    int adId;
                    if (int.TryParse(context.Request.QueryString["adid"], out adId))
                    {
                        //requestArgs.AdId = adId;
                    }

                    requestArgs.EffectType = RequestsManager.GetEffectType(CaptchaSecurityLevel.Low);

                    var insertResult = NewRequestFlow.Instance(requestArgs).Process();
                    if (null != insertResult)
                    {
                        errors.Add(insertResult);
                    }
                    else
                    {
                        captchaRequest = requestArgs.Request;
                        output.Put(CHALLANGE_JO_KEY, captchaRequest.RequestGuid);
                    }
                }
            }
            

            if (errors.IsEmpty)
            {
                RequestsManager.Touch(captchaRequest, null, null, clientIp);
                output.Put(COUNT_OF_FRAMES_JO_KEY, captchaRequest.CountOfFrames);
                output.Put("cid", captchaRequest.RequestGuid);
                //output.Put("clickUrl", string.Format("{0}/click.aspx?cid={1}", API_URL, captchaRequest.RequestGuid));
                //output.Put("likeUrl", captchaRequest.LikeUrl ?? String.Empty);
                //output.Put("spriteUrl", captchaRequest.SpriteUrl);
            }
            else
            {
                output = errors.ToJson();
            }

            return output;
        }
    }
}