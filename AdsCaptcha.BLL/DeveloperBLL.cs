using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.BLL
{    
    public static class DeveloperBLL
    {
        #region Public Methods

        /// <summary>
        /// Get developer by id.
        /// </summary>
        /// <param name="developerId">Developer's id to look for.</param>
        /// <returns>Requested developer.</returns>
        public static TD_DEVELOPER GetDeveloper(int developerId)
        {
            // Check if developer exists.
            if (IsExist(developerId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return developer.
                    return dataContext.TD_DEVELOPERs.SingleOrDefault(i => i.Developer_Id == developerId);
                }
            }
            else
            {
                // Developer does not exist.
                return null;                
            }
        }

        /// <summary>
        /// Get developer by email.
        /// </summary>
        /// <param name="email">Developer's email to look for.</param>
        /// <returns>Requested developer.</returns>
        public static TD_DEVELOPER GetDeveloper(string email)
        {
            // Check if developer exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return developer.
                    return dataContext.TD_DEVELOPERs.Single(i => i.Email.ToLower() == email.ToLower());
                }
            }
            else
            {
                // Developer does not exist.
                return null;
            }
        }

        /// <summary>
        /// Checks if the specific publisher is under a specific developer.
        /// </summary>
        /// <param name="developerId">Developer's id.</param>
        /// <param name="publisherId">Publisher's id.</param>
        /// <returns>Returns whether the specific publisher is under a specific developer.</returns>
        public static bool IsDeveloperPublisher(int developerId, int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_PUBLISHERs.Where(i => i.Publisher_Id == publisherId && i.Developer_Id == developerId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if the specific website is under a specific developer.
        /// </summary>
        /// <param name="developerId">Developer's id.</param>
        /// <param name="websiteId">Website's id.</param>
        /// <returns>Returns whether the specific website is under a specific developer.</returns>
        public static bool IsDeveloperWebsite(int developerId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_WEBSITEs.Where(i => i.Website_Id == websiteId && i.TP_PUBLISHER.Developer_Id == developerId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if the specific captcha is under a specific developer.
        /// </summary>
        /// <param name="developerId">Developer's id.</param>
        /// <param name="captchaId">Captcha's id.</param>
        /// <returns>Returns whether the specific captcha is under a specific developer.</returns>
        public static bool IsDeveloperCaptcha(int developerId, int captchaId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_CAPTCHAs.Where(i => i.Captcha_Id == captchaId && i.TP_PUBLISHER.Developer_Id == developerId).Count() == 0) ? false : true;
            }
        }
        
        /// <summary>
        /// Get developer bank details by id.
        /// </summary>
        /// <param name="developerId">Developer's id to look for.</param>
        /// <returns>Requested developer bank details.</returns>
        public static TD_DEVELOPER_BANK GetDeveloperBankDetails(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPER_BANKs.SingleOrDefault(i => i.Developer_Id == developerId);
            }
        }

        /// <summary>
        /// Get developer check details by id.
        /// </summary>
        /// <param name="developerId">Developer's id to look for.</param>
        /// <returns>Requested developer check details.</returns>
        public static TD_DEVELOPER_CHECK GetDeveloperCheckDetails(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPER_CHECKs.SingleOrDefault(i => i.Developer_Id == developerId);
            }
        }

        /// <summary>
        /// Get developer PayPal details by id.
        /// </summary>
        /// <param name="developerId">Developer's id to look for.</param>
        /// <returns>Requested developer PayPal details.</returns>
        public static TD_DEVELOPER_PAYPAL GetDeveloperPayPalDetails(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPER_PAYPALs.SingleOrDefault(i => i.Developer_Id == developerId);
            }
        }

        /// <summary>
        /// Get all developers list.
        /// </summary>
        /// <returns>Returns all developers list.</returns>
        public static List<TD_DEVELOPER> GetDevelopersList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPERs.ToList<TD_DEVELOPER>();
            }
        }

        /// <summary>
        /// Get developers' publishers list.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <returns>Returns all developers' publishers list.</returns>
        public static List<TP_PUBLISHER> GetPublishersList(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_PUBLISHERs.Where(i => i.Developer_Id == developerId).ToList<TP_PUBLISHER>();
            }
        }

        /// <summary>
        /// Get developers' websites list.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="publisherId">Publisher Id.</param>
        /// <returns>Returns all developers' websites list.</returns>
        public static List<TP_WEBSITE> GetWebsitesList(int developerId, int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITEs.Where(i => i.Publisher_Id == publisherId && i.TP_PUBLISHER.Developer_Id == developerId).ToList<TP_WEBSITE>();
            }
        }

        /// <summary>
        /// Get developer id by email.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <returns>Requested developer id.</returns>
        public static int GetDeveloperIdByEmail(string email)
        {            
            // Check if developer exists.
            if (IsExist(email))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return developer id.
                    return dataContext.TD_DEVELOPERs.Single(i => i.Email.ToLower() == email.ToLower()).Developer_Id;
                }
            }
            else
            {
                // TO DO: Handle developer not exists
                return 0;
            }
        }

        /// <summary>
        /// Checks if developer is waiting for activation.
        /// </summary>
        /// <param name="email">Developer's email to look for.</param>
        /// <returns>Returns whether developer exists in activation list or not.</returns>
        public static bool IsWaitingForActivation(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TD_DEVELOPER_ACTIVATIONs.Where(i => i.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get last payment.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <returns>Last payment.</returns>
        public static TD_PAYMENT GetLastPayment(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_PAYMENTs.Where(i => i.Developer_Id == developerId).OrderByDescending(i => i.Payment_Date).First();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sign up developer (add to activation list).
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
                                    string firstName, string lastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string phone2, int creditMethodId, int minCheckAmount,
                                    bool getEmailAnnouncements, bool getEmailNewsletters,
                                    string bankName, string bankBranchName, string bankAddress, string bankCity, string bankCountry, string bankState, string bankHolderName, string bankAccountNumber, string bankCode, string checkPayeeName, string checkRecipientName, string checkAddress, string checkCity, string checkCountry, string checkState, string checkZipCode, string paypalPayeeAccount)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Generate acivation (guid) code.
                    string activationCode = Guid.NewGuid().ToString();

                    // Create developer.
                    TD_DEVELOPER_ACTIVATION developerActivation = new TD_DEVELOPER_ACTIVATION();
                    developerActivation.ActivationCode = activationCode;
                    developerActivation.Email = email;
                    developerActivation.Password = General.GenerateMD5(password);
                    developerActivation.First_Name = firstName;
                    developerActivation.Last_Name = lastName;
                    developerActivation.Company_Name = companyName;
                    developerActivation.Address = address;
                    developerActivation.City = city;
                    developerActivation.State = state;
                    developerActivation.Country_Id = countryId;
                    developerActivation.Zip_Code = zipCode;
                    developerActivation.Phone = phone;
                    developerActivation.Phone_2 = phone2;
                    developerActivation.Credit_Method_Id = creditMethodId;
                    developerActivation.Min_Check_Amount = minCheckAmount;
                    developerActivation.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                    developerActivation.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                    developerActivation.Sign_Up_Date = DateTime.Today;

                    // Set payment preferences.
                    switch (creditMethodId)
                    {
                        case (int)CreditMethod.BankWire:
                            developerActivation.Bank_Name = bankName;
                            developerActivation.Bank_Branch_Name = bankBranchName;
                            developerActivation.Bank_Address = bankAddress;
                            developerActivation.Bank_City = bankCity;
                            developerActivation.Bank_Country = bankCountry;
                            developerActivation.Bank_State = bankState;
                            developerActivation.Bank_Holder_Name = bankHolderName;
                            developerActivation.Bank_Account_Numer = bankAccountNumber;
                            developerActivation.Bank_Code = bankCode;
                            break;
                        case (int)CreditMethod.Check:
                            developerActivation.Check_Payee_Name = checkPayeeName;
                            developerActivation.Check_Recipient_Name = checkRecipientName;
                            developerActivation.Check_Address = checkAddress;
                            developerActivation.Check_City = checkCity;
                            developerActivation.Check_Country = checkCountry;
                            developerActivation.Check_State = checkState;
                            developerActivation.Check_Zip_Code = checkZipCode;
                            break;
                        case (int)CreditMethod.PayPal:
                            developerActivation.PayPal_Payee_Account = paypalPayeeAccount;
                            break;
                    }

                    // Add developer.
                    dataContext.TD_DEVELOPER_ACTIVATIONs.InsertOnSubmit(developerActivation);

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
                    return dataContext.TD_DEVELOPER_ACTIVATIONs.SingleOrDefault(i => i.Email.ToLower() == email.ToLower()).ActivationCode;
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
                    return dataContext.TD_DEVELOPER_ACTIVATIONs.SingleOrDefault(i => i.ActivationCode == activationCode).Email;
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
            TD_DEVELOPER_ACTIVATION developerActivation = new TD_DEVELOPER_ACTIVATION();
            developerActivation = GetDeveloperActivation(activationCode);

            return (developerActivation == null ? false : true);
        }

        /// <summary>
        /// Activate developer.
        /// </summary>
        /// <param name="activationCode">Activation code.</param>
        /// <returns>New developer id.</returns>
        public static int Activate(string activationCode)
        {
            // Get developer's info by activation code.
            TD_DEVELOPER_ACTIVATION developerActivation = new TD_DEVELOPER_ACTIVATION();
            developerActivation = GetDeveloperActivation(activationCode);

            // Check if activation code was not found.
            if (developerActivation == null)
            {
                return 0;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TD_DEVELOPER developer = new TD_DEVELOPER();

                    // Get credit method.                    
                    int creditMethodId = developerActivation.Credit_Method_Id;

                    // Add developer.
                    developer = Add(dataContext,
                                    developerActivation.Email,
                                    developerActivation.Password,
                                    developerActivation.First_Name,
                                    developerActivation.Last_Name,
                                    developerActivation.Company_Name,
                                    developerActivation.Address,
                                    developerActivation.City,
                                    developerActivation.State,
                                    developerActivation.Country_Id,
                                    developerActivation.Zip_Code,
                                    developerActivation.Phone,
                                    developerActivation.Phone_2,
                                    developerActivation.Credit_Method_Id,
                                    developerActivation.Min_Check_Amount,
                                    developerActivation.Get_Email_Announcements == 1 ? true : false,
                                    developerActivation.Get_Email_Newsletters == 1 ? true : false);

                    // Delete activation details.
                    DeleteActivation(dataContext, activationCode);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Set payment preferences.
                    switch (creditMethodId)
                    {
                        case (int)CreditMethod.BankWire:
                            UpdateBankDetails(developer.Developer_Id, developerActivation.Bank_Name, developerActivation.Bank_Branch_Name, developerActivation.Bank_Address, developerActivation.Bank_City, developerActivation.Bank_State, developerActivation.Bank_Country, developerActivation.Bank_Holder_Name, developerActivation.Bank_Account_Numer, developerActivation.Bank_Code);
                            break;
                        case (int)CreditMethod.Check:
                            UpdateCheckDetails(developer.Developer_Id, developerActivation.Check_Payee_Name, developerActivation.Check_Recipient_Name, developerActivation.Check_Address, developerActivation.Check_City, developerActivation.Check_State, developerActivation.Check_Country, developerActivation.Check_Zip_Code);
                            break;
                        case (int)CreditMethod.PayPal:
                            UpdatePayPalDetails(developer.Developer_Id, developerActivation.PayPal_Payee_Account);
                            break;
                    }

                    // Return new developer id.
                    return developer.Developer_Id;
                }
            }
        }

        /// <summary>
        /// Add new developer (without activation).
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
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <returns>New developer's id.</returns>
        public static int Add(string email, string password, 
                              string firstName, string lastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string phone2, int creditMethodId, int revenueSharePct, int minCheckAmount,
                              bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Generate acivation (guid) code.
                string activationCode = Guid.NewGuid().ToString();

                // Create developer.
                TD_DEVELOPER developer = new TD_DEVELOPER();
                developer.Email = email;
                developer.Password = General.GenerateMD5(password);
                developer.Status_Id = (int)Status.Running;
                developer.First_Name = firstName;
                developer.Last_Name = lastName;
                developer.Company_Name = companyName;
                developer.Address = address;
                developer.City = city;
                developer.State = state;
                developer.Country_Id = countryId;
                developer.Zip_Code = zipCode;
                developer.Phone = phone;
                developer.Phone_2 = phone2;
                developer.Credit_Method_Id = creditMethodId;
                developer.Min_Check_Amount = minCheckAmount;
                developer.Revenue_Share_Pct = revenueSharePct;
                developer.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                developer.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
                developer.Join_Date = DateTime.Today;

                // Add developer.
                dataContext.TD_DEVELOPERs.InsertOnSubmit(developer);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new developer id.
                return developer.Developer_Id;
            }
        }

        /// <summary>
        /// Update developer.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
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
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        public static void Update(int developerId, int statusId, 
                                  string firstName, string lastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string phone2, int creditMethodId, int revenueSharePct, int minCheckAmount,
                                  bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            // Check if developer exists.
            if (IsExist(developerId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TD_DEVELOPER developer = new TD_DEVELOPER();

                    // Get developer data.
                    developer = dataContext.TD_DEVELOPERs.Single(item => item.Developer_Id == developerId);

                    // Update status.
                    if (statusId != developer.Status_Id)
                    {
                        developer.Status_Id = statusId;
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

                    // Update developer data.
                    developer.First_Name = firstName;
                    developer.Last_Name = lastName;
                    developer.Company_Name = companyName;
                    developer.Address = address;
                    developer.City = city;
                    developer.State = state;
                    developer.Country_Id = countryId;
                    developer.Zip_Code = zipCode;
                    developer.Phone = phone;
                    developer.Phone_2 = phone2;
                    developer.Credit_Method_Id = creditMethodId;
                    developer.Revenue_Share_Pct = revenueSharePct;
                    developer.Min_Check_Amount = minCheckAmount;
                    developer.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
                    developer.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Update developer revenue share.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="revenueSharePct">Revenue share percentage.</param>
        public static void Update(int developerId, int revenueSharePct)
        {
            // Check if developer exists.
            if (IsExist(developerId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TD_DEVELOPER developer = new TD_DEVELOPER();

                    // Get developer data.
                    developer = dataContext.TD_DEVELOPERs.Single(item => item.Developer_Id == developerId);

                    // Update developer data.
                    developer.Revenue_Share_Pct = revenueSharePct;

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        public static void UpdateBankDetails(int developerId, string bankName, string branchName, string address, string city, string state, string country, string holderName, string accountNumber, string bankCode)
        {
            // Check if developer exists.
            if (IsExist(developerId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TD_DEVELOPER_BANKs.Where(i => i.Developer_Id == developerId).Count() == 0 ? false : true);

                    TD_DEVELOPER_BANK bank = new TD_DEVELOPER_BANK();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        bank = dataContext.TD_DEVELOPER_BANKs.SingleOrDefault(i => i.Developer_Id == developerId);
                    }

                    // Set/update data.
                    bank.Developer_Id = developerId;
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
                        dataContext.TD_DEVELOPER_BANKs.InsertOnSubmit(bank);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        public static void UpdateCheckDetails(int developerId, string payeeName, string recipientName, string address, string city, string state, string country, string zipCode)
        {
            // Check if developer exists.
            if (IsExist(developerId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TD_DEVELOPER_CHECKs.Where(i => i.Developer_Id == developerId).Count() == 0 ? false : true);

                    TD_DEVELOPER_CHECK check = new TD_DEVELOPER_CHECK();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        check = dataContext.TD_DEVELOPER_CHECKs.SingleOrDefault(i => i.Developer_Id == developerId);
                    }

                    // Set/update data.
                    check.Developer_Id = developerId;
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
                        dataContext.TD_DEVELOPER_CHECKs.InsertOnSubmit(check);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        public static void UpdatePayPalDetails(int developerId, string payeeAccount)
        {
            // Check if developer exists.
            if (IsExist(developerId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    bool isExists = (dataContext.TD_DEVELOPER_PAYPALs.Where(i => i.Developer_Id == developerId).Count() == 0 ? false : true);

                    TD_DEVELOPER_PAYPAL paypal = new TD_DEVELOPER_PAYPAL();

                    // If already exists, get current data and update.
                    if (isExists)
                    {
                        paypal = dataContext.TD_DEVELOPER_PAYPALs.SingleOrDefault(i => i.Developer_Id == developerId);
                    }

                    // Set/update data.
                    paypal.Developer_Id = developerId;
                    paypal.Payee_Account = payeeAccount;

                    // If not exists, add new row data.
                    if (!isExists)
                    {
                        dataContext.TD_DEVELOPER_PAYPALs.InsertOnSubmit(paypal);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Check if password match to developers's password by email.
        /// </summary>
        /// <param name="email">Developer's email.</param>
        /// <param name="password">Password to check if matched.</param>
        /// <returns>Returns whether password match.</returns>
        public static bool CheckPassword(string email, string password)
        {
            // Check if developer exists.
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
                // Developer does not exist.
                return false;
            }
        }

        /// <summary>
        /// Change developer's password.
        /// </summary>
        /// <param name="email">Developer's email.</param>
        /// <param name="password">New password.</param>
        /// <param name="encrypted">Is new password encrypted? or needed to be encrypt?</param>
        /// <returns>Returns whether password changed.</returns>
        public static bool ChangePassword(string email, string password, bool encrypted)
        {
            // Check if developer exists.
            if (IsExist(email) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    if (encrypted == false)
                    {
                        // Encrypt password.
                        password = General.GenerateMD5(password);
                    }

                    // Change developer's password.
                    dataContext.TD_DEVELOPERs.Single(i => i.Email.ToLower() == email.ToLower()).Password = password;

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

        public static int Credit(int developerId, decimal amountToCredit, int creditMethod, string additionalData)
        {
            // Check if developer exists.
            if (IsExist(developerId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get developer details.
                    TD_DEVELOPER developer = GetDeveloper(developerId);

                    // Create payment.
                    TD_PAYMENT payment = new TD_PAYMENT();
                    payment.Developer_Id = developerId;
                    payment.First_Name = developer.First_Name;
                    payment.Last_Name = developer.Last_Name;
                    payment.Company_Name = developer.Company_Name;
                    payment.Address = developer.Address;
                    payment.City = developer.City;
                    payment.Country_Id = (int)developer.Country_Id;
                    payment.State = developer.State;
                    payment.Zip_Code = developer.Zip_Code;
                    payment.Amount = amountToCredit;
                    payment.Payment_Date = DateTime.Now;
                    payment.Credit_Method_Id = creditMethod;
                    payment.Additional_Data = additionalData;

                    // Add payment details.
                    dataContext.TD_PAYMENTs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();

                    return payment.Payment_Id;
                }
            }

            return 0;
        }

        public static bool CreditCheck(int developerId, decimal amountToCredit, string additionalData,
                                       string payeeName, string recipientName, string checkNumber, string authNumber, string address, string city, string state, string country, string zipCode, string checkData)
        {
            int creditMethod = (int)CreditMethod.Check;
            int paymentId = Credit(developerId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TD_PAYMENT_CHECK payment = new TD_PAYMENT_CHECK();
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
                    dataContext.TD_PAYMENT_CHECKs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        public static bool CreditPayPal(int developerId, decimal amountToCredit, string additionalData,
                                       string accountName, string transactionId, string paypalData)
        {
            int creditMethod = (int)CreditMethod.PayPal;
            int paymentId = Credit(developerId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TD_PAYMENT_PAYPAL payment = new TD_PAYMENT_PAYPAL();
                    payment.Payment_Id = paymentId;
                    payment.Account = accountName;
                    payment.Transaction_Id = transactionId;
                    payment.Additional_Data = paypalData;

                    // Add payment details.
                    dataContext.TD_PAYMENT_PAYPALs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        public static bool CreditBank(int developerId, decimal amountToCredit, string additionalData,
                                      string authNumber, string accountNumber, string bankName, string branchName, string holderName, string bankCode, string address, string city, string state, string country, string bankData)
        {
            int creditMethod = (int)CreditMethod.BankWire;
            int paymentId = Credit(developerId, amountToCredit, creditMethod, additionalData);

            if (paymentId == 0)
            {
                return false;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create payment.
                    TD_PAYMENT_BANK payment = new TD_PAYMENT_BANK();
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
                    dataContext.TD_PAYMENT_BANKs.InsertOnSubmit(payment);

                    // Sava changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
        }

        /// <summary>
        /// Calculates total earnings.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_EARNINGs.Where(s => s.Developer_Id == developerId).Sum(i => i.Earnings);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total earnings by publisher.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int developerId, int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_EARNINGs.Where(s => s.Developer_Id == developerId && s.Publisher_Id == publisherId).Sum(i => i.Earnings);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total earnings by website.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int developerId, int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_EARNINGs.Where(s => s.Developer_Id == developerId && s.Publisher_Id == publisherId && s.Website_Id == websiteId).Sum(i => i.Earnings);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total earnings by captcha.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int developerId, int publisherId, int websiteId, int captchaId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_EARNINGs.Where(s => s.Developer_Id == developerId && s.Publisher_Id == publisherId && s.Website_Id == websiteId && s.Captcha_Id == captchaId).Sum(i => i.Earnings);
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
        /// <param name="developerId">Developer id.</param>
        /// <returns>Total payments.</returns>
        public static decimal GetTotalPayments(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TD_PAYMENTs.Where(p => p.Developer_Id == developerId).Sum(i => i.Amount);
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
        /// <param name="developerId">Developer id.</param>
        /// <returns>Total websites.</returns>
        public static int GetTotalPublishers(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TP_PUBLISHERs.Where(i => i.Developer_Id == developerId).Count();
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
        /// <param name="developerId">Developer id.</param>
        /// <param name="paymentId">Payment id.</param>
        /// <returns>Requested payment.</returns>
        public static TD_PAYMENT GetPayment(int developerId, int paymentId)
        {
            // Check if developer exists.
            if (IsExist(developerId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return payment.
                    return dataContext.TD_PAYMENTs.SingleOrDefault(i => i.Developer_Id == developerId && i.Payment_Id == paymentId);
                }
            }
            else
            {
                // Developer does not exist.
                return null;
            }
        }

        public static TD_PAYMENT_PAYPAL GetPaymentPayPal(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TD_PAYMENT_PAYPALs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        public static TD_PAYMENT_BANK GetPaymentBank(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TD_PAYMENT_BANKs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        public static TD_PAYMENT_CHECK GetPaymentCheck(int paymentId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return payment.
                return dataContext.TD_PAYMENT_CHECKs.SingleOrDefault(i => i.Payment_Id == paymentId);
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks if developer exist by id.
        /// </summary>
        /// <param name="developerId">Developer's id to look for.</param>
        /// <returns>Returns whether developer exists or not.</returns>
        private static bool IsExist(int developerId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TD_DEVELOPERs.Where(item => item.Developer_Id == developerId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Checks if developer exist by email.
        /// </summary>
        /// <param name="email">Developer's email to look for.</param>
        /// <returns>Returns whether developer exists or not.</returns>
        private static bool IsExist(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TD_DEVELOPERs.Where(i => i.Email.ToLower() == email.ToLower()).Count() == 0) ? false : true;
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
        /// Gets developer info (before activation).
        /// </summary>
        /// <param name="activationCode">Activation code.</param>
        /// <returns>Developers's info.</returns>
        private static TD_DEVELOPER_ACTIVATION GetDeveloperActivation(string activationCode)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPER_ACTIVATIONs.SingleOrDefault(i => i.ActivationCode == activationCode);
            }
        }

        /// <summary>
        /// Delete activation record.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="activationCode">Activation code.</param>
        private static void DeleteActivation(AdsCaptchaDataContext dataContext, string activationCode)
        {
            // Get developer's info by activation code.
            TD_DEVELOPER_ACTIVATION developerActivation = new TD_DEVELOPER_ACTIVATION();
            developerActivation = GetDeveloperActivation(activationCode);

            // Check if activation code was not found.
            if (developerActivation == null)
            {
                // TO DO: Exception?
            }
            else
            {
                // Attach developer activation object.
                dataContext.TD_DEVELOPER_ACTIVATIONs.Attach(developerActivation);

                // Delete activation details.
                dataContext.TD_DEVELOPER_ACTIVATIONs.DeleteOnSubmit(developerActivation);
            }
        }

        /// <summary>
        /// Add new developer.
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
        /// <param name="getEmailAnnouncements">Get email announcements?</param>
        /// <param name="getEmailNewsletters">Get email newsletters?</param>
        /// <returns>New developer id.</returns>
        private static TD_DEVELOPER Add(AdsCaptchaDataContext dataContext, 
                                        string email, string password, 
                                        string firstName, string lastName, string companyName, string address, string city, string state, int countryId, string zipCode, string phone, string phone2, int creditMethodId, int minCheckAmount,
                                        bool getEmailAnnouncements, bool getEmailNewsletters)
        {
            // Create developer.
            TD_DEVELOPER developer = new TD_DEVELOPER();
            developer.Email = email;
            developer.Password = password;
            developer.Status_Id = (int)Status.Running;
            developer.First_Name = firstName;
            developer.Last_Name = lastName;
            developer.Company_Name = companyName;
            developer.Address = address;
            developer.City = city;
            developer.State = state;
            developer.Country_Id = countryId;
            developer.Zip_Code = zipCode;
            developer.Phone = phone;
            developer.Phone_2 = phone2;
            developer.Credit_Method_Id = creditMethodId;
            developer.Revenue_Share_Pct = ApplicationConfiguration.DEFAULT_REVENUE_SHARE_DEVELOPER;
            developer.Min_Check_Amount = minCheckAmount;
            developer.Get_Email_Announcements = (getEmailAnnouncements ? 1 : 0);
            developer.Get_Email_Newsletters = (getEmailNewsletters ? 1 : 0);
            developer.Join_Date = DateTime.Today;

            // Add developer.
            dataContext.TD_DEVELOPERs.InsertOnSubmit(developer);

            // Return developer.
            return developer;
        }

        /// <summary>
        /// Change developer's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>        
        /// <param name="developerId">Developer id.</param>
        /// <param name="statusId">New status.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int developerId, int statusId)
        {
            // Get developer.
            TD_DEVELOPER developer = new TD_DEVELOPER();
            developer = GetDeveloper(developerId);

            // Change status.
            developer.Status_Id = statusId;
        }

        /// <summary>
        /// Returns developer's password by email (encrypted from DB).
        /// </summary>
        /// <param name="email">Developer's email to look for.</param>
        /// <returns>Requested user's encrypted password.</returns>
        private static string GetPassword(string email)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TD_DEVELOPERs.Single(i => i.Email.ToLower() == email.ToLower()).Password;
            }
        }

        #endregion Private Methods       
    }
}
