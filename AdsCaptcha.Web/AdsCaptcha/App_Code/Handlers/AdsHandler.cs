using System.Web;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Handlers
{
    public class AdsHandler : BaseHandler<JsonObject>
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private const string DELETE_METHOD_NAME = "remove";
        private const string UPDATE_METHOD_NAME = "update";
        private const string CREATE_METHOD_NAME = "create";
        private const string GET_METHOD_NAME = "get";
        private const string GET_MENY_METHOD_NAME = "getList";

        protected override JsonObject Process(string methodName, JsonObject args)
        {
            JsonObject output;

            switch (methodName)
            {
                case GET_METHOD_NAME:
                    if (null == AdvertiserId)
                    {
                        output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
                    }
                    else
                    {
                        output = Get(args);
                    }
                    break;
                case GET_MENY_METHOD_NAME:
                    if (null == AdvertiserId && null == PublisherId)
                    {
                        output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
                    }
                    else
                    {
                        output = GetMeny(args);
                    }
                    break;
                case CREATE_METHOD_NAME:
                    if (null == AdvertiserId && null == PublisherId)
                    {
                        output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
                    }
                    else
                    {
                        output = Create(args);
                    }
                    break;
                case UPDATE_METHOD_NAME:
                    if (null == AdvertiserId)
                    {
                        output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
                    }
                    else
                    {
                        output = Update(args);
                    }
                    break;
                case DELETE_METHOD_NAME:
                    if (null == AdvertiserId && null == PublisherId)
                    {
                        output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
                    }
                    else
                    {
                        output = Delete(args);
                    }
                    break;
                default:
                    output = GetMethodNotFoundJson(methodName);
                    break;
            }
            
            return output;
        }

        private JsonObject Delete(JsonObject args)
        {
            long adId = args.GetLong("adId");
            int currentAdvertiserId;
            if (null == AdvertiserId)
            {
                currentAdvertiserId = PublishersManager.Get(PublisherId.Value).Value.AdvertiserId.Value;
            }
            else
            {
                currentAdvertiserId = AdvertiserId.Value;
            }

            var result = AdsManager.Delete(adId, currentAdvertiserId);
            return result.ToJson();
        }

        private JsonObject GetMeny(JsonObject args)
        {
            JsonObject output = null;
            long campaignId = args.GetLong("campaignId");
            var ds = AdsManager.GetMeny(campaignId);

            var ja = new JsonArray();
            foreach (var row in ds.Ads)
            {
                var jo = new JsonObject();
                jo.Put("adId", row.AdId);
                jo.Put("imageId", row.ImageId);
                jo.Put("clickUrl", row.ClickUrl);
                jo.Put("likeUrl", row.LikeUrl);
                jo.Put("width", row.Width);
                jo.Put("height", row.Height);

                ja.Add(jo);
            }

            output = new JsonObject();
            output.Put("list", ja);

            return output;
        }

        private JsonObject Update(JsonObject args)
        {
            JsonObject output = null;
            AdsCaptchaOperationResult result = null;

            var editAd = new UpdateAdArgs(args);
            editAd.AdvertiserId = AdvertiserId;
            NewImageArgs tmpImage = null;

            if (null != editAd.TmpImageId)
            {    
                tmpImage = ImagesManager.GetTmp(HttpUtility.UrlDecode(editAd.TmpImageId));
                if (null == tmpImage)
                {
                    result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.ImageNotFound);
                }
            }

            if (null == result)
            {
                if (null != tmpImage)
                {
                    tmpImage.ImageType = ImageType.Commercial;
                }

                editAd.NewImage = tmpImage;

                var editAdResult = AdsManager.Update(editAd);
                if (editAdResult.HasError)
                {
                    result = editAdResult;
                }
                else
                {
                    output = AdsCaptchaOperationResult.JsonOk;
                }
            }

            if (null == result)
            {
                if (null != tmpImage)
                {
                    tmpImage.Touch();
                }
            }
            else
            {
                output = GetErrorJson(result);
            }

            return output;
        }

        private JsonObject Create(JsonObject args)
        {
            JsonObject output = null;
            AdsCaptchaOperationResult result = null;

            var newAd = new NewAdArgs(args);

            if (AdvertiserId.HasValue)
            {
                newAd.AdvertiserId = AdvertiserId;
            }
            else
            {
                newAd.PublisherId = PublisherId;
            }
            

            var tmpImage = ImagesManager.GetTmp(HttpUtility.UrlDecode(newAd.TmpImageId));
            if (null == tmpImage)
            {
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.ImageNotFound);
            }

            if (null == result)
            {
                if (null != tmpImage)
                {
                    tmpImage.ImageType = ImageType.Commercial;
                }

                newAd.NewImage = tmpImage;

                var newAdResult = AdsManager.Add(newAd);
                if (newAdResult.HasError)
                {
                    result = newAdResult;
                }
                else
                {
                    output = new JsonObject();
                    output.Put("adId", newAdResult.Value);
                    output.Put("campaignId", newAd.CampaignId);
                    output.Put("clickUrl", newAd.ClickUrl);
                    output.Put("imageId", newAd.NewImage.ImageId);
                    output.Put("width", newAd.NewImage.Width);
                    output.Put("height", newAd.NewImage.Height);
                }
            }

            if (null == result)
            {
                if (null != tmpImage)
                {
                    tmpImage.Touch();
                }
            }
            else
            {
                output = GetErrorJson(result);
            }

            return output;
        }

        private JsonObject Get(JsonObject args)
        {
            throw new System.NotImplementedException();
        }
    }
}