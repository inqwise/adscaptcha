using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ViewRequests : System.Web.UI.Page
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
            labelBreadCrambs.Text = "Requests";

            // Fill publishers list.
            listPublishers.DataValueField = "Publisher_Id";
            listPublishers.DataTextField = "Email";
            listPublishers.DataSource = PublisherBLL.GetPublishersList();
            listPublishers.DataBind();
            listPublishers.Items.Insert(0, new ListItem("-- All Site Owners --", "0"));
            listPublishers.SelectedIndex = 0;

            // Fill websites list.
            listWebsites.DataValueField = "Website_Id";
            listWebsites.DataTextField = "Url";
            ClearWebsitesList();

            // Fill captchas list.
            listCaptchas.DataValueField = "Captcha_Id";
            listCaptchas.DataTextField = "Captcha_Name";
            ClearCaptchasList();

            // Fill captcha types list.
            listCaptchaTypes.DataValueField = "Item_Id";
            listCaptchaTypes.DataTextField = "Item_Desc";
            listCaptchaTypes.DataSource = DictionaryBLL.GetCaptchaTypeList();
            listCaptchaTypes.DataBind();
            listCaptchaTypes.Items.Insert(0, new ListItem("-- All Types --", "0"));
            listCaptchaTypes.SelectedIndex = 0;

            // Set date picker defaults dates (=today).
            PickerFrom.SelectedDate = DateTime.Today;
            PickerTo.SelectedDate = DateTime.Today;
        }

        private void InitFilterData()
        {
            int publisherId = Convert.ToInt16(listPublishers.SelectedValue);
            int websiteId = Convert.ToInt16(listWebsites.SelectedValue);
            int captchaId = Convert.ToInt16(listCaptchas.SelectedValue);
            int typeId = Convert.ToInt16(listCaptchaTypes.SelectedValue);
            DateTime fromDate = PickerFrom.SelectedDate;
            DateTime toDate = PickerTo.SelectedDate;

            FilterData filterData = new FilterData();
            filterData.PublisherId = publisherId;
            filterData.WebsiteId = websiteId;
            filterData.CaptchaId = captchaId;
            filterData.TypeId = typeId;
            filterData.FromDate = fromDate;
            filterData.ToDate = toDate;

            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterPublisher(int publisherId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.PublisherId = publisherId;
            filterData.WebsiteId = 0;
            filterData.CaptchaId = 0;
            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterWebsite(int websiteId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.WebsiteId = websiteId;
            filterData.CaptchaId = 0;
            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterCaptcha(int captchaId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.CaptchaId = captchaId;
            Session[kSessionFilterData] = filterData;
        }

        private void UpdateFilterCaptchaType(int typeId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.TypeId = typeId;
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
                int publisherId = filterData.PublisherId;
                int websiteId = filterData.WebsiteId;
                int captchaId = filterData.CaptchaId;
                int typeId = filterData.TypeId;
                DateTime fromDate = filterData.FromDate;
                DateTime toDate = filterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get requests list.
                Grid.DataSource = dataContext.Admin_GetRequestsList(publisherId, websiteId, captchaId, typeId, fromDate, toDate).ToList();

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

        private void ClearWebsitesList()
        {
            listWebsites.Items.Clear();
            listWebsites.Items.Insert(0, new ListItem("-- All Websites --", "0"));
            listWebsites.SelectedIndex = 0;
            listWebsites.Enabled = false;
        }

        private void ClearCaptchasList()
        {
            listCaptchas.Items.Clear();
            listCaptchas.Items.Insert(0, new ListItem("-- All Captchas --", "0"));
            listCaptchas.SelectedIndex = 0;
            listCaptchas.Enabled = false;
        }

        protected void listPublishers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int publisherId = Convert.ToInt16(listPublishers.SelectedValue);

            if (publisherId != 0)
            {
                // Update websites list.
                listWebsites.DataSource = WebsiteBLL.GetWebsitesList(publisherId);
                listWebsites.DataBind();
                listWebsites.Items.Insert(0, new ListItem("-- All Websites --", "0"));
                listWebsites.SelectedIndex = 0;
                listWebsites.Enabled = true;
                
                ClearCaptchasList();
            }
            else
            {
                ClearWebsitesList();
                ClearCaptchasList();
            }

            // Update grid.
            UpdateFilterPublisher(publisherId);
            GetData(true);
        }

        protected void listWebsites_SelectedIndexChanged(object sender, EventArgs e)
        {
            int publisherId = Convert.ToInt16(listPublishers.SelectedValue);
            int websiteId = Convert.ToInt16(listWebsites.SelectedValue);

            if (websiteId != 0)
            {
                // Update captchas list.
                listCaptchas.DataSource = CaptchaBLL.GetCaptchas(publisherId, websiteId);
                listCaptchas.DataBind();
                listCaptchas.Items.Insert(0, new ListItem("-- All Captchas --", "0"));
                listCaptchas.SelectedIndex = 0;
                listCaptchas.Enabled = true;
            }
            else
            {
                ClearCaptchasList();
            }

            // Update grid.
            UpdateFilterWebsite(websiteId);
            GetData(true);
        }

        protected void listCaptchas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int captchaId = Convert.ToInt16(listCaptchas.SelectedValue);

            // Update grid.
            UpdateFilterCaptcha(captchaId);
            GetData(true);
        }

        protected void listCaptchaTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeId = Convert.ToInt16(listCaptchaTypes.SelectedValue);

            // Update grid.
            UpdateFilterCaptchaType(typeId);
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
            public int PublisherId;
            public int WebsiteId;
            public int CaptchaId;
            public int TypeId;
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