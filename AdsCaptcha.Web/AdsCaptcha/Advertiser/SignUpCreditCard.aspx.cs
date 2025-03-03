using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Advertiser;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class SignUpCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if new advertiser data exists.
            if (Session["NewAdvertiserData"] == null)
                Response.Redirect("SignUp.aspx");

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                InitControls();
            }
        }

        private void InitControls()
        {
            // Get advertiser data.
            NewAdvertiserData advertiser = new NewAdvertiserData();
            advertiser = (NewAdvertiserData)Session["NewAdvertiserData"];

            // Set default amount to credit.
            textAmount.Text = "100";

            // Set min. amount validation rules.
            validatorAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.MIN_PREPAY_FUNDS.ToString();

            // Check if pre or post pay, to decide if to display the amount to charge field.
            if (advertiser.BillingMethod == (int)BillingMethod.Prepay)
            {
                amountHolder.Visible = true;
                validatorAmount1.Enabled = true;
                validatorAmount2.Enabled = true;
            }
            else
            {
                amountHolder.Visible = false;
                validatorAmount1.Enabled = false;
                validatorAmount2.Enabled = false;
            }
                        
            // Fill credit cards list.
            listCreditCard.DataTextField = "Item_Desc";
            listCreditCard.DataValueField = "Item_Id";
            listCreditCard.DataSource = DictionaryBLL.GetCreditCardList();
            listCreditCard.DataBind();
            listCreditCard.ClearSelection();
            listCreditCard.Items.Insert(0, new ListItem("-- Select --", "0"));
            listCreditCard.SelectedIndex = 0;

            // Expiration month.
            for (int i = 1; i <= 12; i++)
            {
                int month = i;
                listExpirationMonth.Items.Add(month.ToString().PadLeft(2, '0'));
            }

            // Expiration year.
            for (int i = 0; i < 10; i++)
            {
                int year = DateTime.Today.Year + i;
                listExpirationYear.Items.Add(year.ToString());
            }
        }

        private int CreateAdvertiser(NewAdvertiserData advertiser, NewCampaignData campaign)
        {
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

            bool bonusCampaign = false;

            throw new NotImplementedException();

            /*
            // Create new campaign.
            int campaignId = CampaignBLL.Add(advertiserId,
                                             campaign.CampaignName,
                                             campaign.LanguagesList,
                                             campaign.CountriesList,
                                             campaign.CategoriesList,
                                             campaign.KeywordsList,
                                             campaign.DailyBudget,
                                             campaign.ScheduleDatesList,
                                             bonusCampaign,
                                             campaign.CampaignPaymentType);

            // Create new ad.
            int adId = AdBLL.Add(advertiserId,
                                 campaignId,
                                 campaign.AdName,
                                 campaign.AdType,
                                 campaign.Width,
                                 campaign.Height,
                                 campaign.AdSlogan,
                                 campaign.AdImage,
                                 campaign.AdVideo,
                                 campaign.ClickUrl,
                                 campaign.LikeUrl,
                                 campaign.MaxBid,
                                 campaign.AdRtl);
            
            // Send welcome mail.
            Mail.SendNewAdvertiserMail(advertiser.Email);

            // Send mail to administrator.
            Mail.SendNewCampaignAdminMail(advertiserId, advertiser.Email, campaignId, campaign.CampaignName);

            // Send mail to administrator.
            Mail.SendNewAdAdminMail(advertiserId, advertiser.Email, campaignId, campaign.CampaignName, adId, campaign.AdName, campaign.AdType, campaign.AdImage);

             * 
             */
            // Clear session data.
            Session["NewCampaignData"] = null;
            Session["NewAdvertiserData"] = null;

            // Save advertiser in session.
            Session["AdvertiserId"] = advertiserId;
            Session["AdvertiserEmail"] = advertiser.Email;

            return advertiserId;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorCreditCard.IsValid)
                    listCreditCard.Focus();
                else if (!validatorCardNumber.IsValid)
                    textCardNumber.Focus();
                else if (!validatorAmount1.IsValid)
                    textAmount.Focus();
                else if (!validatorAmount2.IsValid)
                    textAmount.Focus();

                return;
            }

            // Get form data.
            int creditCard = Convert.ToInt16(listCreditCard.SelectedValue);
            string cardNumber = textCardNumber.Text.Trim();
            string cardExpiration = listExpirationMonth.SelectedValue + listExpirationYear.SelectedValue.Substring(2, 2);

            // Get advertiser data.
            NewAdvertiserData advertiser = new NewAdvertiserData();
            advertiser = (NewAdvertiserData)Session["NewAdvertiserData"];

            int amount;
            CreditCardBLL.CreditCardResult result = new CreditCardBLL.CreditCardResult();

            // Check billing method.
            switch (advertiser.BillingMethod)
            {
                case (int)BillingMethod.Postpay:
                    amount = ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT;
                    break;
                case (int)BillingMethod.Prepay:
                    amount = Convert.ToInt32(textAmount.Text);
                    break;
                default:
                    throw new Exception("Billing method not allowed");
            }

            // Check if credit card is valid.
            result = CreditCardBLL.IsValid(cardNumber, cardExpiration, amount);

            // Check if card is valid.
            if (!result.Status)
            {
                labelError.Text = result.ErrorMessage;
                labelError.Visible = true;
                return;
            }

            // Get campaign data.
            NewCampaignData campaign = new NewCampaignData();
            campaign = (NewCampaignData)Session["NewCampaignData"];

            // Create advertiser.
            int advertiserId = CreateAdvertiser(advertiser, campaign);

            // Get card id.
            string cardId = result.CardID;

            // Update advertiser's credit card id.
            AdvertiserBLL.UpdateCreditCard(advertiserId, cardId, cardExpiration);

            // If prepay, charge advertiser.
            if (advertiser.BillingMethod == (int)BillingMethod.Prepay)
            {
                // Charge advertiser.
                result = CreditCardBLL.Debit(cardId, cardExpiration, amount);
                
                // If charged, add charge transaction.
                if (result.Status)
                {
                    // Add charge transaction.
                    AdvertiserBLL.ChargeCreditCard(advertiserId, amount, null, result.TransID, result.AuthorizationNumber, null);

                    // Send confirmation mail.
                    Mail.SendCreditCardPaymentMail(advertiser.Email);
                }
            }

            // Redirect to manage campaings page.
            Response.Redirect("ManageCampaigns.aspx");
        }

        #region Validation Controls

        protected void checkAmount_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            decimal tmp;
            bool isNum = decimal.TryParse(textAmount.Text, out tmp);
            int amount = Convert.ToInt32(tmp);

            if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        #endregion Validation Controls
    }
}
