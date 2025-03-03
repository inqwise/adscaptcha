using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Inqwise.AdsCaptcha.Demo
{
    public partial class DemoHouseAds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string width = Request.QueryString["w"];
            string height = Request.QueryString["h"];
            string demo = string.Empty;
            string adid = !string.IsNullOrEmpty(Request.QueryString["imgid"]) ? Request.QueryString["imgid"] : string.Empty;


            string script = "<script type='text/javascript' src='" +
                    ConfigurationManager.AppSettings["URL"] +
                    "HouseAdsSlider/Slider/Get.ashx?w=" + width +
                    "&h=" + height +
                    "&demo=" + demo +
                    "&imgid=" + adid + "'>" +
                    "</script>";

            form1.InnerHtml = script;
        }
    }
}
