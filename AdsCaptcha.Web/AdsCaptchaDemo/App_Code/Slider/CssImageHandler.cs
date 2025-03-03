using System.Web;
using System.Configuration;
using System.IO;

namespace Slider
{
    public class CssImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string baseUrl = ConfigurationManager.AppSettings["CSSImagesFolder300"];

            if (context.Request.QueryString["w"] == "180")
                baseUrl = ConfigurationManager.AppSettings["CSSImagesFolder180"];

            string bgImage = baseUrl + context.Request.QueryString["n"] + "." + context.Request.QueryString["e"];
            using (FileStream fs = new FileStream(bgImage, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(bytes, 0, bytes.Length);
                    //imgTemp.Save(ms, codec, ep);

                    // Send the encoded image to the browser
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "image/jpeg";
                    ms.WriteTo(HttpContext.Current.Response.OutputStream);
                    //ms.Close();
                }
            }
            context.Response.End();
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