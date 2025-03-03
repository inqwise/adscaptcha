using System.Web;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Handlers
{
    public class CaptchasHandler : BaseHandler<JsonObject>
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private const string CREATE_METHOD_NAME = "create";
        private const string UPDATE_METHOD_NAME = "update";

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
                    case UPDATE_METHOD_NAME:
                        output = Update(args);
                        break;
                    default:
                        output = GetMethodNotFoundJson(methodName);
                        break;
                }
            }

            return output;
        }

        private JsonObject Update(JsonObject args)
        {
            JsonObject output = null;
            AdsCaptchaOperationResult result = null;

            var editCaptcha = new UpdateCaptchaArgs(args);
            editCaptcha.PublisherId = PublisherId;

            if (null == result)
            {
                var editCaptchaResult = CaptchasManager.Update(editCaptcha);
                if (editCaptchaResult.HasError)
                {
                    result = editCaptchaResult;
                }
            }

            if (null == result)
            {
                output = AdsCaptchaOperationResult.JsonOk;
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

            var newCaptchaArgs = new NewCaptchaArgs(args);
            newCaptchaArgs.PublisherId = PublisherId;
            newCaptchaArgs.SendAdminEmail = true;

            if (null == result)
            {
                var newCaptchaResult = CaptchasManager.Add(newCaptchaArgs);
                if (newCaptchaResult.HasError)
                {
                    result = newCaptchaResult;
                }
                else
                {
                    output = new JsonObject();
                    output.Put("captchaId", newCaptchaResult.Value);
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