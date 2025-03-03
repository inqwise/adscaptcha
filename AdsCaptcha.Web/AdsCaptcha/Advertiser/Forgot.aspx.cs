using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class Forgot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {            
            panelForm.Visible = true;
            panelSent.Visible = false;
            labelErrorMessage.Visible = false;
            labelErrorMessage.Text = "An error occured while sending mail. Please try again later.<br />";
        }

        protected void buttonRemindMe_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                textEmail.Focus();
                return;
            }

            // Get email value.
            string email = textEmail.Text;

            // Get advertiser.
            int advertiserId = AdvertiserBLL.GetAdvertiserIdByEmail(email);            
            TA_ADVERTISER advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);
            
            // Check if advertiser found.
            if (advertiser == null)
            {
                return;
            }

            // Save old password.
            string oldPassword = advertiser.Password;

            // Randomize new password.
            string randomPassword = General.GeneratePassword();

            // Change password.
            AdvertiserBLL.ChangePassword(email, randomPassword, false);

            try
            {
                // Send new password mail.
                Mail.SendNewPasswordMail((int)Modules.Advertiser, email, randomPassword);

                panelForm.Visible = false;
                panelSent.Visible = true;
                labelErrorMessage.Visible = false;
            }
            catch
            {
                // Restore old password.
                AdvertiserBLL.ChangePassword(email, oldPassword, true);

                labelErrorMessage.Visible = true;
            }
        }

        /// <summary>
        /// Validates that the advertiser email exists.
        /// </summary>
        protected void checkEmailExists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if advvertiser exsists (by email).
            e.IsValid = AdvertiserBLL.GetAdvertiser(textEmail.Text) == null ? false : true;
        }
    }
}
