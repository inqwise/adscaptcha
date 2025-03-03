using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class EditWebsite : System.Web.UI.Page
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private IWebsite _website;
        private int _developerId = 0;
        private int publisherId;
        private int websiteId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                _developerId = int.Parse(Page.Request.QueryString["DeveloperId"]);
            }
            catch { }

            try
            {
                publisherId = int.Parse(Page.Request.QueryString["PublisherId"]);
                websiteId = int.Parse(Page.Request.QueryString["WebsiteId"]);

                var websiteResult = WebsitesManager.Get(websiteId);

                if (websiteResult.HasError)
                {
                    Response.Redirect("ManageWebsites.aspx?PublisherId=" + publisherId);
                }
                else
                {
                    _website = websiteResult.Value;
                }
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (!IsPostBack)
            {
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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            string publisher = null;
            try
            {
                publisher = PublisherBLL.GetPublisher(_website.PublisherId).Email;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(String.Format("Init Controls: Publisher not found. publisherId: '{0}'", _website.PublisherId), ex);
            }

            if (_developerId == 0)
            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _website.PublisherId.ToString() + "'>" + publisher + "</a> (<a href='EditPublisher.aspx?PublisherId=" + _website.PublisherId.ToString() + "'>edit</a>)" + " » " +
                                        "Website: " + "<a href='ManageCaptchas.aspx?PublisherId=" + _website.PublisherId.ToString() + "&WebsiteId=" + _website.Id.ToString() + "'>" + _website.Url + "</a>" + " » " +
                                        "Edit";
            }
            else
            {
                TD_DEVELOPER developer = DeveloperBLL.GetDeveloper(_developerId);

                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManageDevelopers.aspx'>" + "Developers" + "</a>" + " » " +
                                        "Developer: " + "<a href='ManagePublishers.aspx?DeveloperId=" + _developerId.ToString() + "'>" + developer.Email + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _website.PublisherId.ToString() + "'>" + publisher + "</a> (<a href='EditPublisher.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _website.PublisherId.ToString() + "'>edit</a>)" + " » " +
                                        "Website: " + "<a href='ManageCaptchas.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _website.PublisherId.ToString() + "&WebsiteId=" + _website.Id.ToString() + "'>" + _website.Url + "</a>" + " » " +
                                        "Edit";
            }

            // Security level slider.
            if (WebsiteBLL.GetTotalCaptchas(_website.PublisherId, _website.Id) == 1)
            {
                panelSecurityLevel.Visible = true;

                //TP_CAPTCHA _captcha = CaptchaBLL.GetCaptchas(_website.PublisherId, _website.Id).First();
                //int securityLevelId = _captcha.Security_Level_Id - (int)DecodeTables.SecurityLevel * 1000;
                sliderSecurityLevel.Value = (int)CaptchaSecurityLevel.Low;
            }

            // Fill website url.
            textUrl.Text = _website.Url;

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.FindByValue(((int)_website.Status).ToString()).Selected = true;

            // Fill languages list.                
            listLanguage.DataSource = DictionaryBLL.GetLanguageList();
            listLanguage.DataBind();
            listLanguage.ClearSelection();

            foreach (int language in WebsiteBLL.GetWebsiteLanguages(_website.PublisherId, _website.Id))
            {
                listLanguage.Items.FindByValue(language.ToString()).Selected = true;
            }

            // Fill countries list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataBind();
            listCountry.ClearSelection();

            foreach (int country in WebsiteBLL.GetWebsiteCountries(_website.PublisherId, _website.Id))
            {
                listCountry.Items.FindByValue(country.ToString()).Selected = true;
            }

            // Fill categories boxes.
            foreach (T_CATEGORY category in DictionaryBLL.GetCategoryList())
            {
                ListItem item = new ListItem(category.Category_Desc, category.Category_Id.ToString());
                checkCategory.Items.Add(item);
            }
            
            foreach (int category in WebsiteBLL.GetWebsiteCategories(_website.PublisherId, _website.Id))
            {
                ListItem findByValue = checkCategory.Items.FindByValue(category.ToString());
                if (null != findByValue)
                {
                    findByValue.Selected = true;
                }
            }

            // Fill keywords.
            foreach (string keyword in WebsiteBLL.GetWebsiteKeywords(_website.PublisherId, _website.Id))
            {
                textKeywords.Text += keyword;
                textKeywords.Text += ",";
            }
            if (textKeywords.Text.Length > 0) textKeywords.Text = textKeywords.Text.Substring(0, textKeywords.Text.Length - 1);

            // Set bonus.
            //totalRevenue.Text = Math.Round(_website.TotalRevenue).ToString();
            //textBonusLimit.Text = _website.BonusLimit.ToString();
            checkBonus.Checked = false;// (_website.Allow_Bonus == 1 ? true : false);
            checkBonus.Visible = false;
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                return;
            }

            List<int> languagesList = new List<int>();
            List<int> countriesList = new List<int>();
            List<int> categoriesList = new List<int>();
            List<string> keywordsList = new List<string>();

            // Get values.
            int publisherId = _website.PublisherId;
            int websiteId = _website.Id;
            string url = textUrl.Text.Trim();
            int statusId = int.Parse(listStatus.SelectedItem.Value);
            bool allowBonus = checkBonus.Checked;
            int bonusLimit = (String.IsNullOrEmpty(textBonusLimit.Text) ? 0 : int.Parse(textBonusLimit.Text));
            int oldStatusId = (int)_website.Status;
            int languageId = Convert.ToInt16(listLanguage.SelectedValue);
            int regionId = Convert.ToInt16(listCountry.SelectedValue);
            languagesList.Add(Convert.ToInt16(listLanguage.SelectedValue));
            countriesList.Add(Convert.ToInt16(listCountry.SelectedValue));

            // Build selected categories list.
            foreach (ListItem category in checkCategory.Items)
            {
                // Check if current category is selected.
                if (category.Selected)
                {
                    categoriesList.Add(int.Parse(category.Value));
                }
            }

            // Build selected keywords list.
            if (textKeywords.Text != "")
            {
                keywordsList = textKeywords.Text.Split(',').ToList();
            }

            // Update website.
            WebsiteBLL.Update(publisherId,
                              websiteId,
                              url,
                              statusId,
                              languagesList,
                              countriesList,
                              categoriesList,
                              keywordsList,
                              allowBonus,
                              bonusLimit);

            // If website was pending and changed t running, send a message to publisher.
            if (statusId == (int)Status.Running && oldStatusId == (int)Status.Pending)
            {
                TP_PUBLISHER publisher = PublisherBLL.GetPublisher(publisherId);
                int noSecurity = CaptchaBLL.GetCaptchas(publisherId, websiteId).Where(c => c.Type_Id != (int)CaptchaType.SecurityOnly).Count();

                // Send mail to publisher.
                if (noSecurity > 0)
                {
                    Mail.SendWebsiteApproveMail(publisher.Email, publisherId, websiteId, url);
                }
                else
                {
                    Mail.SendWebsiteSecurityOnlyApproveMail(publisher.Email, publisherId, websiteId, url);
                }                
            }

            // Redirect to manage websites page.
            if (_developerId == 0)
            {
                Response.Redirect("ManageWebsites.aspx?PublisherId=" + _website.PublisherId.ToString());
            }
            else
            {
                Response.Redirect("ManageWebsites.aspx?DeveloperId=" + _developerId.ToString() + "&PublisherId=" + _website.PublisherId.ToString());
            }
        }

        /// <summary>
        /// Validates that the url does not already exist.
        /// </summary>
        protected void checkWebsiteExist_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            // Check and return if website already exists (by url).
            e.IsValid = (WebsiteBLL.IsDuplicateUrlForPublisher(_website.PublisherId, _website.Id, textUrl.Text) ? false : true);
        }

        /// <summary>
        /// Validates that the all keywords are is the right length.
        /// </summary>
        protected void checkKeywordsLength_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
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
