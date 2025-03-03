using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class EditWebsite : System.Web.UI.Page, IPublisherEditWebsite
    {
        protected IWebsite _website;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Session["PublisherId"]);
                int websiteId = Convert.ToInt16(Page.Request.QueryString["WebsiteId"]);

                var websiteResult = WebsitesManager.Get(websiteId, publisherId);

                if (websiteResult.HasError)
                {
                    // TODO: Handle website not exsists
                    throw new Exception("Website not exists");
                }
                else
                {
                    _website = websiteResult.Value;
                }
            }
            catch
            {
                Response.Redirect("ManageWebsites.aspx");
            }
    
            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                //InitControls();
            }
        }



		/*
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
		*/
		
		
		/*
        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            TP_PUBLISHER publisher = new TP_PUBLISHER();
            publisher = PublisherBLL.GetPublisher(_website.PublisherId);

            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                       "Website: " + "<a href=\"ManageCaptchas.aspx?Website_Id=" + _website.Id.ToString() + "\">" + _website.Url + "</a>" + " &gt; " +
                                       "Edit";

            // Fill website url.
            labelUrl.Text = _website.Url;

            // Check if website is rejected or pending.
            if (publisher.Status_Id != (int)Status.Running ||
                _website.Status == Status.Rejected ||
                _website.Status == Status.Pending)
            {
                // Fill statuses list.
                listStatus.Items.Add(new ListItem(DictionaryBLL.GetNameById((int)_website.Status), ((int)_website.Status).ToString()));

                listStatus.Visible = listStatus.Enabled = false;
                labelStatus.Visible = true;
                labelStatus.CssClass = listStatus.SelectedItem.Text;

                labelStatus.Text = listStatus.SelectedItem.Text;
            }
            else
            {
                // Fill statuses list.
                listStatus.Items.Add(new ListItem(Status.Running.ToString(), ((int)Status.Running).ToString()));
                listStatus.Items.Add(new ListItem(Status.Paused.ToString(), ((int)Status.Paused).ToString()));

                listStatus.Visible = listStatus.Enabled = true;
                labelStatus.Visible = false;
            }

            // If website is pending, display explanation message.
            if (_website.Status == Status.Pending)
                labelStatusPending.Visible = true;
            else
                labelStatusPending.Visible = false;

            // Set status.
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
        }
		*/

		/*
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit
            if (!Page.IsValid)
            {
                if (!validatorStatus.IsValid)
                    listStatus.Focus();
                else if (!validatorLanguage.IsValid)
                    listLanguage.Focus();
                //else if (!validatorRegion.IsValid)
                //    listCountry.Focus();
                
                return;
            }

            List<int> languagesList = new List<int>();
            List<int> countriesList = new List<int>();
            List<int> categoriesList = new List<int>();
            List<string> keywordsList = new List<string>();

            // Get values.
            int publisherId = _website.PublisherId;
            int websiteId = _website.Id;
            string url = _website.Url.Trim();
            Status status;
            if (_website.Status == Status.Pending || _website.Status == Status.Rejected)
            {
                status = _website.Status;
            }
            else 
            {
                status = (Status)int.Parse(listStatus.SelectedItem.Value);
            }
            //bool allowBonus = (_website.Allow_Bonus == 1 ? true : false);
            //int bonusLimit = _website.BonusLimit;
            languagesList.Add(Convert.ToInt16(listLanguage.SelectedValue));
            //countriesList.Add(Convert.ToInt16(listCountry.SelectedValue));

            // Update website.
            WebsiteBLL.Update(publisherId,
                              websiteId,
                              url,
                              (int)status,
                              languagesList,
                              countriesList,
                              categoriesList,
                              keywordsList,
                              false,
                              100);

            Response.Redirect("ManageWebsites.aspx");
        }
		
		*/
    }
}
