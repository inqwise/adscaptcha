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

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class StartPage : Page
    {
		
    	protected string ApiUrl {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }
		
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is not logged in, redirect to manage page.
            if (Session["AdvertiserId"] != null && Session["AdvertiserEmail"] != null)
                Response.Redirect("ManageCampaigns.aspx");

            if (!Page.IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.AdvertiserStart, Master.Page.Header);

                if (Request.Cookies["AdsCaptchaAdvertiser"] != null)
                {
                    try
                    {
                        textEmail.Text = Server.HtmlEncode(Request.Cookies["AdsCaptchaAdvertiser"]["Email"]);
                        textPassword.Focus();
                        textPassword.Attributes.Add("Value", Server.HtmlEncode(Request.Cookies["AdsCaptchaAdvertiser"]["Password"]));
                        buttonLogin.Focus();
                    }
                    catch { }
                }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            divLoginClick.Visible = true;
            if (string.IsNullOrEmpty(textEmail.Text))
            {
                labelMessage.Text = "Please enter your e-mail";
            }
            else if (string.IsNullOrEmpty(textPassword.Text))
            {
                labelMessage.Text = "Please enter your password";
            }
            else if (AdvertiserBLL.CheckPassword(textEmail.Text, textPassword.Text) == true)
            {
                try
                {
                    if (checkRememberMe.Checked == true)
                    {
                        // Set publisher cookie.
                        HttpCookie cookie = new HttpCookie("AdsCaptchaAdvertiser");
                        cookie["Email"] = textEmail.Text;
                        cookie["Password"] = textPassword.Text;
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie cookie = Request.Cookies["AdsCaptchaAdvertiser"];
                        cookie.Expires = DateTime.Now.AddYears(-30);
                        Response.Cookies.Add(cookie);
                    }
                }
                catch { }

                // Save logged advertiser details to session.
                Session["AdvertiserId"] = AdvertiserBLL.GetAdvertiserIdByEmail(textEmail.Text).ToString();
                Session["AdvertiserEmail"] = textEmail.Text;

                // TO DO: Write login to DB
                Response.Redirect("ManageCampaigns.aspx");
            }
            else
            {
                labelMessage.Text = "Invalid e-mail or password";
            }
        }
    }
}
