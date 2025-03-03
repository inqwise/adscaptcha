using System.Threading.Tasks;
using System.Web.Services;
using Inqwise.AdsCaptcha.API.Slider;
using Inqwise.AdsCaptchaDemo.Handlers;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;
using System;
using System.Text;
using System.Web;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.Workflows;

namespace Inqwise.AdsCaptchaDemo.Slider
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Get_ashx : BaseHandler<string>, System.Web.SessionState.IRequiresSessionState
    {
        //private const int DEFAULT_WIDTH = 300;
        //private const int DEFAULT_HEIGHT = 250;
        private const string STATIC_CONTENT_URL = "//s3.amazonaws.com/Inqwise/static/slider/";
        private const string LANGUAGE_JO_KEY = "language";
        private const string BASE_URL_KEY = "baseUrl";
        private const string DEMO_KEY = "demo";
        private const string STATIC_CONTENT_URL_JO_KEY = "imagesUrl";
        private const string DEFAULT_CLICK_URL = @"http://www.Inqwise.com";
        private const string DEFAULT_LIKE_URL = @"http://www.Inqwise.com";

        //private const string LOADER_JS_URL = "//192.168.1.27:8080/Inqwise/scripts/Inqwise.loader.min.js?ver=1";
        private const string LOADER_JS_URL = "//Inqwise.dev.s3.amazonaws.com/static/slider/scripts/Inqwise.loader.min.js?ver=2";

        protected override string Process(HttpContext context)
        {
            ContextType = "application/javascript";
            Callback = null;
            var sb = new StringBuilder();
            sb.AppendFormat("var Inqwise={0}", Get(context));
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("document.write('<' + 'scr' + 'ipt type=\"text/javascript\" s' + 'rc=\"{0}\"><' + '/scr' + 'ipt' + '>');", LOADER_JS_URL);
            sb.AppendLine();
            return sb.ToString();
        }

        private JsonObject Get(HttpContext context)
        {
            HttpRequest request = context.Request;
            var errors = new AdsCaptchaOperationResults();
            
            var requestArgs = new NewRequestArgs();

            int width;
            int height;
            int adId;
            //string adId = string.IsNullOrEmpty(context.Request.QueryString["adid"]) ? string.Empty : context.Request.QueryString["adid"];
            string demoUrl = string.IsNullOrEmpty(context.Request.QueryString["demo"]) ? string.Empty : context.Request.QueryString["demo"];
            string clickUrl = DEFAULT_CLICK_URL;
            string likeUrl = DEFAULT_LIKE_URL;

            if (int.TryParse(context.Request.QueryString["w"], out width))
            {
                requestArgs.Width = width;
            }

            if (int.TryParse(context.Request.QueryString["h"], out height))
            {
                requestArgs.Height = height;
            }

            if (int.TryParse(context.Request.QueryString["adid"], out adId))
            {
                //requestArgs.AdId = adId;
            }

            /*
            if (demoUrl != string.Empty)
            {
                using (var ent = new AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities())
                {
                    var demo = ent.T_DEMOS.FirstOrDefault(d => d.DemoUrlName == demourl);
                    if (demo != null)
                    {
                        clickUrl = demo.ClickUrl ?? clickUrl;
                        likeUrl = demo.LikeUrl ?? likeUrl;
                    }
                }
            }
            */

            JsonObject jo;
            if(errors.IsEmpty){

                requestArgs.SessionId = context.Session.SessionID;
                requestArgs.ClientIp = request.ClientIpFromRequest(true);
                requestArgs.ReferrerUrl = request.GetUrlReferrer();
                requestArgs.CountOfFrames = RequestsManager.GenerateCountOfFrames();
                requestArgs.CorrectIndex = RequestsManager.GenerateCorrectIndex(requestArgs.CountOfFrames);
                requestArgs.ImageType = ImageType.Demo;

                requestArgs.EffectType = RequestsManager.GetEffectType(CaptchaSecurityLevel.Low);

                var apiUrl = API_URL;
                jo = new JsonObject();
                jo.Put(CHALLANGE_JO_KEY, requestArgs.RequestGuid);
                jo.Put(WIDTH_JO_KEY, width);
                jo.Put(HEIGHT_JO_KEY, height);
                jo.Put(LANGUAGE_JO_KEY, "en");
                jo.Put(BASE_URL_KEY, apiUrl);
                jo.Put(DEMO_KEY, string.Empty);
                jo.Put(COUNT_OF_FRAMES_JO_KEY, requestArgs.CountOfFrames);
                jo.Put(STATIC_CONTENT_URL_JO_KEY, STATIC_CONTENT_URL);

                Task.Run(new Func<AdsCaptchaOperationResult>(NewRequestFlow.Instance(requestArgs).Process));
       
            } 
            else 
            {
                jo = errors.ToJson();
            }

            return jo;
        }
    }
}