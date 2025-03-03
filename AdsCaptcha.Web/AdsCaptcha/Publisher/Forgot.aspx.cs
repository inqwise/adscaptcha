using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class Forgot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

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

            // Get publisher.
            int publisherId = PublisherBLL.GetPublisherIdByEmail(email);
            TP_PUBLISHER publisher = PublisherBLL.GetPublisher(publisherId);

            // Check if publisher found.
            if (publisher == null)
            {
                return;
            }

            // Save old password.
            string oldPassword = publisher.Password;

            // Randomize new password.
            string randomPassword = General.GeneratePassword();

            // Change password.
            PublisherBLL.ChangePassword(email, randomPassword, false);

            try
            {
                // Send new password mail.
                Mail.SendNewPasswordMail((int)Modules.Publisher, email, randomPassword);

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
            // Check and return if publisher exsists (by email).
            e.IsValid = PublisherBLL.GetPublisher(textEmail.Text) == null ? false : true;
        }
    }
}
