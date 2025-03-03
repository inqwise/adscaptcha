using System;
using System.Collections.Generic;
using System.Linq;
using AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;

namespace  Inqwise.AdsCaptcha.BLL
{
    public static class AdBLL
    {
        #region Public Methods

        /// <summary>
        /// Check if a ad exists.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adName">Ad Name.</param>
        /// <returns>Returns whether ad exists or not.</returns>
        public static bool IsExist(int advertiserId, int campaignId, string adName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_ADs.Where(item =>
                                                 item.Advertiser_Id == advertiserId &&
                                                 item.Campaign_Id == campaignId &&
                                                 item.Ad_Name.ToLower() == adName.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Check if ad name already exists in the same campaign.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="adName">Ad name.</param>
        /// <returns>Returns whether ad exists or not.</returns>
        public static bool IsDuplicateNameForCampaign(int advertiserId, int campaignId, int adId, string adName)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_ADs.Where(item =>
                                                 item.Advertiser_Id == advertiserId &&
                                                 item.Campaign_Id == campaignId &&
                                                 item.Ad_Id != adId && 
                                                 item.Ad_Name.ToLower() == adName.ToLower()).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get ad.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <returns>Returns a specific ad of a advertiser and campaign.</returns>           
        public static TA_AD GetAd(int advertiserId, int campaignId, int adId)
        {
            // Check if ad exists.
            if (IsExist(advertiserId, campaignId, adId) == true)
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Return ad.
                    return dataContext.TA_ADs.SingleOrDefault(item =>
                                                              item.Advertiser_Id == advertiserId &&
                                                              item.Campaign_Id == campaignId &&
                                                              item.Ad_Id == adId);
                }
            }
            else
            {
                // Ad does not exist.
                return null;
            }
        }

