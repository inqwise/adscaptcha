﻿using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ViewCharges : System.Web.UI.Page
    {
        private const string kSessionFilterData = "FilterData";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            if (!Page.IsPostBack && !Grid.IsCallback)
            {
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

        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "Finance" + " » " + "Charges From Advertisers";

            // Fill advertisers list.
            listAdvertisers.DataValueField = "Advertiser_Id";
            listAdvertisers.DataTextField = "Email";
            listAdvertisers.DataSource = AdvertiserBLL.GetAdvertisersList();
            listAdvertisers.DataBind();
            listAdvertisers.Items.Insert(0, new ListItem("-- All Advertisers --", "0"));
            listAdvertisers.SelectedIndex = 0;

            // Set date picker defaults dates.
            PickerFrom.SelectedDate = DateTime.Today.AddMonths(-6);
            PickerTo.SelectedDate = DateTime.Today;
        }

        private void InitFilterData()
        {
            int advertiserId = Convert.ToInt16(listAdvertisers.SelectedValue);
            DateTime fromDate = PickerFrom.SelectedDate;
            DateTime toDate = PickerTo.SelectedDate;

            FilterData filterData = new FilterData();
            filterData.AdvertiserId = advertiserId;
            filterData.FromDate = fromDate;
            filterData.ToDate = toDate;

            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterAdvertiser(int advertiserId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.AdvertiserId = advertiserId;
            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterToDate(DateTime date)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.ToDate = date;
            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterFromDate(DateTime date)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.FromDate = date;
            Session[kSessionFilterData] = filterData;
        }
        
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionFilterData] != null)
            {
                // Get filter data from session.
                FilterData filterData = (FilterData)Session[kSessionFilterData];

                // Set filter parameters variables.
                int advertiserId = filterData.AdvertiserId;
                DateTime fromDate = filterData.FromDate;
                DateTime toDate = filterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get requests list.
                Grid.DataSource = dataContext.Admin_GetChargesList(advertiserId, fromDate, toDate).ToList();

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

        protected void listAdvertisers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int advertiserId = Convert.ToInt16(listAdvertisers.SelectedValue);

            // Update grid.
            UpdateFilterAdvertiser(advertiserId);
            GetData(true);
        }

        protected void PickerFrom_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = PickerFrom.SelectedDate;
            CalendarFrom.SelectedDate = date;

            // Update grid.
            UpdateFilterFromDate(date);
            GetData(true);
        }

        protected void PickerTo_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = PickerTo.SelectedDate;
            CalendarTo.SelectedDate = date;

            // Update grid.
            UpdateFilterToDate(date);
            GetData(true);
        }

        protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = CalendarFrom.SelectedDate;
            PickerFrom.SelectedDate = date;

            // Update grid.
            UpdateFilterFromDate(date);
            GetData(true);
        }

        protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
        {
            DateTime date = CalendarTo.SelectedDate;
            PickerTo.SelectedDate = date;

            // Update grid.
            UpdateFilterToDate(date);
            GetData(true);
        }

        [Serializable]
        internal class FilterData
        {
            public int AdvertiserId;
            public DateTime FromDate;
            public DateTime ToDate;
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