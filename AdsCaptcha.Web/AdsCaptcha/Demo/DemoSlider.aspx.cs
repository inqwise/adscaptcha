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
    public partial class DemoSlider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string width = Request.QueryString["w"];
            string height = Request.QueryString["h"];
            string demo = string.Empty;
            string adid = !string.IsNullOrEmpty(Request.QueryString["adid"]) ? Request.QueryString["adid"] :  string.Empty;

            if (!string.IsNullOrEmpty(Request.QueryString["isran"]))
            {
                switch (Request.QueryString["isran"])
                {
                    case "0":
                        if (width == "300")
                            demo = ConfigurationManager.AppSettings["DemoSliderPublisherRandom300"];
                        else if (width == "180")
                                demo = ConfigurationManager.AppSettings["DemoSliderPublisherRandom180"];
                        break;

                    case "1":
                        if (width == "300")
                            demo = ConfigurationManager.AppSettings["DemoSliderPublisherCommercial300"];
                        else if (width == "180")
                            demo = ConfigurationManager.AppSettings["DemoSliderPublisherCommercial180"];

                        break;
                    case "2":
                        if (adid == "")
                            demo = ConfigurationManager.AppSettings["DemoSliderAdvertiserStart"];

                        break;
                }
            }

            string script = "<script type='text/javascript' src='" + 
                    ConfigurationManager.AppSettings["DemoSliderUrl"] +
                    "Get.ashx?w=" + width + 
                    "&h=" + height + 
                    "&demo=" + demo + 
                    "&adid=" + adid + "'>" + 
                    "</script>";

            form1.InnerHtml = script;
        }
    }
}
