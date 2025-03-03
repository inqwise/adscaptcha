using System;
using System.Collections.Generic;
using System.Linq;
using AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class CampaignBLL
    {
        #region Public Methods

        /// <summary>
        /// Check if a campaign exists.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignName">Campaign name.</param>
        /// <returns>Returns whether campaign exists or not.</returns>
        public static bool IsExist(int advertiserId, string campaignName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_CAMPAIGNs.Where(item =>
                                                       item.Advertiser_Id == advertiserId &&
                                                       item.Campaign_Name.ToLower() == campaignName.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get campaign.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns requested campaign.</returns>
        public static TA_CAMPAIGN GetCampaign(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                if (IsExist(advertiserId, campaignId))
                {
                    // Return website.
                    return dataContext.TA_CAMPAIGNs.SingleOrDefault(item =>
                                                                    item.Advertiser_Id == advertiserId &&
                                                                    item.Campaign_Id == campaignId);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get campaign list.
        /// </summary>
        /// <returns>Returns all campaigns.</returns>
        public static List<TA_CAMPAIGN> GetCampaignsList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return website list.
                return dataContext.TA_CAMPAIGNs.ToList<TA_CAMPAIGN>();
            }
        }

        /// <summary>
        /// Get campaign list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Returns all campaigns of a specifiv advertiser.</returns>
        public static List<TA_CAMPAIGN> GetCampaignsList(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return website list.
                return dataContext.TA_CAMPAIGNs.Where(item =>
                                                      item.Advertiser_Id == advertiserId).ToList<TA_CAMPAIGN>();
            }
        }

        /// <summary>
        /// Check if campaign name already exists for the specific advertiser.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="campaignName">Campagin name to check.</param>
        /// <returns>Whether campaign name already exists or not.</returns>
        public static bool IsDuplicateNameForAdvertiser(int advertiserId, int campaignId, string campaignName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Count how many times does the same campaign name exists (for the specific afvertiser) not including
                // the checked campaign itself.
                int exists = dataContext.TA_CAMPAIGNs.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id != campaignId && c.Campaign_Name.ToLower() == campaignName.ToLower()).Count();

                return (exists == 0) ? false : true;
            }
        }

        /// <summary>
        /// Add new campaign.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignName">Campaign name.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        /// <param name="listCountryId">Country id's list.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        /// <param name="listKeyword">Keywords list.</param>
        /// <param name="dailyBudget">Daily budget.</param>
        /// <param name="scheduleDates">Scheduling dates - start and end date.</param>
        /// <returns>Returns new campaign's id.</returns>
        [Obsolete("Use AdsCaptcha.CampaignsManager", true)]
        public static int Add(int advertiserId, string campaignName, List<int> listLanguageId, List<int> listCountryId, List<int> listCategoryId, List<string> listKeyword, int dailyBudget, List<DateTime> scheduleDates, bool bonusCampaign, int campaignPaymentType)
        {
            // Check if campaign name exsits.
            if (IsExist(advertiserId, campaignName))
            {
                // TODO: Handle campaign not exsists
                return 0;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create campaign.
                    TA_CAMPAIGN campaign = new TA_CAMPAIGN();
                    campaign.Advertiser_Id = advertiserId;
                    campaign.Campaign_Name = campaignName;
                    campaign.Daily_Budget = dailyBudget;
                    campaign.Status_Id = (int)Status.Running;
                    campaign.Is_Bonus = (bonusCampaign ? 1 : 0);
                    campaign.Add_Date = DateTime.Today;
                    campaign.Modify_Date = DateTime.Today;
                    campaign.CampaignPaymentType = campaignPaymentType;

                    // Set scheduling.
                    if (scheduleDates.Count == 2)
                    {
                        campaign.Schedule_Limit = 1;
                        campaign.Schedule_Start_Date = scheduleDates[0];
                        campaign.Schedule_End_Date = scheduleDates[1];
                    }
                    else
                    {
                        campaign.Schedule_Limit = 0;
                        campaign.Schedule_Start_Date = null;
                        campaign.Schedule_End_Date = null;
                    }

                    // Add campaign.
                    dataContext.TA_CAMPAIGNs.InsertOnSubmit(campaign);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Get new campaign id.
                    int campaignId = campaign.Campaign_Id;

                    try
                    {
                        // Update targeting (language, country, category, keyword).
                        campaign.Target_By_Language = UpdateCampaignLanguages(dataContext, advertiserId, campaignId, listLanguageId);
                        campaign.Target_By_Country = UpdateCampaignCountries(dataContext, advertiserId, campaignId, listCountryId);
                        campaign.Target_By_Category = UpdateCampaignCategories(dataContext, advertiserId, campaignId, listCategoryId);
                        campaign.Target_By_Keyword = UpdateCampaignKeywords(dataContext, advertiserId, campaignId, listKeyword);
                    }
                    catch
                    {
                        // On any error after adding the campaign, delete campaign (rollback).
                        dataContext.TA_CAMPAIGNs.DeleteOnSubmit(campaign);
                    }

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Update campaign targeting.
                    dataContext.UpdateCampaignTargeting(campaignId);

                    return campaignId;
                }
            }
        }

        /// <summary>
        /// Update campaign.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="campaignName">Campaign name.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        /// <param name="listCountryId">Country id's list.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        /// <param name="listKeyword">Keywords list.</param>
        /// <param name="dailyBudget">Daily budget.</param>
        /// <param name="scheduleDates">Scheduling dates - start and end date.</param>
        public static void Update(int advertiserId, int campaignId, string campaignName, int statusId, List<int> listLanguageId, List<int> listCountryId, List<int> listCategoryId, List<string> listKeyword, int dailyBudget, List<DateTime> scheduleDates, bool bonusCampaign, int campaignPaymentType)
        {
            // Check if website exists.
            if (IsExist(advertiserId, campaignId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TA_CAMPAIGN campaign = new TA_CAMPAIGN();

                    // Get campaign data.
                    campaign = dataContext.TA_CAMPAIGNs.SingleOrDefault(item =>
                                                                        item.Advertiser_Id == advertiserId &&
                                                                        item.Campaign_Id == campaignId);

                    // Update campaign data.
                    campaign.Campaign_Name = campaignName;
                    campaign.Daily_Budget = dailyBudget;
                    campaign.Is_Bonus = (bonusCampaign ? 1 : 0);
                    campaign.Modify_Date = DateTime.Today;

                    if (statusId != campaign.Status_Id)
                    {
                        switch (statusId)
                        {
                            case (int)Status.Running:
                                Activate(ref campaign);
                                break;
                            case (int)Status.Paused:
                                Pause(ref campaign);
                                break;
                            case (int)Status.Pending:
                                Pending(ref campaign);
                                break;
                            case (int)Status.Rejected:
                                Reject(ref campaign);
                                break;
                        }
                    }

                    // Set scheduling.
                    if (scheduleDates.Count == 2)
                    {
                        campaign.Schedule_Limit = 1;
                        campaign.Schedule_Start_Date = scheduleDates[0];
                        campaign.Schedule_End_Date = scheduleDates[1];
                    }
                    else
                    {
                        campaign.Schedule_Limit = 0;
                        campaign.Schedule_Start_Date = null;
                        campaign.Schedule_End_Date = null;
                    }

                    campaign.CampaignPaymentType = campaignPaymentType;

                    // Update targeting (language, country, category, keyword).
                    campaign.Target_By_Language = UpdateCampaignLanguages(dataContext, advertiserId, campaignId, listLanguageId);
                    campaign.Target_By_Country = UpdateCampaignCountries(dataContext, advertiserId, campaignId, listCountryId);
                    campaign.Target_By_Category = UpdateCampaignCategories(dataContext, advertiserId, campaignId, listCategoryId);
                    campaign.Target_By_Keyword = UpdateCampaignKeywords(dataContext, advertiserId, campaignId, listKeyword);

                    // Save changes.
                    dataContext.SubmitChanges();

                    // Update campaign targeting.
                    dataContext.UpdateCampaignTargeting(campaignId);
                }
            }
            else
            {
                // TODO: Handle campaign not exsists
            }
        }

        /// <summary>
        /// Calculates total charges.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Total charges.</returns>
        public static decimal GetTotalCharges(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_CHARGEs.Where(s => s.Advertiser_Id == advertiserId && s.Campaign_Id == campaignId).Sum(i => i.Charges);
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Calculates total ads.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Total ads.</returns>
        public static int GetTotalAds(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                try
                {
                    return dataContext.TA_ADs.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId).Count();
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get campaign's countries list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <rereturns>Campaign's countries list.</rereturns>
        public static List<int> GetCampaignCountries(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_CAMPAIGN_COUNTRies.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId && c.Country_Id != 0).Select(c => c.Country_Id).ToList();
            }
        }

        /// <summary>
        /// Get campaign's languages list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <rereturns>Campaign's languages list.</rereturns>
        public static List<int> GetCampaignLanguages(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_CAMPAIGN_LANGUAGEs.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId && c.Language_Id != 0).Select(c => c.Language_Id).ToList();
            }            
        }

        /// <summary>
        /// Get campaign's categories list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <rereturns>Campaign's categories list.</rereturns>
        public static List<int> GetCampaignCategories(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_CAMPAIGN_CATEGORies.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId && c.Category_Id != 0).Select(c => c.Category_Id).ToList();
            }
        }

        /// <summary>
        /// Get campaign's keywords list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <rereturns>Campaign's keywords list.</rereturns>
        public static List<string> GetCampaignKeywords(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return dataContext.TA_CAMPAIGN_KEYWORDs.Where(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId).Select(c => c.Keyword).ToList();
            }
        }

        /// <summary>
        /// Update used budget (increase/decrease only).
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">campaign id.</param>
        /// <param name="budgetDiff">Budget difference.</param>
        /// <returns></returns>
        public static bool UpdateUsedBudget(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, decimal budgetDiff)
        {
            try
            {
                // Get campaign.
                TA_CAMPAIGN campaign = dataContext.TA_CAMPAIGNs.SingleOrDefault(c => c.Advertiser_Id == advertiserId && c.Campaign_Id == campaignId);
                
                // Update used budget.
                campaign.Used_Budget = campaign.Used_Budget + budgetDiff;

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
        /// Check if a campaign exists.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns whether campaign exists or not.</returns>
        private static bool IsExist(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_CAMPAIGNs.Where(item =>
                                                       item.Advertiser_Id == advertiserId &&
                                                       item.Campaign_Id == campaignId).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Activate campaign.
        /// </summary>
        /// <param name="campaign">Campaign to activate.</param>
        private static void Activate(ref TA_CAMPAIGN campaign)
        {
            // Loop each campaign ads (which are not already running).
            foreach (TA_AD ad in campaign.TA_ADs.Where(c => c.Status_Id != (int)Status.Running))
            {
                // Change ad status to last status.
                ad.Status_Id = ad.Last_Status_Id;
                ad.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already running.
            if (campaign.Status_Id != (int)Status.Running)
            {
                // Change campaign's status to running (=active).
                campaign.Status_Id = (int)Status.Running;
                campaign.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Pause campaign.
        /// </summary>
        /// <param name="campaign">Campaign to pause.</param>
        private static void Pause(ref TA_CAMPAIGN campaign)
        {
            // Loop each campaign ads (running ONLY).
            foreach (TA_AD ad in campaign.TA_ADs.Where(c => c.Status_Id == (int)Status.Running))
            {
                // Set previous status.
                ad.Last_Status_Id = ad.Status_Id;

                // Change ad status to paused.
                ad.Status_Id = (int)Status.Paused;
                ad.Modify_Date = DateTime.Today;
            }

            // Change campaign's status to pause ONLY IF current status is running.
            if (campaign.Status_Id == (int)Status.Running)
            {
                // Change campaign's status to pause.
                campaign.Status_Id = (int)Status.Paused;
                campaign.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Reject campaign.
        /// </summary>
        /// <param name="campaign">Campaign to reject.</param>
        private static void Reject(ref TA_CAMPAIGN campaign)
        {
            // Loop each campaign ads (which are not already rejected).
            foreach (TA_AD ad in campaign.TA_ADs.Where(c => c.Status_Id != (int)Status.Rejected))
            {
                if (ad.Status_Id != (int)Status.Pending &&
                    ad.Status_Id != (int)Status.Rejected)
                {
                    // Set previous status.
                    ad.Last_Status_Id = ad.Status_Id;
                }

                // Change ad status to rejected.                    
                ad.Status_Id = (int)Status.Rejected;
                ad.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already rejected.
            if (campaign.Status_Id != (int)Status.Rejected)
            {
                // Change website's status to rejected.
                campaign.Status_Id = (int)Status.Rejected;
                campaign.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Pending campaign.
        /// </summary>
        /// <param name="campaign">Campaign to pending.</param>
        private static void Pending(ref TA_CAMPAIGN campaign)
        {
            // Loop each campaign captchas (which are not already pending).
            foreach (TA_AD ad in campaign.TA_ADs.Where(c => c.Status_Id != (int)Status.Pending))
            {
                if (ad.Status_Id != (int)Status.Pending)
                {
                    // Set previous status.
                    ad.Last_Status_Id = ad.Status_Id;
                }

                // Change ad status to Pending.
                ad.Status_Id = (int)Status.Pending;
                ad.Modify_Date = DateTime.Today;
            }

            // Check if current status isn't already pending.
            if (campaign.Status_Id != (int)Status.Pending)
            {
                // Change campaign's status to pending.
                campaign.Status_Id = (int)Status.Pending;
                campaign.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Change specific campaign's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="statusId">New status.</param>
        private static void ChangeStatus(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int statusId)
        {
            // Get campaign.
            TA_CAMPAIGN campaign = new TA_CAMPAIGN();
            campaign = GetCampaign(advertiserId, campaignId);

            // Check if campaign exists.
            if (campaign == null)
            {
                // TODO: Handle campaign not exsists
            }
            else
            {
                // Change status.
                campaign.Status_Id = statusId;
                campaign.Modify_Date = DateTime.Today;
            }
        }

        /// <summary>
        /// Clear campaign's language targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        private static void ClearCampaignLanguages(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId)
        {            
            // Get campaign's records.
            List<TA_CAMPAIGN_LANGUAGE> list = dataContext.TA_CAMPAIGN_LANGUAGEs.Where(i => i.Advertiser_Id == advertiserId && i.Campaign_Id == campaignId) .ToList();

            // Loop all records and delete them.
            foreach (TA_CAMPAIGN_LANGUAGE currRecord in list)
            {
                dataContext.TA_CAMPAIGN_LANGUAGEs.DeleteOnSubmit(currRecord);
            }            
        }

        /// <summary>
        /// Clear campaign's country targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        private static void ClearCampaignCountries(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId)
        {
            // Get campaign's records.
            List<TA_CAMPAIGN_COUNTRY> list = dataContext.TA_CAMPAIGN_COUNTRies.Where(i => i.Advertiser_Id == advertiserId && i.Campaign_Id == campaignId).ToList();

            // Loop all records and delete them.
            foreach (TA_CAMPAIGN_COUNTRY currRecord in list)
            {
                dataContext.TA_CAMPAIGN_COUNTRies.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Clear campaign's category targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        private static void ClearCampaignCategories(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId)
        {
            // Get campaign's records.
            List<TA_CAMPAIGN_CATEGORY> list = dataContext.TA_CAMPAIGN_CATEGORies.Where(i => i.Advertiser_Id == advertiserId && i.Campaign_Id == campaignId).ToList();

            // Loop all records and delete them.
            foreach (TA_CAMPAIGN_CATEGORY currRecord in list)
            {
                dataContext.TA_CAMPAIGN_CATEGORies.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Clear campaign's keyword targeting.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        private static void ClearCampaignKeywords(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId)
        {
            // Get campaign's records.
            List<TA_CAMPAIGN_KEYWORD> list = dataContext.TA_CAMPAIGN_KEYWORDs.Where(i => i.Advertiser_Id == advertiserId && i.Campaign_Id == campaignId).ToList();

            // Loop all records and delete them.
            foreach (TA_CAMPAIGN_KEYWORD currRecord in list)
            {
                dataContext.TA_CAMPAIGN_KEYWORDs.DeleteOnSubmit(currRecord);
            }
        }

        /// <summary>
        /// Update campaign's language targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="listLanguageId">Language id's list.</param>
        private static int UpdateCampaignLanguages(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, List<int> listLanguageId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearCampaignLanguages(dataContext, advertiserId, campaignId);

                if (listLanguageId.Count == 0)
                {
                    TA_CAMPAIGN_LANGUAGE language = new TA_CAMPAIGN_LANGUAGE();
                    language.Advertiser_Id = advertiserId;
                    language.Campaign_Id = campaignId;
                    language.Language_Id = 0;

                    dataContext.TA_CAMPAIGN_LANGUAGEs.InsertOnSubmit(language);
                }
                else
                {
                    // Loop all languages and add it to the campaign's targeting.
                    foreach (int currLanguageId in listLanguageId.Distinct())
                    {
                        TA_CAMPAIGN_LANGUAGE language = new TA_CAMPAIGN_LANGUAGE();
                        language.Advertiser_Id = advertiserId;
                        language.Campaign_Id = campaignId;
                        language.Language_Id = currLanguageId;

                        dataContext.TA_CAMPAIGN_LANGUAGEs.InsertOnSubmit(language);
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
        /// Update campaign's country targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="listCountryId">Country id's list.</param>
        private static int UpdateCampaignCountries(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, List<int> listCountryId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearCampaignCountries(dataContext, advertiserId, campaignId);

                if (listCountryId.Count == 0)
                {
                    TA_CAMPAIGN_COUNTRY country = new TA_CAMPAIGN_COUNTRY();
                    country.Advertiser_Id = advertiserId;
                    country.Campaign_Id = campaignId;
                    country.Country_Id = 0;

                    dataContext.TA_CAMPAIGN_COUNTRies.InsertOnSubmit(country);
                }
                else
                {
                    // Loop all countries and add it to the campaign's targeting.
                    foreach (int currCountryId in listCountryId.Distinct())
                    {
                        TA_CAMPAIGN_COUNTRY country = new TA_CAMPAIGN_COUNTRY();
                        country.Advertiser_Id = advertiserId;
                        country.Campaign_Id = campaignId;
                        country.Country_Id = currCountryId;

                        dataContext.TA_CAMPAIGN_COUNTRies.InsertOnSubmit(country);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = !listCountryId.Distinct().Any() ? 1 : listCountryId.Distinct().Count();
            }

            return total;
        }

        /// <summary>
        /// Update campaign's category targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="listCategoryId">Category id's list.</param>
        private static int UpdateCampaignCategories(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, List<int> listCategoryId)
        {
            int total = 0;

            try
            {
                // Clear.
                ClearCampaignCategories(dataContext, advertiserId, campaignId);

                if (listCategoryId.Count == 0)
                {
                    TA_CAMPAIGN_CATEGORY category = new TA_CAMPAIGN_CATEGORY();
                    category.Advertiser_Id = advertiserId;
                    category.Campaign_Id = campaignId;
                    category.Category_Id = 0;

                    dataContext.TA_CAMPAIGN_CATEGORies.InsertOnSubmit(category);
                }
                else
                {
                    // Loop all categories and add it to the campaign's targeting.
                    foreach (int currCategoryId in listCategoryId.Distinct())
                    {
                        TA_CAMPAIGN_CATEGORY category = new TA_CAMPAIGN_CATEGORY();
                        category.Advertiser_Id = advertiserId;
                        category.Campaign_Id = campaignId;
                        category.Category_Id = currCategoryId;

                        dataContext.TA_CAMPAIGN_CATEGORies.InsertOnSubmit(category);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                // Return number of elements.
                total = !listCategoryId.Any() ? 1 : listCategoryId.Count();
            }

            return total;
        }

        /// <summary>
        /// Update campaign's keyword targeting list.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="listKeyword">Keywords list.</param>
        private static int UpdateCampaignKeywords(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, List<string> listKeyword)
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
                ClearCampaignKeywords(dataContext, advertiserId, campaignId);

                // Loop all keywords and add it to the campaign's targeting.
                foreach (string currKeyword in listKeyword.Distinct())
                {
                    TA_CAMPAIGN_KEYWORD keyword = new TA_CAMPAIGN_KEYWORD();
                    keyword.Advertiser_Id = advertiserId;
                    keyword.Campaign_Id = campaignId;
                    keyword.Keyword = currKeyword;

                    dataContext.TA_CAMPAIGN_KEYWORDs.InsertOnSubmit(keyword);
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
