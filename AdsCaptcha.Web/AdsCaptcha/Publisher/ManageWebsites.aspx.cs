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
    public partial class ManageWebsites : System.Web.UI.Page, IPublisherManageWebsites
    {
        private const string kSessionWebsitesListData = "WebsitesListData";
        
        private TP_PUBLISHER _publisher;

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

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    // TODO: Handle publisher not exists
                    throw new Exception("Publisher not exists");
                }
            }
            catch 
            {
                // TODO: Handle publisher not exists exception. All pages init exception comes here.
            }

            if (!Page.IsPostBack && !Grid.IsCallback)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);

                // Set navigation path.
                labelNavigationPath.Text = "<a href=\"ManageWebsites.aspx\">Websites</a>" + " &gt; " +
                                           "Manage";


                // Check if publisher has any websites. If no, display suitable message.
                if (PublisherBLL.GetTotalWebsites(_publisher.Publisher_Id) == 0)
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

            // Set total earnings.
            decimal totalEarnings = PublisherBLL.GetTotalEarnings(_publisher.Publisher_Id);
            labelEarningsSum.Text = string.Format("${0:#,##0.00}", totalEarnings);

            // Set status filter to "all statuses".
            linkFilterStatusRunningPending.CssClass = linkFilterStatusPausedRejected.CssClass = "filter";
            linkFilterStatusAll.CssClass = "filterSelected";
            
            // If bank details is not entered, show nessage.
            if (_publisher.Is_Signed_Up == 0 || _publisher.Credit_Method_Id == (int)CreditMethod.Later)
            {
                string s = (_publisher.Is_Signed_Up == 0 ? "account" : "") + (_publisher.Is_Signed_Up == 0 && _publisher.Credit_Method_Id == (int)CreditMethod.Later ? " and " : "") + (_publisher.Credit_Method_Id == (int)CreditMethod.Later ? "preferred payment method" : "");
                labelSystemMessages.Text = "You haven't entered your " + s + " details.<br />In order to receive your payments, please enter your details <a href='AccountPreferences.aspx'>here</a>.";
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

            WebsitesFilterData websiteFilterData = new WebsitesFilterData();
            websiteFilterData.PublisherId = _publisher.Publisher_Id;
            websiteFilterData.StatusId = statusId;
            websiteFilterData.FromDate = PickerFrom.SelectedDate;
            websiteFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionWebsitesListData] = websiteFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            WebsitesFilterData websiteFilterData = (WebsitesFilterData)Session[kSessionWebsitesListData];
            websiteFilterData.FromDate = fromDate;
            websiteFilterData.ToDate = toDate;
            Session[kSessionWebsitesListData] = websiteFilterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id list.</param>
        private void UpdateFilterStatus(string statusId)
        {
            WebsitesFilterData websiteFilterData = (WebsitesFilterData)Session[kSessionWebsitesListData];
            websiteFilterData.StatusId = statusId;
            Session[kSessionWebsitesListData] = websiteFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionWebsitesListData] != null)
            {
                // Get filter data from session.
                WebsitesFilterData websiteFilterData = (WebsitesFilterData)Session[kSessionWebsitesListData];

                // Set filter parameters variables.
                int publisherId = websiteFilterData.PublisherId;
                string statusId = websiteFilterData.StatusId;
                DateTime fromDate = websiteFilterData.FromDate;
                DateTime toDate = websiteFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get website list.
                Grid.DataSource = dataContext.GetWebsitesList(publisherId,
                                                              statusId,
                                                              fromDate,
                                                              toDate).ToList();

                /*
                string grid = "";

                grid += "<table>";
                grid += "<thead><tr><th>Url</th><th>Status</th><th>Captchas</th><th>Security</th><th>Types</th><th>Earnings</th><th>Add Date</th><th>Modify Date</th><th>Actions</th></thead>";
                grid += "<tbody>";

                foreach (GetWebsitesListResult row in dataContext.GetWebsitesList(publisherId, statusId, fromDate, toDate))
                {
                    grid += "<tr>";
                    grid += "<td>" + row.Url + "</td>";
                    grid += "<td>" + row.Status + "</td>";
                    grid += "<td>" + row.Captchas + "</td>";
                    grid += "<td>" + row.Security_Only_Types + "</td>";
                    grid += "<td>" + row.PPT_Types + "</td>";
                    grid += "<td>" + row.Earnings + "</td>";
                    grid += "<td>" + row.Add_Date + "</td>";
                    grid += "<td>" + row.Modify_Date + "</td>";
                    grid += "</tr>";
                }

                grid += "</tbody>";
                grid += "</table>";

                table.Text = grid;
                */

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
internal class WebsitesFilterData
{
    public int PublisherId;
    public string StatusId;
    public DateTime FromDate;
    public DateTime ToDate;
}