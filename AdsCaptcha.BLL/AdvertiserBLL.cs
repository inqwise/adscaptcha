using System;
using System.Collections.Generic;
using System.Linq;
using AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.BLL
{    
    public static class AdvertiserBLL
    {
        #region Public Methods

        /// <summary>
        /// Get advertiser by id.
        /// </summary>
        /// <param name="advertiserId">Advertiser's id to look for.</param>
        /// <returns>Requested advertiser.</returns>
        public static TA_ADVERTISER GetAdvertiser(int advertiserId)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return advertiser.
                    return dataContext.TA_ADVERTISERs.SingleOrDefault(item => item.Advertiser_Id == advertiserId);
                }
            }
            else
            {
                // Advertiser does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get advertiser by email.
        /// </summary>
        /// <param name="email">Advertiser's email to look for.</param>
        /// <returns>Requested advertiser.</returns>
        public static TA_ADVERTISER GetAdvertiser(string email)
        {
            // Check if advertiser exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return advertiser.
                    return dataContext.TA_ADVERTISERs.Single(item => item.Email == email);
                }
            }
            else
            {
                // Advertiser does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get all advertisers list.
        /// </summary>
        /// <returns>Returns all advertisers list.</returns>
        public static List<TA_ADVERTISER> GetAdvertisersList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_ADVERTISERs.ToList<TA_ADVERTISER>();
            }
        }

        /// <summary>
        /// Get advertiser id by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Requested advertiser id.</returns>
        public static int GetAdvertiserIdByEmail(string email)
        {            
            // Check if advertiser exists.
            if (IsExist(email))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return advertiser id.
                    return dataContext.TA_ADVERTISERs.Single(item => item.Email == email).Advertiser_Id;
                }
            }
            else
            {
                // TODO: Handle advertiser not exsists
                return 0;
            }
        }

        /// <summary>
        /// Get advertiser billing address by id.
        /// </summary>
        /// <param name="advertiserId">Advertiser's id to look for.</param>
        /// <returns>Requested advertiser.</returns>
        public static TA_ADVERTISER_BILLING GetAdvertiserBillingDetails(int advertiserId)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return advertiser.
                    return dataContext.TA_ADVERTISER_BILLINGs.SingleOrDefault(i => i.Advertiser_Id == advertiserId);
                }
            }
            else
            {
                // Advertiser does not exist.
                return null;
            }
        }

        /// <summary>
        /// Sign up advertiser.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="billingMethod">Billing method id.</param>
        /// <param name="paymentMethod">Payment method id.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <param name="billingFirstName">Billing first name.</param>
        /// <param name="billingLastName">Billing last name.</param>
        /// <param name="companyName">Billing company name.</param>
        /// <param name="address">Billing address</param>
        /// <param name="city">Billing city.</param>
        /// <param name="state">Billing state</param>
        /// <param name="countryId">Billing country id.</param>
        /// <param name="zipCode">Billing zip code.</param>
        /// <param name="phone">Billing phone number.</param>
        /// <param name="fax">Billing fax number.</param>
        /// <returns>Returns activation code.</returns>
        public static int SignUp(string email, string password, int billingMethod, int paymentMethod, bool getEmailAnnouncements, bool getEmailNewsletters,
                                 string billingFirstName, string billingLastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string fax)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {               
                // Create advertiser.
                TA_ADVERTISER advertiser = new TA_ADVERTISER();
                advertiser.Email = email;
                advertiser.Password = General.GenerateMD5(password);
                advertiser.Status_Id = (int)Status.Running;
                advertiser.Billing_Method_Id = billingMethod;
                advertiser.Payment_Method_Id = paymentMethod;
                advertiser.Min_Billing_Amount = ApplicationConfiguration.DEFAULT_MIN_BILLING_AMOUNT;
                advertiser.Get_Email_Announcements = (getEmailAnnouncements == true) ? 1 : 0;
                advertiser.Get_Email_Newsletters = (getEmailNewsletters == true) ? 1 : 0;                
                advertiser.Join_Date = DateTime.Today;

                advertiser.TA_ADVERTISER_BILLING = new TA_ADVERTISER_BILLING();
                advertiser.TA_ADVERTISER_BILLING.First_Name = billingFirstName;
                advertiser.TA_ADVERTISER_BILLING.Last_Name = billingLastName;
                advertiser.TA_ADVERTISER_BILLING.Company_Name = companyName;
                advertiser.TA_ADVERTISER_BILLING.Address = address;
                advertiser.TA_ADVERTISER_BILLING.City = city;
                advertiser.TA_ADVERTISER_BILLING.State = state;
                advertiser.TA_ADVERTISER_BILLING.Country_Id = countryId;
                advertiser.TA_ADVERTISER_BILLING.Zip_Code = zipCode;
                advertiser.TA_ADVERTISER_BILLING.Phone = phone;
                advertiser.TA_ADVERTISER_BILLING.Fax = fax;
                advertiser.TA_ADVERTISER_BILLING.Card_Id = "0";

                // Add advertiser.
                dataContext.TA_ADVERTISERs.InsertOnSubmit(advertiser);

                // Save changes.
                dataContext.SubmitChanges();

                // Return advertiser id.
                return advertiser.Advertiser_Id;
            }
        }

        /// <summary>
        /// Update advertiser.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="billingMethod">Billing method id.</param>
        /// <param name="paymentMethod">Payment method id.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <param name="minBillingAmount">Minimum billing amount.</param>
        /// <param name="billingName">Billing name.</param>
        /// <param name="companyName">Billing company name.</param>
        /// <param name="address">Billing address</param>
        /// <param name="city">Billing city.</param>
        /// <param name="state">Billing state</param>
        /// <param name="countryId">Billing country id.</param>
        /// <param name="zipCode">Billing zip code.</param>
        /// <param name="phone">Billing phone number.</param>
        /// <param name="fax">Billing fax number.</param>
        public static void Update(int advertiserId, int statusId, int billingMethod, int paymentMethod, bool getEmailAnnouncements, bool getEmailNewsletters, int minBillingAmount, 
                                  string billingFirstName, string billingLastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string fax)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TA_ADVERTISER advertiser = new TA_ADVERTISER();

                    // Get advertiser data.
                    advertiser = dataContext.TA_ADVERTISERs.SingleOrDefault(item => item.Advertiser_Id == advertiserId);

                    if (null == advertiser)
                    {
                        throw new NullReferenceException(String.Format("Advertiser #{0} not found", advertiserId));
                    }

                    // Update status.
                    if (statusId != advertiser.Status_Id)
                    {
                        advertiser.Status_Id = statusId;
                        /*
                        switch (statusId)
                        {
                            case (int)Status.Running:
                                Activate(ref advertiser);
                                break;
                            case (int)Status.Paused:
                                Pause(ref advertiser);
                                break;
                            case (int)Status.Pending:
                                Pending(ref advertiser);
                                break;
                            case (int)Status.Rejected:
                                Reject(ref advertiser);
                                break;
                        }
                        */
                    }

                    // Update advertiser data.
                    advertiser.Billing_Method_Id = billingMethod;
                    advertiser.Payment_Method_Id = paymentMethod;
                    advertiser.Min_Billing_Amount = minBillingAmount;
                    advertiser.Get_Email_Announcements = (getEmailAnnouncements == true) ? 1 : 0;
                    advertiser.Get_Email_Newsletters = (getEmailNewsletters == true) ? 1 : 0;

                    if (null == advertiser.TA_ADVERTISER_BILLING)
                    {
                        advertiser.TA_ADVERTISER_BILLING = new TA_ADVERTISER_BILLING();
                    }
                    advertiser.TA_ADVERTISER_BILLING.First_Name = billingFirstName;
                    advertiser.TA_ADVERTISER_BILLING.Last_Name = billingLastName;
                    advertiser.TA_ADVERTISER_BILLING.Company_Name = companyName;
                    advertiser.TA_ADVERTISER_BILLING.Address = address;
                    advertiser.TA_ADVERTISER_BILLING.City = city;
                    advertiser.TA_ADVERTISER_BILLING.State = state;
                    advertiser.TA_ADVERTISER_BILLING.Country_Id = countryId;
                    advertiser.TA_ADVERTISER_BILLING.Zip_Code = zipCode;
                    advertiser.TA_ADVERTISER_BILLING.Phone = phone;
                    advertiser.TA_ADVERTISER_BILLING.Fax = fax;

                    if (paymentMethod != (int)PaymentMethod.CreditCard)
                    {
                        advertiser.TA_ADVERTISER_BILLING.Card_Id = "0";
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Update advertiser.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="cardId">Credit card id.</param>
        public static void UpdateCreditCard(int advertiserId, string cardId, string cardExpiration)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TA_ADVERTISER advertiser = new TA_ADVERTISER();

                    // Get advertiser data.
                    advertiser = dataContext.TA_ADVERTISERs.SingleOrDefault(item => item.Advertiser_Id == advertiserId);

                    // If card id is null or empty - set as 0.
                    if (String.IsNullOrEmpty(cardId))
                        cardId = "0";

                    // Update advertiser credit card data.
                    advertiser.TA_ADVERTISER_BILLING.Card_Id = cardId;
                    advertiser.TA_ADVERTISER_BILLING.Card_Verify = cardExpiration;

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Check if password match to advertisers's password by email.
        /// </summary>
        /// <param name="email">Advertiser's email.</param>
        /// <param name="password">Password to check if matched.</param>
        /// <returns>Returns whether password match.</returns>
        public static bool CheckPassword(string email, string password)
        {
            // Check if advertiser exists.
            if (IsExist(email) == true)
            {
                // Encrypt password.
                password = General.GenerateMD5(password);

                // Checks if passoword match.
                if (GetPassword(email) == password)
                {
                    using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                    {
                        // TODO: Write login to DB
                        return true;
                    }
                }
                else
                {
                    // Password does not match.
                    return false;
                }
            }
            else
            {
                // Advertiser does not exist.
                return false;
            }
        }

        /// <summary>
        /// Change advertiser's password.
        /// </summary>
        /// <param name="email">Advertiser's email.</param>
        /// <param name="password">New password.</param>
        /// <param name="encrypted">Is new password encrypted? or needed to be encrypt?</param>
        /// <returns>Returns whether password changed.</returns>
        public static bool ChangePassword(string email, string password, bool encrypted)
        {
            // Check if advertiser exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    if (encrypted == false)
                    {
                        // Encrypt password.
                        password = General.GenerateMD5(password);
                    }

                    // Change advertiser's password.
                    dataContext.TA_ADVERTISERs.Single(item => item.Email == email).Password = password;

                    // Sava changes.
                    dataContext.SubmitChanges();

                    return true;
                }
            }
            else
            {
                // Publiser does not exist.
                return false;
            }
        }

        /// <summary>
        /// Charge advertiser - logs payments BASIC details.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="amountToCharge">Amount to charge.</param>
        /// <param name="paymentMethod">Payment method.</param>
        /// <param name="additionalData">Additional data.</param>
        /// <returns></returns>
        public static int Charge(int advertiserId, decimal amountToCharge, int paymentMethod, string additionalData)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get publisher details.
                    TA_ADVERTISER advertiser = GetAdvertiser(advertiserId);
                    TA_ADVERTISER_BILLING billing = AdvertiserBLL.GetAdvertiserBillingDetails(advertiserId);

                    // Create payment.
                    TA_PAYMENT payment = new TA_PAYMENT();
                    payment.Advertiser_Id = advertiserId;
                    payment.First_Name = billing.First_Name;
                    payment.Last_Name = billing.Last_Name;
                    payment.Company_Name = billing.Company_Name;
                    payment.Address = billing.Address;
                    payment.City = billing.City;
                    payment.Country_Id = billing.Country_Id;
                    payment.State = billing.State;
                    payment.Zip_Code = billing.Zip_Code;
                    payment.Phone = billing.Phone;
                    payment.Fax = billing.Fax;
                    payment.Payment_Method_Id = paymentMethod;
                    payment.Amount = amountToCharge;
                    payment.Payment_Date = DateTime.Now;
                    payment.Additional_Data = additionalData;

                    // Add payment details.
                    dataContext.TA_PAYMENTs.InsertOnSubmit(payment);

                    // Update current balance.
                    UpdateCurrBalance(dataContext, advertiserId, amountToCharge);

                    // Sava changes.
                    dataContext.SubmitChanges();

                    return payment.Payment_Id;
                }
            }

            return 0;
        }

        /// <summary>
        /// Charge advertiser with PayPal.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="amountToCharge">Amount to charge.</param>
        /// <param name="additionalData">General additional data.</param>
        /// <param name="transactionId">PayPal transaction id.</param>
        /// <param name="account">Payer PayPal account name.</param>
        /// <param name="paypalData">PayPal additional data.</param>
        /// <returns></returns>
        public static bool ChargePayPal(int advertiserId, decimal amountToCharge, string additionalData,
                                        string transactionId, string account, string paypalData)
        {
            int paymentMethod = (int)PaymentMethod.PayPal;            
            int paymentId = Charge(advertiserId, amountToCharge, paymentMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TA_PAYMENT_PAYPAL payment = new TA_PAYMENT_PAYPAL();
                    payment.Payment_Id = paymentId;
                    payment.Transaction_Id = transactionId;
                    payment.Account = account;
                    payment.Additional_Data = paypalData;

                    // Add payment details.
                    dataContext.TA_PAYMENT_PAYPALs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }
                
                return true;
            }            
        }

        /// <summary>
        /// Charge advertiser with credit card.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="amountToCharge">Amount to charge.</param>
        /// <param name="additionalData">General additional data.</param>
        /// <param name="transactionCode">Transaction code.</param>
        /// <param name="authNumber">Authentication number.</param>
        /// <param name="cardDigits">Card 4 last digits.</param>
        /// <param name="creditData">Credit card additional data.</param>
        /// <returns></returns>
        public static bool ChargeCreditCard(int advertiserId, decimal amountToCharge, string additionalData, string transactionCode, string authNumber, string creditData)
        {
            int paymentMethod = (int)PaymentMethod.CreditCard;
            int paymentId = Charge(advertiserId, amountToCharge, paymentMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TA_PAYMENT_CREDIT_CARD payment = new TA_PAYMENT_CREDIT_CARD();
                    payment.Payment_Id = paymentId;
                    payment.Transaction_Code = transactionCode;
                    payment.Auth_Number = authNumber;
                    payment.Additional_Data = creditData;

                    // Add payment details.
                    dataContext.TA_PAYMENT_CREDIT_CARDs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        /// <summary>
        /// Get balance.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Current balance.</returns>
        public static decimal GetBalance(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_TRANSACTIONs.Where(i => i.Advertiser_Id == advertiserId).OrderByDescending(i => i.Date).First().Balance;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get last payment.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Last payment.</returns>
        public static TA_PAYMENT GetLastPayment(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_PAYMENTs.Where(i => i.Advertiser_Id == advertiserId).OrderByDescending(i => i.Payment_Date).First();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Calculates total charges.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Total charges.</returns>
        public static decimal GetTotalCharges(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_CHARGEs.Where(s => s.Advertiser_Id == advertiserId).Sum(i => (i.Charges as Nullable<decimal>)).GetValueOrDefault();
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total payments.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Total payments.</returns>
        public static decimal GetTotalPayments(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_PAYMENTs.Where(p => p.Advertiser_Id == advertiserId).Sum(i => i.Amount as decimal?).GetValueOrDefault();
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total campaigns.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Total campaigns.</returns>
        public static int GetTotalCampaigns(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_CAMPAIGNs.Where(c => c.Advertiser_Id == advertiserId).Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get payment details.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="paymentId">Payment id.</param>
        /// <returns>Requested payment.</returns>
        public static TA_PAYMENT GetPayment(int advertiserId, int paymentId)
        {
            // Check if advertiser exists.
            if (IsExist(advertiserId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return payment.
                    return dataContext.TA_PAYMENTs.SingleOrDefault(i => i.Advertiser_Id == advertiserId && i.Payment_Id == paymentId);
                }
            }
            else
            {
                // Publisher does not exist.
                return null;
            }
        }

        public static TA_PAYMENT_PAYPAL GetPaymentPayPal(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TA_PAYMENT_PAYPALs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        public static TA_PAYMENT_CREDIT_CARD GetPaymentCreditCard(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TA_PAYMENT_CREDIT_CARDs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        /// <summary>
        /// Update current balance (increase/decrease only).
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="balanceDiff">BalanceDiff difference.</param>
        /// <returns></returns>
        public static bool UpdateCurrBalance(AdsCaptchaDataContext dataContext, int advertiserId, decimal balanceDiff)
        {
            try
            {
                // Get advertiser.
                TA_ADVERTISER advertiser = dataContext.TA_ADVERTISERs.SingleOrDefault(a => a.Advertiser_Id == advertiserId);

                // Update current balance.
                advertiser.Curr_Balance = advertiser.Curr_Balance + balanceDiff;

                dataContext.UpdateAdvertiserCurrentBalance(advertiserId);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks if advertiser exist by id.
        /// </summary>
        /// <param name="advertiserId">Advertiser's id to look for.</param>
        /// <returns>Returns whether advertiser exists or not.</returns>
        private static bool IsExist(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_ADVERTISERs.Where(item => item.Advertiser_Id == advertiserId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if advertiser exist by email.
        /// </summary>
        /// <param name="email">Advertiser's email to look for.</param>
        /// <returns>Returns whether advertiser exists or not.</returns>
        private static bool IsExist(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_ADVERTISERs.Where(item => item.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Change advertiser's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>        
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="statusId">New status.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int advertiserId, int statusId)
        {
            // Get advertiser.
            TA_ADVERTISER advertiser = new TA_ADVERTISER();
            advertiser = GetAdvertiser(advertiserId);

            // Change status.
            advertiser.Status_Id = statusId;
        }

        /// <summary>
        /// Returns advertiser's password by email (encrypted from DB).
        /// </summary>
        /// <param name="email">Advertiser's email to look for.</param>
        /// <returns>Requested user's encrypted password.</returns>
        private static string GetPassword(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_ADVERTISERs.Single(item => item.Email == email).Password;
            }
        }

        #endregion Private Methods       
    }
}
