using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ManageCaptchas : System.Web.UI.Page
    {
        private const string kSessionFilterData = "AdminFilterData";

        private TP_WEBSITE _website;
        private int publisherId;
        private int websiteId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                publisherId = int.Parse(Page.Request.QueryString["PublisherId"].ToString());
                websiteId = int.Parse(Page.Request.QueryString["WebsiteId"].ToString());

                _website = WebsiteBLL.GetWebsite(publisherId, websiteId);
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (_website == null)
            {
                throw new Exception("Website not exists");
            }

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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            string publisher = PublisherBLL.GetPublisher(_website.Publisher_Id).Email;

            {
                // Set "bread-crambs" text.
                labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                        "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "'>" + publisher + "</a> (<a href='EditPublisher.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "'>edit</a>)" + " » " +
                                        "Website: " + _website.Url + " (<a href='EditWebsite.aspx?PublisherId=" + _website.Publisher_Id.ToString() + "&WebsiteId=" + _website.Website_Id.ToString() + "'>edit</a>)";
            }
            
            // Fill statuses list.
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
            int statusId = Convert.ToInt16(listStatus.SelectedValue);

            FilterData filterData = new FilterData();
            filterData.DeveloperId = 0;
            filterData.PublisherId = _website.Publisher_Id;
            filterData.WebsiteId = _website.Website_Id;
            filterData.StatusId = statusId;

            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id list.</param>
        private void UpdateFilterStatus(int statusId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.StatusId = statusId;
            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionFilterData] != null)
            {
                // Get filter data from session.
                FilterData filterData = (FilterData)Session[kSessionFilterData];

                // Set filter parameters variables.
                int developerId = filterData.DeveloperId;
                int publisherId = filterData.PublisherId;
                int websiteId = filterData.WebsiteId;
                int statusId = filterData.StatusId;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get captchas list.
                Grid.DataSource = dataContext.Admin_GetCaptchasList(developerId, publisherId, websiteId, statusId).ToList();

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
        /// Filter by status.
        /// </summary>
        protected void listStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int statusId = Convert.ToInt16(listStatus.SelectedValue);

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

        [Serializable]
        internal class FilterData
        {
            public int DeveloperId;
            public int PublisherId;
            public int WebsiteId;
            public int StatusId;
        }
    }
}