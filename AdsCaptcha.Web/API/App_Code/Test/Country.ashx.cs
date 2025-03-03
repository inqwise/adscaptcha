using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.API.Test
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Country : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["c"] != null)
            {
                string res = CountryDetection.IP2Country.convertIPToLong(context.Request["c"]).ToString() + "; " ;
                res += CaptchaServerBLL.Ip2country(context.Request["c"]);
                context.Response.Write(res);
            }
            else
            {
                context.Response.Write("Hello World");
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
