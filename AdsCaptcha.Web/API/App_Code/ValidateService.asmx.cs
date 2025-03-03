using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Inqwise.AdsCaptcha.Model;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.API
{
    [WebService(Namespace = "http://service.com/",
                Description = "AdsCaptcha: Stop SPAM, Make MONEY!")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ValidateService : System.Web.Services.WebService
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        [WebMethod]
        [Obsolete("Not in use", true)]
        public string Validate(int CaptchaId, string PrivateKey, string ChallengeCode, string UserResponse, string RemoteAddress)
        {
            bool isError = false;
            string errorMessage = "";

            try
            {
                // Get publisher and website by public key.
                int publisherId;
                int websiteId;
                WebsiteBLL.GetWebsiteByPrivateKey(PrivateKey, out publisherId, out websiteId);

                // Check if private key exists and matchs to captcha id.
                if (publisherId == 0 || websiteId == 0)
                {
                    Log.Warn("PrivateKey does not exists: publisherId: '{0}', websiteId: '{1}', CaptchaId: '{2}', privateKey: '{3}'", publisherId, websiteId, CaptchaId, PrivateKey);
                    //General.WriteServiceError(ChallengeCode, "ValidateService", ApplicationConfiguration.ErrorType.Warning, new Exception("PrivateKey does not exists (PrivateKey=" + PrivateKey + ")"));
                    isError = true;
                    errorMessage = "privatekey-not-exists";
                }
                else if (!CaptchaBLL.IsExist(publisherId, websiteId, CaptchaId))
                {
                    Log.Warn("PrivateKey and CaptchaId do not match: publisherId: '{0}', websiteId: '{1}', CaptchaId: {2}, privateKey: '{3}'", publisherId, websiteId, CaptchaId, PrivateKey);
                    //General.WriteServiceError(ChallengeCode, "ValidateService", ApplicationConfiguration.ErrorType.Warning, new Exception("PrivateKey and CaptchaId do not match (CaptchaID=" + CaptchaId + ",PrivateKey=" + PrivateKey + ")"));
                    isError = true;
                    errorMessage = "privatekey-not-match-captchaid";
                }

                if (isError)
                    return errorMessage;

                return CaptchaServerBLL.Answer(ChallengeCode, UserResponse);
            }
            catch (Exception ex)
            {
                errorMessage += ((errorMessage == "" ? "" : ",") + "unexpected-error");
                //General.WriteServiceError(ChallengeCode, "ValidateService", ApplicationConfiguration.ErrorType.Error, ex);
                return errorMessage;
            }
        }
    }
}
