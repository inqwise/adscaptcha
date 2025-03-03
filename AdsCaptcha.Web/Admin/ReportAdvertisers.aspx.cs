using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ReportAdvertisers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack)
            {
                Grid.Visible = false;
                InitControls();
            }
            else
            {
                Grid.Visible = true;
                UpdateData();
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
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "Advertisers";

            // Fill advertiser statuses list.
            listAdvertiserStatus.DataSource = DictionaryBLL.GetStatusList();
            listAdvertiserStatus.DataValueField = "Item_Id";
            listAdvertiserStatus.DataTextField = "Item_Desc";
            listAdvertiserStatus.DataBind();
            listAdvertiserStatus.ClearSelection();
            listAdvertiserStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listAdvertiserStatus.SelectedIndex = 0;

            // Fill campaign statuses list.
            listCampaignStatus.DataSource = DictionaryBLL.GetStatusList();
            listCampaignStatus.DataValueField = "Item_Id";
            listCampaignStatus.DataTextField = "Item_Desc";
            listCampaignStatus.DataBind();
            listCampaignStatus.ClearSelection();
            listCampaignStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listCampaignStatus.SelectedIndex = 0;

            // Fill campaign categories list.
            listCampaignCategory.DataSource = DictionaryBLL.GetCategoryList();
            listCampaignCategory.DataValueField = "Category_Id";
            listCampaignCategory.DataTextField = "Category_Desc";
            listCampaignCategory.DataBind();
            listCampaignCategory.ClearSelection();
            listCampaignCategory.Items.Insert(0, new ListItem("-- Any Category --", "0"));
            listCampaignCategory.SelectedIndex = 0;

            // Fill ad statuses list.
            listAdStatus.DataSource = DictionaryBLL.GetStatusList();
            listAdStatus.DataValueField = "Item_Id";
            listAdStatus.DataTextField = "Item_Desc";
            listAdStatus.DataBind();
            listAdStatus.ClearSelection();
            listAdStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listAdStatus.SelectedIndex = 0;

            // Fill ad types list.
            listAdType.DataSource = DictionaryBLL.GetAdTypeList();
            listAdType.DataValueField = "Item_Id";
            listAdType.DataTextField = "Item_Desc";
            listAdType.DataBind();
            listAdType.ClearSelection();
            listAdType.Items.Insert(0, new ListItem("-- All Types --", "0"));
            listAdType.SelectedIndex = 0;

            // Fill countries list.
            listCountry.DataSource = DictionaryBLL.GetCountryList();
            listCountry.DataValueField = "Country_Id";
            listCountry.DataTextField = "Country_Name";
            listCountry.DataBind();
            listCountry.ClearSelection();
            listCountry.Items.Insert(0, new ListItem("-- All Countries --", "0"));
            listCountry.Items.Add(new ListItem("-- Unknown --", "-1"));
            listCountry.SelectedIndex = 0;

            // Set js functions.
            listDatesFilter.Attributes.Add("onChange", "javascript:OnDatesFilterChange();");
        }

        private void UpdateData()
        {
            // Get filter values.
            int advertiserId = Int32.TryParse(textAdvertiserId.Text, out advertiserId) ? advertiserId : 0;
            int advertiserStatusId = Int32.TryParse(listAdvertiserStatus.SelectedValue, out advertiserStatusId) ? advertiserStatusId : 0;
            string advertiserEmail = textAdvertiserEmail.Text.Trim();
            int campaignId = Int32.TryParse(textCampaignId.Text, out campaignId) ? campaignId : 0;
            int campaignStatusId = Int32.TryParse(listCampaignStatus.SelectedValue, out campaignStatusId) ? campaignStatusId : 0;
            int campaignCategoryId = Int32.TryParse(listCampaignCategory.SelectedValue, out campaignCategoryId) ? campaignCategoryId : 0;
            int addDateDiff = Int32.TryParse(listAddDate.SelectedValue, out addDateDiff) ? addDateDiff : 0;
            int adId = Int32.TryParse(textAdId.Text, out adId) ? adId : 0;
            int adStatusId = Int32.TryParse(listAdStatus.SelectedValue, out adStatusId) ? adStatusId : 0;
            int adTypeId = Int32.TryParse(listAdType.SelectedValue, out adTypeId) ? adTypeId : 0;
            bool checkFromDate = !String.IsNullOrEmpty(textFromDate.Text.Trim());
            bool checkToDate = !String.IsNullOrEmpty(textToDate.Text.Trim());
            DateTime fromDate = DateTime.Today;
            if (checkFromDate)
            {
                int year = fromDate.Year;
                int.TryParse(textFromDate.Text.Substring(6, 4), out year);
                int month = fromDate.Month;
                int.TryParse(textFromDate.Text.Substring(3, 2), out month);
                int day = fromDate.Day;
                int.TryParse(textFromDate.Text.Substring(0, 2), out day);
                fromDate = new DateTime(year, month, day);
            }
            DateTime toDate = DateTime.Today;
            if (checkToDate)
            {
                int year = fromDate.Year;
                int.TryParse(textToDate.Text.Substring(6, 4), out year);
                int month = fromDate.Month;
                int.TryParse(textToDate.Text.Substring(3, 2), out month);
                int day = fromDate.Day;
                int.TryParse(textToDate.Text.Substring(0, 2), out day);
                toDate = new DateTime(year, month, day);
            }
            int countryId = Int32.TryParse(listCountry.SelectedValue, out countryId) ? countryId : 0;

            string orderBy = listOrderBy.SelectedValue;
            bool isDesc = listOrderByDirection.SelectedItem.Value == "0";
            int numOfRows = Int32.TryParse(listNumOfRows.SelectedValue, out numOfRows) ? numOfRows : 0;

            // Get data.
            Grid.DataSource = ReportsBLL.Advertisers(
                                            advertiserId, advertiserStatusId, advertiserEmail,
                                            campaignId, campaignStatusId, campaignCategoryId, addDateDiff,
                                            adId, adStatusId, adTypeId,
                                            checkFromDate, fromDate, checkToDate, toDate,
                                            countryId,
                                            orderBy, isDesc, numOfRows);
            Grid.DataBind();
        }

        public void OnNeedRebind(object sender, EventArgs oArgs)
        {
            UpdateData();
        }

        public void OnNeedDataSource(object sender, EventArgs oArgs)
        {
            UpdateData();
        }
    }
}