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

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class Presskit : System.Web.UI.Page
    {
        private const string kSessionFilterData = "AdminFilterData";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null)
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack)
            {
                btnMenu1.Attributes["class"] = "selected";
            }

            if (!string.IsNullOrEmpty(Request["edit"]))
            {

                btnMenu1.Attributes["class"] = "";
                btnMenu2.Attributes["class"] = "";
                btnMenu3.Attributes["class"] = "selected";
                ctrlPressInquires.Visible = false;
                ctrlPressAddNews.Visible = true;
                ctrlNews.Visible = false;

            }
            else  if (!string.IsNullOrEmpty(Request["tab"]))
            {
                switch (Request["tab"])
                {
                    case "2":
                        btnMenu1.Attributes["class"] = "";
                        btnMenu2.Attributes["class"] = "selected";
                        btnMenu3.Attributes["class"] = "";
                        ctrlPressInquires.Visible = false;
                        ctrlPressAddNews.Visible = false;
                        ctrlNews.Visible = true;
                        break;
                    case "3":
                        btnMenu1.Attributes["class"] = "";
                        btnMenu2.Attributes["class"] = "";
                        btnMenu3.Attributes["class"] = "selected";
                        ctrlPressInquires.Visible = false;
                        ctrlPressAddNews.Visible = true;
                        ctrlNews.Visible = false;
                        break;
                    default:
                        btnMenu1.Attributes["class"] = "selected";
                        btnMenu2.Attributes["class"] = "";
                        btnMenu3.Attributes["class"] = "";
                        ctrlPressInquires.Visible = true;
                        ctrlPressAddNews.Visible = false;
                        ctrlNews.Visible = false;
                        break;

                }


            }
            else 
            {
                btnMenu1.Attributes["class"] = "selected";
                btnMenu2.Attributes["class"] = "";
                btnMenu3.Attributes["class"] = "";
                ctrlPressInquires.Visible = true;
                ctrlPressAddNews.Visible = false;
                ctrlNews.Visible = false;
            }



           

        }

        /// <summary>
        /// Check if browser is Chrome or Safari.
        /// If so, on pages with Grid - Disable partial page rendering of the ScriptManager.
        /// </summary>
        protected void scriptManagerOnInit(object sender, EventArgs e)
        {
            if (Request.Browser.Browser.ToUpper().Contains("SAFARI") || Request.Browser.Browser.ToUpper().Contains("CHROME"))
            {
                ScriptManager.EnablePartialRendering = false;
            }
        }

        protected void btnMenu1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Presskit.aspx?tab=" + ((LinkButton)sender).CommandArgument);

        }
    }
}
