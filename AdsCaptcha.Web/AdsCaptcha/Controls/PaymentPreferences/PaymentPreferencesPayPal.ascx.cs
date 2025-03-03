using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Controls.PaymentPreferences
{
    public partial class PaymentPreferencesPayPal : System.Web.UI.UserControl
    {
        private TP_PUBLISHER _publisher = null;
        private TD_DEVELOPER _developer = null;

        private Modules currMoudle;

        protected void Page_Load(object sender, EventArgs e)
        {
            currMoudle = (Modules)Session[ApplicationConfiguration.SESSION_PAYMENT_PREFS_CONTEXT];

            if (currMoudle == Modules.Publisher && Session["PublisherId"] == null ||
                currMoudle == Modules.Developer && Session["DeveloperId"] == null)
            {
                // Publisher/Developer ID does not set in the session
            }
            else
            {
                GetEntityFromDB();

                if (!IsPostBack)
                {
                    FillDataFromDB();
                }
            }
        }

        #region Properties
        public string PayeeAccount { get { return textPayeeAccount.Text.Trim(); } }
        #endregion

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        public void SubmitChanges()
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorPayeeAccount.IsValid)
                {
                    textPayeeAccount.Focus();
                }

                // Hide changes saved status.
                labelChangesSaved.Visible = false;
                return;
            }

            if (!IsValidEntity())
            {
                labelChangesSaved.Visible = false;
                return;
            }

            // Get form data.
            string payeeAccount = PayeeAccount;

            // Save data to DB
            UpdateDBwithChanges(payeeAccount);
            
            // Show changes saved status.
            labelChangesSaved.Visible = true;
        }

        #region Module Dependant Data Handling

        private void GetEntityFromDB()
        {
            switch (currMoudle)
            {
                case Modules.Publisher:
                    int publisherId = Convert.ToInt16(Session["PublisherId"]);
                    _publisher = PublisherBLL.GetPublisher(publisherId);
                    break;
                case Modules.Developer:
                    int developerID = Convert.ToInt16(Session["DeveloperId"]);
                    _developer = DeveloperBLL.GetDeveloper(developerID);
                    break;
                default:
                    break;
            }
        }

        private bool IsValidEntity()
        {
            switch (currMoudle)
            {
                case Modules.Publisher:
                    return _publisher != null;
                case Modules.Developer:
                    return _developer != null;
                default:
                    return false;
            }
        }

        private void FillDataFromDB()
        {
            //Not the current saved method.
            if (currMoudle == Modules.Publisher && _publisher.Credit_Method_Id != (int)CreditMethod.PayPal ||
                currMoudle == Modules.Developer && _developer.Credit_Method_Id != (int)CreditMethod.PayPal)
            {
                return;
            }

            switch (currMoudle)
            {
                case Modules.Publisher:
                    TP_PUBLISHER_PAYPAL payPalPub = PublisherBLL.GetPublisherPayPalDetails(_publisher.Publisher_Id);
                    if (payPalPub != null)
                    {
                        SetDetailsToUI(payPalPub.Payee_Account);
                    }
                    break;
                case Modules.Developer:
                    TD_DEVELOPER_PAYPAL paypalDev = DeveloperBLL.GetDeveloperPayPalDetails(_developer.Developer_Id);
                    if (paypalDev != null)
                    {
                        SetDetailsToUI(paypalDev.Payee_Account);
                    }
                        break;
                default:
                    break;
            }
        }

        private void UpdateDBwithChanges(string payeeAccount)
        {
            switch (currMoudle)
            {
                case Modules.Publisher:
                    PublisherBLL.UpdatePayPalDetails(_publisher.Publisher_Id, payeeAccount);
                    break;
                case Modules.Developer:
                    DeveloperBLL.UpdatePayPalDetails(_developer.Developer_Id, payeeAccount);
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void SetDetailsToUI(string payeeAccount)
        {
            textPayeeAccount.Text = payeeAccount;
        }
    }
}