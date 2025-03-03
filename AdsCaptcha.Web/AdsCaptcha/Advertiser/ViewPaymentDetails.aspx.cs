using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class ViewPaymentDetails : System.Web.UI.Page
    {        
        private TA_PAYMENT _payment;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is not logged in, redirect to login page.
            if (Session["AdvertiserId"] == null) 
                Response.Redirect("Login.aspx");

            try
            {
                int advertiserId = Convert.ToInt16(Session["AdvertiserId"]);
                int paymentId = Convert.ToInt16(Page.Request.QueryString["PaymentId"].ToString());

                _payment = AdvertiserBLL.GetPayment(advertiserId, paymentId);

                if (_payment == null)
                {
                    // TODO: Handle payment not exsists
                    throw new Exception("Payment not exists");
                }
            }
            catch
            {
                // Redirect to manage campaigns page.
                Response.Redirect("ManageCampaigns.aspx");
            }
    
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
        private void InitControls()
        {
            Dictionary<string, string> listPaymentDetails = new Dictionary<string, string>();

            // Set payment data.
            string paymentDate = _payment.Payment_Date.ToShortDateString();
            string paymentAmount = String.Format("${0:#,##0.00}", _payment.Amount);
            string paymentMethod = DictionaryBLL.GetNameById(_payment.Payment_Method_Id);
            string paymentFirstName = _payment.First_Name;
            string paymentLastName = _payment.Last_Name;
            string paymentCompany = _payment.Company_Name;
            string paymentAddress = _payment.Address;
            string paymentCity = _payment.City;
            string paymentCountry = (_payment.Country_Id == 0 ? null : DictionaryBLL.GetCountryById(_payment.Country_Id));
            string paymentState = _payment.State;
            string paymentZipCode = _payment.Zip_Code;
            string paymentPhone = _payment.Phone;
            string paymentFax = _payment.Fax;
            string paymentAdditionalDate = _payment.Additional_Data;

            // Add values to dictionary.
            listPaymentDetails.Add("Payment Date", paymentDate);
            listPaymentDetails.Add("Amount", paymentAmount);
            listPaymentDetails.Add("Payment Method", paymentMethod);

            switch (_payment.Payment_Method_Id)
            {
                case (int)PaymentMethod.Manual:
                    if (!String.IsNullOrEmpty(paymentAdditionalDate)) listPaymentDetails.Add("Additional Data", paymentAdditionalDate);                    
                    break;
                case (int)PaymentMethod.PayPal:
                    TA_PAYMENT_PAYPAL _paypal = AdvertiserBLL.GetPaymentPayPal(_payment.Payment_Id);
                    string paymentAccount = _paypal.Account;
                    string paymentTransactionId = _paypal.Transaction_Id.ToString();
                    if (!String.IsNullOrEmpty(paymentAccount)) listPaymentDetails.Add("PayPal Account", paymentAccount);
                    if (!String.IsNullOrEmpty(paymentTransactionId)) listPaymentDetails.Add("Transaction Id", paymentTransactionId);
                    break;
                case (int)PaymentMethod.CreditCard:
                    TA_PAYMENT_CREDIT_CARD _card = AdvertiserBLL.GetPaymentCreditCard(_payment.Payment_Id);
                    string paymentTransactionCode = _card.Transaction_Code;
                    if (!String.IsNullOrEmpty(paymentTransactionCode)) listPaymentDetails.Add("Transaction Code", paymentTransactionCode);
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(paymentFirstName)) listPaymentDetails.Add("First Name", paymentFirstName);
            if (!String.IsNullOrEmpty(paymentLastName)) listPaymentDetails.Add("Last Name", paymentLastName);
            if (!String.IsNullOrEmpty(paymentCompany)) listPaymentDetails.Add("Company Name", paymentCompany);
            if (!String.IsNullOrEmpty(paymentAddress)) listPaymentDetails.Add("Address", paymentAddress);
            if (!String.IsNullOrEmpty(paymentCity)) listPaymentDetails.Add("City", paymentCity);
            if (!String.IsNullOrEmpty(paymentCountry)) listPaymentDetails.Add("Country", paymentCountry);
            if (!String.IsNullOrEmpty(paymentState)) listPaymentDetails.Add("State/Province", paymentState);
            if (!String.IsNullOrEmpty(paymentZipCode)) listPaymentDetails.Add("Zip/Postal Code", paymentZipCode);
            if (!String.IsNullOrEmpty(paymentPhone)) listPaymentDetails.Add("Phone", paymentPhone);
            if (!String.IsNullOrEmpty(paymentFax)) listPaymentDetails.Add("Fax", paymentFax);
           
            // Display payment data.
            foreach (KeyValuePair<string, string> detail in listPaymentDetails)
            {
                TableRow row = new TableRow();
                
                TableHeaderCell header = new TableHeaderCell();
                TableCell cell = new TableCell();
                header.Text = detail.Key;
                cell.Text = detail.Value;
                
                row.Cells.Add(header);
                row.Cells.Add(cell);

                tablePaymentDetails.Rows.Add(row);
            }
        }
    }
}
