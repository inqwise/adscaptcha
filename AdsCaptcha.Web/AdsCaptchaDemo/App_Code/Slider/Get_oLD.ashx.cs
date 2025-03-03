/*
using System.Web;
using System.Linq;
using Jayrock.Json;

namespace Inqwise.AdsCaptchaDemo.Slider
{
    public class GetHandler_Old : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";

            if (context.Request.Browser.Crawler)
            {
                context.Response.Write("");
                return;
            }

            ImagesDAL.GetInstance();

            string width = context.Request.QueryString["w"];
            string height = context.Request.QueryString["h"];

            string demourl = string.IsNullOrEmpty(context.Request.QueryString["demo"]) ? string.Empty : context.Request.QueryString["demo"];

            string adid = string.IsNullOrEmpty(context.Request.QueryString["adid"]) ? string.Empty : context.Request.QueryString["adid"];

            string clickUrl = @"http://www.Inqwise.com";
            string likeUrl = @"http://www.Inqwise.com";

            if (demourl != string.Empty)
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

            var apiUrl = System.Configuration.ConfigurationManager.AppSettings["API"];
            var jo = new JsonObject();
            jo.Put("captchaId", 111);
            jo.Put("publicKey", 111);
            jo.Put("challenge", string.Empty);
            jo.Put("hideChallenge", false);
            jo.Put("ad", true);
            jo.Put("adId", adid);
            jo.Put("clickUrl", clickUrl);
            jo.Put("width", width);
            jo.Put("height", height);
            jo.Put("type", 101);
            jo.Put("direction", string.Empty);
            jo.Put("language", "en");
            jo.Put("logo", false);
            jo.Put("server", apiUrl);
            jo.Put("adServer", string.Empty);
            jo.Put("demo", string.Empty);
            jo.Put("likeUrl", "");
            jo.Put("baseUrl", apiUrl);

            context.Response.Write("var Inqwise = " + jo);
            context.Response.Write("\n\n" + "document.write('<' + 'scr' + 'ipt type=\"text/javascript\" s' + 'rc=\"//s3.amazonaws.com/Inqwise/static/slider/scripts/Inqwise.loader.min.js?ver=1\"><' + '/scr' + 'ipt' + '>');" + "\n");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}
*/