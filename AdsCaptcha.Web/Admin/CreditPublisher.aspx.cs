using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class CreditPublisher : System.Web.UI.Page
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
            // Set "bread-crambs" text.
            labelBreadCrambs.Text = "<a href='ManagePublishers.aspx'>" + "Site Owners" + "</a>" + " » " +
                                    "Site Owner: " + "<a href='ManageWebsites.aspx?PublisherId=" + _publisher.Publisher_Id.ToString() + "'>" + _publisher.Email + "</a>" + " » " +
                                    "Credit";

            // Fill credit method list.
            radioCreditMethod.DataTextField = "Item_Desc";
            radioCreditMethod.DataValueField = "Item_Id";
            radioCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            radioCreditMethod.DataBind();
            radioCreditMethod.ClearSelection();
            radioCreditMethod.Items.Insert(0, new ListItem("Manual", "0"));
            radioCreditMethod.SelectedIndex = 0;

            int later = (int)CreditMethod.Later;
            radioCreditMethod.Items.Remove(radioCreditMethod.Items.FindByValue(later.ToString()));

            labelCreditMethod.Text = DictionaryBLL.GetNameById(_publisher.Credit_Method_Id);
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
            int creditMethod = Convert.ToInt32(radioCreditMethod.SelectedValue);

            switch (creditMethod)
            {
                case (int)CreditMethod.BankWire:
                    Response.Redirect("CreditPublisherBank.aspx?PublisherId=" + _publisher.Publisher_Id.ToString());
                    break;
                case (int)CreditMethod.PayPal:
                    Response.Redirect("CreditPublisherPayPal.aspx?PublisherId=" + _publisher.Publisher_Id.ToString());
                    break;
                case (int)CreditMethod.Check:
                    Response.Redirect("CreditPublisherCheck.aspx?PublisherId=" + _publisher.Publisher_Id.ToString());
                    break;
                case (int)CreditMethod.Manual:
                default:
                    Response.Redirect("CreditPublisherManual.aspx?PublisherId=" + _publisher.Publisher_Id.ToString());
                    break;
            }
        }
    }
}
