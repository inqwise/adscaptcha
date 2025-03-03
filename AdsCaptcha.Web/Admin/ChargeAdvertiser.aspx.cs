using System;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Admin
{
    public partial class ChargeAdvertiser : System.Web.UI.Page
    {
        private TA_ADVERTISER _advertiser;
        private int advertiserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["AdminLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["AdminId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                advertiserId = int.Parse(Page.Request.QueryString["AdvertiserId"].ToString());

                _advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);

                if (_advertiser == null)
                {
                    throw new Exception("Advertiser not exists");
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
            labelBreadCrambs.Text = "<a href='ManageAdvertisers.aspx'>" + "Advertisers" + "</a>" + " » " +
                                    "Advertiser: " + "<a href='ManageCampaigns.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>" + _advertiser.Email + "</a> (<a href='EditAdvertiser.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString() + "'>edit</a>)" + " » " +
                                    "Charge";

            // Fill payment method list.
            radioChargeMethod.DataTextField = "Item_Desc";
            radioChargeMethod.DataValueField = "Item_Id";
            radioChargeMethod.DataSource = DictionaryBLL.GetPaymentMethodList();
            radioChargeMethod.DataBind();
            radioChargeMethod.ClearSelection();
            radioChargeMethod.Items.Insert(0, new ListItem("Manual", "0"));
            radioChargeMethod.SelectedIndex = 0;

            int paypal = (int)PaymentMethod.PayPal;
            radioChargeMethod.Items.FindByValue(paypal.ToString()).Enabled = false;

            labelChargeMethod.Text = DictionaryBLL.GetNameById(_advertiser.Payment_Method_Id);
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

            int chargeMethod = Convert.ToInt32(radioChargeMethod.SelectedValue);
            
            switch (chargeMethod)
            {
                case (int)PaymentMethod.CreditCard:
                    Response.Redirect("ChargeAdvertiserCreditCard.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString());
                    break;
                case (int)PaymentMethod.Manual:
                default:
                    Response.Redirect("ChargeAdvertiserManual.aspx?AdvertiserId=" + _advertiser.Advertiser_Id.ToString());
                    break;
            }
        }
    }
}
