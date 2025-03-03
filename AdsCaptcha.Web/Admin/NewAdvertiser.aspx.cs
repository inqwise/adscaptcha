using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class NewAdvertiser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

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
                                    "New Advertiser";

            // Set defaults.
            textMinBillingAmount.Text = ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT.ToString();

            // Fill payment method list.
            listBillingMethod.DataTextField = "Item_Desc";
            listBillingMethod.DataValueField = "Item_Id";
            listBillingMethod.DataSource = DictionaryBLL.GetBillingMethodList();
            listBillingMethod.DataBind();
            listBillingMethod.ClearSelection();
            listBillingMethod.Items.Insert(0, new ListItem("-- Select --", "0"));
            listBillingMethod.SelectedIndex = 0;

            // Fill payment method list.
            listPaymentMethod.DataTextField = "Item_Desc";
            listPaymentMethod.DataValueField = "Item_Id";
            listPaymentMethod.DataSource = DictionaryBLL.GetPaymentMethodList();
            listPaymentMethod.DataBind();
            int paymentManual = (int)PaymentMethod.Manual;
            listPaymentMethod.Items.Insert(listPaymentMethod.Items.Count, new ListItem("Manual", paymentManual.ToString()));
            listPaymentMethod.Items.Insert(0, new ListItem("-- Select --", "0"));
            listPaymentMethod.ClearSelection();
            listPaymentMethod.SelectedIndex = 0;

            // Fill country list.
            listCountry.DataTextField = "Country_Name";
            listCountry.DataValueField = "Country_Id";
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.ClearSelection();
            listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCountry.SelectedIndex = 0;
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
            string email = textEmail.Text.Trim();
            string password = textPassword.Text;
            int billingMethod = Convert.ToInt16(listBillingMethod.SelectedValue);
            int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);
            string firstName = textFirstName.Text.Trim();
            string lastName = textLastName.Text.Trim();
            string companyName = textCompanyName.Text.Trim();
            string address = textAddress.Text.Trim();
            string city = textCity.Text.Trim();
            string state = textState.Text.Trim();
            int country = Convert.ToInt16(listCountry.SelectedValue);
            string zipCode = textZipCode.Text.Trim();
            string phone = textPhone.Text.Trim();
            string fax = textFax.Text.Trim();
            bool getEmailAnnouncements = checkGetEmailAnnouncements.Checked;
            bool getEmailNewsletters = checkGetEmailNewsletters.Checked;

            // Sign Up advertiser.
            int advertiserId = AdvertiserBLL.SignUp(email,
                                                    password,
                                                    billingMethod,
                                                    paymentMethod,
                                                    getEmailAnnouncements,
                                                    getEmailNewsletters,
                                                    firstName,
                                                    lastName,
                                                    companyName,
                                                    address,
                                                    city,
                                                    state,
                                                    country,
                                                    zipCode,
                                                    phone,
                                                    fax
                                                    );

            // Send mail to administrator.
            Mail.SendNewAdvertiserAdminMail(advertiserId, email);

            // Redirect to manage advertisers page.
            Response.Redirect("ManageAdvertisers.aspx");
        }

        /// <summary>
        /// Validates that the user is exsists.
        /// </summary>
        protected void checkEmailExsists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            string email = textEmail.Text.Trim();

            // Check if advertiser already exists (by email).
            if (AdvertiserBLL.GetAdvertiser(email) == null)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected void checkBillingMethod_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            int billingMethod = Convert.ToInt16(listBillingMethod.SelectedValue);
            int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);

            // Postpay is not allowed using PayPal.
            if (billingMethod == (int)BillingMethod.Postpay &&
                paymentMethod == (int)PaymentMethod.PayPal)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
    }
}
