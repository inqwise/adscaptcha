using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ReportPublishers : System.Web.UI.Page
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

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
            labelBreadCrambs.Text = "Site Owners";

            // Fill publisher statuses list.
            listPublisherStatus.DataSource = DictionaryBLL.GetStatusList();
            listPublisherStatus.DataValueField = "Item_Id";
            listPublisherStatus.DataTextField = "Item_Desc";
            listPublisherStatus.DataBind();
            listPublisherStatus.ClearSelection();
            listPublisherStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listPublisherStatus.SelectedIndex = 0;

            // Fill website statuses list.
            listWebsiteStatus.DataSource = DictionaryBLL.GetStatusList();
            listWebsiteStatus.DataValueField = "Item_Id";
            listWebsiteStatus.DataTextField = "Item_Desc";
            listWebsiteStatus.DataBind();
            listWebsiteStatus.ClearSelection();
            listWebsiteStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listWebsiteStatus.SelectedIndex = 0;

            // Fill website categories list.
            listWebsiteCategory.DataSource = DictionaryBLL.GetCategoryList();
            listWebsiteCategory.DataValueField = "Category_Id";
            listWebsiteCategory.DataTextField = "Category_Desc";
            listWebsiteCategory.DataBind();
            listWebsiteCategory.ClearSelection();
            listWebsiteCategory.Items.Insert(0, new ListItem("-- Any Category --", "0"));
            listWebsiteCategory.SelectedIndex = 0;

            // Fill captcha statuses list.
            listCaptchaStatus.DataSource = DictionaryBLL.GetStatusList();
            listCaptchaStatus.DataValueField = "Item_Id";
            listCaptchaStatus.DataTextField = "Item_Desc";
            listCaptchaStatus.DataBind();
            listCaptchaStatus.ClearSelection();
            listCaptchaStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listCaptchaStatus.SelectedIndex = 0;

            // Fill captcha types list.
            listCaptchaType.DataSource = DictionaryBLL.GetCaptchaTypeList();
            listCaptchaType.DataValueField = "Item_Id";
            listCaptchaType.DataTextField = "Item_Desc";
            listCaptchaType.DataBind();
            listCaptchaType.ClearSelection();
            listCaptchaType.Items.Insert(0, new ListItem("-- All Types --", "0"));
            listCaptchaType.SelectedIndex = 0;

            // Fill developer statuses list.
            listDeveloperStatus.DataSource = DictionaryBLL.GetStatusList();
            listDeveloperStatus.DataValueField = "Item_Id";
            listDeveloperStatus.DataTextField = "Item_Desc";
            listDeveloperStatus.DataBind();
            listDeveloperStatus.ClearSelection();
            listDeveloperStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listDeveloperStatus.SelectedIndex = 0;

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
            int publisherId = Int32.TryParse(textPublisherId.Text, out publisherId) ? publisherId : 0;
            int publisherStatusId = Int32.TryParse(listPublisherStatus.SelectedValue, out publisherStatusId) ? publisherStatusId : 0;
            string publisherEmail = textPublisherEmail.Text.Trim();
            int addDateDiff = Int32.TryParse(listAddDate.SelectedValue, out addDateDiff) ? addDateDiff : 0;
            int websiteId = Int32.TryParse(textWebsiteId.Text, out websiteId) ? websiteId : 0;
            int websiteStatusId = Int32.TryParse(listWebsiteStatus.SelectedValue, out websiteStatusId) ? websiteStatusId : 0;
            int websiteCategoryId = Int32.TryParse(listWebsiteCategory.SelectedValue, out websiteCategoryId) ? websiteCategoryId : 0;            
            string websiteUrl = textWebsiteUrl.Text.Trim();
            int captchaId = Int32.TryParse(textCaptchaId.Text, out captchaId) ? captchaId : 0;
            int captchaStatusId = Int32.TryParse(listCaptchaStatus.SelectedValue, out captchaStatusId) ? captchaStatusId : 0;
            int captchaTypeId = Int32.TryParse(listCaptchaType.SelectedValue, out captchaTypeId) ? captchaTypeId : 0;
            int developerId = Int32.TryParse(textDeveloperId.Text, out developerId) ? developerId : 0;
            int developerStatusId = Int32.TryParse(listDeveloperStatus.SelectedValue, out developerStatusId) ? developerStatusId : 0;
            string developerEmail = textDeveloperEmail.Text.Trim();
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

            Log.Info("UpdateData: countryId:'{0}', orderBy: '{1}', isDesc: '{2}', publisherId: '{3}', captchaId: '{4}', fromDate: '{5}', toDate: '{6}', checkToDate: '{7}', checkFromDate: '{8}', publisherStatusId: '{9}', publisherEmail: '{10}'", countryId, orderBy, isDesc, publisherId, captchaId, fromDate, toDate, checkToDate, checkFromDate, publisherStatusId, publisherEmail);

            // Get data.
            Grid.DataSource = ReportsBLL.SiteOwners(
                                            publisherId, publisherStatusId, publisherEmail,
                                            websiteId, websiteStatusId, websiteCategoryId, websiteUrl, addDateDiff,
                                            captchaId, captchaStatusId, captchaTypeId,
                                            developerId, developerStatusId, developerEmail,
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