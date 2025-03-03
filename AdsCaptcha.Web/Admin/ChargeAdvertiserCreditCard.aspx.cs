using System;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ChargeAdvertiserCreditCard : System.Web.UI.Page
    {
        private TA_ADVERTISER _advertiser;
        private int advertiserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                advertiserId = int.Parse(Page.Request.QueryString["AdvertiserId"].ToString());

                _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

                if (_advertiser == null)
                {
                    throw new Exception("Advertiser not exists");
                }
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (!IsPostBack)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + "<a href='ManageCampaigns.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>" + _advertiser.Email + "</a> (<a href='EditAdvertiser.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Charge";

            string errorMessage = "";

            if (_advertiser.Payment_Method_Id != (int)PaymentMethod.CreditCard)
            {
                errorMessage = "Advertiser's payment method is not by credit card.";
            }

            TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);

            if (billing.Card_Id == "0")
            {
                errorMessage = "Advertiser's credit card is not set or invalid.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                panelError.Visible = true;
                panelForm.Visible = false;
                labelErrorMessage.Text = errorMessage;
                return;
            }

            // Fill data from db.
            labelFirstName.Text = billing.First_Name;
            labelLastName.Text = billing.Last_Name;
            labelCompany.Text = billing.Company_Name;
            labelAddress.Text = billing.Address;
            labelCity.Text = billing.City;
            labelCountry.Text = (billing.Country_Id == 0 ? null : DictionaryBLL.GetCountryById((int)billing.Country_Id));
            labelState.Text = billing.State;
            labelZipCode.Text = billing.Zip_Code;
            labelPhone.Text = billing.Phone;
            labelFax.Text = billing.Fax;
            labelMinBillingAmount.Text = "$" + _advertiser.Min_Billing_Amount.ToString();

            // Get balance.
            decimal totalCharges = AdvertiserBLL.GetTotalCharges(_advertiser.Advertiser_Id);
            decimal totalPayments = AdvertiserBLL.GetTotalPayments(_advertiser.Advertiser_Id);
            decimal balance = totalCharges - totalPayments;

            labelBalance.Text = String.Format("${0:#,##0.00}", balance);

            // Set amount to charge restrictions.
            validatorAmountToChargeRange.MinimumValue = ApplicationConfiguration.MIN_FUNDS_WARNING.ToString();
            validatorAmountToChargeRange.MaximumValue = "99999";

            textAmountToCharge.Text = String.Format("${0:#,##0.00}", (balance <= 0 ? 0 : balance));
        }

        /// <summary>
        /// Submit form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            // Get billing information.
            TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);
            
            // Get data.
            int advertiserId = _advertiser.Advertiser_Id;
            string additionalData = textAdditionalData.Text;
            decimal tmp;
            bool isNum = decimal.TryParse(textAmountToCharge.Text, out tmp);
            int amount = Convert.ToInt32(tmp);

            if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS) throw new Exception("Amount is invalid");

            // Get card id.
            string cardId = billing.Card_Id;

            // Charge advertiser.
            CreditCardBLL.CreditCardResult result = new CreditCardBLL.CreditCardResult();
            result = CreditCardBLL.Debit(cardId, billing.Card_Verify, amount);

            // If charged, add charge transaction.
            if (result.Status)
            {
                // Add charge transaction.
                AdvertiserBLL.ChargeCreditCard(_advertiser.Advertiser_Id, amount, additionalData, result.TransID, result.AuthorizationNumber, null);

                // Send confirmation mail.
                Mail.SendCreditCardPaymentMail(_advertiser.Email);
            }
            else
            {
                labelError.Text = result.ErrorMessage;
                labelError.Visible = true;
                return;
            }

            // Redirect to advertisers to be charged page.
            Response.Redirect("AdvertisersToBeCharged.aspx");
        }
    }
}
