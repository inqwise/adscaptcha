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

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class Test : System.Web.UI.Page
    {
        public string _action;
        public TA_ADVERTISER _advertiser;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                InitControls();
            }
        }

        private void InitControls()
        {
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

            // Fill countries list.
            listAvailableCountries.DataTextField = "Country_Name";
            listAvailableCountries.DataValueField = "Country_Id";
            listAvailableCountries.DataSource = DictionaryBLL.GetCountryList();
            listAvailableCountries.DataBind();

            // Fill languages list.
            listAvailableLanguages.DataTextField = "Language_Name";
            listAvailableLanguages.DataValueField = "Language_Id";
            listAvailableLanguages.DataSource = DictionaryBLL.GetLanguageList();
            listAvailableLanguages.DataBind();

            // Fill categories list.
            listAvailableCategories.DataTextField = "Category_Desc";
            listAvailableCategories.DataValueField = "Category_Id";
            listAvailableCategories.DataSource = DictionaryBLL.GetCategoryList();
            listAvailableCategories.DataBind();

            // Schedule dates.
            scheduleDates.Style.Add("display", "none");
            PickerFrom.SelectedDate = CalendarFrom.SelectedDate = DateTime.Today;
            PickerTo.SelectedDate = CalendarTo.SelectedDate = DateTime.Today.AddMonths(3);

            // Hide/display targeting options.
            countryTargeting.Style.Add("display", (radioCountry.SelectedIndex == 0) ? "none" : "block");
            languageTargeting.Style.Add("display", (radioLanguage.SelectedIndex == 0) ? "none" : "block");
            categoryTargeting.Style.Add("display", (radioCategory.SelectedIndex == 0) ? "none" : "block");
            keywordsTargeting.Style.Add("display", (radioKeywords.SelectedIndex == 0) ? "none" : "block");
            scheduleDates.Style.Add("display", (radioSchedule.SelectedIndex == 0) ? "none" : "block");

            // Hide image uploader validation message.
            labelImageValidation.Style.Add("display", "none");
        }
    }
}
