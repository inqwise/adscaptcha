﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.AdvertiserLogin, Master.Page.Header);

                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
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

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            if (AdvertiserBLL.CheckPassword(textEmail.Text, textPassword.Text) == true)
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

                // TODO: Write login to DB                

                if (Session["AdvertiserLastPage"] == null)
                {
                    Response.Redirect("ManageCampaigns.aspx");
                }
                else
                {
                    Response.Redirect(Session["AdvertiserLastPage"].ToString());
                }
            }
            else
            {
                labelMessage.Text = "Invalid e-mail or password";
            }
        }
    }
}
