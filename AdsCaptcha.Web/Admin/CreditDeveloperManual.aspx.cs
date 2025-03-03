using System;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class CreditDeveloperManual : System.Web.UI.Page
    {
        private TD_DEVELOPER _developer;
        private int developerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null)
                Response.Redirect("Login.aspx");

            try
            {
                developerId = int.Parse(Page.Request.QueryString["DeveloperId"].ToString());

                _developer = DeveloperBLL.GetDeveloper(developerId);

                if (_developer == null)
                {
                    throw new Exception("Developer not exists");
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
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManageDevelopers.aspx'>" + "Developers" + "</a>" + " » " +
                                    "Developer: " + _developer.Email + " (<a href='EditDeveloper.aspx?DeveloperId=" + _developer.Developer_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Credit";

            // Fill data from db.
            labelMinCheckAmount.Text = "$" + _developer.Min_Check_Amount.ToString();

            // Get balance.
            decimal totalEarnings = DeveloperBLL.GetTotalEarnings(_developer.Developer_Id);
            decimal totalPayments = DeveloperBLL.GetTotalPayments(_developer.Developer_Id);
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
            int developerId = _developer.Developer_Id;
            decimal amountToCredit = Convert.ToDecimal(textAmountToCredit.Text);
            string additionalData = textAdditionalData.Text.Trim();

            // Credit publisher.
            DeveloperBLL.Credit(developerId, amountToCredit, (int)CreditMethod.Manual, additionalData);

            // Redirect to developers to be paid page.
            Response.Redirect("DevelopersToBePaid.aspx");
        }
    }
}
