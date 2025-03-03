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

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            if (Request.Cookies["AdsCaptchaAdmin"] != null)
            {
                try
                {
                    textEmail.Text = Server.HtmlEncode(Request.Cookies["AdsCaptchaAdmin"]["Email"]);
                    textPassword.Focus();
                    textPassword.Attributes.Add("Value", Server.HtmlEncode(Request.Cookies["AdsCaptchaAdmin"]["Password"]));
                    buttonLogin.Focus();
                }
                catch { }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            if (AdminBLL.CheckPassword(textEmail.Text, textPassword.Text) == true)
            {
                // Set publisher cookie.
                HttpCookie cookie = new HttpCookie("AdsCaptchaAdmin");
                cookie["Email"] = textEmail.Text;
                cookie["Password"] = textPassword.Text;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                
                FormsAuthentication.SetAuthCookie(textEmail.Text, false);

                // Save logged admin details to session.
                Session["AdminId"] = AdminBLL.GetAdminIdByEmail(textEmail.Text).ToString();
                Session["AdminEmail"] = textEmail.Text;

                if (Session["AdminLastPage"] == null)
                {
                    Response.Redirect("StartPage.aspx");
                }
                else
                {
                    Response.Redirect(Session["AdminLastPage"].ToString());
                } 
            }
            else
            {
                labelMessage.Text = "Invalid Email or Password. Try Again.";
            }
        }
    }
}
