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
    public partial class Activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set metadata (title, keywords and description).
            Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

            string activationCode = "";

            try
            {
                activationCode = Request.QueryString["c"];
            }
            catch
            {
                panelError.Visible = true;
                panelActivated.Visible = false;
            }

            if (!PublisherBLL.IsActivationCodeExists(activationCode))
            {
                panelError.Visible = true;
                panelActivated.Visible = false;
            }
            else
            {
                int publisherId = PublisherBLL.Activate(activationCode);

                TP_PUBLISHER publisher = PublisherBLL.GetPublisher(publisherId);

                // Send notifier to administrator.
                Mail.SendNewPublisherAdminMail(publisherId, publisher.Email);
            }
        }
    }
}
