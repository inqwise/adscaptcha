using System;
using System.Collections.Generic;
using System.Web;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.Common;

namespace Admin.Handlers
{
    public class ImagesHandler : BaseHandler<JsonObject>
    {
        private const string TO_DATE_ARG_NAME = "toDate";
        private const string FROM_DATE_ARG_NAME = "fromDate";
        private const string IMAGE_STATUS_ID_ARG_NAME = "imageStatusId";

        private const string DELETE_MENY_METHOD_NAME = "deleteList";
        private const string GET_MENY_METHOD_NAME = "getList";
        private const string GET_METHOD_NAME = "get";
        private const string CHANGE_STATUS_METHOD_NAME = "changeStatus";
        public const string IMAGE_ID_KEY = "imageId";
        public const string IMAGE_STATUS_ID_KEY = "imageStatusId";

        protected override JsonObject Process(string methodName, JsonObject args)
        {
            JsonObject output;
            switch (methodName)
            {
                case GET_MENY_METHOD_NAME:
                    output = GetMeny(args);
                    break;
                case DELETE_MENY_METHOD_NAME:
                    output = DeleteMeny(args);
                    break;
                case CHANGE_STATUS_METHOD_NAME:
                    output = ChangeStatus(args);
                    break;
                case GET_METHOD_NAME:
                default:
                    output = GetMethodNotFoundJson(methodName);
                    break;
            }

            return output;
        }

        private JsonObject ChangeStatus(JsonObject args)
        {
            long imageId = args.GetLong(IMAGE_ID_KEY);
            int imageStatusId = args.GetInt(IMAGE_STATUS_ID_KEY);

            ImagesManager.ChangeStatus(imageId, imageStatusId);

            return GetJsonOk();
        }

        private JsonObject DeleteMeny(JsonObject args)
        {
            IEnumerable<long> ids = args.GetMenyLong(LIST_PARAM_NAME);
            foreach (var id in ids)
            {
                ImagesManager.Delete(id, null);
            }

            return GetJsonOk();
        }

        private JsonObject GetMeny(JsonObject args)
        {
            var output = new JsonObject();
            DateTime? fromDate = args.OptDate(FROM_DATE_ARG_NAME);
            DateTime? toDate = args.OptDate(TO_DATE_ARG_NAME);
            ImageStatus imageStatus = (ImageStatus)args.OptInt(IMAGE_STATUS_ID_ARG_NAME, (int)ImageStatus.Pending);
            var images = ImagesManager.GetMeny(fromDate, toDate, new[] { ImageType.Demo, ImageType.Random }, imageStatus, false).Images;

            var adsJa = new JsonArray();
            foreach (var image in images)
            {
                var jo = new JsonObject();
                jo.Put(IMAGE_ID_KEY, image.ImageId);
                if (!image.IsAdvertiserIdNull())
                {
                    jo.Put("adId", image.AdId); 
                }

                if (!image.IsAdvertiserIdNull())
                {
                    jo.Put("advertiserId", image.AdvertiserId); 
                }
                if (!image.IsCampaignIdNull())
                {
                    jo.Put("campaignId", image.CampaignId); 
                }
                jo.Put("imagePath", image.ImagePath);
                jo.Put("imageType", (ImageType)image.ImageTypeId);
                jo.Put("imageTypeId", image.ImageTypeId);
                jo.Put("insertDate", image.InsertDate.ToString("MMM dd, yyyy HH:mm:ss"));
                jo.Put("width", image.Width);
                jo.Put("height", image.Height);
                if (!image.IsExpirationDateNull())
                {
                    jo.Put("expirationDate", image.ExpirationDate.ToString("MMM dd, yyyy HH:mm:ss"));
                }
                jo.Put("status", (ImageStatus)image.StatusId);
                jo.Put("statusId", image.StatusId);

                /*
                jo.Put("adId", ad.Ads_Id);
                jo.Put("adName", ad.AdName);
                jo.Put("campaignId", ad.CampaignId);
                jo.Put("campaignName", ad.CampaignName);
                jo.Put("imageClicks", ad.ClickedOnImage);
                jo.Put("imageCTR", ad.CtrImage);
                jo.Put("imageFitted", ad.FittedImage);
                jo.Put("thumbnailUrl", ResourcesManager.GetResourceUrl(ad.ThumbnailPath, (ResourceFilePathType)ad.ThumbnailPathTypeId));
                jo.Put("imageUrl", ResourcesManager.GetResourceUrl(ad.ImagePath, (ResourceFilePathType)ad.ImagePathTypeId));
                jo.Put("slideStarted", ad.SlideStarted);
                jo.Put("tagUrl", ResourcesManager.GetTagUrl(ad.AUID));
                jo.Put("served", ad.Served);
                jo.Put("percentSlideStartedFromServed", ad.PercentSlideStartedFromServed);
                jo.Put("ctrFromFit", ad.CtrFromFit);
                */
                adsJa.Add(jo);
            }

            output.Put(LIST_PARAM_NAME, adsJa);

            return output;
        }
    }
}