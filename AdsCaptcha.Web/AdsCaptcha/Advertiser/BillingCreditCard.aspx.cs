using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Advertiser;
using AjaxControlToolkit;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class BillingCreditCard : System.Web.UI.Page, IAdvertiserBillingCreditCard
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
                Response.Redirect(Page.Request.Url.ToString());
            }

            if (_advertiser.Payment_Method_Id == (int)PaymentMethod.PayPal)
                Response.Redirect("BillingPayPal.aspx");

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                if (_advertiser.Payment_Method_Id != (int)PaymentMethod.CreditCard)
                {
                    panelForm.Visible = false;
                    labelMessage.Text = "Your payment method is not credit card.";
                    panelMessage.Visible = true;
                }
                else
                {
                    InitControls();
                }
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"BillingSummary.aspx\">Billing</a>" + " &gt; ";

            switch (_advertiser.Billing_Method_Id)
            {
                case (int)BillingMethod.Postpay:
                    labelTitle.Text = "Set Credit Card";
                    labelNavigationPath.Text += "Set Credit Card";
                    amountHolder.Visible = false;
                    break;
                case (int)BillingMethod.Prepay:
                    labelTitle.Text = "Add Credit";
                    labelNavigationPath.Text += "Add Credit";
                    amountHolder.Visible = true;
                    break;
                default:
                    Response.Redirect("BillingSummary.aspx");
                    break;
            }

            // Fill credit cards list.
            listCreditCard.DataTextField = "Item_Desc";
            listCreditCard.DataValueField = "Item_Id";
            listCreditCard.DataSource = DictionaryBLL.GetCreditCardList();
            listCreditCard.DataBind();
            listCreditCard.ClearSelection();
            listCreditCard.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCreditCard.SelectedIndex = 0;

            // Expiration date.
            for (int i = 1; i <= 12; i++)
            {
                int month = i;
                listExpirationMonth.Items.Add(month.ToString().PadLeft(2, '0'));
            }
            for (int i = 0; i < 10; i++)
            {
                int year = DateTime.Today.Year + i;
                listExpirationYear.Items.Add(year.ToString());
            }

            // Set default amount to credit.
            textAmount.Text = ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT.ToString();
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorAmount.IsValid)
                    textAmount.Focus();

                return;
            }

            try
            {
                // Get billing information.
                TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);

                CreditCardBLL.CreditCardResult result = new CreditCardBLL.CreditCardResult();

                // Get data.
                int creditCard;
                if (string.IsNullOrEmpty(listCreditCard.SelectedValue) || !int.TryParse(listCreditCard.SelectedValue, out creditCard))
                {
                    labelError.Text = "Please select your credit card type";
                    labelError.Visible = true;
                    return;
                }

                textCardNumber.Text = textCardNumber.Text.Trim().Replace("-", "");
                if (string.IsNullOrEmpty(textCardNumber.Text))
                {
                    labelError.Text = "Please type your credit card number";
                    labelError.Visible = true;
                    return;
                }
                string cardNumber = textCardNumber.Text;

                if (string.IsNullOrEmpty(listExpirationMonth.SelectedValue) || string.IsNullOrEmpty(listExpirationYear.SelectedValue))
                {
                    labelError.Text = "Please select your credit card expiration date";
                    labelError.Visible = true;
                    return;
                }
                string cardExpiration = listExpirationMonth.SelectedValue + listExpirationYear.SelectedValue.Substring(2, 2);

                int checkAmount = (_advertiser.Billing_Method_Id == (int)BillingMethod.Postpay ? ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT : 10);

                // Validate credit card and get card id.
                result = CreditCardBLL.IsValid(cardNumber, cardExpiration, checkAmount);

                // Check if card is valid.
                if (!result.Status)
                {
                    labelError.Text = result.ErrorMessage;
                    labelError.Visible = true;
                    return;
                }

                // Get card id.
                string cardId = result.CardID;

                // Update credit card data.
                AdvertiserBLL.UpdateCreditCard(_advertiser.Advertiser_Id, cardId, cardExpiration);

                if (_advertiser.Billing_Method_Id == (int)BillingMethod.Postpay)
                {
                    Response.Redirect("BillingSummary.aspx", false);
                }
                else if (_advertiser.Billing_Method_Id == (int)BillingMethod.Prepay)
                {
                    // Get form data.
                    decimal tmp;
                    bool isNum = decimal.TryParse(textAmount.Text, out tmp);
                    int amount = Convert.ToInt32(tmp);

                    if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS) throw new Exception("Amount is invalid");

                    bool paymentStatus = false;

                    // Charge advertiser.
                    result = CreditCardBLL.Debit(cardId, cardExpiration, amount);

                    // If charged, add charge transaction.
                    if (result.Status)
                    {
                        try
                        {
                            // Add charge transaction.
                            AdvertiserBLL.ChargeCreditCard(_advertiser.Advertiser_Id, amount, null, result.TransID, result.AuthorizationNumber, null);
                        }
                        catch
                        {
                            AdvertiserBLL.ChargeCreditCard(_advertiser.Advertiser_Id, amount, null, result.TransID, result.AuthorizationNumber, null);
                        }
                        finally
                        {

                            try
                            {
                                Mail.SendCreditCardPaymentMail(_advertiser.Email);
                            }
                            catch
                            {
                                Mail.SendCreditCardPaymentMail(_advertiser.Email);
                            }

                        }
                        // Send confirmation mail.
                        

                        paymentStatus = true;
                    }
                    else
                    {
                        labelError.Text = result.ErrorMessage;
                        labelError.Visible = true;
                    }

                    if (paymentStatus)
                    {
                        panelForm.Visible = false;
                        panelMessage.Visible = true;

                        labelMessage.Text = "Thank you for your payment. It is currently being verified.<br/>Following verification, the payment will appear in your account balance.";
                    }
                    else
                    {
                        panelForm.Visible = true;
                        panelMessage.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                labelError.Text = "An error occured, please contact our support team";
                labelError.Visible = true;
                
                NLogManager.logger.ErrorException("An error occured while trying to bill an advertiser!", ex);
            }
        }

        #region Validation Controls

        protected void checkAmount_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            if (_advertiser.Billing_Method_Id != (int)BillingMethod.Prepay)
            {
                e.IsValid = true;
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(textAmount.Text))
                {
                    validatorAmount.ErrorMessage = "* Required";
                    e.IsValid = false;
                    return;
                }                

                decimal tmp;
                bool isNum = decimal.TryParse(textAmount.Text, out tmp);
                int amount = Convert.ToInt32(tmp);

                if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS)
                {
                    validatorAmount.ErrorMessage = "* Min. $" + ApplicationConfiguration.MIN_PREPAY_FUNDS.ToString();
                    e.IsValid = false;
                    return;
                }
                else
                {
                    e.IsValid = true;
                    return;
                }
            }
        }

        #endregion Validation Controls    
    }
}
