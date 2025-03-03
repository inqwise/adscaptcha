using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ReportCountry : System.Web.UI.Page
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
            labelBreadCrambs.Text = "Countries";

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
            Grid.DataSource = ReportsBLL.Countries(
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