        /// <summary>
        /// Get ads list.
        /// </summary>
        /// <returns>Returns all ads.</returns>
        public static List<TA_AD> GetAdsList()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return ads list.
                return dataContext.TA_ADs.ToList<TA_AD>();
            }
        }

        /// <summary>
        /// Get ads list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <returns>Returns all ads of a specific advertiser.</returns>
        public static List<TA_AD> GetAdsList(int advertiserId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return ads list.
                return dataContext.TA_ADs.Where(item => item.Advertiser_Id == advertiserId).ToList<TA_AD>();
            }
        }

        /// <summary>
        /// Get ads list.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <returns>Returns all ads of a specific advertiser and campaign.</returns>
        public static List<TA_AD> GetAdsList(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return ads list.
                return dataContext.TA_ADs.Where(item =>
                                                item.Advertiser_Id == advertiserId &&
                                                item.Campaign_Id == campaignId).ToList<TA_AD>();
            }
        }

        /// <summary>
        /// Adds new ad.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="adName">Ad name.</param>
        /// <param name="typeId">Type id.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="adSlogan">Ad slogan.</param>
        /// <param name="adImage">Ad image.</param>
        /// <param name="adVideo">Ad video.</param>
        /// <param name="adUrl">Ad url.</param>
        /// <param name="maxCpt">Max CPT.</param>
        /// <returns>New ad id.</returns>
        [Obsolete("Use AdsCaptcha.CampaignsManager", true)]
        public static int Add(int advertiserId, int campaignId, string adName, int typeId, Nullable<int> width, Nullable<int> height, string adSlogan, string adImage, string adVideo, string adUrl, string adLikeUrl, decimal maxCpt, bool rtl)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                TA_CAMPAIGN campaign = CampaignBLL.GetCampaign(advertiserId, campaignId);

                // Create ad.
                TA_AD ad = new TA_AD();
                ad.Advertiser_Id = advertiserId;
                ad.Campaign_Id = campaignId;
                ad.Ad_Name = adName;
                ad.Type_Id = typeId;
                ad.Width = width;
                ad.Height = height;
                ad.Ad_Slogan = (typeId == (int)AdType.Slide2Fit ? null : adSlogan);
                ad.Ad_Image = adImage;
                ad.Ad_Video = adVideo;
                ad.Ad_Url = adUrl;
                ad.Max_Cpt = maxCpt;
                ad.Rtl = rtl;
                ad.Status_Id = campaign.Status_Id;                        // Get status from campaign
                ad.Last_Status_Id = (int)Status.Running;  // Set previous status to Running
                ad.Add_Date = DateTime.Now;
                ad.Modify_Date = DateTime.Now;
                ad.Ad_Like_Url = adLikeUrl;

                // Add ad.
                dataContext.TA_ADs.InsertOnSubmit(ad);

                // Save changes.
                dataContext.SubmitChanges();

                // Return new ad id.
                return ad.Ad_Id;
            }
        }

        /// <summary>
        /// Set ad status to running.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Caotcha id.</param>
        public static void Activate(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int adId)
        {
            // Change ad status to running.
            ChangeStatus(dataContext, advertiserId, campaignId, adId, (int)Status.Running);
        }

        /// <summary>
        /// Set ad status to paused.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Caotcha id.</param>
        public static void Pause(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int adId)
        {
            // Change ad status to paused.
            ChangeStatus(dataContext, advertiserId, campaignId, adId, (int)Status.Paused);
        }

        /// <summary>
        /// Set ad status to pending.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Caotcha id.</param>
        public static void Pending(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int adId)
        {
            // Change ad status to pending.
            ChangeStatus(dataContext, advertiserId, campaignId, adId, (int)Status.Pending);
        }

        /// <summary>
        /// Set ad status to rejected.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Caotcha id.</param>
        public static void Reject(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int adId)
        {
            // Change ad status to rejected.
            ChangeStatus(dataContext, advertiserId, campaignId, adId, (int)Status.Rejected);
        }

        /// <summary>
        /// Update ad.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="adName">Ad name.</param>
        /// <param name="statusId">Status id.</param>
        /// <param name="typeId">Type id.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="adSlogan">Ad slogan.</param>
        /// <param name="adImage">Ad image.</param>
        /// <param name="adVideo">Ad video.</param>
        /// <param name="adUrl">Ad url.</param>
        /// <param name="maxCpt">Max CPT.</param>
        
        public static void Update(int advertiserId, int campaignId, int adId, string adName, int statusId, int typeId, Nullable<int> width, Nullable<int> height, string adSlogan, string adImage, string adVideo, string adUrl, string adLikeUrl, decimal maxCpt, bool rtl)
        {
            // Check if ad exists.
            if (IsExist(advertiserId, campaignId, adId))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    TA_AD ad = new TA_AD();

                    // Get ad data.
                    ad = dataContext.TA_ADs.SingleOrDefault(item =>
                                                            item.Advertiser_Id == advertiserId &&
                                                            item.Campaign_Id == campaignId &&
                                                            item.Ad_Id == adId);

                    // Update ad data.
                    ad.Ad_Name = adName;
                    ad.Status_Id = statusId;
                    ad.Type_Id = typeId;
                    ad.Width = width;
                    ad.Height = height;
                    ad.Ad_Slogan = (typeId == (int)AdType.Slide2Fit ? null : adSlogan);
                    ad.Ad_Image = adImage;
                    ad.Ad_Video = adVideo;
                    ad.Ad_Url = adUrl;
                    ad.Ad_Like_Url = adLikeUrl;
                    ad.Max_Cpt = maxCpt;
                    ad.Rtl = rtl;
                    ad.Modify_Date = DateTime.Now;

                    // Change campaign status.
                    ChangeStatus(advertiserId, campaignId, adId, statusId);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
            else
            {
                // TODO: Handle campaign not exsists
            }
        }

        /// <summary>
        /// Change specific ad's status.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="statusId">New status.</param>
        public static void ChangeStatus(AdsCaptchaDataContext dataContext, int advertiserId, int campaignId, int adId, int statusId)
        {
            // Get ad.
            TA_AD ad = new TA_AD();
            ad = GetAd(advertiserId, campaignId, adId);

            // Check if ad exists.
            if (ad == null)
            {
                // TODO: Handle ad not exsists
            }
            else
            {
                // Check if status changed.
                if (ad.Last_Status_Id != statusId)
                {
                    if (ad.Status_Id != (int)Status.Pending &&
                        ad.Status_Id != (int)Status.Rejected)
                    {
                        // Set previous status.
                        ad.Last_Status_Id = ad.Status_Id;
                    }

                    // Change status.
                    ad.Status_Id = statusId;
                    ad.Modify_Date = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Change specific ad's status.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="statusId">New status.</param>
        public static void ChangeStatus(int advertiserId, int campaignId, int adId, int statusId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Change statsus.
                ChangeStatus(dataContext, advertiserId, campaignId, adId, statusId);

                // Save changes.
                dataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Check if dimensions are valid for selected ad type.
        /// </summary>
        /// <param name="adType">Ad type.</param>
        /// <param name="width">Ad width.</param>
        /// <param name="height">Ad height.</param>
        /// <returns>Wheather dimensions are valid or not.</returns>
        public static bool IsDimensionValid(int adType, int width, int height)
        {
            int isSlider = (adType == (int)AdType.Slide2Fit ? 1 : 0);
            int isImage  = (adType == (int)AdType.SloganAndImage || adType == (int)AdType.SloganAndCoupon ? 1 : 0);
            int isVideo  = (adType == (int)AdType.SloganAndVideo ? 1 : 0);

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.T_AD_DIMENSIONs.Where(d => d.Width == width &&
                                                               d.Height == height &&
                                                               ( (isImage == 1 && d.Is_Image == isImage) || (isSlider == 1 && d.Is_Slider == isSlider) || (isVideo == 1 && d.Is_Video == isVideo) )
                                                         ).Count() == 0) ? false : true;
            }
        }

        // <summary>
        /// Check if dimensions are valid for selected ad type (admin).
        /// </summary>
        /// <param name="adType">Ad type.</param>
        /// <param name="width">Ad width.</param>
        /// <param name="height">Ad height.</param>
        /// <returns>Wheather dimensions are valid or not.</returns>
        public static bool IsDimensionValidForAdmin(int adType, int width, int height)
        {
            int isSlider = (adType == (int)AdType.Slide2Fit ? 1 : 0);
            int isImage = (adType == (int)AdType.SloganAndImage || adType == (int)AdType.SloganAndCoupon ? 1 : 0);
            int isVideo = (adType == (int)AdType.SloganAndVideo ? 1 : 0);

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.T_AD_DIMENSIONs.Where(d => d.Width == width &&
                                                               d.Height == height 
                                                               //&& ((isImage == 1 && d.Is_Image == isImage) || (isSlider == 1 && d.Is_Slider == isSlider) || (isVideo == 1 && d.Is_Video == isVideo))
                                                         ).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Get ad.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <returns>Returns a specific ad of a advertiser and campaign.</returns>           
        public static TA_AD GetLastCampaignAd(int advertiserId, int campaignId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Return ad.
                return (from a in dataContext.TA_ADs
                        where a.Advertiser_Id == advertiserId &&
                                                      a.Campaign_Id == campaignId
                        orderby a.Modify_Date descending
                        select a).FirstOrDefault();
            }
        }


        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Check if a ad exists.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <returns>Returns whether ad exists or not.</returns>
        private static bool IsExist(int advertiserId, int campaignId, int adId)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TA_ADs.Where(item =>
                                                 item.Advertiser_Id == advertiserId &&
                                                 item.Campaign_Id == campaignId &&
                                                 item.Ad_Id == adId).Count() == 0) ? false : true;
            }
        }

        #endregion Private Methods
    }
}
