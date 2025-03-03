using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class StartPage : System.Web.UI.Page
    {
        
		protected string ApiUrl {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }
		
		protected void Page_Load(object sender, EventArgs e)
        {
            // If user is not logged in, redirect to manage page.
            if (Session["PublisherId"] != null && Session["PublisherEmail"] != null)
                Response.Redirect("ManageWebsites.aspx");

            if (!Page.IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.PublisherStart, Master.Page.Header);

                if (Request.Cookies["AdsCaptchaPublisher"] != null)
                {
                    try
                    {
                        textEmail.Text = Server.HtmlEncode(Request.Cookies["AdsCaptchaPublisher"]["Email"]);
                        textPassword.Focus();
                        textPassword.Attributes.Add("Value", Server.HtmlEncode(Request.Cookies["AdsCaptchaPublisher"]["Password"]));
                        buttonLogin.Focus();
                    }
                    catch { }
                }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            divLoginClick.Visible = true;

            if (PublisherBLL.CheckPassword(textEmail.Text, textPassword.Text) == true)
            {
                try
                {
                    if (checkRememberMe.Checked == true)
                    {
                        // Set publisher cookie.
                        HttpCookie cookie = new HttpCookie("AdsCaptchaPublisher");
                        cookie["Email"] = textEmail.Text;
                        cookie["Password"] = textPassword.Text;
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = Request.Cookies["AdsCaptchaPublisher"];
                        cookie.Expires = DateTime.Now.AddYears(-30);
                        Response.Cookies.Add(cookie);
                    }
                }
                catch { }

                // Save logged publisher details to session.
                Session["PublisherId"] = PublisherBLL.GetPublisherIdByEmail(textEmail.Text).ToString();
                Session["PublisherEmail"] = textEmail.Text;

                // TO DO: Write login to DB
                Response.Redirect("ManageWebsites.aspx");
            }
            else
            {
                labelMessage.Text = "Invalid e-mail or password";
            }
        }
    }
}
