using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.HouseAdsSlider
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OldGet : IHttpHandler
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

            //string clickUrl = @"http://www.Inqwise.com";
            string likeUrl = @"http://www.Inqwise.com";

            if (imgid != 0)
            {
                adid = imgid.ToString();
            }

            Random rand = new Random();
            int correct = 31 - (2 + Math.Abs(((rand.Next(999, 9999) * DateTime.Now.Millisecond) % 26)));

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
