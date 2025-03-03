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
    public partial class PaymentHistory : System.Web.UI.Page, IPublisherPaymentHistory
    {
        private const string kSessionPaymentListData = "PaymentListData";

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
            labelNavigationPath.Text = "<a href=\"PaymentHistory.aspx\">Earnings</a>" + " &gt; " +
                                       "Payment History";

            decimal totalEarnings = PublisherBLL.GetTotalEarnings(_publisher.Publisher_Id);
            decimal totalPayments = PublisherBLL.GetTotalPayments(_publisher.Publisher_Id);
            decimal totalDiff = totalEarnings - totalPayments;

            // Set total values.
            labelEarningsSum.Text = String.Format("${0:#,##0.00}", totalEarnings);
            labelPaymentsSum.Text = String.Format("${0:#,##0.00}", totalPayments);
            labelDiff.Text = String.Format("${0:#,##0.00}", totalDiff);

            listFilterDate.Items.Clear();

            // Set date filter values.
            listFilterDate.Items.Add(new ListItem("This year", "1"));
            listFilterDate.Items.Add(new ListItem("Last year", "2"));
            listFilterDate.Items.Add(new ListItem("All dates", "3"));
            listFilterDate.Items.Add(new ListItem("Custom...", "9"));

            listFilterDate.SelectedIndex = 0;

            // Set date picker defaults dates (=this year).
            PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, 1, 1);
            PickerTo.SelectedDate = DateTime.Today;

            // Hide custom dates picker panel.
            panelCustomDatesFilter.Visible = false;
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            PaymentFilterData paymentFilterData = new PaymentFilterData();
            paymentFilterData.PublisherId = _publisher.Publisher_Id;
            paymentFilterData.FromDate = PickerFrom.SelectedDate;
            paymentFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionPaymentListData] = paymentFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            PaymentFilterData paymentFilterData = (PaymentFilterData)Session[kSessionPaymentListData];
            paymentFilterData.FromDate = PickerFrom.SelectedDate;
            paymentFilterData.ToDate = PickerTo.SelectedDate;
            Session[kSessionPaymentListData] = paymentFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionPaymentListData] != null)
            {
                // Get filter data from session.
                PaymentFilterData paymentFilterData = (PaymentFilterData)Session[kSessionPaymentListData];

                // Set filter parameters variables.
                int publisherId = paymentFilterData.PublisherId;
                DateTime fromDate = paymentFilterData.FromDate;
                DateTime toDate = paymentFilterData.ToDate; 
                
                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();
                
                // TODO: Move to BLL?
                Grid.DataSource = dataContext.GetPaymentList(publisherId, fromDate, toDate).ToList();

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
                    // This year
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, 1, 1);
                    PickerTo.SelectedDate = DateTime.Today;
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "2":
                    // Last year
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
                    PickerTo.SelectedDate = new DateTime(DateTime.Today.AddYears(-1).Year, 12, 31);
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "3":
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
    }
}

[Serializable]
internal class PaymentFilterData
{
    public int PublisherId;
    public DateTime FromDate;
    public DateTime ToDate;
}