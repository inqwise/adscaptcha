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
    public partial class SignUpPayPal : System.Web.UI.Page
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
            // Get advertiser data.
            NewAdvertiserData advertiser = new NewAdvertiserData();
            advertiser = (NewAdvertiserData)Session["NewAdvertiserData"];

            switch (advertiser.BillingMethod)
            {
                case (int)BillingMethod.Postpay:
                    Response.Redirect("SignUp.aspx");
                    break;
                case (int)BillingMethod.Prepay:                    
                    break;
                default:
                    throw new Exception("Billing method not allowed");
            }

            // Set default amount to credit.
            textAmount.Text = "100";

            // Set min. amount validation rules.
            validatorAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.MIN_PREPAY_FUNDS.ToString();
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
                if (!validatorAmount1.IsValid)
                    textAmount.Focus();
                if (!validatorAmount2.IsValid)
                    textAmount.Focus();

                return;
            }

            // Get form data.
            decimal tmp;
            bool isNum = decimal.TryParse(textAmount.Text, out tmp);
            int amount = Convert.ToInt32(tmp);

            if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS) throw new Exception("Amount is invalid");

            // Get advertiser data.
            NewAdvertiserData advertiser = new NewAdvertiserData();
            advertiser = (NewAdvertiserData)Session["NewAdvertiserData"];

            // Get campaign data.
            NewCampaignData campaign = new NewCampaignData();
            campaign = (NewCampaignData)Session["NewCampaignData"];

            // Create advertiser.
            CreateAdvertiser(advertiser, campaign);

            // Redirect to deposit money via PalPal.
            Response.Redirect("PayPalGateway.aspx?amt=" + amount.ToString());
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
