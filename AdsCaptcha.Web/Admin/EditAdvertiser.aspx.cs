using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditAdvertiser : System.Web.UI.Page
    {
        private IAdvertiser _advertiser;
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

                var advertiserResult = AdvertisersManager.Get(advertiserId);

                if (advertiserResult.HasError)
                {
                    throw new Exception("Advertiser not exists");
                }
                else
                {
                    _advertiser = advertiserResult.Value;
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

        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + _advertiser.Email;

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.FindByValue(((int)_advertiser.Status).ToString()).Selected = true;

            // Fill payment method list.
            listBillingMethod.DataTextField = "Item_Desc";
            listBillingMethod.DataValueField = "Item_Id";
            listBillingMethod.DataSource = DictionaryBLL.GetBillingMethodList();
            listBillingMethod.DataBind();

            // Fill payment method list.
            listPaymentMethod.DataTextField = "Item_Desc";
            listPaymentMethod.DataValueField = "Item_Id";
            listPaymentMethod.DataSource = DictionaryBLL.GetPaymentMethodList();
            listPaymentMethod.DataBind();
            int paymentManual = (int)PaymentMethod.Manual;
            listPaymentMethod.Items.Insert(listPaymentMethod.Items.Count, new ListItem("Manual", paymentManual.ToString()));

            // Fill country list.
            listCountry.DataTextField = "Country_Name";
            listCountry.DataValueField = "Country_Id";
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));

            TA_ADVERTISER_BILLING billing = new TA_ADVERTISER_BILLING();
            billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Id);

            // Fill data from db.
            labelEmail.Text = _advertiser.Email;
            if (BillingMethod.Undefined == _advertiser.BillingMethod)
            {
                listBillingMethod.Items.Clear();
                textMinBillingAmount.Text = _advertiser.MinimumBillingAmount.ToString();
            }
            else
            {
                listBillingMethod.Items.FindByValue(((int)_advertiser.BillingMethod).ToString()).Selected = true;
                textMinBillingAmount.Text = _advertiser.MinimumBillingAmount.ToString();
            }

            listPaymentMethod.Items.FindByValue(((int)_advertiser.PaymentMethod).ToString()).Selected = true;
            if (BillingMethod.Undefined == _advertiser.BillingMethod)
            {
                textFirstName.Enabled =
                textLastName.Enabled =
                textCompanyName.Enabled =
                textAddress.Enabled =
                textCity.Enabled = 
                listCountry.Enabled =
                textState.Enabled =
                textZipCode.Enabled =
                textPhone.Enabled =
                textFax.Enabled = false;
            }
            else
            {
                textFirstName.Text = billing.First_Name;
                textLastName.Text = billing.Last_Name;
                textCompanyName.Text = billing.Company_Name;
                textAddress.Text = billing.Address;
                textCity.Text = billing.City;
                listCountry.Items.FindByValue(billing.Country_Id.ToString()).Selected = true;
                textState.Text = billing.State;
                textZipCode.Text = billing.Zip_Code;
                textPhone.Text = billing.Phone;
                textFax.Text = billing.Fax;
            }
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

            // Get form data.
            int statusId = int.Parse(listStatus.SelectedItem.Value);
            int billingMethod = (int) BillingMethod.Prepay;
            int.TryParse(listBillingMethod.SelectedValue, out billingMethod);
            int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);
            string billingFirstName = textFirstName.Text.Trim();
            string billingLastName = textLastName.Text.Trim();
            string companyName = textCompanyName.Text.Trim();
            string address = textAddress.Text.Trim();
            string city = textCity.Text.Trim();
            string state = textState.Text.Trim();
            int country = Convert.ToInt16(listCountry.SelectedValue);
            string zipCode = textZipCode.Text.Trim();
            string phone = textPhone.Text.Trim();
            string fax = textFax.Text.Trim();
            int minBillingAmount = Convert.ToInt16(textMinBillingAmount.Text.Trim());

            // Update advertiser data.
            AdvertiserBLL.Update(_advertiser.Id,
                                 statusId,
                                 billingMethod,
                                 paymentMethod,
                                 _advertiser.EmailAnnouncements,
                                 _advertiser.EmailNewsletters,
                                 minBillingAmount,
                                 billingFirstName,
                                 billingLastName,
                                 companyName,
                                 address,
                                 city,
                                 state,
                                 country,
                                 zipCode,
                                 phone,
                                 fax
                                 );

            // Change password
            if (textPassword.Text != "" && textPasswordConfirm.Text == textPassword.Text)
            {
                AdvertiserBLL.ChangePassword(_advertiser.Email, textPassword.Text, false);
            }

            // Redirect to manage advertisers page.
            Response.Redirect("ManageAdvertisers.aspx");
        }

        protected void checkPaymentMethod_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            var paymentMethod = (PaymentMethod)Convert.ToInt32(listPaymentMethod.SelectedValue);

            // When postpay, changes not allowed (unless manual).
            if (_advertiser.BillingMethod == BillingMethod.Postpay &&
                AdvertiserBLL.GetBalance(_advertiser.Id) < 0 &&
                _advertiser.PaymentMethod != paymentMethod && 
                paymentMethod != PaymentMethod.Manual &&
                _advertiser.PaymentMethod != PaymentMethod.Manual)
            {
                validatorPaymentMethod2.ErrorMessage = "* Your balance is in debit. Changes are not possible.";
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        protected void checkBillingMethod_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            var billingMethod = (BillingMethod)Convert.ToInt32(listBillingMethod.SelectedValue);
            var paymentMethod = (PaymentMethod)Convert.ToInt32(listPaymentMethod.SelectedValue);

            // Postpay is not allowed using PayPal.
            if (billingMethod == BillingMethod.Postpay &&
                paymentMethod == PaymentMethod.PayPal)
            {
                validatorBillingMethod2.ErrorMessage = "* Postpay is not permitted with Paypal payments.";
                e.IsValid = false;
            }
            else
            {
                // When postpay, changes not allowed.
                if (_advertiser.BillingMethod == BillingMethod.Postpay &&
                    AdvertiserBLL.GetBalance(_advertiser.Id) < 0 &&
                    _advertiser.BillingMethod != billingMethod &&
                    paymentMethod != PaymentMethod.Manual &&
                    _advertiser.PaymentMethod != PaymentMethod.Manual)
                {
                    validatorBillingMethod2.ErrorMessage = "* Your balance is in debit. Changes are not possible.";
                    e.IsValid = false;
                }
                else
                {
                    e.IsValid = true;
                }
            }
        }
    }
}
