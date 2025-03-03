using System;
using System.Web;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.HouseAdsSlider
{
    public class GetHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            if (context.Request.Browser.Crawler)
            {
                context.Response.Write("");
                return;
            }

            string width = context.Request.QueryString["w"];
            string height = context.Request.QueryString["h"];

            string clickUrl = string.IsNullOrEmpty(context.Request.QueryString["clickurl"]) ? string.Empty : context.Request.QueryString["clickurl"];

            int imgid = string.IsNullOrEmpty(context.Request.QueryString["imgid"]) ? 0 : Convert.ToInt32(context.Request.QueryString["imgid"]);

            string adid = string.Empty;

            string likeUrl = @"http://www.Inqwise.com";

            if (imgid != 0)
            {
                adid = imgid.ToString();
            }

            //Random rand = new Random();
            //int correct = 31 - (2 + Math.Abs(((rand.Next(999, 9999) * DateTime.Now.Millisecond) % 26)));

            /*
            var jo = new JsonObject();

            jo.Add("captchaId", 111);
            jo.Add("key", "111");
            jo.Add("challenge", "''");
            jo.Add("hideChallenge", false);    
            jo.Add("Ad", true);   
            jo.Add("ClickUrl", clickUrl);
            jo.Add("Width", width);
            jo.Add("Height", height);
            jo.Add("Type", 101);  
            jo.Add("slider_data", "");
            jo.Add("direction", "");
            jo.Add("Language", "en");
            jo.Add("logo", false);
            jo.Add("Server", System.Configuration.ConfigurationManager.AppSettings["URL"] + @"HouseAdsSlider/");
            jo.Add("ad_server", "");
            jo.Add("Demo", "''");
            jo.Add("AdId:", adid);
            jo.Add("LikeUrl", likeUrl);
            */

            context.Response.Write(
            @"var Inqwise = {
                        Captcha: 111" + @",
                        Key: '111" + @"', 
                        Challenge: '" + @"',
                        Hide_challenge: false" + @",    
                        Ad: true" + @",    
                        ClickUrl: '" + clickUrl + "'" + @",
                        Width: " + width + @",
                        Height: " + height + @",
                        Type: 101,   
                        slider_data: '" + "" + @"',
                        direction: '" + "" + @"',
                        Language: '" + "en" + @"',
                        logo: false" + @",
                        Server: '" + System.Configuration.ConfigurationManager.AppSettings["URL"] + @"HouseAdsSlider/',
                        ad_server: '" + @"',
                        Demo: '" + @"',
                        AdId: '" + adid + @"',
                        LikeUrl: '" + likeUrl + "'};");

            string version = "1.1";



            context.Response.Write("\n\n" + "document.write('<' + 'scr' + 'ipt type=\"text/javascript\" s' + 'rc=\"http://demo.Inqwise.com/slider/js/Inqwise.loader.js" + "?ver=" + version + "\"><' + '/scr' + 'ipt' + '>');" + "\n");

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