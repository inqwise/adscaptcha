using System.Data;
using System.IO;
using System.Linq;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Mails;
using NLog;
using System;
using Inqwise.AdsCaptcha.Entities;

namespace Inqwise.AdsCaptcha.Managers
{
    public static class CampaignsManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static AdsCaptchaOperationResult<INewCampaignResult> Add(NewCampaignArgs args, bool deleteTmpImageOnError = false)
        {
            AdsCaptchaOperationResult<INewCampaignResult> result = null;
            try
            {
                if (null == result)
                {
                    int campaignId;
                    int adId;

                    if (!args.CategoriesList.Any())
                    {
                        args.CategoriesList.Add(0);
                    }

                    if (!args.CountriesList.Any())
                    {
                        args.CountriesList.Add(0);
                    }

                    CampaignsDataAccess.Insert(args, out campaignId, out adId);
                    args.NewAd.CampaignId = campaignId;
                    args.NewAd.AdId = adId;
                }

                if (null == result && null != args.NewAd.NewImage)
                {
                    var newImage = args.NewAd.NewImage;
                    newImage.ImageType = ImageType.Commercial;
                    newImage.AdId = args.NewAd.AdId;

                    newImage.CampaignId = args.NewAd.CampaignId;
                    var imageResult = ImagesManager.Add(newImage);
                    if (imageResult.HasValue)
                    {
                        args.NewAd.NewImage.ImageId = imageResult.Value;
                    }
                }

                if (null == result)
                {
                    result = args;

                    try
                    {
                        // Send mail to administrator.
                        SendNewCampaignAdminMail(args);

                        // Send mail to administrator.
                        AdsManager.SendNewAdAdminEmail(args.NewAd, args.CampaignName);
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("Add : Failed to send email(s)", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Add: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<INewCampaignResult>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());

                // Delete Image
                if (null != args.NewAd.NewImage && deleteTmpImageOnError)
                {
                    try
                    {
                        File.Delete(args.NewAd.NewImage.AbsoluteFilePath);
                    }
                    catch (Exception)
                    {
                        Log.Error("Failed to delete image: " + args.NewAd.NewImage.AbsoluteFilePath, ex);
                    }
                }
            }

            return result;
        }

        private struct NewCampaignAdminMailArgs : INewCampaignAdminMailArgs
        {
            public int AdvertiserId { get; set; }
            public int CampaignId { get; set; }
            public string CampaignName { get; set; }
        }

        private static void SendNewCampaignAdminMail(NewCampaignArgs args)
        {
                if (null == args.AdvertiserId) throw new ArgumentNullException("args.AdvertiserId");
                if (null == args.NewAd.CampaignId) throw new ArgumentNullException("args.NewAd.CampaignId");
            
                // Send mail to administrator.
                MailBuilder.GetInstance(new NewCampaignAdminMailArgs
                {
                    AdvertiserId = args.AdvertiserId.Value,
                    CampaignId = args.NewAd.CampaignId.Value,
                    CampaignName = args.CampaignName,
                }).Build().Send();
            
        }

        public static AdsCaptchaOperationResult<ICampaign> Get(int campaignId, int? advertiserId)
        {
            AdsCaptchaOperationResult<ICampaign> result = null;
            try
            {
                using (IDataReader reader = CampaignsDataAccess.GetReader(campaignId, advertiserId))
                {
                    if (reader.Read())
                    {
                        result = new CampaignEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<ICampaign>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<ICampaign>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }
    }
}
