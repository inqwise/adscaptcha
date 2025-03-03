using System;
using System.Web.UI;

namespace Inqwise.AdsCaptcha.Controls.PaymentPreferences
{
    public partial class PaymentPreferencesLater : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                return;
            }
        }

        public void SubmitChanges()
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                // Hide changes saved status.
                labelChangesSaved.Visible = false;
                return;
            }

            // Show changes saved status.
            labelChangesSaved.Visible = true;
        }
    }
}