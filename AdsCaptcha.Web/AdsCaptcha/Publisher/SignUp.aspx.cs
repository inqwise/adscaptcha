using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Reset publisher session.
            Session["PublisherId"] = null;
            
            // Set PaymentPrefs Session context
            Session[ApplicationConfiguration.SESSION_PAYMENT_PREFS_CONTEXT] = Modules.Publisher;

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.PublisherSignUp, Master.Page.Header);

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
            // Fill country list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.ClearSelection();
            listCountry.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCountry.SelectedIndex = 0;

            // Add country change script.
            listCountry.Attributes["onChange"] = "changeCountry();";

            // Fill credit method list.
            listCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            listCreditMethod.DataBind();
            listCreditMethod.ClearSelection();

            // Add Event to change value
            listCreditMethod.Attributes.Add("onChange", "javascript:OnPaymentMethodChange();");
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorEmail1.IsValid || !validatorEmail2.IsValid || !validatorEmail3.IsValid || !validatorEmail4.IsValid)
                    textEmail.Focus();
                else if (!validatorPassword1.IsValid || !validatorPassword2.IsValid)
                    textPassword.Focus();
                else if (!validatorConfirmPassword1.IsValid || !validatorConfirmPassword2.IsValid)
                    textPasswordConfirm.Focus();
                //else if (!validatorFirstName.IsValid)
                //    textFirstName.Focus();
                //else if (!validatorLastName.IsValid)
                //    textLastName.Focus();
                //else if (!validatorCountry.IsValid)
                //    listCountry.Focus();
                //else if (!validatorAddress.IsValid)
                //    textAddress.Focus();
                //else if (!validatorCity.IsValid)
                //    textCity.Focus();
                //else if (!validatorPhone.IsValid)
                //    textPhone.Focus();
                else if (!validatorCertifyPolicy.IsValid)
                    checkCertifyPolicy.Focus();

                return;
            }

            // Get form data.
            string email = textEmail.Text.Trim();
            string password = textPassword.Text;
            string firstName = textFirstName.Text.Trim();
            string lastName = textLastName.Text.Trim();
            string companyName = textCompanyName.Text.Trim();
            string address = textAddress.Text.Trim();
            string city = textCity.Text.Trim();
            string state = textState.Text.Trim();
            int? country = listCountry.SelectedIndex > 0 ? (int?)Convert.ToInt32(listCountry.SelectedValue) : null;
            string zipCode = textZipCode.Text.Trim();
            string phone = textPhone.Text.Trim();
            string phone2 = textPhone2.Text.Trim();
            int creditMethod = Convert.ToInt16(listCreditMethod.SelectedValue);
            int minCheckAmount = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT;
            bool getEmailAnnouncements = checkGetEmailAnnouncements.Checked;
            bool getEmailNewsletters = checkGetEmailNewsletters.Checked;

            // Get payment preferences.
            string bankName = ctlPaymentBank.BankName;
            string bankBranchName = ctlPaymentBank.BranchName;
            string bankAddress = ctlPaymentBank.Address;
            string bankCity = ctlPaymentBank.City;
            string bankCountry = ctlPaymentBank.Country;
            string bankState = ctlPaymentBank.State;
            string bankHolderName = ctlPaymentBank.HolderName;
            string bankAccountNumber = ctlPaymentBank.AccountNumber;
            string bankCode = ctlPaymentBank.Code;
            string checkPayeeName = ctlPaymentCheck.PayeeName;
            string checkRecipientName = ctlPaymentCheck.RecipientName;
            string checkAddress = ctlPaymentCheck.Address;
            string checkCity = ctlPaymentCheck.City;
            string checkCountry = ctlPaymentCheck.Country;
            string checkState = ctlPaymentCheck.State;
            string checkZipCode = ctlPaymentCheck.ZipCode;
            string paypalPayeeAccount = ctlPaymentPayPal.PayeeAccount;

            // Get activation code.
            //string activationCode = PublisherBLL.SignUp(email,
            //                                            password, 
            //                                            firstName, 
            //                                            lastName, 
            //                                            companyName,
            //                                            address, 
            //                                            city,
            //                                            state,
            //                                            country, 
            //                                            zipCode, 
            //                                            phone, 
            //                                            phone2,
            //                                            creditMethod,
            //                                            minCheckAmount,
            //                                            getEmailAnnouncements,
            //                                            getEmailNewsletters,
            //                                            bankName, bankBranchName, bankAddress, bankCity, bankCountry, bankState, bankHolderName, bankAccountNumber, bankCode, checkPayeeName, checkRecipientName, checkAddress, checkCity, checkCountry, checkState, checkZipCode, paypalPayeeAccount);

            // Save activation code in session.
            //Session["PublisherActivationCode"] = activationCode;

            // Redirect to activation message page.
           // Response.Redirect("Activation.aspx");

            int pubID = PublisherBLL.QuickSignUp(email, password, getEmailAnnouncements, getEmailNewsletters);
            // Send notifier to administrator.
            Mail.SendNewPublisherAdminMail(pubID, email);

            Session["PublisherId"] = pubID;
            Session["PublisherEmail"] = email;

            //HttpCookie cookie = Request.Cookies["AdsCaptchaPublisher"];
            //cookie.Expires = DateTime.Now.AddYears(-30);
            //Response.Cookies.Add(cookie);

            Response.Redirect("SignUp2.aspx?pubid=" + pubID.ToString());
        }

        #region Validation Controls

        /// <summary>
        /// Validates that the user is exsists.
        /// </summary>
        protected void checkEmailExsists_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            string email = textEmail.Text.Trim();

            // Check if publisher already exists (by email).
            if (PublisherBLL.GetPublisher(email) == null)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        /// <summary>
        /// Validates that the user is not waiting for activation.
        /// </summary>
        protected void checkWaitForActivation_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            string email = textEmail.Text.Trim();

            // Check if publisher already waiting for activation.
            e.IsValid = (PublisherBLL.IsWaitingForActivation(email) == false);
        }

        /// <summary>
        /// Validates the user checked certify policy.
        /// </summary>
        protected void checkCertifyPolicy_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            e.IsValid = (checkCertifyPolicy.Checked == true);
        }

        #endregion Validation Controls
    }
}
