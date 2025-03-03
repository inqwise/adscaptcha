using System;
using System.Linq;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class CRM_Requests : System.Web.UI.Page
    {
        private const string kSessionFilterData = "AdminFilterData";
        
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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "Support";

            // Fill statuses list.
            listStatus.DataSource = DictionaryBLL.GetCrmStatusList();
            listStatus.DataBind();
            listStatus.ClearSelection();
            listStatus.Items.Insert(0, new ListItem("-- All Statuses --", "0"));
            listStatus.SelectedIndex = 0;

            // Fill subjects list.
            listSubject.DataSource = DictionaryBLL.GetCrmSubjectList();
            listSubject.DataBind();
            listSubject.ClearSelection();
            listSubject.Items.Insert(0, new ListItem("-- All Subjects --", "0"));
            listSubject.SelectedIndex = 0;
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            int statusId = Convert.ToInt16(listStatus.SelectedValue);
            int subjectId = Convert.ToInt16(listSubject.SelectedValue);

            FilterData filterData = new FilterData();
            filterData.StatusId = statusId;
            filterData.SubjectId = subjectId;

            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Update status filter data to session.
        /// </summary>
        /// <param name="statusId">Status id.</param>
        private void UpdateFilterStatus(int statusId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.StatusId = statusId;
            Session[kSessionFilterData] = filterData;
        }

        /// <summary>
        /// Update subject filter data to session.
        /// </summary>
        /// <param name="subjectId">Subject id.</param>
        private void UpdateFilterSubject(int subjectId)
        {
            FilterData filterData = (FilterData)Session[kSessionFilterData];
            filterData.SubjectId = subjectId;
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
                int statusId = filterData.StatusId;
                int subjectId = filterData.SubjectId;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();

                // TODO: Move to BLL?
                // Get requests list.
                Grid.DataSource = dataContext.CRM_GetRequestsList(statusId, subjectId).ToList();

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

        /// <summary>
        /// Filter by subject.
        /// </summary>
        protected void listSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int subjectId = Convert.ToInt16(listSubject.SelectedValue);

            // Update grid.
            UpdateFilterSubject(subjectId);
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
            public int StatusId;
            public int SubjectId;
        }
    }
}