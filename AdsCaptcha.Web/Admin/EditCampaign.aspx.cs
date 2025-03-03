using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditCampaign : System.Web.UI.Page
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
                                    "Campaign: " + "<a href='ManageAds.aspx?AdvertiserId=" + _campaign.Advertiser_Id.ToString() + "&CampaignId=" + _campaign.Campaign_Id.ToString() + "'>" + _campaign.Campaign_Name + "</a>" + " » " +
                                    "Edit";

            // Set campaign name.
            textCampaignName.Text = _campaign.Campaign_Name;

            // Set bonus.
            checkBonusCampaign.Checked = (_campaign.Is_Bonus == 1 ? true : false);

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.FindByValue(_campaign.Status_Id.ToString()).Selected = true;

            // Set radio buttons (targeting options).
            radioCountry.SelectedIndex = (_campaign.Target_By_Country == 0) ? 0 : 1;
            radioLanguage.SelectedIndex = (_campaign.Target_By_Language == 0) ? 0 : 1;
            radioCategory.SelectedIndex = (_campaign.Target_By_Category == 0) ? 0 : 1;
            radioKeywords.SelectedIndex = (_campaign.Target_By_Keyword == 0) ? 0 : 1;
            radioSchedule.SelectedIndex = (_campaign.Schedule_Limit == 0) ? 0 : 1;

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

            // Get budget.
            textDailyBudget.Text = _campaign.Daily_Budget.ToString();

            // Schedule dates.
            if (_campaign.Schedule_Limit == 0)
            {
                PickerFrom.SelectedDate = CalendarFrom.SelectedDate = DateTime.Today;
                PickerTo.SelectedDate = CalendarTo.SelectedDate = DateTime.Today.AddMonths(3);
            }
            else
            {
                PickerFrom.SelectedDate = CalendarFrom.SelectedDate = (DateTime)_campaign.Schedule_Start_Date;
                PickerTo.SelectedDate = CalendarTo.SelectedDate = (DateTime)_campaign.Schedule_End_Date;
            }

            // Show/hide schedule dates box.
            scheduleDates.Style.Add("display", (_campaign.Schedule_Limit == 0) ? "none" : "block");

            // If targeting by country, set countries list.
            if (_campaign.Target_By_Country != 0)
            {
                foreach (int country in CampaignBLL.GetCampaignCountries(_campaign.Advertiser_Id, _campaign.Campaign_Id))
                {
                    checkCountry.Items.FindByValue(country.ToString()).Selected = true;
                }
            }

            // If targeting by language, set languages list.
            if (_campaign.Target_By_Language != 0)
            {
                foreach (int language in CampaignBLL.GetCampaignLanguages(_campaign.Advertiser_Id, _campaign.Campaign_Id))
                {
                    checkLanguage.Items.FindByValue(language.ToString()).Selected = true;
                }
            }

            // If targeting by category, set categories list.
            if (_campaign.Target_By_Category != 0)
            {
                foreach (int category in CampaignBLL.GetCampaignCategories(_campaign.Advertiser_Id, _campaign.Campaign_Id))
                {
                    checkCategory.Items.FindByValue(category.ToString()).Selected = true;
                }
            }

            // If targeting by keywords, set keywords list.
            if (_campaign.Target_By_Keyword != 0)
            {
                foreach (string keyword in CampaignBLL.GetCampaignKeywords(_campaign.Advertiser_Id, _campaign.Campaign_Id))
                {
                    textKeywords.Text += keyword;
                    textKeywords.Text += ",";
                }
                textKeywords.Text = textKeywords.Text.Substring(0, textKeywords.Text.Length - 1);
            }

            rbCampaignPaymentTypeClick.Checked = _campaign.CampaignPaymentType == (int)CampaignPaymentType.Click;
            rbCampaignPaymentTypeFit.Checked = _campaign.CampaignPaymentType == (int)CampaignPaymentType.Fit; ;

        }

        /// <summary>
        /// Show/hide dynamic content.
        /// </summary>
        private void ShowHide()
        {
            // Hide/display targeting options.
            countryTargeting.Style.Add("display", (radioCountry.SelectedIndex == 0) ? "none" : "block");
            languageTargeting.Style.Add("display", (radioLanguage.SelectedIndex == 0) ? "none" : "block");
            categoryTargeting.Style.Add("display", (radioCategory.SelectedIndex == 0) ? "none" : "block");
            keywordsTargeting.Style.Add("display", (radioKeywords.SelectedIndex == 0) ? "none" : "block");
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                return;
            }

            // Get values.
            string campaignName = textCampaignName.Text.Trim();
            bool bonusCampaign = checkBonusCampaign.Checked;
            int statusId = int.Parse(listStatus.SelectedItem.Value);
            int dailyBudget = int.Parse(textDailyBudget.Text.Trim());

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

            // Update campaign.
            CampaignBLL.Update(_campaign.Advertiser_Id,
                               _campaign.Campaign_Id,
                               campaignName,
                               statusId,
                               selectedLanguagesList,
                               selectedCountriesList,
                               selectedCategoriesList,
                               selectedKeywordsList,
                               dailyBudget,
                               scheduleDatesList,
                               bonusCampaign,
                               campaignPaymentType);

            // Redirect to manage campaigns page.
            Response.Redirect("ManageCampaigns.aspx" + "?AdvertiserId=" + _campaign.Advertiser_Id.ToString());
        }

        /// <summary>
        /// Validates that the campaign doesn't already exist.
        /// </summary>
        protected void checkCampaignExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if campagin already exists (by name).
            e.IsValid = (CampaignBLL.IsDuplicateNameForAdvertiser(_campaign.Advertiser_Id, _campaign.Campaign_Id, textCampaignName.Text) ? false : true);
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
    }
}
