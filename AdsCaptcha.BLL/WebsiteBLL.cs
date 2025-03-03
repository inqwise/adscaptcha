using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class WebsiteBLL
    {
        #region Public Methods

        /// <summary>
        /// Check if a website exists.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="url">Website url.</param>
        /// <returns>Returns whether website exists or not.</returns>
        public static bool IsExist(int publisherId, string url)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_WEBSITEs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Url.ToLower() == url.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Check if website url already exists for the same publisher.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="capthcaName">Captcha name.</param>
        /// <returns>Returns whether url exists or not.</returns>
        public static bool IsDuplicateUrlForPublisher(int publisherId, int websiteId, string url)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_WEBSITEs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Website_Id != websiteId &&
                                                      item.Url.ToLower() == url.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get website.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Returns requested website.</returns>
        public static TP_WEBSITE GetWebsite(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return website.
                return dataContext.TP_WEBSITEs.SingleOrDefault(item =>
                                                               item.Publisher_Id == publisherId &&
                                                               item.Website_Id == websiteId);
            }
        }

        /// <summary>
        /// Get website by private key.
        /// </summary>
        /// <param name="privateKey">Private key.</param>
        /// <param name="publisherId">Requested website publisher id.</param>
        /// <param name="websiteId">Requested website id.</param>
        public static void GetWebsiteByPrivateKey(string privateKey, out int publisherId, out int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Get website by public key.
                TP_WEBSITE website = dataContext.TP_WEBSITEs.SingleOrDefault(item => item.Private_Key == privateKey);

                // Check if website found.
                if (website == null)
                {
                    publisherId = 0;
                    websiteId = 0;
                }
                else
                {
                    publisherId = website.Publisher_Id;
                    websiteId = website.Website_Id;
                }
            }
        }

        /// <summary>
        /// Get website list.
        /// </summary>
        /// <returns>Returns all websites.</returns>
        public static List<TP_WEBSITE> GetWebsitesList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return website list.
                return dataContext.TP_WEBSITEs.ToList<TP_WEBSITE>();
            }
        }

        /// <summary>
        /// Get website list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <returns>Returns all websites of a specifiv publisher.</returns>
        public static List<TP_WEBSITE> GetWebsitesList(int publisherId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return website list.
                return dataContext.TP_WEBSITEs.Where(item =>
                                                     item.Publisher_Id == publisherId).ToList<TP_WEBSITE>();
            }
        }

        /// <summary>
        /// Add new website.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="url">Website url.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        /// <param name="listCountryId">Country id's list.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        /// <param name="listKeyword">Keywords list.</param>
        /// <returns>Returns new website's id.</returns>
        [Obsolete("Use AdsCaptcha.Managers.WebsitesManager.Add", true)]
        public static int Add(int publisherId, string url, List<int> listLanguageId, List<int> listCountryId, List<int> listCategoryId, List<string> listKeyword, bool allowBonus, int bonusLimit)
        {
            // TO DO: Handle publisher not exists?

            // Check if website url exsits.
            if (IsExist(publisherId, url))
            {
                return 0;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create website.
                    TP_WEBSITE website = new TP_WEBSITE();
                    website.Publisher_Id = publisherId;
                    website.Url = url;
                    website.Status_Id = (int)Status.Pending;
                    website.Public_Key = General.GenerateGuid();
                    website.Private_Key = General.GenerateGuid();                    
                    website.Allow_Bonus = (allowBonus ? 1 : 0);
                    website.Bonus_Limit = bonusLimit;
                    website.Add_Date = DateTime.Today;
                    website.Modify_Date = DateTime.Today;

                    // Add website.
                    dataContext.TP_WEBSITEs.InsertOnSubmit(website);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Get new website id.
                    int websiteId = website.Website_Id;

                    try
                    {
                        // Update targeting (language, country, category, keyword).
                        website.Target_By_Language = UpdateWebsiteLanguages(dataContext, publisherId, websiteId, listLanguageId);
                        website.Target_By_Country = UpdateWebsiteCountries(dataContext, publisherId, websiteId, listCountryId);
                        website.Target_By_Category = UpdateWebsiteCategories(dataContext, publisherId, websiteId, listCategoryId);
                        website.Target_By_Keyword = UpdateWebsiteKeywords(dataContext, publisherId, websiteId, listKeyword);
                    }
                    catch
                    {
                        // On any error after adding the website, delete website (rollback).
                        dataContext.TP_WEBSITEs.DeleteOnSubmit(website);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Update website targeting.
                    dataContext.UpdateWebsiteTargeting(websiteId);
                    
                    // TODO: Adds new website flag to administrator for approval.

                    return websiteId;
                }
            }
        }

        /// <summary>
        /// Update website.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="url">Website url.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        /// <param name="listCountryId">Country id's list.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        /// <param name="listKeyword">Keywords list.</param>
        public static void Update(int publisherId, int websiteId, string url, int statusId, List<int> listLanguageId, List<int> listCountryId, List<int> listCategoryId, List<string> listKeyword, bool allowBonus, int bonusLimit)
        {
            // Check if website exists.
            if (IsExist(publisherId, websiteId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_WEBSITE website = new TP_WEBSITE();

                    // Get website data.
                    website = dataContext.TP_WEBSITEs.SingleOrDefault(item =>
                                                                      item.Publisher_Id == publisherId &&
                                                                      item.Website_Id == websiteId);

                    // Update website data.
                    website.Url = url;
                    website.Allow_Bonus = (allowBonus ? 1 : 0);
                    website.Bonus_Limit = bonusLimit;
                    website.Modify_Date = DateTime.Today;
                    
                    if (statusId != website.Status_Id)
                    {
                        switch (statusId)
                        {
                            case (int)Status.Running:
                                Activate(ref website);
                                break;
                            case (int)Status.Paused:
                                Pause(ref website);
                                break;
                            case (int)Status.Pending:
                                Pending(ref website);
                                break;
                            case (int)Status.Rejected:
                                Reject(ref website);
                                break;
                        }
                    }

                    // Update targeting (language, country, category, keyword).
                    website.Target_By_Language = UpdateWebsiteLanguages(dataContext, publisherId, websiteId, listLanguageId);
                    website.Target_By_Country = UpdateWebsiteCountries(dataContext, publisherId, websiteId, listCountryId);
                    website.Target_By_Category = UpdateWebsiteCategories(dataContext, publisherId, websiteId, listCategoryId);
                    website.Target_By_Keyword = UpdateWebsiteKeywords(dataContext, publisherId, websiteId, listKeyword);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Update website targeting.
                    dataContext.UpdateWebsiteTargeting(websiteId);
                }
            }
            else
            {
                // TO DO: Handle website not exists
            }
        }

        /// <summary>
        /// Change website's status.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="statusId">Status id.</param>
        public static void ChangeStatus(int publisherId, int websiteId, int statusId)
        {
            // Check if website exists.
            if (IsExist(publisherId, websiteId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TP_WEBSITE website = new TP_WEBSITE();

                    // Get website data.
                    website = dataContext.TP_WEBSITEs.SingleOrDefault(item =>
                                                                      item.Publisher_Id == publisherId &&
                                                                      item.Website_Id == websiteId);

                    // Update modify date.
                    website.Modify_Date = DateTime.Today;

                    // Update status.
                    if (statusId != website.Status_Id)
                    {
                        switch (statusId)
                        {
                            case (int)Status.Running:
                                Activate(ref website);
                                break;
                            case (int)Status.Paused:
                                Pause(ref website);
                                break;
                            case (int)Status.Pending:
                                Pending(ref website);
                                break;
                            case (int)Status.Rejected:
                                Reject(ref website);
                                break;
                        }
                    }

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
            else
            {
                // TO DO: Handle website not exists
            }
        }

        /// <summary>
        /// Get website's countries list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <rereturns>Website's countries list.</rereturns>
        public static List<int> GetWebsiteCountries(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITE_COUNTRies.Where(c => c.Publisher_Id == publisherId && c.Website_Id == websiteId && c.Country_Id != 0).Select(c => c.Country_Id).ToList();
            }
        }

        /// <summary>
        /// Get website's languages list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <rereturns>Website's languages list.</rereturns>
        public static List<int> GetWebsiteLanguages(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITE_LANGUAGEs.Where(c => c.Publisher_Id == publisherId && c.Website_Id == websiteId && c.Language_Id != 0).Select(c => c.Language_Id).ToList();
            }
        }

        /// <summary>
        /// Get website's categories list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <rereturns>Website's categories list.</rereturns>
        public static List<int> GetWebsiteCategories(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITE_CATEGORies.Where(c => c.Publisher_Id == publisherId && c.Website_Id == websiteId && c.Category_Id != 0).Select(c => c.Category_Id).ToList();
            }
        }

        /// <summary>
        /// Get website's keywords list.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <rereturns>Website's keywords list.</rereturns>
        public static List<string> GetWebsiteKeywords(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TP_WEBSITE_KEYWORDs.Where(c => c.Publisher_Id == publisherId && c.Website_Id == websiteId).Select(c => c.Keyword).ToList();
            }
        }

        /// <summary>
        /// Calculates total earnings.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Total earnings.</returns>
        public static decimal GetTotalEarnings(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return (decimal)dataContext.T_REPORTS_PUBLISHER_REPORTMAINs.Where(s => s.PublisherId == publisherId && s.WebsiteId == websiteId).Sum(i => i.Earning);
                    //return dataContext.TP_EARNINGs.Where(s => s.Publisher_Id == publisherId && s.Website_Id == websiteId).Sum(i => i.Earnings);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total captchas.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Total captchas.</returns>
        public static int GetTotalCaptchas(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TP_CAPTCHAs.Where(c => c.Publisher_Id == publisherId && c.Website_Id == websiteId).Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Check if a website exists.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <returns>Returns whether website exists or not.</returns>
        private static bool IsExist(int publisherId, int websiteId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TP_WEBSITEs.Where(item =>
                                                      item.Publisher_Id == publisherId &&
                                                      item.Website_Id == websiteId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Activate website.
        /// </summary>
        /// <param name="website">Website to activate.</param>
        private static void Activate(ref TP_WEBSITE website)
        {
            // Loop each website captchas (which are not already running).
            foreach (TP_CAPTCHA captcha in website.TP_CAPTCHAs.Where(c => c.Status_Id != (int)Status.Running))
            {
                // Change captcha status to last status.
                captcha.Status_Id = captcha.Last_Status_Id;
                captcha.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already running.
            if (website.Status_Id != (int)Status.Running)
            {
                // Change website's status to running (=active).
                website.Status_Id = (int)Status.Running;
                website.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Pause website.
        /// </summary>
        /// <param name="website">Website to pause.</param>
        private static void Pause(ref TP_WEBSITE website)
        {
            /*
            // Loop each website captchas (paused ONLY).
            foreach (TP_CAPTCHA captcha in website.TP_CAPTCHAs.Where(c => c.Status_Id == (int)Status.Paused))
            {
                // Set previous status.
                captcha.Last_Status_Id = captcha.Status_Id;
            }
            */

            // Loop each website captchas (running ONLY).
            foreach (TP_CAPTCHA captcha in website.TP_CAPTCHAs.Where(c => c.Status_Id == (int)Status.Running))
            {
                // Set previous status.
                captcha.Last_Status_Id = captcha.Status_Id;

                // Change captcha status to paused.
                captcha.Status_Id = (int)Status.Paused;
                captcha.Modify_Date = DateTime.Today;
            }

            // Change website's status to pause ONLY IF current status is running.
            if (website.Status_Id == (int)Status.Running)
            {
                // Change website's status to pause.
                website.Status_Id = (int)Status.Paused;
                website.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Reject website.
        /// </summary>
        /// <param name="website">Website to reject.</param>
        private static void Reject(ref TP_WEBSITE website)
        {
            // Loop each website captchas (which are not already rejected).
            foreach (TP_CAPTCHA captcha in website.TP_CAPTCHAs.Where(c => c.Status_Id != (int)Status.Rejected))
            {
                if (captcha.Status_Id != (int)Status.Pending &&
                    captcha.Status_Id != (int)Status.Rejected)
                {
                    // Set previous status.
                    captcha.Last_Status_Id = captcha.Status_Id;
                }

                // Change captcha status to rejected.                    
                captcha.Status_Id = (int)Status.Rejected;
                captcha.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already rejected.
            if (website.Status_Id != (int)Status.Rejected)
            {
                // Change website's status to rejected.
                website.Status_Id = (int)Status.Rejected;
                website.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Pending website.
        /// </summary>
        /// <param name="website">Website to pending.</param>
        private static void Pending(ref TP_WEBSITE website)
        {
            // Loop each website captchas (which are not already pending).
            foreach (TP_CAPTCHA captcha in website.TP_CAPTCHAs.Where(c => c.Status_Id != (int)Status.Pending))
            {
                if (captcha.Status_Id != (int)Status.Pending)
                {
                    // Set previous status.
                    captcha.Last_Status_Id = captcha.Status_Id;
                }

                // Change captcha status to Pending.
                captcha.Status_Id = (int)Status.Pending;
                captcha.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already pending.
            if (website.Status_Id != (int)Status.Pending)
            {
                // Change website's status to pending.
                website.Status_Id = (int)Status.Pending;
                website.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Change specific website's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="statusId">New status.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, int statusId)
        {
            // Get website.
            TP_WEBSITE website = new TP_WEBSITE();
            website = GetWebsite(publisherId, websiteId);

            // Check if website exists.
            if (website == null)
            {
                // TO DO: Handle website not exists
            }
            else
            {
                // Change status.
                website.Status_Id = statusId;
                website.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Clear website's language targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        private static void ClearWebsiteLanguages(AdsCaptchaDataContext dataContext, int publisherId, int websiteId)
        {
            // Get website's records.
            List<TP_WEBSITE_LANGUAGE> list = dataContext.TP_WEBSITE_LANGUAGEs.Where(i => i.Publisher_Id == publisherId && i.Website_Id == websiteId).ToList();

            // Loop all records and delete them.
            foreach (TP_WEBSITE_LANGUAGE currRecord in list)
            {
                dataContext.TP_WEBSITE_LANGUAGEs.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Clear website's country targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        private static void ClearWebsiteCountries(AdsCaptchaDataContext dataContext, int publisherId, int websiteId)
        {
            // Get website's records.
            List<TP_WEBSITE_COUNTRY> list = dataContext.TP_WEBSITE_COUNTRies.Where(i => i.Publisher_Id == publisherId && i.Website_Id == websiteId).ToList();

            // Loop all records and delete them.
            foreach (TP_WEBSITE_COUNTRY currRecord in list)
            {
                dataContext.TP_WEBSITE_COUNTRies.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Clear website's category targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        private static void ClearWebsiteCategories(AdsCaptchaDataContext dataContext, int publisherId, int websiteId)
        {
            // Get website's records.
            List<TP_WEBSITE_CATEGORY> list = dataContext.TP_WEBSITE_CATEGORies.Where(i => i.Publisher_Id == publisherId && i.Website_Id == websiteId).ToList();

            // Loop all records and delete them.
            foreach (TP_WEBSITE_CATEGORY currRecord in list)
            {
                dataContext.TP_WEBSITE_CATEGORies.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Clear website's keyword targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        private static void ClearWebsiteKeywords(AdsCaptchaDataContext dataContext, int publisherId, int websiteId)
        {
            // Get website's records.
            List<TP_WEBSITE_KEYWORD> list = dataContext.TP_WEBSITE_KEYWORDs.Where(i => i.Publisher_Id == publisherId && i.Website_Id == websiteId).ToList();

            // Loop all records and delete them.
            foreach (TP_WEBSITE_KEYWORD currRecord in list)
            {
                dataContext.TP_WEBSITE_KEYWORDs.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Update website's language targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        private static int UpdateWebsiteLanguages(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, List<int> listLanguageId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearWebsiteLanguages(dataContext, publisherId, websiteId);

                if (listLanguageId.Count == 0)
                {
                    TP_WEBSITE_LANGUAGE language = new TP_WEBSITE_LANGUAGE();
                    language.Publisher_Id = publisherId;
                    language.Website_Id = websiteId;
                    language.Language_Id = 0;

                    dataContext.TP_WEBSITE_LANGUAGEs.InsertOnSubmit(language);
                }
                else
                {
                    // Loop all languages and add it to the website's targeting.
                    foreach (int currLanguageId in listLanguageId.Distinct())
                    {
                        TP_WEBSITE_LANGUAGE language = new TP_WEBSITE_LANGUAGE();
                        language.Publisher_Id = publisherId;
                        language.Website_Id = websiteId;
                        language.Language_Id = currLanguageId;

                        dataContext.TP_WEBSITE_LANGUAGEs.InsertOnSubmit(language);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = listLanguageId.Distinct().Count();
            }

            return total;
        }

        /// <summary>
        /// Update website's country targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="listCountryId">Country id's list.</param>
        private static int UpdateWebsiteCountries(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, List<int> listCountryId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearWebsiteCountries(dataContext, publisherId, websiteId);

                if (listCountryId.Count == 0)
                {
                    TP_WEBSITE_COUNTRY country = new TP_WEBSITE_COUNTRY();
                    country.Publisher_Id = publisherId;
                    country.Website_Id = websiteId;
                    country.Country_Id = 0;

                    dataContext.TP_WEBSITE_COUNTRies.InsertOnSubmit(country);
                }
                else
                {
                    // Loop all countries and add it to the website's targeting.
                    foreach (int currCountryId in listCountryId.Distinct())
                    {
                        TP_WEBSITE_COUNTRY country = new TP_WEBSITE_COUNTRY();
                        country.Publisher_Id = publisherId;
                        country.Website_Id = websiteId;
                        country.Country_Id = currCountryId;

                        dataContext.TP_WEBSITE_COUNTRies.InsertOnSubmit(country);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = listCountryId.Distinct().Count();
            }

            return total;
        }

        /// <summary>
        /// Update website's category targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        private static int UpdateWebsiteCategories(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, List<int> listCategoryId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearWebsiteCategories(dataContext, publisherId, websiteId);

                if (listCategoryId.Count == 0)
                {
                    TP_WEBSITE_CATEGORY category = new TP_WEBSITE_CATEGORY();
                    category.Publisher_Id = publisherId;
                    category.Website_Id = websiteId;
                    category.Category_Id = 0;

                    dataContext.TP_WEBSITE_CATEGORies.InsertOnSubmit(category);
                }
                else
                {
                    // Loop all categories and add it to the website's targeting.
                    foreach (int currCategoryId in listCategoryId.Distinct())
                    {
                        TP_WEBSITE_CATEGORY category = new TP_WEBSITE_CATEGORY();
                        category.Publisher_Id = publisherId;
                        category.Website_Id = websiteId;
                        category.Category_Id = currCategoryId;

                        dataContext.TP_WEBSITE_CATEGORies.InsertOnSubmit(category);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = !listCategoryId.Any() ? 1 : listCategoryId.Distinct().Count();
            }

            return total;
        }

        /// <summary>
        /// Update website's keyword targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="listKeyword">Keywords list.</param>
        private static int UpdateWebsiteKeywords(AdsCaptchaDataContext dataContext, int publisherId, int websiteId, List<string> listKeyword)
        {
            int total = 0;

            try
            {
                // Trim and lower case all keywords.
                for (int i = 0; i < listKeyword.Count(); i++)
                {
                    listKeyword[i] = listKeyword[i].Trim().ToLower();
                }

                // Clear.
                ClearWebsiteKeywords(dataContext, publisherId, websiteId);

                // Loop all keywords and add it to the website's targeting.
                foreach (string currKeyword in listKeyword.Distinct())
                {
                    TP_WEBSITE_KEYWORD keyword = new TP_WEBSITE_KEYWORD();
                    keyword.Publisher_Id = publisherId;
                    keyword.Website_Id = websiteId;
                    keyword.Keyword = currKeyword;

                    dataContext.TP_WEBSITE_KEYWORDs.InsertOnSubmit(keyword);
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = listKeyword.Distinct().Count();
            }

            return total;
        }

        #endregion Private Methods
    }
}
