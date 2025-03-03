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
    public partial class PaymentPreferencesCheck : System.Web.UI.UserControl
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
        public string PayeeName { get { return textPayeeName.Text.Trim(); } }
        public string RecipientName { get { return textRecipientName.Text.Trim(); } }
        public string Address { get { return textAddress.Text.Trim(); } }
        public string City { get { return textCity.Text.Trim(); } }
        public string Country { get { return textCountry.Text.Trim(); } }
        public string State { get { return textState.Text.Trim(); } }
        public string ZipCode { get { return textZipCode.Text.Trim(); } }
        #endregion

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        public void SubmitChanges()
        {
            // If form is not valid, exit.
            if (!Page.IsValid || !IsValidEntity())
            {
                // Hide changes saved status.
                labelChangesSaved.Visible = false;

                return;
            }

            // Get form data.
            string payeeName = PayeeName;
            string recipientName = RecipientName;
            string address = Address;
            string city = City;
            string country = Country;
            string state = State;
            string zipCode = ZipCode;

            UpdateDBwithChanges(payeeName, recipientName, address, city, state, country, zipCode);

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
            if (currMoudle == Modules.Publisher && _publisher.Credit_Method_Id != (int)CreditMethod.Check ||
                currMoudle == Modules.Developer && _developer.Credit_Method_Id != (int)CreditMethod.Check)
            {
                return;
            }

            switch (currMoudle)
            {
                case Modules.Publisher:
                    TP_PUBLISHER_CHECK checkPub = PublisherBLL.GetPublisherCheckDetails(_publisher.Publisher_Id);
                    if (checkPub != null)
                    {
                        SetDetailsToUI(checkPub.Payee_Name, checkPub.Recipient_Name, checkPub.Address, checkPub.City, checkPub.Country, checkPub.State, checkPub.Zip_Code);
                    }
                    break;
                case Modules.Developer:
                    TD_DEVELOPER_CHECK checkDev = DeveloperBLL.GetDeveloperCheckDetails(_developer.Developer_Id);
                    if (checkDev != null)
                    {
                        SetDetailsToUI(checkDev.Payee_Name, checkDev.Recipient_Name, checkDev.Address, checkDev.City, checkDev.Country, checkDev.State, checkDev.Zip_Code);
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateDBwithChanges(string payeeName, string recipientName, string address, string city, string state, string country, string zipCode)
        {
            switch (currMoudle)
            {
                case Modules.Publisher:
                    PublisherBLL.UpdateCheckDetails(_publisher.Publisher_Id,
                                                    payeeName,
                                                    recipientName,
                                                    address,
                                                    city,
                                                    state,
                                                    country,
                                                    zipCode
                                                   );
                    break;
                case Modules.Developer:
                    DeveloperBLL.UpdateCheckDetails(_developer.Developer_Id,
                                                    payeeName,
                                                    recipientName,
                                                    address,
                                                    city,
                                                    state,
                                                    country,
                                                    zipCode
                                                   );
                    break;
                default:
                    break;
            }

        }

        #endregion

        private void SetDetailsToUI(string payeeName, string recipientName, string address, string city, string country, string state, string zipCode)
        {
            textPayeeName.Text = payeeName;
            textRecipientName.Text = recipientName;
            textAddress.Text = address;
            textCity.Text = city;
            textCountry.Text = country;
            textState.Text = state;
            textZipCode.Text = zipCode;        
        }
    }
}