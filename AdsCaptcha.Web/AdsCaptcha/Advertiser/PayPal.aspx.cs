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

namespace Inqwise.AdsCaptcha.Advertiser
{
    public partial class PayPal : System.Web.UI.Page
    {     
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Step 1a: Modify the POST string.
                string formPostData = "cmd=_notify-validate";
                foreach (String postKey in Request.Form)
                {
                    string postValue = Encode(Request.Form[postKey]);
                    formPostData += string.Format("&{0}={1}", postKey, postValue);
                }

                // Step 1b: POST the data back to PayPal.
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] postByteArray = Encoding.ASCII.GetBytes(formPostData);
                string PayPalURL = System.Configuration.ConfigurationSettings.AppSettings["PayPal_Url"].ToString();
                byte[] responseArray;
                
                try
                {
                    responseArray = client.UploadData(PayPalURL, "POST", postByteArray);
                }
                catch
                {
                    responseArray = client.UploadData(PayPalURL, "POST", postByteArray);
                }
                string response = Encoding.ASCII.GetString(responseArray);
               
                // Step 1c: Process the response from PayPal.
                switch (response)
                {
                    case "VERIFIED":
                    {
                        // Get payment notification data.
                        string paymentStatus = Request.Form["payment_status"];
                        string transactionId = Request.Form["txn_id"].Trim();
                        decimal amount = Convert.ToDecimal(Request.Form["mc_gross"]);
                        decimal fee = Convert.ToDecimal(Request.Form["mc_fee"]);
                        string currency = Request.Form["mc_currency"];
                        int advertiserId = Convert.ToInt16(Request.Form["item_number"]);
                        string paymentData = formPostData.Replace("&", "<br/>");
                        string payerEmail = Encode(Request.Form["payer_email"].Trim());
                        TA_ADVERTISER advertiser = AdvertiserBLL.GetAdvertiser(advertiserId);
                        string advertiserEmail = advertiser.Email;

                        if ((paymentStatus.ToLower() == "completed"))
                        {
                            if (advertiser != null)
                            {
                                if (AdvertiserBLL.ChargePayPal(advertiser.Advertiser_Id, amount, null, transactionId, payerEmail, null))
                                {
                                    // Send confirmation mail to advertiser.
                                    Mail.SendPayPalPaymentMail(advertiserEmail, transactionId, amount, currency);

                                    // Send confirmation mail to administrator.
                                    Mail.SendPayPalPaymentAdminMail("Confirmation", transactionId, paymentStatus, amount, fee, currency, advertiserId, advertiserEmail, paymentData);
                                }
                                else
                                {
                                    //Advertiser wasn't charged.
                                }
                            }
                            else
                            {
                                //Advertiser not exists.
                            }
                        }
                        else
                        {
                            // Send notofication mail to administrator.
                            Mail.SendPayPalPaymentAdminMail("Notification", transactionId, paymentStatus, amount, fee, currency, advertiserId, advertiserEmail, paymentData);
                        }
                        
                        //Payment Status Check 
                        //In this step, you should check that the "payment_status" form field is "Completed". This ensures that the customer's payment has been processed by PayPal, and it has been added to the seller's account. 

                        //Transaction Duplication Check 
                        //In this step, you should check that the "txn_id" form field, transaction ID, has not already been processed by your automation system. A good thing to do is to store the transaction ID in a database or file for duplication checking. If the transaction ID posted by PayPal is a duplicate, you should not continue your automation process for this transaction. Otherwise, this could result in sending the same product to a customer twice. 

                        //Seller Email Validation 
                        //In this step, you simply make sure that the transaction is for your account. Your account will have specific email addresses assigned to it. You should verify that the "receiver_email" field has a value that corresponds to an email associated with your account. 

                        //Payment Validation
                        //As of now, this step is not listed on other sites as a requirement, but it is very important. Because any customer who is familiar with query strings can modify the cost of a seller's product, you should verify that the "payment_gross" field corresponds with the actual price of the item that the customer is purchasing. It is up to you to determine the exact price of the item the customer is purchasing using the form fields. Some common fields you may use to lookup the item being purchased include "item_name" and "item_number". To see for yourself, follow these steps: 
                        //Click on a button used to purchase one of your products. 
                        //Locate the "payment_gross" field in the query string and change its value. 
                        //Use the changed URL and perform a re-request, typically by hitting [ENTER] in the browser Address Bar. 
                        //Notice the changed price for your product on the PayPal order page.

                        // Perform steps 2-5 above. 
                        // Continue with automation processing if all steps succeeded.
                        break;
                    }
                    default:
                    {
                        // Possible fraud. Log for investigation.
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                string adminEmail = System.Configuration.ConfigurationSettings.AppSettings["AdminEmail"];
                Mail.SendMail(adminEmail, "PayPal Intgration Error", "An error occured in the PayPal integratino service:" + "<br/><br/>" + ex.Message + "<br/><br/>" + ex.StackTrace);
            }
        }

        private string Encode(string oldValue)
        {
            string newValue = oldValue.Replace("\"", "'");
            newValue = System.Web.HttpUtility.UrlEncode(newValue);
            newValue = newValue.Replace("%2f", "/");
            return newValue;
        }
    }
}
