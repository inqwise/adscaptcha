using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL;
using Advertiser;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class ManageAds : System.Web.UI.Page, IAdvertiserManageAds
    {
        private const string kSessionAdsListData = "AdsListData";

        private TA_CAMPAIGN _campaign;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdvertiserLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdvertiserId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int advertiserId = Convert.ToInt16(Session["AdvertiserId"]);
                int campaignId = Convert.ToInt16(Page.Request.QueryString["CampaignId"].ToString());

                _campaign = CampaignBLL.GetCampaign(advertiserId, campaignId);

                if (_campaign == null)
                {
                    // TODO: Handle campaign not exists
                    throw new Exception("Campaign not exists");
                }
            }
            catch
            {
                Response.Redirect("ManageCampaigns.aspx");
            }

            if (!Page.IsPostBack && !Grid.IsCallback)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                // Set navigation path.
                labelNavigationPath.Text = "<a href=\"ManageCampaigns.aspx\">Campaigns</a>" + " &gt; " +
                                           "Campaign: " + _campaign.Campaign_Name;

                // Check if campaign contains any ads. If no, display suitable message.
                if (CampaignBLL.GetTotalAds(_campaign.Advertiser_Id, _campaign.Campaign_Id) == 0)
                {
                    PanelGrid.Visible = false;
                    PanelNoCampaigns.Visible = true;
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
            // If campaign is pending, add system message.
            if (_campaign.Status_Id == (int)Status.Pending)
                labelSystemMessages.Text = "Your campaign is being reviewed by our team.<br />As long as the status of your campaign is pending, your ads will not be displayed on AdsCaptcha.";

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

            // Set total charges.
            decimal totalCharges = CampaignBLL.GetTotalCharges(_campaign.Advertiser_Id, _campaign.Campaign_Id);
            labelChargesSum.Text = String.Format("${0:#,##0.00}", totalCharges);

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

            AdsFilterData adFilterData = new AdsFilterData();
            adFilterData.AdvertiserId = _campaign.Advertiser_Id;
            adFilterData.CampaignId = _campaign.Campaign_Id;
            adFilterData.StatusId = statusId;
            adFilterData.FromDate = PickerFrom.SelectedDate;
            adFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionAdsListData] = adFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            AdsFilterData adFilterData = (AdsFilterData)Session[kSessionAdsListData];
            adFilterData.FromDate = fromDate;
            adFilterData.ToDate = toDate;
            Session[kSessionAdsListData] = adFilterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id list.</param>
        private void UpdateFilterStatus(string statusId)
        {
            AdsFilterData adFilterData = (AdsFilterData)Session[kSessionAdsListData];
            adFilterData.StatusId = statusId;
            Session[kSessionAdsListData] = adFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionAdsListData] != null)
            {
                // Get filter data from session.
                AdsFilterData adFilterData = (AdsFilterData)Session[kSessionAdsListData];

                // Set filter parameters variables.
                int advertiserId = adFilterData.AdvertiserId;
                int campaignId = adFilterData.CampaignId;
                string statusId = adFilterData.StatusId;
                DateTime fromDate = adFilterData.FromDate;
                DateTime toDate = adFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                
                
                // TODO: Move to BLL?
                // Get ads list.
                var DataList = dataContext.GetAdsList(advertiserId,
                                                                campaignId,
                                                                statusId,
                                                                fromDate,
                                                                toDate).ToList();

                Grid.DataSource = DataList;
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
internal class AdsFilterData
{
    public int AdvertiserId;
    public int CampaignId;
    public string StatusId;
    public DateTime FromDate;
    public DateTime ToDate;
}