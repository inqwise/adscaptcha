using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Ad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Set CDN URL. If SSL (HTTPS), replace URL string.
                string CDN_URL = ConfigurationSettings.AppSettings["AWSCloudFront"];
                bool isHttps = Request.Url.ToString().ToLower().StartsWith("https://");
                if (isHttps) CDN_URL = CDN_URL.Replace("http://", "https://");

                // Get challange guid.
                string cid = Request.QueryString["cid"];

                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TCS_REQUEST challenge = dataContext.TCS_REQUESTs.Where(i => i.Request_Guid == cid).SingleOrDefault();

                    string adFile = challenge.TCS_REQUEST_ADVERTISER.TA_AD.Ad_Image;
                    string adUrl = CDN_URL + adFile;

                    Response.Redirect(adUrl, true);
                    /*
                    string contentType;
                    ImageFormat imageFormat;
                    
                    WebResponse wr = WebRequest.Create(adUrl).GetResponse();
                    contentType = wr.ContentType;

                    switch (contentType.ToLower())
                    {
                        case "image/bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case "image/gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case "image/jpg":
                        case "image/jpeg":
                        default:
                            imageFormat = ImageFormat.Jpeg;
                            break;
                    }

                    this.Response.Clear();
                    this.Response.ContentType = contentType;

                    StreamReader reader = new StreamReader(wr.GetResponseStream());

                    Bitmap bitmap = new Bitmap(reader.BaseStream);
                    bitmap.Save(this.Response.OutputStream, imageFormat);
                    */
                }
            }
            catch 
            {
                this.Response.Clear();
                this.Response.ContentType = "image/jpeg";
            }
        }
    }
}
