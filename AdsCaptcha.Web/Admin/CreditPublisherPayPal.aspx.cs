using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class CreditPublisherPayPal : System.Web.UI.Page
    {
        private TP_PUBLISHER _publisher;
        private int publisherId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                publisherId = int.Parse(Page.Request.QueryString["publisherId"].ToString());

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    throw new Exception("Publisher not exists");
                }
            }
            catch
            {
                Response.Redirect("StartPage.aspx");
            }

            if (!IsPostBack)
            {
                InitControls();
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
            // Get check details.
            TP_PUBLISHER_PAYPAL paypal = PublisherBLL.GetPublisherPayPalDetails(_publisher.Publisher_Id);
            
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                    "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _publisher.Publisher_Id.ToString() + "'>" + _publisher.Email + "</a>" + " » " +
                                    "Credit";

            if (paypal != null)
            {
                textAccountName.Text = paypal.Payee_Account;
            }

            // Fill data from db.
            labelMinCheckAmount.Text = "$" + _publisher.Min_Check_Amount.ToString();

            // Get balance.
            decimal totalEarnings = PublisherBLL.GetTotalEarnings(_publisher.Publisher_Id);
            decimal totalPayments = PublisherBLL.GetTotalPayments(_publisher.Publisher_Id);
            decimal balance = totalEarnings - totalPayments;
            labelBalance.Text = String.Format("${0:#,##0.00}", balance);

            // Set amount to credit.
            textAmountToCredit.Text = (balance <= 0 ? "0" : balance.ToString());
        }

        /// <summary>
        /// Submit form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            // Get data.
            int publisherId = _publisher.Publisher_Id;
            string accountName = textAccountName.Text.Trim();
            string transactionId = textTransactionId.Text.Trim();
            decimal amountToCredit = Convert.ToDecimal(textAmountToCredit.Text);
            string additionalData = textAdditionalData.Text.Trim();

            // Credit publisher.
            PublisherBLL.CreditPayPal(publisherId, amountToCredit, null, accountName, transactionId, additionalData);

            // Redirect to publishers to be paid page.
            Response.Redirect("PublishersToBePaid.aspx");
        }
    }
}
