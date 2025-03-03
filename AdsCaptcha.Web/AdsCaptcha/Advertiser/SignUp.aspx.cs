using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Advertiser;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;
using NLog;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class SignUp : System.Web.UI.Page
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private NewCampaignArgs _newCampaignArgs;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if new campaign data exists.
            _newCampaignArgs = GetNewCampaignArgs();

            if (null == _newCampaignArgs)
            {
                Response.Redirect("NewCampaign.aspx?Action=SignUp");
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.AdvertiserSignUp, Master.Page.Header);
                
                InitControls();
            }
        }

        private NewCampaignArgs GetNewCampaignArgs()
        {
            NewCampaignArgs newCampaign = null;
            string strNewCampaignArgs = null;
            try
            {
                strNewCampaignArgs = Request["campaign"];
                if (strNewCampaignArgs != null)
                {
                    newCampaign = new NewCampaignArgs(JsonConvert.Import<JsonObject>(strNewCampaignArgs));
                }
            }
            catch (Exception ex)
            {
                Log.WarnException("Page_Load: failed to parse campaign: " + strNewCampaignArgs, ex);
            }
            return newCampaign;
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
            // Fill payment method list.
            listBillingMethod.DataTextField = "Item_Desc";
            listBillingMethod.DataValueField = "Item_Id";
            listBillingMethod.DataSource = DictionaryBLL.GetBillingMethodList();
            listBillingMethod.DataBind();
            listBillingMethod.Items.Insert(0, new ListItem("-- Select --", "0"));
            listBillingMethod.SelectedIndex = 0;

            // Fill payment method list.
            listPaymentMethod.DataTextField = "Item_Desc";
            listPaymentMethod.DataValueField = "Item_Id";
            listPaymentMethod.DataSource = DictionaryBLL.GetPaymentMethodList();
            listPaymentMethod.DataBind();
            listPaymentMethod.Items.Insert(0, new ListItem("-- Select --", "0"));
            listPaymentMethod.SelectedIndex = 0;
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorEmail1.IsValid || !validatorEmail2.IsValid || !validatorEmail3.IsValid)
                    textEmail.Focus();
                else if (!validatorPassword1.IsValid || !validatorPassword2.IsValid)
                    textPassword.Focus();
                else if (!validatorConfirmPassword1.IsValid || !validatorConfirmPassword2.IsValid)
                    textPasswordConfirm.Focus();
                else if (!validatorPaymentMethod.IsValid)
                    listPaymentMethod.Focus();
                else if (!validatorBillingMethod1.IsValid)
                    listBillingMethod.Focus();
                else if (!validatorBillingMethod2.IsValid)
                    listBillingMethod.Focus();

                return;
            }

            // Get form data.
            string email = textEmail.Text.Trim();
            string password = textPassword.Text;
            int billingMethod = Convert.ToInt16(listBillingMethod.SelectedValue);
            int paymentMethod = Convert.ToInt16(listPaymentMethod.SelectedValue);
            bool getEmailAnnouncements = checkGetEmailAnnouncements.Checked;
            bool getEmailNewsletters = checkGetEmailNewsletters.Checked;
            string firstName = null;
            string lastName = null;
            string companyName = null;
            string address = null;
            string city = null;
            string state = null;
            int country = 0;
            string zipCode = null;
            string phone = null;
            string fax = null;

            NewAdvertiserData advertiser = new NewAdvertiserData();
            advertiser.Email = email;
            advertiser.Password = password;
            advertiser.BillingMethod = billingMethod;
            advertiser.PaymentMethod = paymentMethod;
            advertiser.GetEmailAnnouncements = getEmailAnnouncements;
            advertiser.GetEmailNewsletters = getEmailNewsletters;
            advertiser.FirstName = firstName;
            advertiser.LastName = lastName;
            advertiser.CompanyName = companyName;
            advertiser.Address = address;
            advertiser.City = city;
            advertiser.State = state;
            advertiser.Country = country;
            advertiser.ZipCode = zipCode;
            advertiser.Phone = phone;
            advertiser.Fax = fax;

            // Save new advertiser data in session.
            //Session["NewAdvertiserData"] = advertiser;

            // Get campaign data.
            
            // Create advertiser.
            int advertiserId = CreateAdvertiser(advertiser);

            // Log advertiser.
            Session["AdvertiserId"] = advertiserId.ToString();
            Session["AdvertiserEmail"] = textEmail.Text;

            // Redirect to billing page.
            switch (paymentMethod)
            {
                case (int)PaymentMethod.CreditCard:
                    Response.Redirect("BillingCreditCard.aspx");
                    break;
                case (int)PaymentMethod.PayPal:
                    Response.Redirect("BillingPayPal.aspx");
                    break;
                default:
                    throw new Exception("Payment method not allowed");
            }
        }

        #region Validation Controls

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

        /// <summary>
        /// Validates the user checked certify policy.
        /// </summary>
        protected void checkCertifyPolicy_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            e.IsValid = (checkCertifyPolicy.Checked == true);
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

        #endregion Validation Controls

        private int CreateAdvertiser(NewAdvertiserData advertiser)
        {
            //TODO: Refactoring
            // Sign Up advertiser.
            int advertiserId = AdvertiserBLL.SignUp(advertiser.Email,
                                                    advertiser.Password,
                                                    advertiser.BillingMethod,
                                                    advertiser.PaymentMethod,
                                                    advertiser.GetEmailAnnouncements,
                                                    advertiser.GetEmailNewsletters,
                                                    advertiser.FirstName,
                                                    advertiser.LastName,
                                                    advertiser.CompanyName,
                                                    advertiser.Address,
                                                    advertiser.City,
                                                    advertiser.State,
                                                    advertiser.Country,
                                                    advertiser.ZipCode,
                                                    advertiser.Phone,
                                                    advertiser.Fax
                                                    );

            // Send notifier to administrator.
            Mail.SendNewAdvertiserAdminMail(advertiserId, advertiser.Email);
            // Send welcome mail.
            Mail.SendNewAdvertiserMail(advertiser.Email);
            //END TODO: Refactoring

            _newCampaignArgs.AdvertiserId = advertiserId;
            INewCampaignResult campaignResult;
            AdsCaptchaOperationResult<INewCampaignResult> result = CampaignsManager.Add(_newCampaignArgs, true);
            if (result.HasError)
            {
                throw new Exception(result.ToJson().ToString());
            }
            else
            {
                campaignResult = result.Value;
            }
           
            // Clear session data.
            //Session["NewAdvertiserData"] = null;

            // Save advertiser in session.
            Session["AdvertiserId"] = advertiserId;
            Session["AdvertiserEmail"] = advertiser.Email;

            return advertiserId;
        }
    }
}
