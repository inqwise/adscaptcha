using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Handlers
{
    public class CampaignsHandler : BaseHandler<JsonObject>
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private const string CREATE_METHOD_NAME = "create";
        private const string GET_METHOD_NAME = "get";

        protected override JsonObject Process(string methodName, JsonObject args)
        {
            JsonObject output;
            
            {
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
                    case CREATE_METHOD_NAME:
                        output = Create(args);
                        break;
                    default:
                        output = GetMethodNotFoundJson(methodName);
                        break;
                }
            }

            return output;    
        }

        private JsonObject Create(JsonObject args)
        {
            JsonObject output = null;
            AdsCaptchaOperationResult result = null;

            bool temporary = args.GetBool("temporary");

            var newCampaign = new NewCampaignArgs(args);
            var tmpImage = ImagesManager.GetTmp(HttpContext.Current.Server.UrlDecode(newCampaign.NewAd.TmpImageId));
            if (null == tmpImage)
            {
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.ImageNotFound);
            }
            else
            {
                tmpImage.ImageType = ImageType.Commercial;
                newCampaign.NewAd.NewImage = tmpImage;
            }

            if (null == result)
            {   
                newCampaign.AdvertiserId = AdvertiserId;    

                if (temporary)
                {
                    output = newCampaign.ToJson();
                }
                else
                {
                    var newCampaignResult = CampaignsManager.Add(newCampaign);
                    if (newCampaignResult.HasError)
                    {
                        result = newCampaignResult;
                    }
                    else
                    {
                        output = new JsonObject();
                        output.Put("campaignId", newCampaignResult.Value.CampaignId);
                    }
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