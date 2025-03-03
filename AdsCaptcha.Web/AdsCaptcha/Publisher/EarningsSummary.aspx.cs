using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class EarningsSummary : System.Web.UI.Page, IPublisherEarningsSummary
    {
        private const string kSessionEarningsListData = "EarningsListData";

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
                    // TODO: Handle publisher not exsists
                    throw new Exception("Publisher not exists");
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

                InitControls();
                InitFilterData();
                GetData(true);
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
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"EarningsSummary.aspx\">Earnings</a>" + " &gt; " +
                                       "Earnings Summary";

            listFilterDate.Items.Clear();

            // Set date filter values.
            listFilterDate.Items.Add(new ListItem("This month", "1"));
            listFilterDate.Items.Add(new ListItem("Last month", "2"));
            listFilterDate.Items.Add(new ListItem("Custom...", "9"));

            listFilterDate.SelectedIndex = 0;

            // Set date picker defaults dates (=this month).
            PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            PickerTo.SelectedDate = DateTime.Today;

            // Hide custom dates picker panel.
            panelCustomDatesFilter.Visible = false;

            // Fill website list.
            listWebsite.DataSource = PublisherBLL.GetWebsitesList(_publisher.Publisher_Id);
            listWebsite.DataBind();
            listWebsite.ClearSelection();
            listWebsite.Items.Insert(0, new ListItem("-- All Websites --", "0"));
            listWebsite.SelectedIndex = 0;

            // Get last payment.
            TP_PAYMENT lastPayment = PublisherBLL.GetLastPayment(_publisher.Publisher_Id);
            labelLastPayment.Text = (lastPayment == null) ? "None" : String.Format("${0:#,##0.00}", lastPayment.Amount);
            
            // Get minimum check amount.
            labelMinCheckAmount.Text = String.Format("${0:#,##0.00}", _publisher.Min_Check_Amount);
                
            // Set total earnings.
            decimal totalEarnings = PublisherBLL.GetTotalEarnings(_publisher.Publisher_Id);
            labelEarningsSum.Text = String.Format("${0:#,##0.00}", totalEarnings);

            // Set payment method.
            
            string paymentMethod = "Not set";
            if (_publisher.Credit_Method_Id > 0 && _publisher.Credit_Method_Id != (int)CreditMethod.Later)
                    paymentMethod = DictionaryBLL.GetNameById(_publisher.Credit_Method_Id);
            labelPaymentMethod.Text = paymentMethod;
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            EarningsFilterData earningsFilterData = new EarningsFilterData();
            earningsFilterData.PublisherId = _publisher.Publisher_Id;
            earningsFilterData.FromDate = PickerFrom.SelectedDate;
            earningsFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionEarningsListData] = earningsFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            EarningsFilterData earningsFilterData = (EarningsFilterData)Session[kSessionEarningsListData];
            earningsFilterData.FromDate = PickerFrom.SelectedDate;
            earningsFilterData.ToDate = PickerTo.SelectedDate;
            Session[kSessionEarningsListData] = earningsFilterData;
        }

        /// <summary>
        /// Update website filter data to session.
        /// </summary>
        /// <param name="websiteId">Website id list.</param>
        private void UpdateFilterWebsite(int websiteId)
        {
            EarningsFilterData earningsFilterData = (EarningsFilterData)Session[kSessionEarningsListData];
            earningsFilterData.WebsiteId = websiteId;
            Session[kSessionEarningsListData] = earningsFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionEarningsListData] != null)
            {
                // Get filter data from session.
                EarningsFilterData earningsFilterData = (EarningsFilterData)Session[kSessionEarningsListData];

                // Set filter parameters variables.
                int publisherId = earningsFilterData.PublisherId;
                int websiteId = earningsFilterData.WebsiteId;
                DateTime fromDate = earningsFilterData.FromDate;
                DateTime toDate = earningsFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();

                // TODO: Move to BLL?
                // Get earnings list.
                Grid.DataSource = dataContext.GetEarningsList(publisherId,
                                                              websiteId,
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
                    // This month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "2":
                    // Last month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
                    PickerTo.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month));
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
        /// Filter by website.
        /// </summary>
        protected void listWebsite_SelectedIndexChanged(object sender, EventArgs e)
        {
            int websiteId = Convert.ToInt16(listWebsite.SelectedValue);

            // Update grid.
            UpdateFilterWebsite(websiteId);
            GetData(true);
        }
    }
}

[Serializable]
internal class EarningsFilterData
{
    public int PublisherId;
    public int WebsiteId;
    public DateTime FromDate;
    public DateTime ToDate;
}