using System.Web;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Handlers
{
    public class WebsitesHandler : BaseHandler<JsonObject>
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private const string CREATE_METHOD_NAME = "create";

        protected override JsonObject Process(string methodName, JsonObject args)
        {
            JsonObject output;
            if (null == PublisherId)
            {
                output = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.NotLoggedIn).ToJson();
            }
            else
            {
                switch (methodName)
                {
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

            var newWebsite = new NewWebsiteArgs(args);
            newWebsite.PublisherId = PublisherId;
            newWebsite.SendAdminEmail = true;

            if (null == result)
            {
                var newWebsiteResult = WebsitesManager.Add(newWebsite);
                if (newWebsiteResult.HasError)
                {
                    result = newWebsiteResult;
                }
                else
                {
                    output = new JsonObject();
                    output.Put("websiteId", newWebsiteResult.Value.WebsiteId);
                    output.Put("captchaId", newWebsiteResult.Value.CaptchaId);
                }
            }

            if (null != result)
            {
                output = GetErrorJson(result);
            }

            return output;
        }
    }
}