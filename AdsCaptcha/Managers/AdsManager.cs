using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Mails;
using Inqwise.AdsCaptcha.SystemFramework;
using NLog;
using Inqwise.AdsCaptcha.Entities;

namespace Inqwise.AdsCaptcha.Managers
{
    public class AdsManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static Lazy<CacheManager<AdsCaptchaOperationResult<IAd>>> _cachedAds =
            new Lazy<CacheManager<AdsCaptchaOperationResult<IAd>>>(() => new CacheManager<AdsCaptchaOperationResult<IAd>>(DEFAULT_AD_CACHE_TIMEOUT));
        private static TimeSpan DEFAULT_AD_CACHE_TIMEOUT = TimeSpan.FromSeconds(10);

        public static AdsCaptchaOperationResult<long> Add(NewAdArgs args)
        {
            AdsCaptchaOperationResult<long> result = null;
            long? adId = null;
            try
            {
                if (null != args.PublisherId)
                {
                    if (null == args.CampaignId)
                    {
                        result = IdentifyPublisherCampaignId(args);
                    }
                    else if (null == args.AdvertiserId)
                    {
                        args.AdvertiserId = PublishersManager.Get(args.PublisherId.Value).Value.AdvertiserId;
                    }
                }

                if (null == result && null == args.AdvertiserId)
                {
                    result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.AdvertiseridNotSet);
                }

                if (null == result)
                {
                    adId = AdsDataAccess.Insert(args);
                    args.AdId = adId;

                    // Add new Image
                    var newImage = args.NewImage;
                    if (null != newImage)
                    {
                        newImage.ImageType = ImageType.Commercial;
                        newImage.AdId = adId;

                        var imageResult = ImagesManager.Add(args.NewImage);
                        if (imageResult.HasValue)
                        {
                            newImage.ImageId = imageResult.Value;
                        }
                    }

                    
                }

                if (null == result)
                {
                    result = adId;
                    try
                    {
                        SendNewAdAdminEmail(args);
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("Add: Failed to send mail", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Add: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.GeneralError,
                                                                 description: ex.ToString());
            }

            return result;
        }

        private static AdsCaptchaOperationResult<long> IdentifyPublisherCampaignId(NewAdArgs args)
        {
            AdsCaptchaOperationResult<long> result = null;
            IPublisher publisher = null;
            IWebsite website = null;

            // Find/Create Campaign for publisher
            var publisherResult = PublishersManager.Get(args.PublisherId.Value);
            if (publisherResult.HasError)
            {
                result = AdsCaptchaOperationResult<long>.ToError(publisherResult);
            }
            else
            {
                publisher = publisherResult.Value;
            }

            // Website
            if (null == result && args.WebsiteId.HasValue)
            {
                var websiteResult = WebsitesManager.Get(args.WebsiteId.Value, args.PublisherId.Value);
                if (websiteResult.HasError)
                {
                    result = AdsCaptchaOperationResult<long>.ToError(websiteResult);
                }
                else
                {
                    website = websiteResult.Value;
                }
            }

            // Publisher - Advertiser
            if (null == result)
            {
                // Check if publisher has linked advertiser
                if (publisher.AdvertiserId.HasValue)
                {
                    args.AdvertiserId = publisher.AdvertiserId;
                }
                else
                {
                    // Create Advertiser for publisher
                    var advertiserResult = publisher.CreateAdvertiser();
                    if (advertiserResult.HasError)
                    {
                        result = AdsCaptchaOperationResult<long>.ToError(advertiserResult);
                    }
                    else
                    {
                        args.AdvertiserId = advertiserResult.Value;
                    }
                }
            }

            // Campaign
            if (null == result)
            {
                if (null == website)
                {
                    var newCampaignName = GetNewCampaignName();
                    // Campaign without website
                    args.CampaignId = WebsitesDataAccess.InsertCampaign(null, args.PublisherId.Value, newCampaignName);
                }
                else
                {
                    // Website's Campaign
                    if (website.CampaignId.HasValue)
                    {
                        args.CampaignId = website.CampaignId;
                    }
                    else
                    {
                        var newCampaignName = GetNewCampaignName();
                        AdsCaptchaOperationResult<int> campaignResult = website.CreateCampaign(newCampaignName);
                        if (campaignResult.HasError)
                        {
                            result = AdsCaptchaOperationResult<long>.ToError(campaignResult);
                        }
                        else
                        {
                            args.CampaignId = campaignResult.Value;
                        }
                    } 
                }
            }
            return result;
        }

        private static string GetNewCampaignName()
        {
            return "ha_" + Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
        }

        private struct NewAdAdminMailArgs : INewAdAdminMailArgs
        {
            public int AdvertiserId { get; set; }
            public string CampaignName { get; set; }
            public string AdName { get; set; }
            public long AdId { get; set; }
            public int CampaignId { get; set; }
            public long? ImageId { get; set; }
        }

        internal static void SendNewAdAdminEmail(NewAdArgs args, string campaignName = null)
        {
            AdsCaptchaOperationResult result = null;

            if (null == result)
            {
                if (null == args.AdId) throw new ArgumentNullException("args.AdId");
                if (null == args.AdvertiserId) throw new ArgumentNullException("args.AdvertiserId");
                if (null == args.CampaignId) throw new ArgumentNullException("args.CampaignId");
            }

            if (null == result && null == campaignName)
            {
                ICampaign campaign = null;
                var campaignResult = CampaignsManager.Get(args.CampaignId.Value, null);
                if (campaignResult.HasError)
                {
                    result = campaignResult;
                }
                else
                {
                    campaign = campaignResult.Value;
                    campaignName = campaign.Name;
                }
            }

            if (null == result)
            {
                if (null == args.NewImage)
                {
                    Log.Warn("SendNewAdAdminEmail: Ad #{0} without image.", args.AdId);
                }

                // Send mail to administrator.
                MailBuilder.GetInstance(new NewAdAdminMailArgs
                    {
                        AdId = args.AdId.Value,
                        AdName = args.AdName,
                        AdvertiserId = args.AdvertiserId.Value,
                        CampaignId = args.CampaignId.Value,
                        CampaignName = campaignName,
                        ImageId = (null == args.NewImage ? default(long?) : args.NewImage.ImageId),
                    }).Build().Send();
            }
        }

        public static AdsCaptchaOperationResult Update(UpdateAdArgs args)
        {
            AdsCaptchaOperationResult result = null;
            try
            {
                AdsDataAccess.Update(args);

                if (null != args.NewImage)
                {
                    args.NewImage.ImageType = ImageType.Commercial;
                    ImagesManager.Add(args.NewImage);
                }
                result = AdsCaptchaOperationResult.Ok;
            }
            catch (Exception ex)
            {
                Log.ErrorException("Update: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult<IAd> Get(long adId, int? advertiserId)
        {
            AdsCaptchaOperationResult<IAd> result = null;
            try
            {
                using (IDataReader reader = AdsDataAccess.GetReader(adId, advertiserId))
                {
                    if (reader.Read())
                    {
                        result = new AdEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get1: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult<IAd> Get(string adExternalId)
        {
            return _cachedAds.Value.GetOrAddCachedItem(adExternalId, ()=> new Tuple<AdsCaptchaOperationResult<IAd>, TimeSpan>(GetInternal(adExternalId), DEFAULT_AD_CACHE_TIMEOUT));
        }

        private static AdsCaptchaOperationResult<IAd> GetInternal(string adExternalId)
        {
            AdsCaptchaOperationResult<IAd> result = null;
            try
            {
                using (IDataReader reader = AdsDataAccess.GetReader(adExternalId))
                {
                    if (reader.Read())
                    {
                        result = new AdEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get2: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult<IAd> GetLast(int campaignId, int? advertiserId)
        {
            AdsCaptchaOperationResult<IAd> result = null;
            try
            {
                using (IDataReader reader = AdsDataAccess.GetLastReader(campaignId, advertiserId))
                {
                    if (reader.Read())
                    {
                        result = new AdEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("GetLast: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IAd>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }

        public static DsAds GetMeny(long campaignId)
        {
            return AdsDataAccess.GetMeny(campaignId);
        }

        public static AdsCaptchaOperationResult Delete(long adId, int? advertiserId)
        {
            AdsCaptchaOperationResult result;
            try
            {
                AdsDataAccess.Delete(adId, advertiserId);
                result = AdsCaptchaOperationResult.Ok;
            }
            catch (Exception ex)
            {
                Log.ErrorException("Delete: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }
    }
}