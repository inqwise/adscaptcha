using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class PayPalGateway : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            PayPalForm.Action = ConfigurationSettings.AppSettings["PayPal_Url"];

            bool closeForm = true;

            if (Request.QueryString.Count == 1)
            {
                TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);
                string depositAmount = Request.QueryString["amt"];

                int amt;
                bool isNum = int.TryParse(depositAmount, out amt);

                if (!isNum || amt < ApplicationConfiguration.MIN_PREPAY_FUNDS) throw new Exception("Amount is invalid");

                amount.Value = amt.ToString();
                business.Value = ConfigurationSettings.AppSettings["PayPal_Account"];
                notify_url.Value = ConfigurationSettings.AppSettings["PayPal_Notify_Url"];

                item_number.Value = _advertiser.Advertiser_Id.ToString();
                item_name.Value = "AdsCaptcha advertisement";
                page_style.Value = "";
                address1.Value = billing.Address;
                city.Value = billing.City;
                var billingCountry = DictionaryBLL.GetCountryList().Where(c => c.Country_Id == billing.Country_Id).SingleOrDefault();
                country.Value = (billingCountry == null ? null : billingCountry.Country_Prefix);
                email.Value = _advertiser.Email;
                first_name.Value = billing.First_Name;
                last_name.Value = billing.Last_Name;
                state.Value = billing.State;
                zip.Value = billing.Zip_Code;
                night_phone_b.Value = billing.Phone;

                closeForm = false;
            }

            if (closeForm)
            {
                Server.Transfer("BillingPayPal.aspx");
            }
        }
    }
}
