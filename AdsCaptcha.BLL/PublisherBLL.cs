using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.BLL
{    
    public static class PublisherBLL
    {
        #region Public Methods

        /// <summary>
        /// Get publisher by id.
        /// </summary>
        /// <param name="publisherId">Publisher's id to look for.</param>
        /// <returns>Requested publisher.</returns>
        public static TP_PUBLISHER GetPublisher(int publisherId)
        {
            // Check if publisher exists.
            if (IsExist(publisherId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return publisher.
                    return dataContext.TP_PUBLISHERs.SingleOrDefault(item => item.Publisher_Id == publisherId);
                }
            }
            else
            {
                // Publisher does not exist.
                return null;                
            }
        }

        /// <summary>
        /// Get publisher by email.
        /// </summary>
        /// <param name="email">Publisher's email to look for.</param>
        /// <returns>Requested publisher.</returns>
        public static TP_PUBLISHER GetPublisher(string email)
        {
            // Check if publisher exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return publisher.
                    return dataContext.TP_PUBLISHERs.Single(item => item.Email.ToLower() == email.ToLower());
                }
            }
            else
            {
                // Publisher does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get publisher bank details by id.
        /// </summary>
        /// <param name="publisherId">Publisher's id to look for.</param>
        /// <returns>Requested publisher bank details.</returns>
        public static TP_PUBLISHER_BANK GetPublisherBankDetails(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHER_BANKs.SingleOrDefault(i => i.Publisher_Id == publisherId);
            }
        }

        /// <summary>
        /// Get publisher check details by id.
        /// </summary>
        /// <param name="publisherId">Publisher's id to look for.</param>
        /// <returns>Requested publisher check details.</returns>
        public static TP_PUBLISHER_CHECK GetPublisherCheckDetails(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHER_CHECKs.SingleOrDefault(i => i.Publisher_Id == publisherId);
            }
        }

        /// <summary>
        /// Get publisher PayPal details by id.
        /// </summary>
        /// <param name="publisherId">Publisher's id to look for.</param>
        /// <returns>Requested publisher PayPal details.</returns>
        public static TP_PUBLISHER_PAYPAL GetPublisherPayPalDetails(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHER_PAYPALs.SingleOrDefault(i => i.Publisher_Id == publisherId);
            }
        }

        /// <summary>
        /// Get all publishers list.
        /// </summary>
        /// <returns>Returns all publishers list.</returns>
        public static List<TP_PUBLISHER> GetPublishersList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHERs.ToList<TP_PUBLISHER>();
            }
        }

        /// <summary>
        /// Get publishers' websites list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Returns all publishers' websites list.</returns>
        public static List<TP_WEBSITE> GetWebsitesList(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITEs.Where(i => i.Publisher_Id == publisherId).ToList<TP_WEBSITE>();
            }
        }

        /// <summary>
        /// Get publisher id by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Requested publisher id.</returns>
        public static int GetPublisherIdByEmail(string email)
        {            
            // Check if publisher exists.
            if (IsExist(email))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return publisher id.
                    return dataContext.TP_PUBLISHERs.Single(item => item.Email == email).Publisher_Id;
                }
            }
            else
            {
                // TO DO: Handle publisher not exists
                return 0;
            }
        }

        /// <summary>
        /// Checks if publisher is waiting for activation.
        /// </summary>
        /// <param name="email">Publisher's email to look for.</param>
        /// <returns>Returns whether publisher exists in activation list or not.</returns>
        public static bool IsWaitingForActivation(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_PUBLISHER_ACTIVATIONs.Where(item => item.Email == email).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get last payment.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Last payment.</returns>
        public static TP_PAYMENT GetLastPayment(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TP_PAYMENTs.Where(i => i.Publisher_Id == publisherId).OrderByDescending(i => i.Payment_Date).First();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sign up publisher (add to activation list).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="address">Address</param>
        /// <param name="city">City.</param>
        /// <param name="state">State</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="zipCode">Zip code.</param>
        /// <param name="phone">Phone number 1.</param>
        /// <param name="phone2">Phone number 2.</param>
        /// <param name="minCheckAmount">Minimum check amount.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <returns>Returns activation code.</returns>
        public static string SignUp(string email, string password, 
                                    string firstName, string lastName, string companyName, string address, string city, string state, int? countryId, string zipCode, string phone, string phone2, int creditMethodId, int minCheckAmount,
                                    bool getEmailAnnouncements, bool getEmailNewsletters,
                                    string bankName, string bankBranchName, string bankAddress, string bankCity, string bankCountry, string bankState, string bankHolderName, string bankAccountNumber, string bankCode, string checkPayeeName, string checkRecipientName, string checkAddress, string checkCity, string checkCountry, string checkState, string checkZipCode, string paypalPayeeAccount)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Generate acivation (guid) code.
                    string activationCode = Guid.NewGuid().ToString();

                    // Create publisher.
                    TP_PUBLISHER_ACTIVATION publisherActivation = new TP_PUBLISHER_ACTIVATION();
                    publisherActivation.ActivationCode = activationCode;
                    publisherActivation.Email = email;
                    publisherActivation.Password = General.GenerateMD5(password);
                    publisherActivation.First_Name = firstName;
                    publisherActivation.Last_Name = lastName;
                    publisherActivation.Company_Name = companyName;
                    publisherActivation.Address = address;
                    publisherActivation.City = city;
                    publisherActivation.State = state;
                    publisherActivation.Country_Id = countryId;
                    publisherActivation.Zip_Code = zipCode;
                    publisherActivation.Phone = phone;
                    publisherActivation.Phone_2 = phone2;
                    publisherActivation.Credit_Method_Id = creditMethodId;
                    publisherActivation.Min_Check_Amount = minCheckAmount;
                    publisherActivation.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                    publisherActivation.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                    publisherActivation.Sign_Up_Date = DateTime.Today;

                    // Set payment preferences.
                    switch (creditMethodId)
                    {
                        case (int)CreditMethod.BankWire:
                            publisherActivation.Bank_Name = bankName;
                            publisherActivation.Bank_Branch_Name = bankBranchName;
                            publisherActivation.Bank_Address = bankAddress;
                            publisherActivation.Bank_City = bankCity;
                            publisherActivation.Bank_Country = bankCountry;
                            publisherActivation.Bank_State = bankState;
                            publisherActivation.Bank_Holder_Name = bankHolderName;
                            publisherActivation.Bank_Account_Numer = bankAccountNumber;
                            publisherActivation.Bank_Code = bankCode;
                            break;
                        case (int)CreditMethod.Check:
                            publisherActivation.Check_Payee_Name = checkPayeeName;
                            publisherActivation.Check_Recipient_Name = checkRecipientName;
                            publisherActivation.Check_Address = checkAddress;
                            publisherActivation.Check_City = checkCity;
                            publisherActivation.Check_Country = checkCountry;
                            publisherActivation.Check_State = checkState;
                            publisherActivation.Check_Zip_Code = checkZipCode;
                            break;
                        case (int)CreditMethod.PayPal:
                            publisherActivation.PayPal_Payee_Account = paypalPayeeAccount;
                            break;
                    }
                    
                    // Add publisher.
                    dataContext.TP_PUBLISHER_ACTIVATIONs.InsertOnSubmit(publisherActivation);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Return activation code.
                    return activationCode;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Invite publisher (by developer).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <returns>Returns new publisher id.</returns>
        public static int AddByDeveloper(int developerId, string email, string password)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create publisher.
                TP_PUBLISHER publisher = new TP_PUBLISHER();
                publisher.Developer_Id = developerId;
                publisher.Email = email;
                publisher.Password = General.GenerateMD5(password);
                publisher.Status_Id = (int)Status.Running;
                publisher.Join_Date = DateTime.Today;
                publisher.Revenue_Share_Pct = ApplicationConfiguration.DEFAULT_REVENUE_SHARE_PUBLISHER;
                publisher.Is_Branded = 1;
                publisher.Min_Check_Amount = ApplicationConfiguration.DEFAULT_MIN_CHECK_AMOUNT;
                publisher.Credit_Method_Id = (int)CreditMethod.Later;

                publisher.TP_PUBLISHER_BANK = new TP_PUBLISHER_BANK();

                // Add publisher.
                dataContext.TP_PUBLISHERs.InsertOnSubmit(publisher);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new publisher id.
                return publisher.Publisher_Id;
            }
        }

        /// <summary>
        /// Get activation code by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Requested activation code.</returns>
        public static string GetActivationCodeByEmail(string email)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    return dataContext.TP_PUBLISHER_ACTIVATIONs.SingleOrDefault(item => item.Email == email).ActivationCode;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get email by activation code.
        /// </summary>
        /// <param name="activationCode">Activation Code.</param>
        /// <returns>Requested email.</returns>
        public static string GetEmailByActivationCode(string activationCode)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    return dataContext.TP_PUBLISHER_ACTIVATIONs.SingleOrDefault(item => item.ActivationCode == activationCode).Email;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check if activation code exists.
        /// </summary>
        /// <param name="activationCode">Activation code.</param>
        /// <returns></returns>
        public static bool IsActivationCodeExists(string activationCode)
        {
            TP_PUBLISHER_ACTIVATION publisherActivation = new TP_PUBLISHER_ACTIVATION();
            publisherActivation = GetPublisherActivation(activationCode);

            return (publisherActivation == null ? false : true);
        }

        /// <summary>
        /// Activate publisher.
        /// </summary>
        /// <param name="activationCode">Activation code.</param>
        /// <returns>New publisher id.</returns>
        public static int Activate(string activationCode)
        {
            // Get publisher's info by activation code.
            TP_PUBLISHER_ACTIVATION publisherActivation = new TP_PUBLISHER_ACTIVATION();
            publisherActivation = GetPublisherActivation(activationCode);

            // Check if activation code was not found.
            if (publisherActivation == null)
            {
                return 0;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_PUBLISHER publisher = new TP_PUBLISHER();

                    // Get credit method.                    
                    int creditMethodId = publisherActivation.Credit_Method_Id;

                    // Add publisher.
                    publisher = Add(dataContext,
                                    null,
                                    publisherActivation.Email,
                                    publisherActivation.Password,
                                    publisherActivation.First_Name,
                                    publisherActivation.Last_Name,
                                    publisherActivation.Company_Name,
                                    publisherActivation.Address,
                                    publisherActivation.City,
                                    publisherActivation.State,
                                    publisherActivation.Country_Id,
                                    publisherActivation.Zip_Code,
                                    publisherActivation.Phone,
                                    publisherActivation.Phone_2,
                                    creditMethodId,
                                    publisherActivation.Min_Check_Amount,
                                    true, // is branded
                                    publisherActivation.Get_Email_Announcements == 1 ? true : false,
                                    publisherActivation.Get_Email_Newsletters == 1 ? true : false);

                    // Delete activation details.
                    DeleteActivation(dataContext, activationCode);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Set payment preferences.
                    switch (creditMethodId)
                    {
                        case (int)CreditMethod.BankWire:
                            UpdateBankDetails(publisher.Publisher_Id, publisherActivation.Bank_Name, publisherActivation.Bank_Branch_Name, publisherActivation.Bank_Address, publisherActivation.Bank_City, publisherActivation.Bank_State, publisherActivation.Bank_Country, publisherActivation.Bank_Holder_Name, publisherActivation.Bank_Account_Numer, publisherActivation.Bank_Code);
                            break;
                        case (int)CreditMethod.Check:
                            UpdateCheckDetails(publisher.Publisher_Id, publisherActivation.Check_Payee_Name, publisherActivation.Check_Recipient_Name, publisherActivation.Check_Address, publisherActivation.Check_City, publisherActivation.Check_State, publisherActivation.Check_Country, publisherActivation.Check_Zip_Code);
                            break;
                        case (int)CreditMethod.PayPal:
                            UpdatePayPalDetails(publisher.Publisher_Id, publisherActivation.PayPal_Payee_Account);
                            break;
                    }

                    // Return new publisher id.
                    return publisher.Publisher_Id;
                }
            }
        }

        /// <summary>
        /// Quick publisher signup
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="getEmailAnnouncements"></param>
        /// <param name="getEmailNewsletters"></param>
        /// <returns></returns>
        public static int QuickSignUp(string email, string password,
                              bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create publisher.
                TP_PUBLISHER publisher = new TP_PUBLISHER();
                publisher.Email = email;
                publisher.Password = General.GenerateMD5(password);
                publisher.Status_Id = (int)Status.Running;
                publisher.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                publisher.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                publisher.Is_Signed_Up = 1;
                publisher.Join_Date = DateTime.Today;

                // Add publisher.
                dataContext.TP_PUBLISHERs.InsertOnSubmit(publisher);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new publisher id.
                return publisher.Publisher_Id;
            }
        }

        /// <summary>
        /// Add new publisher (without activation).
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="address">Address</param>
        /// <param name="city">City.</param>
        /// <param name="state">State.</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="zipCode">Zip code.</param>
        /// <param name="phone">Phone number 1.</param>
        /// <param name="phone2">Phone number 2.</param>
        /// <param name="revenueSharePct">Revenue share percentage.</param>
        /// <param name="minCheckAmount">Minimum check amount.</param>
        /// <param name="isBranded">Is branded.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <returns>New publisher's id.</returns>
        public static int Add(Nullable<int> developerId, string email, string password, 
                              string firstName, string lastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string phone2, int creditMethodId, int revenueSharePct, int minCheckAmount, bool isBranded,
                              bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Create publisher.
                TP_PUBLISHER publisher = new TP_PUBLISHER();
                publisher.Developer_Id = (developerId <= 0 ? null : developerId);
                publisher.Email = email;
                publisher.Password = General.GenerateMD5(password);
                publisher.Status_Id = (int)Status.Running;
                publisher.First_Name = firstName;
                publisher.Last_Name = lastName;
                publisher.Company_Name = companyName;
                publisher.Address = address;
                publisher.City = city;
                publisher.State = state;
                publisher.Country_Id = countryId;
                publisher.Zip_Code = zipCode;
                publisher.Phone = phone;
                publisher.Phone_2 = phone2;
                publisher.Credit_Method_Id = creditMethodId;
                publisher.Min_Check_Amount = minCheckAmount;
                publisher.Revenue_Share_Pct = revenueSharePct;
                publisher.Is_Branded = (isBranded ? 1 : 0);
                publisher.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                publisher.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                publisher.Is_Signed_Up = 1;
                publisher.Join_Date = DateTime.Today;

                // Add publisher.
                dataContext.TP_PUBLISHERs.InsertOnSubmit(publisher);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new publisher id.
                return publisher.Publisher_Id;
            }
        }

        /// <summary>
        /// Update publisher.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="address">Address</param>
        /// <param name="city">City.</param>
        /// <param name="state">State.</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="zipCode">Zip code.</param>
        /// <param name="phone">Phone number 1.</param>
        /// <param name="phone2">Phone number 2.</param>
        /// <param name="revenueSharePct">Revenue share percentage.</param>
        /// <param name="minCheckAmount">Minimum check amount.</param>
        /// <param name="isBranded">Is branded.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        public static void Update(int publisherId, int statusId, 
                                  string firstName, string lastName, string companyName, string address, string city, string state, int? countryId, string zipCode, string phone, string phone2, int creditMethodId, int revenueSharePct, int minCheckAmount, bool isBranded,
                                  bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            // Check if publisher exists.
            if (IsExist(publisherId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_PUBLISHER publisher = new TP_PUBLISHER();

                    // Get publisher data.
                    publisher = dataContext.TP_PUBLISHERs.Single(item => item.Publisher_Id == publisherId);

                    // Update status.
                    if (statusId != publisher.Status_Id)
                    {
                        publisher.Status_Id = statusId;
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

                    // Update publisher data.
                    publisher.First_Name = firstName;
                    publisher.Last_Name = lastName;
                    publisher.Company_Name = companyName;
                    publisher.Address = address;
                    publisher.City = city;
                    publisher.State = state;
                    publisher.Country_Id = countryId;
                    publisher.Zip_Code = zipCode;
                    publisher.Phone = phone;
                    publisher.Phone_2 = phone2;
                    publisher.Credit_Method_Id = creditMethodId;
                    publisher.Revenue_Share_Pct = revenueSharePct;
                    publisher.Min_Check_Amount = minCheckAmount;
                    publisher.Is_Branded = (isBranded ? 1 : 0);                    
                    publisher.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                    publisher.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                    publisher.Is_Signed_Up = 1;

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }


        public static void UpdateBankDetails(int publisherId, string bankName, string branchName, string address, string city, string state, string country, string holderName, string accountNumber, string bankCode)
        {
            // Check if publisher exists.
            if (IsExist(publisherId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TP_PUBLISHER_BANKs.Where(i => i.Publisher_Id == publisherId).Count() == 0 ? false : true);

                    TP_PUBLISHER_BANK bank = new TP_PUBLISHER_BANK();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        bank = dataContext.TP_PUBLISHER_BANKs.SingleOrDefault(i => i.Publisher_Id == publisherId);
                    }

                    // Set/update data.
                    bank.Publisher_Id = publisherId;
                    bank.Bank_Name = bankName;
                    bank.Branch_Name = branchName;
                    bank.Address = address;
                    bank.City = city;
                    bank.State = state;
                    bank.Country = country;
                    bank.Holder_Name = holderName;
                    bank.Account_Numer = accountNumber;
                    bank.Code = bankCode;

                    // If not exists, add new row data.
                    if (!isExists)
                    {
                        dataContext.TP_PUBLISHER_BANKs.InsertOnSubmit(bank);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        public static void UpdateCheckDetails(int publisherId, string payeeName, string recipientName, string address, string city, string state, string country, string zipCode)
        {
            // Check if publisher exists.
            if (IsExist(publisherId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TP_PUBLISHER_CHECKs.Where(i => i.Publisher_Id == publisherId).Count() == 0 ? false : true);

                    TP_PUBLISHER_CHECK check = new TP_PUBLISHER_CHECK();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        check = dataContext.TP_PUBLISHER_CHECKs.SingleOrDefault(i => i.Publisher_Id == publisherId);
                    }

                    // Set/update data.
                    check.Publisher_Id = publisherId;
                    check.Payee_Name = payeeName;
                    check.Recipient_Name = recipientName;
                    check.Address = address;
                    check.City = city;
                    check.State = state;
                    check.Country = country;
                    check.Zip_Code = zipCode;

                    // If not exists, add new row data.
                    if (!isExists)
                    {
                        dataContext.TP_PUBLISHER_CHECKs.InsertOnSubmit(check);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        public static void UpdatePayPalDetails(int publisherId, string payeeAccount)
        {
            // Check if publisher exists.
            if (IsExist(publisherId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TP_PUBLISHER_PAYPALs.Where(i => i.Publisher_Id == publisherId).Count() == 0 ? false : true);

                    TP_PUBLISHER_PAYPAL paypal = new TP_PUBLISHER_PAYPAL();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        paypal = dataContext.TP_PUBLISHER_PAYPALs.SingleOrDefault(i => i.Publisher_Id == publisherId);
                    }

                    // Set/update data.
                    paypal.Publisher_Id = publisherId;
                    paypal.Payee_Account = payeeAccount;

                    // If not exists, add new row data.
                    if (!isExists)
                    {
                        dataContext.TP_PUBLISHER_PAYPALs.InsertOnSubmit(paypal);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Check if password match to publishers's password by email.
        /// </summary>
        /// <param name="email">Publisher's email.</param>
        /// <param name="password">Password to check if matched.</param>
        /// <returns>Returns whether password match.</returns>
        public static bool CheckPassword(string email, string password)
        {
            // Check if publisher exists.
            if (IsExist(email) == true)
            {
                // Encrypt password.
                password = General.GenerateMD5(password);

                // Checks if passoword match.
                if (GetPassword(email) == password)
                {
                    using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                    {
                        // TO DO: Write login to DB
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
                // Publisher does not exist.
                return false;
            }
        }

        /// <summary>
        /// Change publisher's password.
        /// </summary>
        /// <param name="email">Publisher's email.</param>
        /// <param name="password">New password.</param>
        /// <param name="encrypted">Is new password encrypted? or needed to be encrypt?</param>
        /// <returns>Returns whether password changed.</returns>
        public static bool ChangePassword(string email, string password, bool encrypted)
        {
            // Check if publisher exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    if (encrypted == false)
                    {
                        // Encrypt password.
                        password = General.GenerateMD5(password);
                    }

                    // Change publisher's password.
                    dataContext.TP_PUBLISHERs.Single(item => item.Email == email).Password = password;

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

        public static int Credit(int publisherId, decimal amountToCredit, int creditMethod, string additionalData)
        {
            // Check if advertiser exists.
            if (IsExist(publisherId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get publisher details.
                    TP_PUBLISHER publisher = GetPublisher(publisherId);

                    // Create payment.
                    TP_PAYMENT payment = new TP_PAYMENT();
                    payment.Publisher_Id = publisherId;
                    payment.First_Name = publisher.First_Name;
                    payment.Last_Name = publisher.Last_Name;
                    payment.Company_Name = publisher.Company_Name;
                    payment.Address = publisher.Address;
                    payment.City = publisher.City;
                    payment.Country_Id = (int)publisher.Country_Id;
                    payment.State = publisher.State;
                    payment.Zip_Code = publisher.Zip_Code;
                    payment.Amount = amountToCredit;
                    payment.Payment_Date = DateTime.Now;
                    payment.Credit_Method_Id = creditMethod;
                    payment.Additional_Data = additionalData;

                    // Add payment details.
                    dataContext.TP_PAYMENTs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();

                    return payment.Payment_Id;
                }
            }

            return 0;
        }

        public static bool CreditCheck(int publisherId, decimal amountToCredit, string additionalData,
                                       string payeeName, string recipientName, string checkNumber, string authNumber, string address, string city, string state, string country, string zipCode, string checkData)
        {
            int creditMethod = (int)CreditMethod.Check;
            int paymentId = Credit(publisherId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TP_PAYMENT_CHECK payment = new TP_PAYMENT_CHECK();
                    payment.Payment_Id = paymentId;
                    payment.Payee_Name = payeeName;
                    payment.Recipient_Name = recipientName;
                    payment.Check_Number = checkNumber;
                    payment.Auth_Number = authNumber;
                    payment.Address = address;
                    payment.City = city;
                    payment.State = state;
                    payment.Country = country;
                    payment.Zip_Code = zipCode;
                    payment.Additional_Data = checkData;

                    // Add payment details.
                    dataContext.TP_PAYMENT_CHECKs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        public static bool CreditPayPal(int publisherId, decimal amountToCredit, string additionalData,
                                       string accountName, string transactionId, string paypalData)
        {
            int creditMethod = (int)CreditMethod.PayPal;
            int paymentId = Credit(publisherId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TP_PAYMENT_PAYPAL payment = new TP_PAYMENT_PAYPAL();
                    payment.Payment_Id = paymentId;
                    payment.Account = accountName;
                    payment.Transaction_Id = transactionId;
                    payment.Additional_Data = paypalData;

                    // Add payment details.
                    dataContext.TP_PAYMENT_PAYPALs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        public static bool CreditBank(int publisherId, decimal amountToCredit, string additionalData,
                                      string authNumber, string accountNumber, string bankName, string branchName, string holderName, string bankCode, string address, string city, string state, string country, string bankData)
        {
            int creditMethod = (int)CreditMethod.BankWire;
            int paymentId = Credit(publisherId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TP_PAYMENT_BANK payment = new TP_PAYMENT_BANK();
                    payment.Payment_Id = paymentId;
                    payment.Bank_Name = bankName;
                    payment.Branch_Name = branchName;
                    payment.Address = address;
                    payment.City = city;
                    payment.State = state;
                    payment.Country = country;
                    payment.Holder_Name = holderName;
                    payment.Account_Number = accountNumber;
                    payment.Code = bankCode;
                    payment.Additional_Data = bankData;
                    payment.Auth_Number = authNumber;

                    // Add payment details.
                    dataContext.TP_PAYMENT_BANKs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        /// <summary>
        /// Calculates total earnings.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return (decimal)(dataContext.T_REPORTS_PUBLISHER_REPORTMAINs.Where(s => s.PublisherId == publisherId).Sum(i => i.Earning as float?).GetValueOrDefault());
                   //return dataContext.TP_EARNINGs.Where(s => s.Publisher_Id == publisherId).Sum(i => i.Earnings);
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
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Total payments.</returns>
        public static decimal GetTotalPayments(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TP_PAYMENTs.Where(p => p.Publisher_Id == publisherId).Sum(i => i.Amount as decimal?).GetValueOrDefault();
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total websites.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Total websites.</returns>
        public static int GetTotalWebsites(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TP_WEBSITEs.Where(w => w.Publisher_Id == publisherId).Count();
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
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="paymentId">Payment id.</param>
        /// <returns>Requested payment.</returns>
        public static TP_PAYMENT GetPayment(int publisherId, int paymentId)
        {
            // Check if publisher exists.
            if (IsExist(publisherId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return payment.
                    return dataContext.TP_PAYMENTs.SingleOrDefault(i => i.Publisher_Id == publisherId && i.Payment_Id == paymentId);
                }
            }
            else
            {
                // Publisher does not exist.
                return null;
            }
        }

        public static TP_PAYMENT_PAYPAL GetPaymentPayPal(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TP_PAYMENT_PAYPALs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        public static TP_PAYMENT_BANK GetPaymentBank(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TP_PAYMENT_BANKs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        public static TP_PAYMENT_CHECK GetPaymentCheck(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TP_PAYMENT_CHECKs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks if publisher exist by id.
        /// </summary>
        /// <param name="publisherId">Publisher's id to look for.</param>
        /// <returns>Returns whether publisher exists or not.</returns>
        private static bool IsExist(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_PUBLISHERs.Where(item => item.Publisher_Id == publisherId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if publisher exist by email.
        /// </summary>
        /// <param name="email">Publisher's email to look for.</param>
        /// <returns>Returns whether publisher exists or not.</returns>
        private static bool IsExist(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_PUBLISHERs.Where(item => item.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
            }
        }

        private static bool IsBankDetails(string bankName, string branchName, string bankAddress, string bankCity, string bankCountry, string bankState, string accountHolder, string accountNumber, string bankCode)
        {
            if (bankName == null || bankName == "" ||
                branchName == null || branchName == "" ||
                bankAddress == null || bankAddress == "" ||
                bankCity == null || bankCity == "" ||
                bankCountry == null || bankCountry == "" ||
                accountHolder == null || accountHolder == "" ||
                accountNumber == null || accountNumber == "" ||
                bankCode == null || bankCode == "")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets publisher info (before activation).
        /// </summary>
        /// <param name="activationCode">Activation code.</param>
        /// <returns>Publishers's info.</returns>
        private static TP_PUBLISHER_ACTIVATION GetPublisherActivation(string activationCode)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHER_ACTIVATIONs.SingleOrDefault(item => item.ActivationCode == activationCode);
            }
        }

        /// <summary>
        /// Delete activation record.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="activationCode">Activation code.</param>
        private static void DeleteActivation(AdsCaptchaDataContext dataContext, string activationCode)
        {
            // Get publisher's info by activation code.
            TP_PUBLISHER_ACTIVATION publisherActivation = new TP_PUBLISHER_ACTIVATION();
            publisherActivation = GetPublisherActivation(activationCode);

            // Check if activation code was not found.
            if (publisherActivation == null)
            {
                // TO DO: Exception?
            }
            else
            {
                // Attach publisher activation object.
                dataContext.TP_PUBLISHER_ACTIVATIONs.Attach(publisherActivation);

                // Delete activation details.
                dataContext.TP_PUBLISHER_ACTIVATIONs.DeleteOnSubmit(publisherActivation);
            }
        }

        /// <summary>
        /// Add new publisher.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="address">Address</param>
        /// <param name="city">City.</param>
        /// <param name="state">State.</param>
        /// <param name="countryId">Country id.</param>
        /// <param name="zipCode">Zip code.</param>
        /// <param name="phone">Phone number 1.</param>
        /// <param name="phone2">Phone number 2.</param>
        /// <param name="minCheckAmount">Minimum check amount.</param>
        /// <param name="isBranded">Is branded.</param>
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <returns>New publisher id.</returns>
        private static TP_PUBLISHER Add(AdsCaptchaDataContext dataContext, 
                                        Nullable<int> developerId, string email, string password, 
                                        string firstName, string lastName, string companyName, string address, string city, string state, int? countryId, string zipCode, string phone, string phone2, int creditMethodId, int minCheckAmount, bool isBranded,
                                        bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            // Create publisher.
            TP_PUBLISHER publisher = new TP_PUBLISHER();
            publisher.Developer_Id = (developerId <= 0 ? null : developerId);
            publisher.Email = email;
            publisher.Password = password;
            publisher.Status_Id = (int)Status.Running;
            publisher.First_Name = firstName;
            publisher.Last_Name = lastName;
            publisher.Company_Name = companyName;
            publisher.Address = address;
            publisher.City = city;
            publisher.State = state;
            publisher.Country_Id = countryId;
            publisher.Zip_Code = zipCode;
            publisher.Phone = phone;
            publisher.Phone_2 = phone2;
            publisher.Credit_Method_Id = creditMethodId;
            publisher.Revenue_Share_Pct = ApplicationConfiguration.DEFAULT_REVENUE_SHARE_PUBLISHER;
            publisher.Min_Check_Amount = minCheckAmount;
            publisher.Is_Branded = (isBranded ? 1 : 0);
            publisher.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
            publisher.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
            publisher.Join_Date = DateTime.Today;
            publisher.Is_Signed_Up = 1;

            // Add publisher.
            dataContext.TP_PUBLISHERs.InsertOnSubmit(publisher);

            // Return publisher.
            return publisher;
        }

        /// <summary>
        /// Change publisher's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>        
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="statusId">New status.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int publisherId, int statusId)
        {
            // Get publisher.
            TP_PUBLISHER publisher = new TP_PUBLISHER();
            publisher = GetPublisher(publisherId);

            // Change status.
            publisher.Status_Id = statusId;
        }

        /// <summary>
        /// Returns publisher's password by email (encrypted from DB).
        /// </summary>
        /// <param name="email">Publisher's email to look for.</param>
        /// <returns>Requested user's encrypted password.</returns>
        private static string GetPassword(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHERs.Single(item => item.Email == email).Password;
            }
        }

        #endregion Private Methods       
    }
}
