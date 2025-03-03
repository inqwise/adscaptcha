using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace Inqwise.AdsCaptcha.API.Test
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TestRequest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                string headers = string.Empty;

                System.Collections.Specialized.NameValueCollection headersnames = System.Web.HttpContext.Current.Request.Headers;
                for (int i = 0; i < headersnames.Count; i++)
                {
                    string key = headersnames.GetKey(i);
                    string value = headersnames.Get(i);
                    headers += key + ": " + value + "; ";
                }

                string res = "IP - " + ip + ", Headers - " + headers;

                headers = string.Empty;

                headersnames = System.Web.HttpContext.Current.Request.ServerVariables;
                for (int i = 0; i < headersnames.Count; i++)
                {
                    string key = headersnames.GetKey(i);
                    string value = headersnames.Get(i);
                    headers += key + ": " + value + "; ";
                }

                res += ", Server Variables - " + headers;

                context.Response.Write(res);
            }
            catch (Exception exc)
            {
                context.Response.Write(exc.ToString());
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
