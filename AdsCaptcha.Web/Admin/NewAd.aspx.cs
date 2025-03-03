﻿using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class NewAd : System.Web.UI.Page
    {
        private TA_CAMPAIGN _campaign;
        private int advertiserId;
        private int campaignId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                advertiserId = Convert.ToInt16(Page.Request.QueryString["AdvertiserId"].ToString());
                campaignId = Convert.ToInt16(Page.Request.QueryString["CampaignId"].ToString());

                _campaign = CampaignBLL.GetCampaign(advertiserId, campaignId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }
            
            if (_campaign == null)
            {
                Response.Redirect("ManageCampaigns.aspx?AdvertiserId=" + advertiserId.ToString());
            }

            if (!IsPostBack)
            {
                InitControls();
            }

            ShowHide();
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
            string advertiser = AdvertiserBLL.GetAdvertiser(_campaign.Advertiser_Id).Email;

            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + "<a href='ManageCampaigns.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "'>" + advertiser + "</a> (<a href='EditAdvertiser.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Campaign: " + "<a href='ManageAds.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "&CampaignId=" + _campaign.Campaign_Id.ToString() + "'>" + _campaign.Campaign_Name + "</a> (<a href='EditCampaign.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "&CampaignId=" + _campaign.Campaign_Id.ToString() + "'>edit</a>)" + " » " +
                                    "New Ad";

            // Set image restrictions.
            labelImageFormat.Text = ApplicationConfiguration.ALLOWED_IMAGE_TYPE;
            labelImageFileSize.Text = (ApplicationConfiguration.ALLOWED_IMAGE_SIZE / 1024).ToString();

            // Set video restrictions.
            //labelVideoFormat.Text = ApplicationConfiguration.ALLOWED_VIDEO_TYPE;
            //labelVideoFileSize.Text = (ApplicationConfiguration.ALLOWED_VIDEO_SIZE / 1024).ToString();
            //labelVideoDimensions.Text = null; // TODO: Set video dimensions restrictions.

            // Set max slogan length.
            textAdSlogan.MaxLength = ApplicationConfiguration.MAX_SLOGAN_LENGTH;

            // Fill ad type list.
            listAdType.DataSource = DictionaryBLL.GetAdTypeListForAdmin();//DictionaryBLL.GetAdTypeList();
            listAdType.DataBind();

            // Set captcha type default value.
            listAdType.ClearSelection();
            int defaultAdType = (int)AdType.Slide2Fit;
            listAdType.Items.FindByValue(defaultAdType.ToString()).Selected = true;

            listAdType.Attributes.Add("onChange", "javascript:OnAdTypeChange();");

            AdTypeHolder0.Style.Add("display", "none");  // Slogan Only
            AdTypeHolder1.Style.Add("display", "block"); // Slogan & Image
            AdTypeHolder2.Style.Add("display", "none");  // Image Only
            AdTypeHolder3.Style.Add("display", "none");  // Slogan & Coupon
            AdTypeHolder4.Style.Add("display", "none");  // Slogan & Video

            AdTypeImageHolder.Style.Add("display", "table-row");
            AdTypeVideoHolder.Style.Add("display", "none");
            AdTypeCouponHolder.Style.Add("display", "none");

            // Hide image holders.
            UploadedImageHolder.Style.Add("display", "none");
            labelImageValidation.Style.Add("display", "none");

            FillDefaultData();
        }

        private void FillDefaultData()
        {
            TA_AD adv = AdBLL.GetLastCampaignAd(_campaign.Advertiser_Id, _campaign.Campaign_Id);
            if (adv != null)
            {
                textMaxPpt.Text = String.Format("{0:0.##}", adv.Max_Cpt); 
                txtImageClickURL.Text = adv.Ad_Url;
                txtLikeURL.Text = adv.Ad_Like_Url;

            }
        }

        /// <summary>
        /// Show/hide dynamic content.
        /// </summary>
        private void ShowHide()
        {
            string displaySlogan = "none";
            string displayImage = "none";
            string displayVideo = "none";
            string displayCoupon = "none";
            string displayAdGame = "none";
            string displayTextMessageInput = "none";
            string displayImageInput = "none";
            string displayVideoInput = "none";
            string displayCouponInput = "none";

            // Set ad type attributes.
            switch (int.Parse(listAdType.SelectedValue))
            {
                case (int)AdType.SloganOnly:
                    displaySlogan = "block";
                    displayTextMessageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "none");
                    AdTypeClickLikeHolder.Style.Add("display", "none");
                    break;
                case (int)AdType.SloganAndImage:
                    displayImage = "block";
                    displayTextMessageInput = "table-row";
                    displayImageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "block");
                    AdTypeClickURLHolder.Style.Add("display", "table-row");
                    AdTypeClickLikeHolder.Style.Add("display", "block");
                    AdTypeClickLikeHolder.Style.Add("display", "table-row");
                    break;
                case (int)AdType.SloganAndVideo:
                    displayVideo = "block";
                    displayTextMessageInput = "table-row";
                    displayVideoInput = "table-row";
                    break;
                case (int)AdType.Slide2Fit:
                    displayAdGame = "block";
                    displayImageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "block");
                    AdTypeClickURLHolder.Style.Add("display", "table-row");
                    AdTypeClickLikeHolder.Style.Add("display", "block");
                    AdTypeClickLikeHolder.Style.Add("display", "table-row");
                    break;
            }

            AdTypeHolder0.Style.Add("display", displaySlogan); // Slogan Only
            AdTypeHolder1.Style.Add("display", displayImage);  // Slogan & Image
            AdTypeHolder2.Style.Add("display", displayAdGame); // Image Only
            AdTypeHolder3.Style.Add("display", displayCoupon); // Slogan & Coupon
            AdTypeHolder4.Style.Add("display", displayVideo);  // Slogan & Video
            TextMessageHolder.Style.Add("display", displayTextMessageInput);
            AdTypeImageHolder.Style.Add("display", displayImageInput);
            AdTypeVideoHolder.Style.Add("display", displayVideoInput);
            AdTypeCouponHolder.Style.Add("display", displayCouponInput);
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // Set captcha type default value.
            listAdType.ClearSelection();
            int defaultAdType = (int)AdType.Slide2Fit;
            listAdType.Items.FindByValue(defaultAdType.ToString()).Selected = true;
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                return;
            }

            // Check upload validation.
            if (!IsUploadImageExists())
            {
                return;
            }

            // Check image dimension validation.
            if (!IsDimensionValid())
            {
                UploadHidden.Focus();
                return;
            }

            // Get values.
            decimal maxCpt = decimal.Parse(textMaxPpt.Text.Trim());
            string adName = textAdName.Text.Trim();
            int adType = int.Parse(listAdType.SelectedValue);
            string adSlogan = textAdSlogan.Text.Trim();
            bool rtl = checkRtl.Checked;

            string adImage = null;
            string adVideo = null;
            string adUrl = null;
            string adLikeUrl = null;
            Nullable<int> width = null;
            Nullable<int> height = null;

            // Handle ad type attributes.
            switch (int.Parse(listAdType.SelectedValue))
            {
                case (int)AdType.SloganOnly:
                    break;
                case (int)AdType.Slide2Fit:
                case (int)AdType.SloganAndImage:
                    adImage = UploadHidden.Value;
                    // image click url support - START
                    if (!string.IsNullOrEmpty(txtImageClickURL.Text))
                    {
                        adUrl = txtImageClickURL.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtLikeURL.Text))
                    {
                        adLikeUrl = txtLikeURL.Text.Trim();
                    }
                    // image click url support - END
                    width = Convert.ToInt16(UploadWidth.Value);
                    height = Convert.ToInt16(UploadHeight.Value);
                    break;
                case (int)AdType.SloganAndVideo:
                    break;
            }

            // Create new ad.
            throw new NotImplementedException();
            /*
            int adId = AdBLL.Add(_campaign.Advertiser_Id,
                                 _campaign.Campaign_Id,
                                 adName,
                                 adType,
                                 width,
                                 height,
                                 adSlogan,
                                 adImage,
                                 adVideo,
                                 adUrl,
                                 adLikeUrl,
                                 maxCpt,
                                 rtl);
             

            string advertiserEmail = AdvertiserBLL.GetAdvertiser(_campaign.Advertiser_Id).Email;

            // Send mail to administrator.
            Mail.SendNewAdAdminMail(_campaign.Advertiser_Id, advertiserEmail, _campaign.Campaign_Id, _campaign.Campaign_Name, adId, adName, adType, adImage);
*/
            // Redirect to campaign's ads manage page.
            Response.Redirect("ManageAds.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "&CampaignId=" + _campaign.Campaign_Id.ToString());
        }

        /// <summary>
        /// Validates that the ad doesn't already exist.
        /// </summary>
        protected void checkAdExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if ad already exists (by name).
            e.IsValid = (AdBLL.IsExist(_campaign.Advertiser_Id, _campaign.Campaign_Id, textAdName.Text) ? false : true);
        }

        /// <summary>
        /// Check if text message entered only if not slider.
        /// </summary>
        protected void checkTextMessage_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            int adType = int.Parse(listAdType.SelectedValue);
            string textMessage = textAdSlogan.Text.Trim();

            if (adType != (int)AdType.Slide2Fit)
            {
                if (string.IsNullOrEmpty(textMessage))
                {
                    e.IsValid = false;
                    validatorAdSlogan.ErrorMessage = "* Required";
                    return;
                }
                // TODO: Check english letters only
            }

            e.IsValid = true;
        }

        /// <summary>
        /// Validates upload image file.
        /// </summary>
        private bool IsUploadImageExists()
        {
            // First of all, if selected ad type is not image, we can skip the check (actually, Valid=True).
            if (int.Parse(listAdType.SelectedValue) != (int)AdType.SloganAndImage &&
                int.Parse(listAdType.SelectedValue) != (int)AdType.Slide2Fit)
            {
                return true;
            }

            //bool urlCheck = General.IsUrlExists(ConfigurationSettings.AppSettings["URL"] + ApplicationConfiguration.UPLOADS_DIRECTORY + UploadHidden.Value);
            bool urlCheck = General.IsUrlExists(ConfigurationSettings.AppSettings["AWSBucketUrl"] + UploadHidden.Value);

            // Check if temporary (uploaded file) exists.
            if (!urlCheck)
            {
                labelImageValidation.Text = "* Required";
                labelImageValidation.Style["display"] = "block";
                return false;
            }
            else
            {
                labelImageValidation.Style["display"] = "none";
                return true;
            }
        }

        private bool IsDimensionValid()
        {
            int adType = int.Parse(listAdType.SelectedValue);

            // First of all, if selected ad type is not image, we can skip the check (actually, Valid=True).
            if (adType != (int)AdType.SloganAndImage &&
                adType != (int)AdType.Slide2Fit)
            {
                return true;
            }

            int width = int.Parse(AdWidth.Value);
            int height = int.Parse(AdHeight.Value);

            if (!AdBLL.IsDimensionValidForAdmin(adType, width, height))
            {
                labelImageValidation.Text = "* Image dimension is not allowed for ad type";
                labelImageValidation.Style["display"] = "block";
                return false;
            }
            else
            {
                labelImageValidation.Style["display"] = "none";
                return true;
            }
        }
    }
}
