using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class Print : System.Web.UI.Page
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
                Response.Redirect("ManageCampaigns.aspx");
            }

            if (!Page.IsPostBack)
            {
                FillCommonData();
                FillData();
            }
        }

        private void FillCommonData()
        {
            lblAdvetiserAddress.Text = string.Empty;
            TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(_advertiser.Advertiser_Id);
            var countries = DictionaryBLL.GetCountryList();

            /*labelEmail.Text = _advertiser.Email;                        
            textFirstName.Text = billing.First_Name;
            textLastName.Text = billing.Last_Name;
            textCompanyName.Text = billing.Company_Name;
            textAddress.Text = billing.Address;
            textCity.Text = billing.City;
            listCountry.Items.FindByValue(billing.Country_Id.ToString()).Selected = true;
            textState.Text = billing.State;
            textZipCode.Text = billing.Zip_Code;
            textPhone.Text = billing.Phone;
            textFax.Text = billing.Fax;*/

            lblAdvetiserAddress.Text += "Bill to: <br />";
            lblAdvetiserAddress.Text += billing.First_Name + " " + billing.Last_Name + "<br />";
            if (!string.IsNullOrEmpty(billing.Company_Name))
                lblAdvetiserAddress.Text += billing.Company_Name + "<br />";
            if (billing.Country_Id > 0)
                lblAdvetiserAddress.Text += countries.Where(c => c.Country_Id == billing.Country_Id).FirstOrDefault().Country_Name + "<br />";
            lblAdvetiserAddress.Text += billing.City + ", " + billing.Address + "<br />";

            lblInvoiceDates.Text = Request["dates"];
            lblInvoiceDate.Text = DateTime.Today.ToLongDateString();
        }

        /// <summary>
        /// Get data (according to filter data stored in session) and bind it to the grid.
        /// </summary>
        /// <param name="bindGrid">To bind grid?</param>
        private void FillData()
        {

            // Set filter parameters variables.
            int advertiserId = _advertiser.Advertiser_Id;
            int transactionTypeId = Convert.ToInt32(Request["tr"]);

            DateTime from = DateTime.MinValue;
            DateTime to = DateTime.MaxValue;

            string[] dates = Request["dates"].Split('-');
            from = Convert.ToDateTime(dates[0]);
            to = Convert.ToDateTime(dates[1]);

            DateTime fromDate = from;
            DateTime toDate = to;

            AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext();

            // TODO: Move to BLL?
            // Get billing list.
            Grid.DataSource = dataContext.GetAdvertiserTransactionList(advertiserId,
                                                                       transactionTypeId,
                                                                       fromDate,
                                                                       toDate).ToList();

            Grid.DataBind();

        }
    }
}
