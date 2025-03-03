using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class ManageCaptchas : System.Web.UI.Page, IPublisherManageCaptchas
    {
        private const string kSessionCaptchasListData = "CaptchasListData";

        private TP_WEBSITE _website;

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

                _website = WebsiteBLL.GetWebsite(publisherId, websiteId);

                if (_website == null)
                {
                    // TO DO: Handle website not exists
                    throw new Exception("Website not exists");
                }
            }
            catch 
            {
                Response.Redirect("ManageWebsites.aspx");
            }

            if (!Page.IsPostBack && !Grid.IsCallback)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                // Set navigation path.
                labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                           "Website: " + _website.Url;

                // Check if website contains any captchas. If no, display suitable message.
                if (WebsiteBLL.GetTotalCaptchas(_website.Publisher_Id, _website.Website_Id) == 0)
                {
                    PanelGrid.Visible = false;
                    PanelNoWebsites.Visible = true;
                }
                else
                {
                    InitControls();
                    InitFilterData();
                    GetData(true);
                }
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
        /// Check and display system messages.
        /// </summary>
        private void ShowSystemMessages()
        {
            // If website is pending, add system message.
            if (_website.Status_Id == (int)Status.Pending)
                labelSystemMessages.Text = "Your website is being reviewed by our team.<br />As long as the status of your website is pending, you can only receive Security Only CAPTCHAs.";

            if (labelSystemMessages.Text != "")
                SystemMessagesHolder.Visible = true;
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Show system messages.
            ShowSystemMessages();

            listFilterDate.Items.Clear();

            // Set date filter values.
            listFilterDate.Items.Add(new ListItem("Today", "1"));
            listFilterDate.Items.Add(new ListItem("Yesterday", "2"));
            listFilterDate.Items.Add(new ListItem("Last 7 days", "3"));
            listFilterDate.Items.Add(new ListItem("This month", "4"));
            listFilterDate.Items.Add(new ListItem("Last month", "5"));
            listFilterDate.Items.Add(new ListItem("All dates", "6"));
            listFilterDate.Items.Add(new ListItem("Custom...", "9"));

            listFilterDate.SelectedIndex = 5;

            // Set date picker defaults dates (=today).
            PickerTo.SelectedDate = DateTime.Today;
            PickerFrom.SelectedDate = DateTime.Today.AddYears(-10);

            // Hide custom dates picker panel.
            panelCustomDatesFilter.Visible = false;

            // Set total earnings.
            decimal totalEarnings = WebsiteBLL.GetTotalEarnings(_website.Publisher_Id, _website.Website_Id);
            labelEarningsSum.Text = string.Format("${0:#,##0.00}", totalEarnings);

            // Set status filter to "all statuses".
            linkFilterStatusRunningPending.CssClass = linkFilterStatusPausedRejected.CssClass = "filter";
            linkFilterStatusAll.CssClass = "filterSelected";


            listStatus.DataSource = DictionaryBLL.GetStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            string statusId = "0";
            //statusId += ((int)Status.Running).ToString();
            //statusId += ",";
            //statusId += ((int)Status.Paused).ToString();
            //statusId += ",";
            //statusId += ((int)Status.Pending).ToString();
            //statusId += ",";
            //statusId += ((int)Status.Rejected).ToString();

            CaptchasFilterData captchaFilterData = new CaptchasFilterData();
            captchaFilterData.PublisherId = _website.Publisher_Id;
            captchaFilterData.WebsiteId = _website.Website_Id;
            captchaFilterData.StatusId = statusId;
            captchaFilterData.FromDate = PickerFrom.SelectedDate;
            captchaFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionCaptchasListData] = captchaFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            CaptchasFilterData captchaFilterData = (CaptchasFilterData)Session[kSessionCaptchasListData];
            captchaFilterData.FromDate = fromDate;
            captchaFilterData.ToDate = toDate;
            Session[kSessionCaptchasListData] = captchaFilterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id list.</param>
        private void UpdateFilterStatus(string statusId)
        {
            CaptchasFilterData captchaFilterData = (CaptchasFilterData)Session[kSessionCaptchasListData];
            captchaFilterData.StatusId = statusId;
            Session[kSessionCaptchasListData] = captchaFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionCaptchasListData] != null)
            {
                // Get filter data from session.
                CaptchasFilterData captchaFilterData = (CaptchasFilterData)Session[kSessionCaptchasListData];

                // Set filter parameters variables.
                int publisherId = captchaFilterData.PublisherId;
                int websiteId = captchaFilterData.WebsiteId;
                string statusId = captchaFilterData.StatusId;
                DateTime fromDate = captchaFilterData.FromDate;
                DateTime toDate = captchaFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                
                
                // TODO: Move to BLL?
                // Get captchas list.
                Grid.DataSource = dataContext.GetCaptchasList(publisherId,
                                                                          websiteId,
                                                                          statusId,
                                                                          fromDate,
                                                                          toDate).ToList();

                // Bind data.
                if (bindGrid)
                {
                    Grid.DataBind();
                }
            }
        }

        public void OnNeedRebind(object sender, EventArgs oArgs)
        {
            GetData(true);
        }

        public void OnNeedDataSource(object sender, EventArgs oArgs)
        {
            GetData(true);
        }

        /// <summary>
        /// Change dates according to user selection and reload grid data.
        /// </summary>
        protected void listFilterDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listFilterDate.SelectedValue)
            {
                case "1":
                    // Today
                    PickerFrom.SelectedDate = PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "2":
                    // Yesterday
                    PickerFrom.SelectedDate = PickerTo.SelectedDate = DateTime.Today.AddDays(-1);
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "3":
                    // Last 7 days
                    PickerFrom.SelectedDate = DateTime.Today.AddDays(-6);
                    PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "4":
                    // This month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "5":
                    // Last month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
                    PickerTo.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month));
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "6":
                    // All dates
                    PickerFrom.SelectedDate = new DateTime(2000, 1, 1);
                    PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                default:
                    panelCustomDatesFilter.Visible = true;
                    break;
            }

            // Update grid.
            UpdateFilterDates(PickerFrom.SelectedDate, PickerTo.SelectedDate);
            GetData(true);
        }

        /// <summary>
        /// Update grid data for custom dates.
        /// </summary>
        protected void linkUpdateCustomDates_Click(object sender, EventArgs e)
        {
            UpdateFilterDates(PickerFrom.SelectedDate, PickerTo.SelectedDate);
            GetData(true);
        }

        /// <summary>
        /// Filter by status.
        /// </summary>
        protected void listStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int statusId = Convert.ToInt16(listStatus.SelectedValue);

            // Update grid.
            UpdateFilterStatus(statusId.ToString());
            GetData(true);
        }
        /// <summary>
        /// Filter running and pending statuses.
        /// </summary>
        protected void linkFilterStatusRunningPending_Click(object sender, EventArgs e)
        {
            linkFilterStatusPausedRejected.CssClass = linkFilterStatusAll.CssClass = "filter";
            linkFilterStatusRunningPending.CssClass = "filterSelected";

            string statusId = "";
            statusId += ((int)Status.Running).ToString();
            statusId += ",";
            statusId += ((int)Status.Pending).ToString();

            // Update grid.
            UpdateFilterStatus(statusId);
            GetData(true);
        }

        /// <summary>
        /// Filter paused and rejected statuses.
        /// </summary>
        protected void linkFilterStatusPausedRejected_Click(object sender, EventArgs e)
        {
            linkFilterStatusRunningPending.CssClass = linkFilterStatusAll.CssClass = "filter";
            linkFilterStatusPausedRejected.CssClass = "filterSelected";

            string statusId = "";
            statusId += ((int)Status.Paused).ToString();
            statusId += ",";
            statusId += ((int)Status.Rejected).ToString();

            // Update grid.
            UpdateFilterStatus(statusId);
            GetData(true);
        }

        /// <summary>
        /// Filter all statuses.
        /// </summary>
        protected void linkFilterStatusAll_Click(object sender, EventArgs e)
        {
            linkFilterStatusRunningPending.CssClass = linkFilterStatusPausedRejected.CssClass = "filter";
            linkFilterStatusAll.CssClass = "filterSelected";

            string statusId = "";
            statusId += ((int)Status.Running).ToString();
            statusId += ",";
            statusId += ((int)Status.Paused).ToString();
            statusId += ",";
            statusId += ((int)Status.Pending).ToString();
            statusId += ",";
            statusId += ((int)Status.Rejected).ToString();

            // Update grid.
            UpdateFilterStatus(statusId);
            GetData(true);
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            Grid.NeedRebind += new ComponentArt.Web.UI.Grid.NeedRebindEventHandler(OnNeedRebind);
            Grid.NeedDataSource += new ComponentArt.Web.UI.Grid.NeedDataSourceEventHandler(OnNeedDataSource);
        }

        #endregion
    }
}

[Serializable]
internal class CaptchasFilterData
{
    public int PublisherId;
    public int WebsiteId;
    public string StatusId;
    public DateTime FromDate;
    public DateTime ToDate;
}