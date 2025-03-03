using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class NewCampaign : System.Web.UI.Page
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
                advertiserId = Convert.ToInt16(Page.Request.QueryString["AdvertiserId"].ToString());

                _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (_advertiser == null)
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
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + "<a href='ManageCampaigns.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>" + _advertiser.Email + "</a> (<a href='EditAdvertiser.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>edit</a>)" + " » " +
                                    "New Campaign";

            // Set image restrictions.
            labelImageFormat.Text = ApplicationConfiguration.ALLOWED_IMAGE_TYPE;
            labelImageFileSize.Text = (ApplicationConfiguration.ALLOWED_IMAGE_SIZE / 1024).ToString();

            // Set video restrictions.
            //labelVideoFormat.Text = ApplicationConfiguration.ALLOWED_VIDEO_TYPE;
            //labelVideoFileSize.Text = (ApplicationConfiguration.ALLOWED_VIDEO_SIZE / 1024).ToString();
            labelVideoDimensions.Text = null; // TODO: Set video dimensions restrictions.

            // Set max slogan length.
            textAdSlogan.MaxLength = ApplicationConfiguration.MAX_SLOGAN_LENGTH;

            // Fill ad type list.
            listAdType.DataSource = DictionaryBLL.GetAdTypeListForAdmin();//GetAdTypeList();
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

            // Set radio buttons defaults.
            radioCountry.SelectedIndex = 0;
            radioLanguage.SelectedIndex = 0;
            radioCategory.SelectedIndex = 0;
            radioKeywords.SelectedIndex = 0;
            radioSchedule.SelectedIndex = 0;

            radioCountry.Attributes.Add("onClick", "toggleCountryTargeting();");
            radioLanguage.Attributes.Add("onClick", "toggleLanguageTargeting();");
            radioCategory.Attributes.Add("onClick", "toggleCategoryTargeting();");
            radioKeywords.Attributes.Add("onClick", "toggleKeywordsTargeting();");
            radioSchedule.Attributes.Add("onClick", "toggleSchedule();");

            // Fill country boxes.
            foreach (T_COUNTRY country in DictionaryBLL.GetCountryList())
            {
                ListItem item = new ListItem(country.Country_Name, country.Country_Id.ToString());
                checkCountry.Items.Add(item);
            }

            // Fill language boxes.
            foreach (T_LANGUAGE language in DictionaryBLL.GetLanguageList())
            {
                ListItem item = new ListItem(language.Language_Name, language.Language_Id.ToString());
                checkLanguage.Items.Add(item);
            }

            // Fill categories boxes.
            foreach (T_CATEGORY category in DictionaryBLL.GetCategoryList())
            {
                ListItem item = new ListItem(category.Category_Desc, category.Category_Id.ToString());
                checkCategory.Items.Add(item);
            }

            // Schedule dates.
            PickerFrom.SelectedDate = CalendarFrom.SelectedDate = DateTime.Today;
            PickerTo.SelectedDate = CalendarTo.SelectedDate = DateTime.Today.AddMonths(3);

            // Hide image holders.
            UploadedImageHolder.Style.Add("display", "none");
            labelImageValidation.Style.Add("display", "none");
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
                    break;
                case (int)AdType.SloganAndImage:
                    displayImage = "block";
                    displayTextMessageInput = "table-row";
                    displayImageInput = "table-row";
                    AdTypeClickURLHolder.Style.Add("display", "block");
                    AdTypeClickURLHolder.Style.Add("display", "table-row");
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

            // Hide/display targeting options.
            countryTargeting.Style.Add("display", (radioCountry.SelectedIndex == 0) ? "none" : "block");
            languageTargeting.Style.Add("display", (radioLanguage.SelectedIndex == 0) ? "none" : "block");
            categoryTargeting.Style.Add("display", (radioCategory.SelectedIndex == 0) ? "none" : "block");
            keywordsTargeting.Style.Add("display", (radioKeywords.SelectedIndex == 0) ? "none" : "block");
            scheduleDates.Style.Add("display", (radioSchedule.SelectedIndex == 0) ? "none" : "block");
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
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
            string campaignName = textCampaignName.Text.Trim();
            bool bonusCampaign = checkBonusCampaign.Checked;
            int dailyBudget = int.Parse(textDailyBudget.Text.Trim());
            decimal maxCpt = decimal.Parse(textMaxPpt.Text.Trim());
            string adName = textAdName.Text.Trim();
            int adType = int.Parse(listAdType.SelectedValue);
            string adSlogan = textAdSlogan.Text.Trim();
            bool rtl = checkRtl.Checked;

            // Build selected countries list.
            List<int> selectedCountriesList = new List<int>();
            if (radioCountry.SelectedIndex > 0)
            {
                foreach (ListItem country in checkCountry.Items)
                {
                    // Check if current country is selected.
                    if (country.Selected)
                    {
                        selectedCountriesList.Add(int.Parse(country.Value));
                    }
                }
            }

            // Build selected languages list.
            List<int> selectedLanguagesList = new List<int>();
            if (radioLanguage.SelectedIndex > 0)
            {
                foreach (ListItem language in checkLanguage.Items)
                {
                    // Check if current language is selected.
                    if (language.Selected)
                    {
                        selectedLanguagesList.Add(int.Parse(language.Value));
                    }
                }
            }

            // Build selected categories list.
            List<int> selectedCategoriesList = new List<int>();
            if (radioCategory.SelectedIndex > 0)
            {
                foreach (ListItem category in checkCategory.Items)
                {
                    // Check if current category is selected.
                    if (category.Selected)
                    {
                        selectedCategoriesList.Add(int.Parse(category.Value));
                    }
                }
            }

            // Build selected keywords list.
            List<string> selectedKeywordsList = new List<string>();
            if (radioKeywords.SelectedIndex > 0)
            {
                if (textKeywords.Text != "")
                {
                    selectedKeywordsList = textKeywords.Text.Split(',').ToList();
                }
            }

            // Build scheduling dates.
            List<DateTime> scheduleDatesList = new List<DateTime>();
            if (radioSchedule.SelectedIndex == 1)
            {
                scheduleDatesList.Add(PickerFrom.SelectedDate);
                scheduleDatesList.Add(PickerTo.SelectedDate);
            }

            int campaignPaymentType = (int)CampaignPaymentType.Fit;
            if (rbCampaignPaymentTypeClick.Checked) campaignPaymentType = (int)CampaignPaymentType.Click;

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
                    if (string.IsNullOrEmpty(txtImageClickURL.Text) == false)
                    {
                        adUrl = txtImageClickURL.Text.Trim();
                    }
                    if (string.IsNullOrEmpty(txtLikeURL.Text) == false)
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

            throw new NotImplementedException();

            /*
            // Create new campaign.
            int campaignId = CampaignBLL.Add(_advertiser.Advertiser_Id,
                                             campaignName,
                                             selectedLanguagesList,
                                             selectedCountriesList,
                                             selectedCategoriesList,
                                             selectedKeywordsList,
                                             dailyBudget,
                                             scheduleDatesList,
                                             bonusCampaign,
                                             campaignPaymentType);

            // Send mail to administrator.
            Mail.SendNewCampaignAdminMail(_advertiser.Advertiser_Id, _advertiser.Email, campaignId, campaignName);

            // Create new ad.
            int adId = AdBLL.Add(_advertiser.Advertiser_Id,
                                 campaignId,
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

            // Send mail to administrator.
            Mail.SendNewAdAdminMail(_advertiser.Advertiser_Id, _advertiser.Email, campaignId, campaignName, adId, adName, adType, adImage);
            

            // Redirect to campaign's ads manage page.
            Response.Redirect("ManageAds.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "&CampaignId=" + campaignId.ToString());
             */
        }

        /// <summary>
        /// Validates that the campaign doesn't already exist.
        /// </summary>
        protected void checkCampaignExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if campagin already exists (by name).
            e.IsValid = (CampaignBLL.IsExist(_advertiser.Advertiser_Id, textCampaignName.Text) ? false : true);
        }

        /// <summary>
        /// Validates that the all keywords are is the right length.
        /// </summary>
        protected void checkKeywordsLength_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // First of all, if selected not to target by keywords - ignore validation.
            if (radioKeywords.SelectedValue == "1")
            {
                e.IsValid = true;
                return;
            }

            List<string> keywordsList = new List<string>();

            // Build selected keywords list.
            if (textKeywords.Text != "")
            {
                keywordsList = textKeywords.Text.Split(',').ToList();
            }

            foreach (string keyword in keywordsList)
            {
                if (keyword.Trim().Length > 50)
                {
                    e.IsValid = false;
                    return;
                }
            }

            // Check and return if website already exists (by url).
            e.IsValid = true;
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
