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
using Advertiser;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class BillingSummary : System.Web.UI.Page, IAdvertiserBillingSummary
    {
        private const string kSessionBillingsListData = "CampaignsListData";
        
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
                Response.Redirect("ManageCampaigns.aspx");
            }

            if (!Page.IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                // Set navigation path.
                labelNavigationPath.Text = "<a href=\"BillingSummary.aspx\">Billing</a>" + " &gt; " +
                                           "Billing Summary";

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
            listFilterDate.Items.Clear();

            // Set date filter values.
            listFilterDate.Items.Add(new ListItem("This month", "1"));
            listFilterDate.Items.Add(new ListItem("Last month", "2"));
            listFilterDate.Items.Add(new ListItem("Current quarter", "3"));
            listFilterDate.Items.Add(new ListItem("Last quarter", "4"));
            listFilterDate.Items.Add(new ListItem("Custom...", "9"));

            listFilterDate.SelectedIndex = 0;            

            // Set date picker defaults dates (=this minth).
            PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            PickerTo.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            // Hide custom dates picker panel.
            panelCustomDatesFilter.Visible = false;

            // Fill transactions type list.
            listTransaction.DataSource = DictionaryBLL.GetTransactionTypeList();
            listTransaction.DataBind();
            listTransaction.ClearSelection();
            listTransaction.Items.Insert(0, new ListItem("-- All Transactions --", "0"));
            listTransaction.SelectedIndex = 0;

            // Get last payment.
            TA_PAYMENT lastPayment = AdvertiserBLL.GetLastPayment(_advertiser.Advertiser_Id);
            labelLastPayment.Text = (lastPayment == null) ? "None" : String.Format("${0:#,##0.00}", lastPayment.Amount);  
            
            // Get current balance.
            decimal balance = AdvertiserBLL.GetBalance(_advertiser.Advertiser_Id);
            labelBalance.Text = String.Format("${0:#,##0.00}", balance);

            // Set billing amount.
            labelBillingAmount.Text = String.Format("${0:#,##0.00}", _advertiser.Min_Billing_Amount);

            // Set dates title.
            labelDatesTitle.Text = PickerFrom.SelectedDate.ToString("d MMMM, yyyy") + " - " + PickerTo.SelectedDate.ToString("d MMMM, yyyy");

            // Set billing and payment method.
            labelPaymentMethod.Text = DictionaryBLL.GetNameById(_advertiser.Payment_Method_Id);
            labelBillingMethod.Text = DictionaryBLL.GetNameById(_advertiser.Billing_Method_Id);

            // Add credit link.
            if (_advertiser.Billing_Method_Id == (int)BillingMethod.Prepay)
            {
                panelPrePay.Visible = true;

                if (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard)
                {
                    linkAddCredit.PostBackUrl = "BillingCreditCard.aspx";
                }
                else if (_advertiser.Payment_Method_Id == (int)PaymentMethod.PayPal)
                {
                    linkAddCredit.PostBackUrl = "BillingPayPal.aspx";
                }
            }
            else
            {
                panelPostPay.Visible = true;
            }

            if (Page.Request.QueryString["tx"] != null)
            {
                labelSystemMessages.Text = "Your PayPal payment is being reviewed. Once your payment is approved by PayPal, your balance will be updated.";
                SystemMessagesHolder.Visible = true;
            }
        }

        /// <summary>
        /// Updates filter data values to session. 
        /// </summary>
        private void InitFilterData()
        {
            int transactionTypeId = 0;

            BillingsFilterData billingFilterData = new BillingsFilterData();
            billingFilterData.AdvertiserId = _advertiser.Advertiser_Id;
            billingFilterData.TransactionTypeId = transactionTypeId;
            billingFilterData.FromDate = PickerFrom.SelectedDate;
            billingFilterData.ToDate = PickerTo.SelectedDate;

            Session[kSessionBillingsListData] = billingFilterData;
        }

        /// <summary>
        /// Update dates filter data to session.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        private void UpdateFilterDates(DateTime fromDate, DateTime toDate)
        {
            BillingsFilterData billingFilterData = (BillingsFilterData)Session[kSessionBillingsListData];
            billingFilterData.FromDate = fromDate;
            billingFilterData.ToDate = toDate;
            Session[kSessionBillingsListData] = billingFilterData;
        }

        /// <summary>
        /// Update transaction filter data to session.
        /// </summary>
        /// <param name="transactionTypeId">Transaction type id list.</param>
        private void UpdateFilterTransactionType(int transactionTypeId)
        {
            BillingsFilterData billingFilterData = (BillingsFilterData)Session[kSessionBillingsListData];
            billingFilterData.TransactionTypeId = transactionTypeId;
            Session[kSessionBillingsListData] = billingFilterData;
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void GetData(bool bindGrid)
        {
            if (Session[kSessionBillingsListData] != null)
            {
                // Get filter data from session.
                BillingsFilterData billingFilterData = (BillingsFilterData)Session[kSessionBillingsListData];

                // Set filter parameters variables.
                int advertiserId = billingFilterData.AdvertiserId;
                int transactionTypeId = billingFilterData.TransactionTypeId;
                DateTime fromDate = billingFilterData.FromDate;
                DateTime toDate = billingFilterData.ToDate;

                AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();                

                // TODO: Move to BLL?
                // Get billing list.
                Grid.DataSource = dataContext.GetAdvertiserTransactionList(advertiserId,
                                                                           transactionTypeId, 
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
        /// Filter by transaction type.
        /// </summary>
        protected void listTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            int transactionTypeId = Convert.ToInt16(listTransaction.SelectedValue);

            // Update grid.
            UpdateFilterTransactionType(transactionTypeId);
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
                    // This month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    PickerTo.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "2":
                    // Last month
                    PickerFrom.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1);
                    PickerTo.SelectedDate = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, DateTime.DaysInMonth(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month));
                    panelCustomDatesFilter.Visible = false;
                    break;
                case "3":
                    {
                        int quarterNum = (DateTime.Today.Month - 1) / 3 + 1;
                        int fromMonth = quarterNum * 3 - 2;
                        int toMonth = quarterNum * 3;

                        // Current quarter
                        PickerFrom.SelectedDate = new DateTime(DateTime.Today.Year, fromMonth, 1);
                        PickerTo.SelectedDate = new DateTime(DateTime.Today.Year, toMonth, DateTime.DaysInMonth(DateTime.Today.Year, toMonth));
                        panelCustomDatesFilter.Visible = false;
                        break;
                    }
                case "4":
                    {
                        int quarterNum = (DateTime.Today.AddMonths(-3).Month - 1) / 3 + 1; 
                        int fromMonth = quarterNum * 3 - 2;
                        int toMonth = quarterNum * 3;
                        int year = DateTime.Today.AddMonths(-3).Year;

                        // Last quarter
                        PickerFrom.SelectedDate = new DateTime(year, fromMonth, 1);
                        PickerTo.SelectedDate = new DateTime(year, toMonth, DateTime.DaysInMonth(year, toMonth));
                        panelCustomDatesFilter.Visible = false;
                        break;
                    }
                default:
                    panelCustomDatesFilter.Visible = true;
                    break;
            }

            // Update dates title.
            labelDatesTitle.Text = PickerFrom.SelectedDate.ToString("d MMMM, yyyy") + " - " + PickerTo.SelectedDate.ToString("d MMMM, yyyy");

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
internal class BillingsFilterData
{
    public int AdvertiserId;
    public int TransactionTypeId;
    public DateTime FromDate;
    public DateTime ToDate;
}