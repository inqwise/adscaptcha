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
    public class SliderData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        static Random rand = new Random();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string width = context.Request.QueryString["w"];
            string height = context.Request.QueryString["h"];
            string demourl = string.IsNullOrEmpty(context.Request.QueryString["demo"]) ? string.Empty : context.Request.QueryString["demo"];

            int adid = string.IsNullOrEmpty(context.Request.QueryString["adid"]) ? -1 : Convert.ToInt32(context.Request.QueryString["adid"]);


            Random randDir = rand;

            string imagesUrl = "";
            string requid = string.Empty;

            randDir.Next(DateTime.Now.Hour + DateTime.Now.Millisecond + DateTime.Now.Minute + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Day);
            int startimageIndex = 3 + randDir.Next(25);// Math.Abs(((rand.Next(999, 9999) * DateTime.Now.Millisecond) % 25));


            T_DEMOS demo = null;
            using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
            {

                T_IMAGES image = ent.T_IMAGES.Where(m => m.ImageID == adid).OrderByDescending(n => n.InsertDate).FirstOrDefault(); ;

                if (image != null)
                {

                    var lastRequest = ent.T_REQUESTS_IMAGES.Where(ri => ri.SessionID == context.Session.SessionID && ri.Answer != null).OrderByDescending(ri2 => ri2.RequestDate).FirstOrDefault();

                    if (lastRequest != null)
                    {
                        while (startimageIndex == lastRequest.Answer)
                        {
                            startimageIndex = 3 + randDir.Next(25);
                        }
                    }


                    var request = ent.P_Requests_Images_Insert(Guid.NewGuid().ToString(), image.ImageID, 31 - startimageIndex, context.Session.SessionID, null, null, null, null, null, null).SingleOrDefault();
                    requid = request.RequestID;


                    imagesUrl = System.Configuration.ConfigurationManager.AppSettings["RandomImagesUrlBase"] +
                        "slider/image.ashx?CaptchaId=1" +
                        "&PublicKey=1" +
                        "&w=" + width + "&h=" + height + "&dumm=" + DateTime.Now.Ticks.ToString() +
                        "&reqid=" + requid + "&img=";
                }


                var speech = new T_REQUESTS_SPEECH();
                speech.SessionID = context.Session.SessionID;
                speech.RequestID = requid;
                speech.InsertDate = DateTime.Now;
                speech.IsBlocked = false;

                ent.AddToT_REQUESTS_SPEECH(speech);

                ent.SaveChanges();

            }
            string jsArray = "{";
            string[] imageUrls = System.Configuration.ConfigurationManager.AppSettings["RandomImagesUrls"].Split('|');
            int imageUrlIndex = 0;
            if (imagesUrl != string.Empty)
            {

                string adurl = null;
                if (demo != null)
                    adurl = demo.ClickUrl;
                if (adurl != null)
                {
                    if ((adurl.ToLower().IndexOf("http://") == -1) && (adurl.ToLower().IndexOf("https://") == -1))
                        adurl = "http://" + adurl;
                }
                jsArray += "'cid':'" + requid + "',";
                jsArray += "'clickurl':" + (adurl == null ? "''" : "'" + adurl + "'") + ", images : [";

                try
                {
                    for (int i = 0; i < 30; i++)
                    {
                        string tempUrl = imagesUrl.Replace("XXX", imageUrls[imageUrlIndex]);
                        jsArray += "{'src': '" + tempUrl + Convert.ToString(1 + i) + "'},";
                        imageUrlIndex++;
                        if (imageUrlIndex == imageUrls.Length) imageUrlIndex = 0;
                    }
                    jsArray = jsArray.Substring(0, jsArray.Length - 1);
                    jsArray += "]}";

                    if (!string.IsNullOrEmpty(context.Request["callback"]))
                        jsArray = context.Request["callback"] + "(" + jsArray + ")";

                }
                catch (Exception exc)
                {
                    jsArray += "]";
                    if (!string.IsNullOrEmpty(context.Request["callback"]))
                        jsArray = context.Request["callback"] + "(" + jsArray + ")";
                }


            }
            else
            {
                jsArray += "]";
                if (!string.IsNullOrEmpty(context.Request["callback"]))
                    jsArray = context.Request["callback"] + "(" + jsArray + ")";
            }

            context.Response.Write(jsArray);
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
