using System;
using System.Linq;
using System.Drawing;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using am.Charts;
using Inqwise.AdsCaptcha.Admin.Components;
using Inqwise.AdsCaptcha.BLL.Common;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class StartPage : BasePage//System.Web.UI.Page
    {
        private const string kSessionFilterData = "DashboardFilterData";

        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack)
            {
                InitControls();
                InitFilterData();                
                UpdateMeasures();
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

        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "Dashboard";

            listFilterDate.Items.Clear();

            // Set date filter values.
            listFilterDate.Items.Add(new ListItem("Today", "1"));
            listFilterDate.Items.Add(new ListItem("Yesterday", "2"));
            listFilterDate.Items.Add(new ListItem("Last 7 days", "3"));
            listFilterDate.Items.Add(new ListItem("This month", "4"));
            listFilterDate.Items.Add(new ListItem("Last month", "5"));
            listFilterDate.Items.Add(new ListItem("All dates", "6"));
            //listFilterDate.Items.Add(new ListItem("Custom...", "9"));

            listFilterDate.SelectedIndex = 0;

            // Set date picker defaults dates (=today).
            PickerFrom.SelectedDate = PickerTo.SelectedDate = DateTime.Today;

            // Hide custom dates picker panel.
            panelCustomDatesFilter.Visible = false;

            // Get static measures values.
            decimal totalDevelopers = MeasureBLL.GetTotalDevelopers();
            //decimal totalActiveDevelopers = MeasureBLL.GetTotalActiveDevelopers();
            decimal totalPublishers = MeasureBLL.GetTotalPublishers();
            //decimal totalActivePublishers = MeasureBLL.GetTotalActivePublishers();
            decimal totalWebsites = MeasureBLL.GetTotalWebsites();
            //decimal totalActiveWebsites = MeasureBLL.GetTotalActiveWebsites();
            decimal totalAdvertisers = MeasureBLL.GetTotalAdvertisers();
            //decimal totalActiveAdvertisers = MeasureBLL.GetTotalActiveAdvertisers();
            decimal totalAds = MeasureBLL.GetTotalAds();
            //decimal totalActiveAds = MeasureBLL.GetTotalActiveAds();
            decimal averageBid = MeasureBLL.GetAvgBid();
            decimal averageWebsiteValue = MeasureBLL.GetWebsiteAvgValue();
            decimal averageAdvertiserValue = MeasureBLL.GetAdvertiserAvgValue();

            labelDevelopers.Text = totalDevelopers.ToString("N0");
            //labelActiveDevelopers.Text = totalActiveDevelopers.ToString("N0");
            labelPublishers.Text = totalPublishers.ToString("N0");
            //labelActivePublishers.Text = totalActivePublishers.ToString("N0");
            labelWebsites.Text = totalWebsites.ToString("N0");
            //labelActiveWebsites.Text = totalActiveWebsites.ToString("N0");
            labelAdvertisers.Text = totalAdvertisers.ToString("N0");
            //labelActiveAdvertisers.Text = totalActiveAdvertisers.ToString("N0");
            labelAds.Text = totalAds.ToString("N0");
            //labelActiveAds.Text = totalActiveAds.ToString("N0");
            labelAvgBid.Text = averageBid.ToString("C2");
            labelWebsiteAvgValue.Text = averageWebsiteValue.ToString("C2");
            labelAdvertiserAvgValue.Text = averageAdvertiserValue.ToString("C2");

            decimal totalPendingWebsites = MeasureBLL.GetTotalPendingWebsites();
            decimal totalPublishersToBePaid = MeasureBLL.GetTotalPublishersToBePaid();
            decimal totalDevelopersToBePaid = MeasureBLL.GetTotalDevelopersToBePaid();
            decimal totalNewRequests = MeasureBLL.GetTotalNewRequests();

            labelPendingWebsites.Text = totalPendingWebsites.ToString("N0");
            labelPublishersToBePaid.Text = totalPublishersToBePaid.ToString("N0");
            labelDevelopersToBePaid.Text = totalDevelopersToBePaid.ToString("N0");
            labelNewRequests.Text = totalNewRequests.ToString("N0");
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            FilterData filterData = new FilterData();
            filterData.FromDate = PickerFrom.SelectedDate;
            filterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Change dates according to user selection and reload measures.
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
            UpdateMeasures();
        }

        /// <summary>
        /// Update grid data for custom dates.
        /// </summary>
        protected void linkUpdateCustomDates_Click(object sender, EventArgs e)
        {
            UpdateFilterDates(PickerFrom.SelectedDate, PickerTo.SelectedDate);
            UpdateMeasures();
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.FromDate = fromDate;
            filterData.ToDate = toDate;
            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and reload graph.
        /// </summary>
        private void UpdateGraph()
        {
            graph.BorderStyle = BorderStyle.Solid;
            graph.MarginBottom = 30;
            graph.MarginTop = 20;
            graph.MarginLeft = 60;
            graph.MarginRight = 20;
            graph.LegendEnabled = false;
            graph.NoDataErrorMessage = "No data found.";

            //using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            //{
                LineChartGraph graphServed = new LineChartGraph();

                graphServed.LineWidth = 5;
                graphServed.Bullet = LineChartBulletTypes.RoundOutlined;
                graphServed.BulletSize = 10;
                graphServed.BulletColor = Color.DodgerBlue;
                graphServed.BalloonColor = Color.Gray;
                graphServed.BorderStyle = BorderStyle.None;
                graphServed.ForeColor = Color.DodgerBlue;
                graphServed.HoverColor = Color.DodgerBlue;
                graphServed.FillAlpha = 30;
                graphServed.BalloonColor = Color.Gray;               

                int index = 0;
                foreach (Graph_GetRequestsResult current in ContextConnector.DataContext.Graph_GetRequests(DateTime.Today.AddMonths(-1), DateTime.Today).Reverse())
                {
                    LineChartSeriesDataItem series = new LineChartSeriesDataItem(index.ToString(), current.Date.ToShortDateString());
                    graph.Series.Add(series);

                    LineChartValuesDataItem item = new LineChartValuesDataItem(index.ToString(), current.Served);
                    graphServed.Items.Add(item);

                    index++;
                }

                graph.Graphs.Add(graphServed);
//            }
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and reload measures.
        /// </summary>
        private void UpdateMeasures()
        {
            if (Session[kSessionFilterData] != null)
            {
                // Get filter data from session.
                FilterData filterData = (FilterData)Session[kSessionFilterData];

                // Set filter parameters variables.
                DateTime fromDate = filterData.FromDate;
                DateTime toDate = filterData.ToDate;

                // Get measures values.
                decimal totalNewDevelopers = MeasureBLL.GetTotalNewDevelopers(fromDate, toDate);
                decimal totalNewPublishers = MeasureBLL.GetTotalNewPublishers(fromDate, toDate);
                decimal totalNewWebsites = MeasureBLL.GetTotalNewWebsites(fromDate, toDate);
                decimal totalNewAdvertisers = MeasureBLL.GetTotalNewAdvertisers(fromDate, toDate);
                decimal totalNewAds = MeasureBLL.GetTotalNewAds(fromDate, toDate);
                decimal totalServed = MeasureBLL.GetTotalServed(fromDate, toDate);
                decimal totalTyped = MeasureBLL.GetTotalTyped(fromDate, toDate);
                decimal totalTypedCorrect = MeasureBLL.GetTotalTypedCorrect(fromDate, toDate);
                decimal totalTypedIncorrect = totalTyped - totalTypedCorrect;
                decimal totalTypedPct = (totalServed == 0 ? totalServed : totalTyped / totalServed);
                decimal totalTypedCorrectPct = (totalTyped == 0 ? 0 : totalTypedCorrect / totalTyped);
                decimal totalTypedIncorrectPct = (totalTyped == 0 ? 0 : totalTypedIncorrect / totalTyped);
                //decimal totalErrors = MeasureBLL.GetTotalErrors(fromDate, toDate);
                //decimal totalWarnings = MeasureBLL.GetTotalWarnings(fromDate, toDate);
                decimal totalIncome = MeasureBLL.GetTotalIncome(fromDate, toDate);
                decimal totalOutcomeDev = MeasureBLL.GetTotalOutcomeDev(fromDate, toDate);
                decimal totalOutcomePub = MeasureBLL.GetTotalOutcomePub(fromDate, toDate);
                decimal totalProfit = MeasureBLL.GetTotalProfit(fromDate, toDate);

                labelNewDevelopers.Text = totalNewDevelopers.ToString("N0");
                labelNewPublishers.Text = totalNewPublishers.ToString("N0");
                labelNewWebsites.Text = totalNewWebsites.ToString("N0");
                labelNewAdvertisers.Text = totalNewAdvertisers.ToString("N0");
                labelNewAds.Text = totalNewAds.ToString("N0");
                labelServed.Text = totalServed.ToString("N0");
                labelTyped.Text = totalTyped.ToString("N0");
                labelTypedCorrect.Text = totalTypedCorrect.ToString("N0");
                labelTypedIncorrect.Text = totalTypedIncorrect.ToString("N0");
                labelTypedPct.Text = totalTypedPct.ToString("P");
                labelTypedCorrectPct.Text = totalTypedCorrectPct.ToString("P");
                labelTypedIncorrectPct.Text = totalTypedIncorrectPct.ToString("P");
                //labelErrors.Text = totalErrors.ToString("N0");
                //labelWarnings.Text = totalWarnings.ToString("N0");
                labelIncome.Text = totalIncome.ToString("C2");
                labelProfit.Text = totalProfit.ToString("C2");
                labelOutcomeDev.Text = totalOutcomeDev.ToString("C2");
                labelOutcomePub.Text = totalOutcomePub.ToString("C2");

                //if (totalErrors > 0)
                //    labelErrors.ForeColor = System.Drawing.Color.Red;
                //else
                //    labelErrors.ForeColor = System.Drawing.Color.Black;

                //if (totalWarnings > 0)
                //    labelWarnings.ForeColor = System.Drawing.Color.Red;
                //else
                //    labelWarnings.ForeColor = System.Drawing.Color.Black;
            }

            UpdateGraph();
        }
    }
}

[Serializable]
internal class FilterData
{
    public DateTime FromDate;
    public DateTime ToDate;
}
