using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Inqwise.AdsCaptcha.ExchangeWCF.Config
{
    public class ExchangeConfig
    {
        private static List<Exchange> exchanges;
        public static Exchange GetExchange(string country)
        {
            FillConfig();
            var exc = exchanges.Where(e => e.CountryID == country).FirstOrDefault();
            if (exc == null)
            {
                exc = exchanges.Where(e => e.CountryID == "0").FirstOrDefault();
            }
            return exc;
        }

        private static void FillConfig()
        {
            exchanges = new List<Exchange>();

            DataSet ds = new DataSet();
            ds.ReadXml(ConfigurationManager.AppSettings["ExchangeConfigPath"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Exchange exchange = new Exchange();
                exchange.ID = Convert.ToInt32(row["Exchange"].ToString());
                exchange.CountryID = row["CountryID"].ToString();
                exchange.Name = row["Name"].ToString();
                exchange.Pattern = row["Pattern"].ToString();
                exchange.Url = row["ExchangeUrl"].ToString();
                exchange.ExchangePrimaryUrl = row["ExchangePrimaryUrl"].ToString();
                exchange.ExchangeSecondaryUrl = row["ExchangeSecondaryUrl"].ToString();

                exchanges.Add(exchange);
            }


        }
    }
}
