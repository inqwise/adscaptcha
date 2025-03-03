using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Model;

namespace Inqwise.AdsCaptcha.API
{
    [Obsolete("Not in use", true)]
    public partial class NoScript : System.Web.UI.Page
    {
        private int captchaId;
        private string publicKey;
        private CaptchaServerBLL.RequestData request = null;

        public int _CaptchaId;
        public string _PublicKey;
        public int? _Width;
        public string _ChallengeCode;
        public string API_URL;
        private string loggingUniqueId = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Manage cookie
            HttpCookie aCookie = new HttpCookie(string.Empty);
            bool useCookies = false;
            try
            {
                useCookies = Convert.ToString(ConfigurationSettings.AppSettings["UseCookies"]).ToLower() == "true";
                if (useCookies)
                {
                    string cookieName = Convert.ToString(ConfigurationSettings.AppSettings["CookieName"]);
                    if (Request.Cookies[cookieName] == null)
                    {//i.e. No cookie
                        aCookie = new HttpCookie(cookieName, Guid.NewGuid().ToString());
                        int cookieExpirationInDays = Convert.ToInt16(ConfigurationSettings.AppSettings["CookieExpirationInDays"]);
                        aCookie.Expires = DateTime.Now.AddDays(cookieExpirationInDays);
                        Response.Cookies.Add(aCookie);
                    }
                    else
                    {
                        aCookie = Request.Cookies[cookieName];
                    }
                }
            }
            catch (Exception exp)
            {
                BLL.NLogManager.logger.ErrorException("Can't manage cookie saving (NoScript.aspx)", exp);
            }
            #endregion

            bool isError = false;
            string errorMessage = "";

            // Set API URL. If SSL (HTTPS), replace URL string.
            API_URL = ConfigurationSettings.AppSettings["API"];
            bool isHttps = Request.Url.ToString().ToLower().StartsWith("https://");
            if (isHttps) API_URL = API_URL.Replace("http://", "https://");

            int publisherId;
            int websiteId;
            string language;

            try
            {
                LogIt("Start Page_Load");
                #region Check Parameters
                // Check CaptchaId.
                if (string.IsNullOrEmpty(Request.QueryString["CaptchaId"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "captchaid-not-set");
                }
                else
                {
                    try
                    {
                        captchaId = Convert.ToInt16(HttpUtility.HtmlEncode(Request.QueryString["CaptchaId"]));
                    }
                    catch
                    {
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "captchaid-invalid");
                    }
                }

                // Check PublicKey.
                if (string.IsNullOrEmpty(Request.QueryString["PublicKey"]))
                {
                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "publickey-not-set");
                }
                else
                {
                    try
                    {
                        publicKey = HttpUtility.HtmlEncode(Request.QueryString["PublicKey"]);
                    }
                    catch
                    {
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "publickey-invalid");
                    }
                }

                // If an error occured, return error messages.
                if (isError)
                {
                    ShowErrorMessage(errorMessage);
                    return;
                }
                #endregion Check Parameters

                // Check if use amazon captcha only.
                bool useAmazonOnly = (ConfigurationSettings.AppSettings["UseAmazonOnly"] == "true" ? true : false);
                if (useAmazonOnly)
                {
                    GetAmazonCaptcha();
                    return;
                }

