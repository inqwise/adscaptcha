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
    public partial class PaymentPreferencesBank : System.Web.UI.UserControl
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
        public string BankName { get { return textBankName.Text.Trim(); } }
        public string BranchName { get { return textBranchName.Text.Trim(); } }
        public string Address { get { return textAddress.Text.Trim(); } }
        public string City { get { return textCity.Text.Trim(); } }
        public string Country { get { return textCountry.Text.Trim(); } }
        public string State { get { return textState.Text.Trim(); } }
        public string HolderName { get { return textHolderName.Text.Trim(); } }
        public string AccountNumber { get { return textAccountNumber.Text.Trim(); } }
        public string Code { get { return textCode.Text.Trim(); } }
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
            string bankName = BankName;
            string branchName = BranchName;
            string address = Address;
            string city = City;
            string country = Country;
            string state = State;
            string holderName = HolderName;
            string accountNumber = AccountNumber;
            string bankCode = Code;
            
            // Save data to DB
            UpdateDBwithChanges(bankName, branchName, address, city, state, country, holderName, accountNumber, bankCode);

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
            if (currMoudle == Modules.Publisher && _publisher.Credit_Method_Id != (int)CreditMethod.BankWire ||
                currMoudle == Modules.Developer && _developer.Credit_Method_Id != (int)CreditMethod.BankWire)
            {
                return;
            }

            switch (currMoudle)
            {
                case Modules.Publisher:
                    TP_PUBLISHER_BANK bankPub = PublisherBLL.GetPublisherBankDetails(_publisher.Publisher_Id);
                    if (bankPub != null)
                    {
                        SetDetailsToUI(bankPub.Bank_Name, bankPub.Branch_Name, bankPub.Address, bankPub.City, bankPub.Country, bankPub.State, bankPub.Holder_Name, bankPub.Account_Numer, bankPub.Code);
                    }
                    break;
                case Modules.Developer:
                    TD_DEVELOPER_BANK bankDev = DeveloperBLL.GetDeveloperBankDetails(_developer.Developer_Id);
                    if (bankDev != null)
                    {
                        SetDetailsToUI(bankDev.Bank_Name, bankDev.Branch_Name, bankDev.Address, bankDev.City, bankDev.Country, bankDev.State, bankDev.Holder_Name, bankDev.Account_Numer, bankDev.Code);
                    }
                    break;
                default:
                    break;
            }
        }
    
        private void UpdateDBwithChanges(string bankName, string branchName, string address, string city, string state, string country, string holderName, string accountNumber, string bankCode)
        {
            switch (currMoudle)
            {
                case Modules.Publisher:
                    PublisherBLL.UpdateBankDetails(_publisher.Publisher_Id,
                                                   bankName,
                                                   branchName,
                                                   address,
                                                   city,
                                                   state,
                                                   country,
                                                   holderName,
                                                   accountNumber,
                                                   bankCode
                                                  );
                    break;
                case Modules.Developer:
                    DeveloperBLL.UpdateBankDetails(_developer.Developer_Id,
                                                   bankName,
                                                   branchName,
                                                   address,
                                                   city,
                                                   state,
                                                   country,
                                                   holderName,
                                                   accountNumber,
                                                   bankCode
                                                  );
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void SetDetailsToUI(string bankName, string branchName, string address, string city, string country, string state, string holderName, string accountNumber, string code)
        {
            textBankName.Text = bankName;
            textBranchName.Text = branchName;
            textAddress.Text = address;
            textCity.Text = city;
            textCountry.Text = country;
            textState.Text = state;
            textHolderName.Text = holderName;
            textAccountNumber.Text = accountNumber;
            textCode.Text = code;
        }
    }
}