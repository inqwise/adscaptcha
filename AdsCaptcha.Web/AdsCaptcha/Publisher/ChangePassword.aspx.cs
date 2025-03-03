using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using AjaxControlToolkit;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class ChangePassword : System.Web.UI.Page, IPublisherChangePassword
    {
        private TP_PUBLISHER _publisher;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Session["PublisherId"]);

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    // TODO: Handle publisher not exsists
                    throw new Exception("Publisher not exists");
                }
            }
            catch
            {
                Response.Redirect("ManageWebsites.aspx");
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                InitControls();
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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"AccountPreferences.aspx\">My Account</a>" + " &gt; " +
                                       "Change Password";

            panelForm.Visible = true;
            panelPasswordChanged.Visible = false;
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorOldPassword1.IsValid || !validatorOldPassword2.IsValid)
                    textOldPassword.Focus();
                else if (!validatorPassword1.IsValid || !validatorPassword2.IsValid)
                    textPassword.Focus();
                else if (!validatorConfirmPassword1.IsValid || !validatorConfirmPassword2.IsValid)
                    textPasswordConfirm.Focus();

                return;
            }
            
            if (PublisherBLL.CheckPassword(_publisher.Email, textOldPassword.Text) == true)
            {
                // Change password.
                PublisherBLL.ChangePassword(_publisher.Email, textPassword.Text, false);

                panelForm.Visible = false;
                panelPasswordChanged.Visible = true;
            }
        }

        /// <summary>
        /// Validates that the the old password is correct.
        /// </summary>
        protected void checkOldPassword_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if old password is correct.
            e.IsValid = (PublisherBLL.CheckPassword(_publisher.Email, textOldPassword.Text) ? true : false);
        }
    }
}
