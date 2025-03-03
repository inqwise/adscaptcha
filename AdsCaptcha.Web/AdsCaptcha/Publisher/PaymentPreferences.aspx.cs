            using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using AjaxControlToolkit;
            using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class PaymentPreferences : System.Web.UI.Page, IPublisherPaymentPreferences
    {
        private TP_PUBLISHER _publisher;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set PaymentPrefs Session context
            Session[ApplicationConfiguration.SESSION_PAYMENT_PREFS_CONTEXT] = Modules.Publisher;

            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Session["PublisherId"]);

                _publisher = PublisherBLL.GetPublisher(publisherId);

                if (_publisher == null)
                {
                    // TODO: Handle publisher not exsists
                    throw new Exception("Publisher not exists");
                }
            }
            catch 
            {
                Response.Redirect("ManageWebsites.aspx");
            }

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Publisher, Master.Page.Header);
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

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            // Set navigation path.
            labelNavigationPath.Text = "<a href=\"AccountPreferences.aspx\">My Account</a>" + " &gt; " +
                                       "Payment Preferences";

            switch (_publisher.Credit_Method_Id)
            {
                case (int)CreditMethod.Manual:
                    Response.Redirect("AccountPreferences.aspx");
                    break;
                case (int)CreditMethod.PayPal:
                case (int)CreditMethod.BankWire:
                case (int)CreditMethod.Check:
                case (int)CreditMethod.Later:
                default:
                    break;
            }

            // Fill credit method list.
            listCreditMethod.DataSource = DictionaryBLL.GetCreditMethodList();
            listCreditMethod.DataBind();

            // Fill data from db.     
            string paymentMethod = ((int)CreditMethod.Later).ToString();
            if (_publisher.Credit_Method_Id > 0 && _publisher.Credit_Method_Id != (int)CreditMethod.Later)
                paymentMethod = _publisher.Credit_Method_Id.ToString();
            listCreditMethod.Items.FindByValue(paymentMethod).Selected = true;

            //Add Event to change value
            listCreditMethod.Attributes.Add("onChange", "javascript:OnPaymentMethodChange();");
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {            
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                return;
            }

            // Get form data.
            int creditMethod = Convert.ToInt16(listCreditMethod.SelectedValue);

            // Update publisher data.
            PublisherBLL.Update(_publisher.Publisher_Id,
                                _publisher.Status_Id,
                                _publisher.First_Name,
                                _publisher.Last_Name,
                                _publisher.Company_Name,
                                _publisher.Address,
                                _publisher.City,
                                _publisher.State,
                                _publisher.Country_Id,
                                _publisher.Zip_Code,
                                _publisher.Phone,
                                _publisher.Phone_2,
                                creditMethod,
                                _publisher.Revenue_Share_Pct, 
                                _publisher.Min_Check_Amount,
                                (_publisher.Is_Branded == 1 ? true : false),
                                (_publisher.Get_Email_Announcements == 1 ? true : false),
                                (_publisher.Get_Email_Newsletters == 1 ? true : false)
                                );

            switch (creditMethod)
            {
                case (int)CreditMethod.BankWire:
                    ctlPaymentBank.SubmitChanges();
                    break;
                case (int)CreditMethod.Check:
                    ctlPaymentCheck.SubmitChanges();
                    break;
                case (int)CreditMethod.PayPal:
                    ctlPaymentPayPal.SubmitChanges();
                    break;
                case (int)CreditMethod.Later:
                    ctlPaymentLater.SubmitChanges();
                    break;
                case (int)CreditMethod.Manual:
                default:
                    break;
            }
        }

    }
}
