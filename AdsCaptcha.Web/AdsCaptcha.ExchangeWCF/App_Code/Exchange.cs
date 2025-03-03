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

namespace Inqwise.AdsCaptcha.ExchangeWCF.Config
{
    public class Exchange
    {
        public int ID { get; set; }
        public string CountryID { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Url { get; set; }
        public string ExchangePrimaryUrl { get; set; }
        public string ExchangeSecondaryUrl { get; set; }
    }
}
