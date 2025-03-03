using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Advertiser;
using Inqwise.AdsCaptcha.Common;

public partial class Advertiser_AccountPreferences : System.Web.UI.Page, IAdvertiserAccountPreferences
{
    private TA_ADVERTISER _advertiser;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set last page.
        Session["AdvertiserLastPage"] = Page.Request.Url.ToString();

        // If user is not logged in, redirect to login page.
        if (Session["AdvertiserId"] == null) 
            Response.Redirect("Login.aspx");

        try
        {
            int advertiserId = Convert.ToInt16(Session["AdvertiserId"]);

            _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

            if (_advertiser == null)
            {
                // TODO: Handle advertiser not exsists
                throw new Exception("Advertiser not exists");
            }
        }
        catch 
        {
            Response.Redirect("ManageCampaigns.aspx");
        }

        if (!IsPostBack)
        {
            // Set metadata (title, keywords and description).
            Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

            // If saved, display saved message.
            string isSaved = (Request.QueryString["saved"] == null ? "false" : Request.QueryString["saved"]);
            if (isSaved == "true")
                labelChangesSaved.Visible = true;

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

    /// <summary>
    /// Initialize controls.
    /// </summary>
    private void InitControls()
    {
        // Set navigation path.
        labelNavigationPath.Text = "<a href=\"AccountPreferences.aspx\">My Account</a>" + " &gt; " +
                                    "Account Preferences";

        if (_advertiser.Payment_Method_Id == (int)PaymentMethod.Manual)
        {
            labelBillingMethod.Text = DictionaryBLL.GetNameById(_advertiser.Billing_Method_Id);
            labelBillingMethod.Visible = true;
            validatorBillingMethod1.Enabled = false;
            validatorBillingMethod2.Enabled = false;
            listBillingMethod.Visible = false;

            labelPaymentMethod.Text = DictionaryBLL.GetNameById(_advertiser.Payment_Method_Id);
            labelPaymentMethod.Visible = true;
            validatorPaymentMethod1.Enabled = false;
            validatorPaymentMethod2.Enabled = false;
            listPaymentMethod.Visible = false;
        }
        else
        {
            // Fill payment method list.
            listBillingMethod.DataTextField = "Item_Desc";
            listBillingMethod.DataValueField = "Item_Id";
            listBillingMethod.DataSource = DictionaryBLL.GetBillingMethodList();
            listBillingMethod.DataBind();
            listBillingMethod.Items.FindByValue(_advertiser.Billing_Method_Id.ToString()).Selected = true;

            // Fill payment method list.
            listPaymentMethod.DataTextField = "Item_Desc";
            listPaymentMethod.DataValueField = "Item_Id";
            listPaymentMethod.DataSource = DictionaryBLL.GetPaymentMethodList();
            listPaymentMethod.DataBind();
            listPaymentMethod.Items.FindByValue(_advertiser.Payment_Method_Id.ToString()).Selected = true;
        }

        // Fill country list.
        listCountry.DataTextField = "Country_Name";
        listCountry.DataValueField = "Country_Id";
        listCountry.DataSource = DictionaryBLL.GetCountryList();            
        listCountry.DataBind();
        listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));

        TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);

        // Fill data from db.
        labelEmail.Text = _advertiser.Email;                        
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
        checkGetEmailAnnouncements.Checked = (_advertiser.Get_Email_Announcements == 1) ? true : false;
        checkGetEmailNewsletters.Checked = (_advertiser.Get_Email_Newsletters == 1) ? true : false;

        // Check payment method status.
        if (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard)
        {
            if (billing.Card_Id == "0")
            {
                panelCreditCardError.Visible = true;
            }
            else
            {
                panelCreditCardValid.Visible = true;
            }
        }
            
        // Prepay add funds message.
        if (_advertiser.Billing_Method_Id == (int)BillingMethod.Prepay)
        {
            string paymentMethod = (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard ? "CreditCard" : "PayPal");

            panelPrepay.Visible = true;
            labelPrepayBalance.Text = Math.Round(AdvertiserBLL.GetBalance(_advertiser.Advertiser_Id)).ToString();
            linkPrepay.PostBackUrl = "Billing" + paymentMethod + ".aspx";
        }
    }

    /// <summary>
    /// Submit sign up form.
    /// </summary>
    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        // If form is not valid, exit.
        if (!Page.IsValid)
        {
            if (!validatorPaymentMethod1.IsValid)
                listPaymentMethod.Focus();
            else if (!validatorPaymentMethod2.IsValid)
                listPaymentMethod.Focus();
            else if (!validatorBillingMethod1.IsValid)
                listBillingMethod.Focus();
            else if (!validatorBillingMethod2.IsValid)
                listBillingMethod.Focus();

            // Hide changes saved status.
            labelChangesSaved.Visible = false;

            return;
        }

        // Get form data.
        int billingMethod;
        int paymentMethod;
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

        if (_advertiser.Payment_Method_Id == (int)PaymentMethod.Manual)
        {
            billingMethod = _advertiser.Billing_Method_Id;
            paymentMethod = _advertiser.Payment_Method_Id;
        }
        else
        {
            billingMethod = Convert.ToInt16(listBillingMethod.SelectedValue);
            paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);
        }

        // Update advertiser data.
        AdvertiserBLL.Update(_advertiser.Advertiser_Id,
                                _advertiser.Status_Id,
                                billingMethod,
                                paymentMethod,
                                getEmailAnnouncements,
                                getEmailNewsletters,
                                _advertiser.Min_Billing_Amount,
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

        // Redirect to page.
        Response.Redirect("AccountPreferences.aspx?saved=true");
    }

    protected void checkPaymentMethod_ServerValidate(Object sender, ServerValidateEventArgs e)
    {
        int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);

        // When postpay, changes not allowed.
        if (_advertiser.Billing_Method_Id == (int)BillingMethod.Postpay &&
            AdvertiserBLL.GetBalance(_advertiser.Advertiser_Id) < 0 &&
            _advertiser.Payment_Method_Id != paymentMethod)
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
        int billingMethod = Convert.ToInt16(listBillingMethod.SelectedValue);
        int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);

        // Postpay is not allowed using PayPal.
        if (billingMethod == (int)BillingMethod.Postpay &&
            paymentMethod == (int)PaymentMethod.PayPal)
        {
            validatorBillingMethod2.ErrorMessage = "* Postpay is not permitted with Paypal payments.";
            e.IsValid = false;
        }
        else
        {
            // When postpay, changes not allowed.
            if (_advertiser.Billing_Method_Id == (int)BillingMethod.Postpay && 
                AdvertiserBLL.GetBalance(_advertiser.Advertiser_Id) < 0 &&
                _advertiser.Billing_Method_Id != billingMethod) 
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

