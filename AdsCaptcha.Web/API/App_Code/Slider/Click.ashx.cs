using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.API.Slider
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Click : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var Request = context.Request;
            var Response = context.Response;

            Response.ContentType = "text/plain";
            
            try
            {
                // Get challange guid.
                string cid = HttpUtility.HtmlEncode(Request.QueryString["cid"]);

                #region Get User Data
                /*
                string IPAddress = null;
                string reffererUrl = null;
                string userAgent = null;

                string countryPrefix = string.Empty;
                try
                {
                    //IPAddress = detectIPAdress();
                    //countryPrefix = CaptchaServerBLL.Ip2country(IPAddress);
                }
                catch { }

                try
                {
                    reffererUrl = (Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : (Request.ServerVariables["HTTP_REFERER"] != null ? Request.ServerVariables["HTTP_REFERER"] : ""));
                }
                catch { }

                try
                {
                    userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                }
                catch { }
                */
                #endregion

                Task.Run(() => RequestsManager.UpdateClicked(cid));

            }
            catch (Exception ex)
            {
                Response.Write("Sorry, unable to redirect.");
            }
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
