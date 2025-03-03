using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL;
using Advertiser;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class ManageCampaigns : System.Web.UI.Page, IAdvertiserManageCampaigns
    {
        private const string kSessionCampaignsListData = "CampaignsListData";
        
        private TA_ADVERTISER _advertiser;


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

                _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

                if (_advertiser == null)
                {
                    // TODO: Handle advertiser not exists
                    throw new Exception("Advertiser not exists");
                }

                
            }
            catch 
            {
                // TODO: Handle advertiser not exists exception. All pages init exception comes here.
            }

            if (!Page.IsPostBack && !Grid.IsCallback)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                // Set navigation path.
                //labelNavigationPath.Text = "<a href=\"ManageCampaigns.aspx\">Campaigns</a>" + " &gt; " +
                //                           "Manage";


                // Check if advertiser has any campaigns. If no, display suitable message.
                if (AdvertiserBLL.GetTotalCampaigns(_advertiser.Advertiser_Id) == 0)
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
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
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
            decimal totalCharges = AdvertiserBLL.GetTotalCharges(_advertiser.Advertiser_Id);
            labelChargesSum.Text = String.Format("${0:#,##0.00}", totalCharges);

            // Set status filter to "all statuses".
            linkFilterStatusRunningPending.CssClass = linkFilterStatusPausedRejected.CssClass = "filter";
            linkFilterStatusAll.CssClass = "filterSelected";

            // Get payment method.
            string paymentMethod = (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard ? "CreditCard" : "PayPal");

            // Credit card warning mesaages.
            if (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard) 
            {
                // If credit card not set yet.
                TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);

                if (billing.Card_Id == "0")
                {
                    labelSystemMessages.Text = "Please enter your credit card details <a href='BillingCreditCard.aspx'>here</a>.<br />Your ads won't be displayed until you verify your credit card details.";
                }
            }

            // Check if prepay with no/low credit.
            if (_advertiser.Billing_Method_Id == (int)BillingMethod.Prepay)
            {
                decimal balance = AdvertiserBLL.GetBalance(_advertiser.Advertiser_Id);

                if (balance == 0)
                {
                    labelSystemMessages.Text = "There is not enough credit in your account." + "<br/>" + "To keep displaying your ads, click <a href='Billing" + paymentMethod + ".aspx'>here</a> to add credit to your account.";
                }
                else if (balance < ApplicationConfiguration.MIN_FUNDS_WARNING)
                {
                    labelSystemMessages.Text = "You have no credit in your account." + "<br/>" + "To keep displaying your ads, click <a href='Billing" + paymentMethod + ".aspx'>here</a> to add credit to your account.";
                }
            }

            if (labelSystemMessages.Text != "")
                SystemMessagesHolder.Visible = true;

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

            CampaignsFilterData campaignFilterData = new CampaignsFilterData();
            campaignFilterData.AdvertiserId = _advertiser.Advertiser_Id;
            campaignFilterData.StatusId = statusId;
            campaignFilterData.FromDate = PickerFrom.SelectedDate;
            campaignFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionCampaignsListData] = campaignFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            CampaignsFilterData campaignFilterData = (CampaignsFilterData)Session[kSessionCampaignsListData];
            campaignFilterData.FromDate = fromDate;
            campaignFilterData.ToDate = toDate;
            Session[kSessionCampaignsListData] = campaignFilterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id list.</param>
        private void UpdateFilterStatus(string statusId)
        {
            CampaignsFilterData campaignFilterData = (CampaignsFilterData)Session[kSessionCampaignsListData];
            campaignFilterData.StatusId = statusId;
            Session[kSessionCampaignsListData] = campaignFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionCampaignsListData] != null)
            {
                // Get filter data from session.
                CampaignsFilterData campaignFilterData = (CampaignsFilterData)Session[kSessionCampaignsListData];

                // Set filter parameters variables.
                int advertiserId = campaignFilterData.AdvertiserId;
                string statusId = campaignFilterData.StatusId;
                DateTime fromDate = campaignFilterData.FromDate;
                DateTime toDate = campaignFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get campaign list.
                Grid.DataSource = dataContext.GetCampaignsList(advertiserId,
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
        /// Change dates according to user selection and reload grid.
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
internal class CampaignsFilterData
{
    public int AdvertiserId;
    public string StatusId;
    public DateTime FromDate;
    public DateTime ToDate;
}