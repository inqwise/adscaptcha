using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Advertiser;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class NewCampaign : System.Web.UI.Page, IAdvertiserNewCampaign
    {
        public string _action;
        public TA_ADVERTISER _advertiser;
        
        public const string ACTION_SIGNUP = "SignUp";
        public const string ACTION_NEW = "New";

        protected string ApiUrl
        {
            get { return ApplicationConfiguration.ApiUrl.Value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get action parameter.
            _action = Request.QueryString["Action"] ?? ACTION_NEW;
            
            // Check page action.
            switch (_action)
            {
                // Signup.
                case ACTION_SIGNUP:
                    
                    // TODO: TEMPORARY ADVERTISERS ACCESS NOT ALLOWED!
                    string allowAdvertisersSignUp = ConfigurationSettings.AppSettings["AllowAdvertisersSignUp"];
                    if (allowAdvertisersSignUp == "false")
                        Response.Redirect("ContactUs.aspx");
                    
                    break;
                
                // New campaign (for exsiting advertiser).
                case ACTION_NEW:
                    

                    // If user is not logged in, redirect to login page.
                    if (Session["AdvertiserId"] == null)
                        Response.Redirect("Login.aspx");

                    try
                    {
                        int advertiserId = Convert.ToInt16(Page.Session["AdvertiserId"]);

                        _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

                        if (_advertiser == null)
                        {
                            // TO DO: Handle advertiser not exists
                            throw new Exception("Advertiser not exists");
                        }
                    }
                    catch
                    {
                        Response.Redirect("ManageCampaigns.aspx");
                    }
                    break;
            }
            
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                // Reset new campaign session data.
                Session["NewCampaignData"] = null;

                FillControls();
            }

            //ShowHide();
        }

        private void FillControls()
        {
            rptCountries.DataSource = DictionaryBLL.GetCountryList();
            rptCountries.DataBind();

            rptCategories.DataSource = DictionaryBLL.GetCategoryList();
            rptCategories.DataBind();
        }


        /*
        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            if (_action == ACTION_NEW)
                labelNavigationPath.Text = "<a href=\"ManageCampaigns.aspx\">Campaigns</a>" + " &gt; " +
                                           "New Campaign";

            // Set image restrictions.
            labelImageFormat.Text = ApplicationConfiguration.ALLOWED_IMAGE_TYPE;
            labelImageFileSize.Text = (ApplicationConfiguration.ALLOWED_IMAGE_SIZE / 1024).ToString();
 
            // Set max slogan length.
            textAdSlogan.MaxLength = ApplicationConfiguration.MAX_SLOGAN_LENGTH;

            // Fill ad type list.
            listAdType.DataSource = DictionaryBLL.GetAdTypeList();
            listAdType.DataBind();

            // Set captcha type default value.
            listAdType.ClearSelection();
            int defaultAdType = (int)AdType.Slide2Fit;
            listAdType.Items.FindByValue(defaultAdType.ToString()).Selected = true;


            //listAdType.Attributes.Add("onChange", "javascript:OnAdTypeChange();");

            //AdTypeHolder0.Style.Add("display", "none");  // Slogan Only
            //AdTypeHolder1.Style.Add("display", "block"); // Slogan & Image
            //AdTypeHolder2.Style.Add("display", "none");  // Image Only
            //AdTypeHolder3.Style.Add("display", "none");  // Slogan & Coupon
            //AdTypeHolder4.Style.Add("display", "none");  // Slogan & Video

            TextMessageHolder.Style.Add("display", "table-row");
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

            // Hide image uploader validation message.
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

            //AdTypeHolder0.Style.Add("display", displaySlogan); // Slogan Only
            //AdTypeHolder1.Style.Add("display", displayImage);  // Slogan & Image
            //AdTypeHolder2.Style.Add("display", displayAdGame); // Image Only
            //AdTypeHolder3.Style.Add("display", displayCoupon); // Slogan & Coupon
            //AdTypeHolder4.Style.Add("display", displayVideo);  // Slogan & Video
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
            // TODO: Validate upload image/video.

            // If form is not valid, exit
            if (!Page.IsValid)
            {
                if (!validatorCampaignName1.IsValid || !validatorCampaignName1.IsValid)
                    textCampaignName.Focus();
                else if (!validatorAdName.IsValid)
                    textAdName.Focus();
                else if (!validatorAdType.IsValid)
                    listAdType.Focus();
                else if (!validatorAdSlogan.IsValid)
                    textAdSlogan.Focus();
                else if (!validatorKeywords.IsValid)
                    textKeywords.Focus();
                else if (!validatorDailyBudget1.IsValid || !validatorDailyBudget2.IsValid)
                    textDailyBudget.Focus();
                else if (!validatorMaxPpt1.IsValid || !validatorMaxPpt2.IsValid)
                    textMaxPpt.Focus();

                return;
            }

            // Check upload validation.
            if (!IsUploadImageExists())
            {
                UploadHidden.Focus();
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
            int dailyBudget = int.Parse(textDailyBudget.Text.Trim());
            decimal maxCpt = decimal.Parse(textMaxPpt.Text.Trim());
            string adName = textAdName.Text.Trim();
            int adType = int.Parse(listAdType.SelectedValue);
            string adSlogan = textAdSlogan.Text.Trim();
            bool adRtl = checkRtl.Checked;

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

            int campaignPaymentType = (int) CampaignPaymentType.Fit;
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
                    if (string.IsNullOrEmpty(txtImageClickURL.Text)==false)
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

            // Check page action.
            switch (_action)
            {
                // Signup.
                case ACTION_SIGNUP:

                    NewCampaignData campaign = new NewCampaignData();
                    campaign.CampaignName = campaignName;
                    campaign.LanguagesList = selectedLanguagesList;
                    campaign.CountriesList = selectedCountriesList;
                    campaign.CategoriesList = selectedCategoriesList;
                    campaign.KeywordsList = selectedKeywordsList;
                    campaign.DailyBudget = dailyBudget;
                    campaign.ScheduleDatesList = scheduleDatesList;
                    campaign.AdName = adName;
                    campaign.AdType = adType;
                    campaign.Width = width;
                    campaign.Height = height;
                    campaign.AdSlogan = adSlogan;
                    campaign.AdRtl = adRtl;
                    campaign.AdImage = adImage;
                    campaign.AdVideo = adVideo;
                    campaign.ClickUrl = adUrl;
                    campaign.LikeUrl = adLikeUrl;
                    campaign.MaxBid = maxCpt;
                    campaign.CampaignPaymentType = campaignPaymentType;
                    
                    // Save new campaign data in session.
                    Session["NewCampaignData"] = campaign;
                    Session["TempAdId"] = AdId.Value.Trim();


                    // Redirect to sign up (account) page.
                    Response.Redirect("SignUp.aspx");
                    
                    break;

                // New campaign (for exsiting advertiser).
                case ACTION_NEW:

                    bool bonusCampaign = false;
                    
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
                                         adRtl);

                    //TODO:
                    throw new NotImplementedException();
                    
                    //var uploader = new DemoImageUploader.UploaderSoapClient();
                    //if (AdId.Value.Trim() != string.Empty)
                    //{
                    //    uploader.UpdateAd(_advertiser.Advertiser_Id, campaignId, adId, Convert.ToInt32(AdId.Value.Trim()));
                    //}
                    

                    // Send mail to administrator.
                    Mail.SendNewCampaignAdminMail(_advertiser.Advertiser_Id, _advertiser.Email, campaignId, campaignName);

                    // Send mail to administrator.
                    Mail.SendNewAdAdminMail(_advertiser.Advertiser_Id, _advertiser.Email, campaignId, campaignName, adId, adName, adType, adImage);

                    // Redirect to manage campaigns page.
                    Response.Redirect("ManageCampaigns.aspx");
                    
                    break;
            }
        }


        /// <summary>
        /// Validates that the campaign doesn't already exist.
        /// </summary>
        protected void checkCampaignExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            if (_action == ACTION_SIGNUP)
            {
                // If signup, there is no point of checking wheteher already exists.
                e.IsValid = true;
            }
            else
            {
                // Check and return if campagin already exists (by name).
                e.IsValid = (CampaignBLL.IsExist(_advertiser.Advertiser_Id, textCampaignName.Text) ? false : true);
            }
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

            //bool urlCheck = General.IsUrlExists(ConfigurationSettings.AppSettings["URL"] + Constants.UPLOADS_DIRECTORY + UploadHidden.Value);
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

            if (!AdBLL.IsDimensionValid(adType, width, height))
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
         */
    }
         

}
