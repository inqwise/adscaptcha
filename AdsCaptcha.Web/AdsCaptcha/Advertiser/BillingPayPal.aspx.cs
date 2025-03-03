using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Net;
using System.Text;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Advertiser;
using AjaxControlToolkit;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class BillingPayPal : System.Web.UI.Page, IAdvertiserBillingPayPal
    {
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
                    // TODO: Handle advertiser not exsists
                    throw new Exception("Advertiser not exists");
                }
            }
            catch 
            {
                Response.Redirect(Page.Request.Url.ToString());
            }

            if (_advertiser.Payment_Method_Id == (int)PaymentMethod.CreditCard)
                Response.Redirect("BillingCreditCard.aspx");

            if (!IsPostBack)
            {
                // Set metadata (title, keywords and description).
                Metadata.SetMetadata(Metadata.Pages.Advertiser, Master.Page.Header);

                if (_advertiser.Payment_Method_Id != (int)PaymentMethod.PayPal)
                {
                    panelForm.Visible = false;
                    labelMessage.Text = "Your payment method is not PayPal.";
                    panelMessage.Visible = true;
                }
                else
                {
                    InitControls();
                }
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
            //labelNavigationPath.Text = "<a href=\"BillingSummary.aspx\">Billing</a>" + " &gt; " +
            //                           "Add Credit";

            TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);

            // Get data from DB.
            labelFirstName.Text = billing.First_Name;
            labelLastName.Text = billing.Last_Name;
            labelCompany.Text = billing.Company_Name;
            labelAddress.Text = billing.Address;
            labelCity.Text = billing.City;
            labelCountry.Text = (billing.Country_Id == 0 ? null : DictionaryBLL.GetCountryById(billing.Country_Id));
            labelState.Text = billing.State;
            labelZipCode.Text = billing.Zip_Code;
            labelPhone.Text = billing.Phone;
            labelFax.Text = billing.Fax;

            // Set default amount to credit.
            textAmount.Text = "100";
            
            // Set min. amount validation rules.
            validatorAmount2.ErrorMessage = "* Min. $" + ApplicationConfiguration.MIN_PREPAY_FUNDS.ToString();
        }

        /// <summary>
        /// Submit sign up form.
        /// </summary>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            // If form is not valid, exit.
            if (!Page.IsValid)
            {
                if (!validatorAmount1.IsValid)
                    textAmount.Focus();
                else if (!validatorAmount2.IsValid)
                    textAmount.Focus();

                return;
            }

            // Get form data.
            decimal tmp;
            bool isNum = decimal.TryParse(textAmount.Text, out tmp);
            int amount = Convert.ToInt32(tmp);

            if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS) throw new Exception("Amount is invalid");

            panelForm.Visible = false;
            labelMessage.Text = "Thank you for your payment. It is currently being verified.<br/>Following verification, the payment will appear in your account balance.";
            panelMessage.Visible = true;

            // Redirect to deposit money via PalPal.
            Response.Redirect("PayPalGateway.aspx?amt=" + amount.ToString());
        }

        #region Validation Controls

        protected void checkAmount_ServerValidate(Object sender, ServerValidateEventArgs e)
        {
            decimal tmp;
            bool isNum = decimal.TryParse(textAmount.Text, out tmp);
            int amount = Convert.ToInt32(tmp);

            if (!isNum || amount < ApplicationConfiguration.MIN_PREPAY_FUNDS)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }

        #endregion Validation Controls
    }
}
