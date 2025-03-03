using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class ViewPaymentDetails : System.Web.UI.Page
    {
        private TP_PAYMENT _payment;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Set last page.
            Session["PublisherLastPage"] = Page.Request.Url.ToString();

            // If user is not logged in, redirect to login page.
            if (Session["PublisherId"] == null)
                Response.Redirect("Login.aspx");

            try
            {
                int publisherId = Convert.ToInt16(Session["PublisherId"]);
                int paymentId = Convert.ToInt16(Page.Request.QueryString["PaymentId"].ToString());

                _payment = PublisherBLL.GetPayment(publisherId, paymentId);

                if (_payment == null)
                {
                    // TODO: Handle payment not exsists
                    throw new Exception("Payment not exists");
                }
            }
            catch
            {
                // Redirect to manage websites page.
                Response.Redirect("ManageWebsites.aspx");
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
            string paymentMethod = DictionaryBLL.GetNameById(_payment.Credit_Method_Id);
            string paymentFirstName = _payment.First_Name;
            string paymentLastName = _payment.Last_Name;
            string paymentCompany = _payment.Company_Name;
            string paymentAddress = _payment.Address;
            string paymentCity = _payment.City;
            string paymentCountry = DictionaryBLL.GetCountryById(_payment.Country_Id);
            string paymentState = _payment.State;
            string paymentZipCode = _payment.Zip_Code;
            string paymentAdditionalData = _payment.Additional_Data;

            // Add values to dictionary.
            if (!String.IsNullOrEmpty(paymentDate)) listPaymentDetails.Add("Payment Date", paymentDate);
            if (!String.IsNullOrEmpty(paymentAmount)) listPaymentDetails.Add("Amount", paymentAmount);
            if (!String.IsNullOrEmpty(paymentMethod)) listPaymentDetails.Add("Payment Method", paymentMethod);

            switch (_payment.Credit_Method_Id)
            {
                case (int)CreditMethod.Manual:
                    if (!String.IsNullOrEmpty(paymentAdditionalData)) listPaymentDetails.Add("Additional Data", paymentAdditionalData);
                    break;
                case (int)CreditMethod.PayPal:
                    TP_PAYMENT_PAYPAL _paypal = PublisherBLL.GetPaymentPayPal(_payment.Payment_Id);
                    if (!String.IsNullOrEmpty(_paypal.Account)) listPaymentDetails.Add("PayPal Account", _paypal.Account);
                    if (!String.IsNullOrEmpty(_paypal.Transaction_Id)) listPaymentDetails.Add("Transaction Id", _paypal.Transaction_Id);
                    break;
                case (int)CreditMethod.BankWire:
                    TP_PAYMENT_BANK _bank = PublisherBLL.GetPaymentBank(_payment.Payment_Id);
                    if (!String.IsNullOrEmpty(_bank.Auth_Number)) listPaymentDetails.Add("Authentication Code", _bank.Auth_Number);
                    break;
                case (int)CreditMethod.Check:
                    TP_PAYMENT_CHECK _check = PublisherBLL.GetPaymentCheck(_payment.Payment_Id);
                    if (!String.IsNullOrEmpty(_check.Payee_Name)) listPaymentDetails.Add("Payee Name", _check.Payee_Name);
                    if (!String.IsNullOrEmpty(_check.Recipient_Name)) listPaymentDetails.Add("Recipient Name", _check.Recipient_Name);
                    if (!String.IsNullOrEmpty(_check.Auth_Number)) listPaymentDetails.Add("Authentication Code", _check.Auth_Number);
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
