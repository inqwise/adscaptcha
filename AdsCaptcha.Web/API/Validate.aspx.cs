using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;
using System.Configuration;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.API
{
    [Obsolete("Not In Use", true)]
    public partial class Validate : System.Web.UI.Page
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private int captchaId;
        private string privateKey;
        private string challengeCode = null;
        private string userResponse;
        private string remoteAddress;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear and set response.
            Response.Clear();
            Response.ContentType = "text/plain";

            bool isError = false;
            string errorMessage = "";

            int publisherId;
            int websiteId;

            try
            {
                #region Check Parameters
                // Check CaptchaId.
                if (string.IsNullOrEmpty(Request.Form["CaptchaId"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "captchaid-not-set");
                }
                else
                {
                    try
                    {
                        captchaId = Convert.ToInt16(HttpUtility.HtmlEncode(Request.Form["CaptchaId"]));
                    }
                    catch
                    {
                        Log.Warn("Validate: Captcha Invalid.");
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "captchaid-invalid");
                    }
                }

                // Check PrivateKey.
                if (string.IsNullOrEmpty(Request.Form["PrivateKey"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "privatekey-not-set");
                }
                else
                {
                    try
                    {
                        privateKey = HttpUtility.HtmlEncode(Request.Form["PrivateKey"]);
                    }
                    catch
                    {
                        Log.Warn("Validate: PrivateKey Invalid. cid:{0}", captchaId);
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "privatekey-invalid");
                    }
                }

                // Check ChallengeCode.
                if (string.IsNullOrEmpty(Request.Form["ChallengeCode"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "challenge-not-set");
                }
                else
                {
                    try
                    {
                        challengeCode = HttpUtility.HtmlEncode(Request.Form["ChallengeCode"]);
                    }
                    catch
                    {
                        Log.Warn("Validate: Chalange Invalid. cid:{0}", captchaId);
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "challenge-invalid");
                    }
                }

                // Check UserResponse.
                if (string.IsNullOrEmpty(Request.Form["UserResponse"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "response-not-set");
                }
                else
                {
                    try
                    {
                        userResponse = HttpUtility.HtmlEncode(Request.Form["UserResponse"]);
                    }
                    catch
                    {
                        Log.Warn("Validate: UserResponse Invalid. cid:{0}", captchaId);
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "response-invalid");
                    }
                }

                // Check RemoteAddress.
                if (string.IsNullOrEmpty(Request.Form["RemoteAddress"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "remoteaddress-not-set");
                }
                else
                {
                    try
                    {
                        remoteAddress = HttpUtility.HtmlEncode(Request.Form["RemoteAddress"]);

                        /*
                        IPAddress address;
                        if (!IPAddress.TryParse(remoteAddress, out address))
                            throw new Exception();
                        */
                    }
                    catch
                    {
                        Log.Warn("Validate: RemoteAddress Invalid. cid:{0}", captchaId);
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "remoteaddress-invalid");
                    }
                }

                // If an error occured, return error messages.
                if (isError)
                {
                    Response.Write(errorMessage);
                    return;
                }
                #endregion Check Parameters

                //if (!challengeCode.EndsWith("-aws"))
                //{
                //    // Get publisher and website by public key.
                //    WebsiteBLL.GetWebsiteByPrivateKey(privateKey, out publisherId, out websiteId);

                //    // Check if private key exists and matchs to captcha id.
                //    if (publisherId == 0 || websiteId == 0)
                //    {
                //        isError = true;
                //        errorMessage = "privatekey-not-exists";
                //        General.WriteServiceError(challengeCode, "Validate", ApplicationConfiguration.ErrorType.Warning, new Exception("PrivateKey does not exists (PrivateKey=" + privateKey + ")"));
                //        Response.Write(errorMessage);
                //        return;
                //    }
                //    else if (!CaptchaBLL.IsExist(publisherId, websiteId, captchaId))
                //    {
                //        isError = true;
                //        errorMessage = "privatekey-not-match-captchaid";
                //        General.WriteServiceError(challengeCode, "Validate", ApplicationConfiguration.ErrorType.Warning, new Exception("PrivateKey and CaptchaId do not match (CaptchaID=" + captchaId + ",PrivateKey=" + privateKey + ")"));
                //        Response.Write(errorMessage);
                //        return;
                //    }
                //}

                //Response.Write(CaptchaServerBLL.Answer(challengeCode, userResponse));

                int successRate = 0;

                try
                {

                    using (DAL.AdsCaptcha_RequestsEntities ent = new AdsCaptcha.DAL.AdsCaptcha_RequestsEntities())
                    {

                        string[] defaultCaptcha = ConfigurationManager.AppSettings["DefaultCaptcha"].Split(';');
                        int defaultCaptchaId = Convert.ToInt32(defaultCaptcha[0]);
                        string defaultPrivateKey = defaultCaptcha[2];
                        var request = (from r in ent.T_REQUESTS
                                       where r.Request_Guid == challengeCode
                                       //&& ((r.Captcha_Id == captchaId && r.PrivateKey == privateKey) ||
                                       //(r.Captcha_Id == defaultCaptchaId && r.PrivateKey == defaultPrivateKey))
                                       && r.Is_Checked < 2 
                                       && r.Is_Resend == 0 && r.Is_Timed_Out == 0
                                       select r).FirstOrDefault();



                        if (request != null)
                        {
                            request.Is_Checked += 1;
                            request.Is_Typed += 1;
                            request.User_Answer = userResponse;

                            switch (request.Type_Id)
                            {

                                case (int)CaptchaType.RandomImage:
                                case (int)CaptchaType.Slider:

                                    decimal challengeValue = Convert.ToDecimal(request.Challenge);
                                    decimal userAnswer = Convert.ToDecimal(userResponse);
                                    string[] sliderParams = request.ChallengeParams.Split(',');
                                    int minValue = Convert.ToInt32(sliderParams[7]);

                                    int maxValue = Convert.ToInt32(sliderParams[8]);
                                    successRate = CaptchaServerBLL.CheckSliderSuccessRate(challengeValue, userAnswer, minValue, maxValue);

                                    break;

                                case (int)CaptchaType.SlideToFit:

                                    decimal challengeValueSlider = Convert.ToDecimal(request.Challenge);
                                    decimal userAnswerSlider = Convert.ToDecimal(userResponse);
                                    int maxValueSlider = 30;
                                    successRate = Math.Abs(challengeValueSlider - userAnswerSlider) == 0 ? 100 : 100 - (int)((double)Math.Abs(challengeValueSlider - userAnswerSlider) / (double)maxValueSlider * 100.0 * 1.1);


                                    break;
                                case (int)CaptchaType.SecurityOnly:
                                case (int)CaptchaType.PayPerType:
                                case (int)CaptchaType.TypeWords:
                                default:
                                    successRate = CaptchaServerBLL.CheckSuccessRate(request.Challenge.ToLower(), userResponse.ToLower());

                                    break;
                            }
                            request.Success_Rate = successRate;

                            ent.SaveChanges();
                        }
                    }
                }
                catch(Exception exc)
                {
                    Log.ErrorException(String.Format("Validate#1: Unexpected error occured. cid:'{0}'", captchaId), exc);
                    successRate = 0;
                    General.WriteServiceError(challengeCode, "Validate", Constants.ErrorType.Error, exc);
                }

                // Check if user answered correctly.
                if (successRate >= Constants.MIN_SUCCESS_RATE)
                {
                    Log.Debug("Validate: true Captcha #{0} successRate: {1} remoteaddress: {2}", captchaId, successRate, remoteAddress);
                    Response.Write("true");
                }
                else
                {
                    Log.Debug("Validate: false Captcha #{0} successRate: {1} remoteaddress: {2}", captchaId, successRate, remoteAddress);
                    Response.Write("false");
                }

            }
            catch (Exception ex)
            {
                Log.ErrorException("Validate: Unexpected error occured", ex);
                errorMessage += ((errorMessage == "" ? "" : ",") + "unexpected-error");
                General.WriteServiceError(challengeCode, "Validate", Constants.ErrorType.Error, ex);
                Response.Write(errorMessage);
                return;
            }
        }
    }
}