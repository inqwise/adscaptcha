<%@ WebHandler Language="C#"  CodeBehind="\App_Code\Slider\SliderData.ashx.cs" Class="Inqwise.AdsCaptchaDemo.Slider.SliderData_ashx" %>

/*
using System;
using System.Web;
using System.Linq;

public class SliderData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
    
    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        string width = context.Request.QueryString["w"];
        string height = context.Request.QueryString["h"];
        string demourl = string.IsNullOrEmpty(context.Request.QueryString["demo"]) ? string.Empty : context.Request.QueryString["demo"];

        int adid = string.IsNullOrEmpty(context.Request.QueryString["adid"]) ? -1 : Convert.ToInt32(context.Request.QueryString["adid"]);

        //string imageFolder = System.Configuration.ConfigurationManager.AppSettings["RandomImagesFolder300"];
        //if(width == "180")
        //    imageFolder = System.Configuration.ConfigurationManager.AppSettings["RandomImagesFolder180"];

        //string imagePath = System.Configuration.ConfigurationManager.AppSettings["RandomImagesUrl300"];
        //if (width == "180")
        //    imagePath = System.Configuration.ConfigurationManager.AppSettings["RandomImagesUrl180"];


        //var directory = new System.IO.DirectoryInfo(imageFolder);
        //var dirs = (from f in directory.GetDirectories()
        //            orderby f.LastWriteTime descending
        //            select f).ToList();

        Random randDir = Randomizer.Instance().Random;
        //randDir.Next(DateTime.Now.Hour + DateTime.Now.Millisecond + DateTime.Now.Minute + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Day);
        //int indDir =  dirs.Count > 1 ? randDir.Next(dirs.Count) : 1;  // Math.Abs(((randDir.Next(999, 9999) * DateTime.Now.Millisecond) % dirs.Count )) : 1;
        //string sDir = indDir < dirs.Count ? dirs[indDir].Name : dirs[0].Name;
        //if (sDir != string.Empty)
        //{



        string imagesUrl = "";//imagePath + sDir + "/";
        string requid = string.Empty;
        
        randDir.Next(DateTime.Now.Hour + DateTime.Now.Millisecond + DateTime.Now.Minute + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Day);
        int startimageIndex = 3 + randDir.Next(25);// Math.Abs(((rand.Next(999, 9999) * DateTime.Now.Millisecond) % 25));

        AdsCaptcha_DemoModel.T_DEMOS demo = null;
        using (AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities ent = new AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities())
        {

            AdsCaptcha_DemoModel.T_IMAGES image = null;

            if (adid == -1)
            {
                if (demourl != string.Empty)
                {
                    demourl = demourl.Replace(" ", "_");
                    demo = ent.T_DEMOS.Where(d => d.DemoUrlName == demourl).FirstOrDefault();
                    image = ent.P_Images_GetRandomCommercialImage(demo.DemoID).SingleOrDefault();
                }
                else
                {

                    image = ent.P_Images_GetRandomImage(Convert.ToInt32(width), Convert.ToInt32(height)).SingleOrDefault();
                }
            }
            else
            {
                image = ent.T_IMAGES.Where(m => m.Ad_Id == adid).OrderByDescending(n=>n.InsertDate).FirstOrDefault();
            }
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
                
                CacheImagesManager.AddToCache(image.ImageID);

                var request = ent.P_Requests_Images_Insert(image.ImageID, 31 - startimageIndex, context.Session.SessionID, null, null, null, null, null, null).SingleOrDefault();
                requid = request.RequestID;
                
                
                Request cacherequest = new Request();

                Random r = Randomizer.Instance().Random;
                int red = r.Next(256);
                int green = r.Next(256);
                int blue = r.Next(256);
                
                cacherequest.ID = request.RequestID;
                cacherequest.ImageID = image.ImageID;
                cacherequest.Challenge = 31 - startimageIndex;
                cacherequest.TransparentColor = System.Drawing.Color.FromArgb(red, green, blue);
                
                CacheManager.Instance.AddToCache(cacherequest.ID, cacherequest, TimeSpan.FromMinutes(30));
                if (Log.IsDebugEnabled)
                {
                    Log.Debug("SliderData: added request #{0} to cache", cacherequest.ID);
                }
                
                imagesUrl = System.Configuration.ConfigurationManager.AppSettings["RandomImagesUrlBase"] +
                    "slider/image.ashx?CaptchaId=" +
                     "&w=" + width + "&h=" + height + "&dumm=" + DateTime.Now.Ticks.ToString() +
                    "&reqid=" + request.RequestID + "&img=";
            }

            
            var speech = new AdsCaptcha_DemoModel.T_REQUESTS_SPEECH();
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
                    string tempUrl = imagesUrl.Replace("XXX.", "");
                    jsArray += "{'src': '" + tempUrl + Convert.ToString(1 + i) + "'},";
                    imageUrlIndex++;
                    if (imageUrlIndex == imageUrls.Length) imageUrlIndex = 0;
                }
                jsArray = jsArray.Substring(0, jsArray.Length - 1);
                jsArray += "]}";

                if (!string.IsNullOrEmpty(context.Request["callback"]))
                    jsArray = context.Request["callback"] + "(" + jsArray + ")";



                int correct = 31 - startimageIndex;

               

                HttpContext.Current.Session["Correct"] = correct;
            }
            catch (Exception ex)
            {
                Log.ErrorException("SliderData : Unexpected error occured", ex);
                jsArray += "]";
                if (!string.IsNullOrEmpty(context.Request["callback"]))
                    jsArray = context.Request["callback"] + "(" + jsArray + ")";
            }

           
        }
        else
        {
            Log.Warn("SliderData : Image url is null");
            jsArray += "]";
            if (!string.IsNullOrEmpty(context.Request["callback"]))
                jsArray = context.Request["callback"] + "(" + jsArray + ")";
        }

        context.Response.Write(jsArray);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
 */