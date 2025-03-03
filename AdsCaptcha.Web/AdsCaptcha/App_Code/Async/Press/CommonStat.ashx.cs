using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.BLL;
using System.Configuration;

namespace Inqwise.AdsCaptcha.Async
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CommonStat : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(context.Request.QueryString["type"]))
            {
                switch (context.Request.QueryString["type"])
                {
                    case "requests":

                        try
                        {
                            decimal totalServed = MeasureBLL.GetTotalServed();

                            if (ConfigurationSettings.AppSettings["Environment"] == "LiveDemo")
                                totalServed += 17000000;

                            result = String.Format("{0:#,##}", totalServed);

                        }
                        catch(Exception ex)
                        {
                            NLogManager.logger.ErrorException("CommonStat: Unexpected error occured.", ex);
                            result = string.Empty;
                        }

                        break;

                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
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
