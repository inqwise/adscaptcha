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
    public partial class Activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PublisherActivationCode"] == null)
            {
                Response.Redirect("StartPage.aspx");
            }
            else
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                // Get activation code from session.
                string activationCode = Session["PublisherActivationCode"].ToString();
                                
                // Get email by activation code.
                string email = PublisherBLL.GetEmailByActivationCode(activationCode);

                // Send activation mail.
                Mail.SendPublisherActivationMail(email, activationCode);
            }
        }
    }
}
