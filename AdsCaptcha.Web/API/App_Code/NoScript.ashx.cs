using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace Inqwise.AdsCaptcha.API
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class NoScript1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Enable scripts please to see the captcha");
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
