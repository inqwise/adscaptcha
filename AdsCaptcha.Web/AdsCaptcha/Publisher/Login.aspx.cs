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

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.PublisherLogin, Master.Page.Header);

                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
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

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
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
                
                // TODO: Write login to DB                

                if (Request.QueryString["Activation"] == null)
                {
                    if (Session["PublisherLastPage"] == null)
                    {
                        Response.Redirect("ManageWebsites.aspx");
                    }
                    else
                    {
                        Response.Redirect(Session["PublisherLastPage"].ToString());
                    }
                }
                else
                {
                    Response.Redirect("NewWebsite.aspx");
                }                
            }
            else
            {
                labelMessage.Text = "Invalid e-mail or password";
            }
        }
    }
}