                // TODO: Move to BLL?
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get website by public key.
                    TP_WEBSITE website = dataContext.TP_WEBSITEs.SingleOrDefault(i => i.Public_Key == publicKey);
                    if (website == null)
                    {
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "publickey-not-exists");
                        General.WriteServiceError(null, "NoScript", ApplicationConfiguration.ErrorType.Warning, new Exception("PublicKey is not exists or invalid (PublicKey=" + publicKey + ")"));
                        ShowErrorMessage(errorMessage);
                        return;
                    }

                    // Get captcha's details by captcha id and website id (check for match).
                    TP_CAPTCHA captcha = dataContext.TP_CAPTCHAs.SingleOrDefault(i => i.Captcha_Id == captchaId && i.TP_WEBSITE.Website_Id == website.Website_Id);
                    if (captcha == null)
                    {
                        isError = true;
                        errorMessage += ((errorMessage == "" ? "" : ",") + "publickey-not-match-captchaid");
                        General.WriteServiceError(null, "NoScript", ApplicationConfiguration.ErrorType.Warning, new Exception("PublicKey and CaptchaId do not match (CaptchaID=" + captchaId + ",PublicKey=" + publicKey + ")"));
                        ShowErrorMessage(errorMessage);
                        return;
                    }

                    // Get publisher's details.
                    TP_PUBLISHER publisher = dataContext.TP_PUBLISHERs.SingleOrDefault(i => i.Publisher_Id == website.Publisher_Id);
                    if (publisher == null)
                        throw new Exception("Publisher returns null. CAPTCHA ins't found or an unhandled error occured.");

                    // Get CAPTCHA data.
                    publisherId = website.Publisher_Id;
                    websiteId = website.Website_Id;
                    language = website.TP_WEBSITE_LANGUAGEs.First().T_LANGUAGE.Language_Prefix;

                    #region Get User Data

                    string userSession = "";
                    string IPAddress = "";
                    string reffererUrl = "";
                    string countryPrefix = string.Empty;
                    try
                    {
                        userSession = "NoScript_" + Session.SessionID.Substring(0,40);
                    }
                    catch { }

                    try
                    {
                        //IPAddress = (Request.UserHostAddress != null ? Request.UserHostAddress : (Request.ServerVariables["REMOTE_ADDR"] != null ? Request.ServerVariables["REMOTE_ADDR"] : ""));
                        IPAddress = detectIPAdress();
                        countryPrefix = CaptchaServerBLL.Ip2country(IPAddress);

                    }
                    catch { }

                    try
                    {
                        reffererUrl = (Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : (Request.ServerVariables["HTTP_REFERER"] != null ? Request.ServerVariables["HTTP_REFERER"] : ""));
                    }
                    catch { }

                    #endregion

                    string userAgent = "N/A";
                    try
                    {
                        userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
                    }
                    catch { }
                    string logMessage = string.Format("About to start NewSecurityRequest: CapchaId={0}, Country={1}, userAgent={2}", captchaId, countryPrefix, userAgent);
                    LogIt(logMessage);

                    // Create new request for SECURITY ONLY Captcha.
                    request = CaptchaServerBLL.NewSecurityRequest(captcha, userSession, IPAddress, reffererUrl, countryPrefix);

                    // Check if an error occured or captcha does not exists.
                    if (request == null)
                        throw new Exception("NewSecurityRequest returns null. CAPTCHA ins't found or an unhandled error occured.");
                }

                #region Save cookie info (User Visit) into DB using data
                if (useCookies)
                {
                    try
                    {
                        bool result = CaptchaServerBLL.AddUserVisit(request.Request_Id, CaptchaServerBLL.UserDataSource.NoScript, aCookie.Value);
                        if (!result)
                        {
                            BLL.NLogManager.logger.Warn("Failed to save user visit data into DB(NoScript)");
                        }
                    }
                    catch (Exception exp)
                    {
                        BLL.NLogManager.logger.ErrorException("Can't save user visit data into db(NoScript)", exp);
                    }
                }
                #endregion

                // Set front-end variables.
                _CaptchaId = captchaId;
                _PublicKey = publicKey;
                _Width = request.Width;
                _ChallengeCode = request.GUID;
            }
            catch (Exception ex)
            {
                try
                {
                    NLogManager.logger.ErrorException("NoScript | Displays Amazon Captcha", ex);
                    GetAmazonCaptcha();
                }
                catch (Exception ex2)
                {
                    Response.Clear();
                    Response.ContentType = "text/html";

                    isError = true;
                    errorMessage += ((errorMessage == "" ? "" : ",") + "unexpected-error");
                    General.WriteServiceError((request == null ? null : request.GUID), "NoScript", ApplicationConfiguration.ErrorType.Error, ex2);
                    ShowErrorMessage(errorMessage);
                }
            }
        }

        private void ShowErrorMessage(string errorMessage)
        {
            panelCaptcha.Visible = false;
            panelError.Visible = true;
            labelError.Text = errorMessage;
        }

        private void GetAmazonCaptcha()
        {
            // Create new request for simple CAPTCHA.
            request = CaptchaServerBLL.NewSimpleRequest(captchaId);

            _Width = request.Width;
            _ChallengeCode = request.GUID;
        }

        private string detectIPAdress()
        {
            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string proxyIPaddress = strIpAddress;
            if (string.IsNullOrEmpty(strIpAddress))
            {
                strIpAddress = (Request.UserHostAddress != null ? Request.UserHostAddress : (Request.ServerVariables["REMOTE_ADDR"] != null ? Request.ServerVariables["REMOTE_ADDR"] : ""));
            }
            return CaptchaServerBLL.ChooseFirstIP(strIpAddress);
        }

        private bool LogIt(string message, string logUniqueId)
        {
            loggingUniqueId = logUniqueId;
            return LogIt(message);
        }

        private bool LogIt(string message)
        {
            return NLogManager.LogDebug(message, "NoScript.aspx", loggingUniqueId);
        }
    }
}