using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using API.Handlers;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.Workflows;


namespace Inqwise.AdsCaptcha.API.Slider
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Validate_ashx : BaseHandler<string>
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        protected override string Process(HttpContext context)
        {
            HttpRequest request = context.Request;
            long? captchaId = null;
            string privateKey = null;
            string challengeCode = null;
            string remoteAddress;
            string userResponse;
            ImageType? imageType = null;
            
            var errors = new AdsCaptchaOperationResults();


            var strImageTypeId = request["ImageTypeId"];
            int imageTypeId;
            if (!string.IsNullOrEmpty(strImageTypeId) && int.TryParse(strImageTypeId, out imageTypeId))
            {
                imageType = (ImageType) imageTypeId;
            }

            if (imageType.HasValue && imageType.Value != ImageType.Demo)
            {
                errors.Add(AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.InvalidArgument, description:"imageType"));
            }

            if (null == imageType)
            {
                string rawCaptchaId;
                if (string.IsNullOrEmpty(rawCaptchaId = request["CaptchaId"]))
                {
                    errors.Add(AdsCaptchaErrors.CaptchaidNotSet);
                }
                else
                {
                    long tmpCaptchaId;
                    if (!long.TryParse(HttpUtility.HtmlEncode(rawCaptchaId), out tmpCaptchaId))
                    {
                        errors.Add(AdsCaptchaErrors.CaptchaidInvalid);
                    }
                    else
                    {
                        captchaId = tmpCaptchaId;
                    }
                }

                // Check PrivateKey.
                if (string.IsNullOrEmpty(privateKey = context.Request["PrivateKey"]))
                {
                    errors.Add(AdsCaptchaErrors.PrivatekeyNotSet);
                }
                else if (errors.IsEmpty)
                {
                    privateKey = HttpUtility.HtmlEncode(privateKey).Trim();
                }
            }

            // Check ChallengeCode.
            if (string.IsNullOrEmpty(challengeCode = context.Request["ChallengeCode"]))
            {
                errors.Add(AdsCaptchaErrors.ChallengeNotSet);
            }
            else if (errors.IsEmpty)
            {
                challengeCode = HttpUtility.HtmlEncode(challengeCode).Trim();
            }

            // Check UserResponse.
            if (string.IsNullOrEmpty(userResponse = context.Request["UserResponse"]))
            {
                errors.Add(AdsCaptchaErrors.ResponseNotSet);
            }
            else if(errors.IsEmpty)
            {
                userResponse = HttpUtility.HtmlEncode(userResponse).Trim();
            }
            
            // Check RemoteAddress.
            if (string.IsNullOrEmpty(remoteAddress = context.Request["RemoteAddress"]))
            {
                errors.Add(AdsCaptchaErrors.RemoteaddressNotSet);
            }
            else if(errors.IsEmpty)
            {
                remoteAddress = HttpUtility.HtmlEncode(remoteAddress).Trim();
            }
             


            bool isAnswerCorrect = false;
            if (errors.IsEmpty)
            {
                var checkAnswerResult = CheckAnswer(captchaId, challengeCode, privateKey, userResponse, remoteAddress, imageType);
                if (checkAnswerResult.HasError)
                {
                    errors.Add(checkAnswerResult);
                }
                else
                {
                    isAnswerCorrect = checkAnswerResult.Value;
                }
            }

            if (errors.HasErrors)
            {
                StringBuilder s = new StringBuilder();
                foreach (string key in request.Form.Keys)
                {
                    s.AppendLine(key + ": " + request.Form[key]);
                }
                
                Log.Warn("Captcha NOT solved. captchaId: {0}, error(s): {1}, Key/Values: {2}", captchaId, errors.ToJson(), s);
                return errors.ToJson().ToString();
            }

            return isAnswerCorrect.ToString().ToLower();
        }

        private AdsCaptchaOperationResult<bool> CheckAnswer(long? captchaId, string requestGuid, string privateKey, string userResponse, string clientIp, ImageType? imageType)
        {
            AdsCaptchaOperationResult<bool> result = null;

            IRequest request = null;

            int selectedIndex;
            if (!int.TryParse(userResponse, out selectedIndex))
            {
                result = AdsCaptchaOperationResult<bool>.ToError(AdsCaptchaErrors.ResponseInvalid);
            }

            if (null == result)
            {
                var requestResult = RequestsManager.Get(requestGuid, isDemo: imageType.GetValueOrDefault() == ImageType.Demo);
                if (requestResult.HasError)
                {
                    if (requestResult.Error == AdsCaptchaErrors.NoResults)
                    {
                        result = false;
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<bool>.ToError(requestResult);
                    }
                }
                else
                {
                    request = requestResult.Value;
                } 
            }

            if (null == result)
            {
                
            }

            if (null == result)
            {
                if (null == imageType)
                {
                    if (request.CaptchaId != captchaId ||
                        request.PrivateKey != privateKey)
                    {
                        Log.Warn(
                            "CheckAnswer: Request params not match. request #{0}, captchaId:[actual:'{1}', expected:'{2}'], privateKey:[actual:'{3}', expected:'{4}']",
                            request.RequestId, captchaId, request.CaptchaId, privateKey,
                            request.PrivateKey);

                        result = false;
                    }
                }
                
                if (request.ClientIp != clientIp)
                {
                    Log.Warn("CheckAnswer: ClientIp not as expected. request #{0}, clientIp:[actual:'{1}', expected;{2}]", request.RequestId, clientIp, request.ClientIp);
                }
            }

            bool isCorrect = false;

            if (null == result)
            {
                isCorrect = (selectedIndex == request.CorrectIndex);
            }

            if (null != request)
            {
                var touchResult = RequestsManager.Touch(request, selectedIndex, isCorrect ? 100 : 0, clientIp);
                if (isCorrect)
                {
                    RequestsManager.RemoveFromCache(requestGuid);
                }

                result = isCorrect;
            }

            return result;
        }
    }
}