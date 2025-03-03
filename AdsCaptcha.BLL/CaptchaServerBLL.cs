using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Cache;
using CodeSmith.Data.Linq;
using CountryDetection;
using System.Transactions;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class CaptchaServerBLL
    {

        #region Data Members

        private static readonly Random random = new Random();

        [Obsolete("Not in use", true)]
        public class RequestData
        {
            public long Request_Id;
            public string GUID;
            public string Ad;
            public string Challenge;
            public string Link;
            public string ResponseDirection;
            public Nullable<int> SecurityLevel;
            public Nullable<int> Width;
            public Nullable<int> Height;
            //public string Type = ApplicationConfiguration.REQUEST_TYPE_PPT;
            public string Theme;
            public bool Banner;
            public bool Popup;
            public SliderData SliderData;

            [Obsolete("Not in use", true)]
            public RequestData()
            {
                GUID = null;
                Ad = null;
                Challenge = null;
                Link = null;
                ResponseDirection = "ltr";
                SecurityLevel = null;
                Width = null;
                Height = null;
                //Type = ApplicationConfiguration.REQUEST_TYPE_PPT;
                Theme = "default";
                Banner = false;
                Popup = false;
                SliderData = new SliderData();
            }
        }

        public enum UserDataSource
        {
            Get,
            Refresh,
            NoScript
        }

        private static string loggingUniqueId = String.Empty;

        #endregion Data Members

        #region Public Methods

        /// <summary>
        /// Get challange data (code & security level).
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <returns>Requested challange data (code & security level).</returns>
        [Obsolete("Not in use", true)]
        public static ChallengeData GetChallengeData(string requestGuid)
        {
            ChallengeData challenge = new ChallengeData();

            if (requestGuid.EndsWith("-aws"))
            {
                //AmazonAWS aws = new AmazonAWS();

                //challenge.Challenge = aws.GetChallangeCode(requestGuid);
                challenge.SecurityLevel = (int)Constants.SecurityLevel.Medium;
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get request by GUID.
                    TCS_REQUEST request = new TCS_REQUEST();
                    request = dataContext.TCS_REQUESTs.Where(i => i.Request_Guid == requestGuid).SingleOrDefault();

                    // If request not exists, return null.
                    if (request == null)
                        return null;

                    /*
                    // If request already archived, return null.
                    // We don't want to able re-request for the same challenge.
                    if (request.Is_Archived == 1)
                        return null;
                    */

                    // Set challenge code and security level.
                    challenge.Challenge = request.TCS_REQUEST_CHALLENGE.Challenge_Code;
                    challenge.SecurityLevel = request.TCS_REQUEST_CHALLENGE.Security_Level_Id;

                    if (request.TCS_REQUEST_PUBLISHER.Type_Id == (int)CaptchaType.PayPerType)
                        if (request.TCS_REQUEST_ADVERTISER.TA_AD.Rtl.GetValueOrDefault())
                            challenge.Direction = Constants.Direction.RightToLeft;
                    
                    int themeId = request.TCS_REQUEST_PUBLISHER.TP_CAPTCHA.Theme_Id;
                    T_THEME theme = DictionaryBLL.GetThemeById(themeId);
                    challenge.BackColor = theme.Captcha_Background_Color;
                    challenge.ForeColor = theme.Captcha_Text_Color;
                }
            }

            return challenge;
        }

        [Obsolete("Not in use", true)]
        public static RequestData NewCaptchaRequestTrans(TP_CAPTCHA captcha, string sessionId, string ipAddress, string referrerUrl, string countryPrefix)
        {
            LogIt("Start NewCaptcha request with TRANSACTION for captchaId=" + (captcha == null ? "NULL" : captcha.Captcha_Id.ToString()));
            using (var trans = new TransactionScope(
                            TransactionScopeOption.Required,
                            new TransactionOptions
                            {
                                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                            },
                            EnterpriseServicesInteropOption.Automatic))
            {
                // Perform operations using your DC, including submitting changes
                RequestData requestData = NewCaptchaRequest(captcha, sessionId, ipAddress, referrerUrl, countryPrefix);
                trans.Complete();
                return requestData;
            }
        }

        /// <summary>
        /// Create new request for captcha.
        /// Each request is being checked, find matching ads and select an ad & slogan to display.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha Id.</param>
        /// <param name="sessionId">User session id.</param>
        /// <param name="ipAddress">User IP.</param>
        /// <param name="referrerUrl">Reffrer URL.</param>
        /// <returns>New request data.</returns>
        [Obsolete("Not in use", true)]
        public static RequestData NewCaptchaRequest(TP_CAPTCHA captcha, string sessionId, string ipAddress, string referrerUrl, string countryPrefix)
        {
            #region logging for debug
            int captchaIdForLog = captcha == null? -99 : captcha.Captcha_Id;
            int typeIdForLog = captcha == null? -99 : captcha.Type_Id;
            string logMessage = string.Format("About to start NewCaptchaRequest for captchaId={0}, typeId={1}, sessionId={2}, ipAddress={3}, reffereUrl={4}, countryPrefix={5}", captchaIdForLog, typeIdForLog, sessionId, ipAddress, referrerUrl, countryPrefix);
            LogIt(logMessage);
            #endregion

            RequestData rd = new RequestData();

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Generate random GUID string.
                string guid = GenerateGuid();

                // Create new captcha request.
                TCS_REQUEST request = new TCS_REQUEST();
                request.Request_Guid = guid;
                request.Timestamp = DateTime.Now;
                request.Session_Id = sessionId;
                request.IP_Address = ipAddress;
                request.Referrer_Url = referrerUrl;
                bool dummy;

                if (string.IsNullOrEmpty(countryPrefix))
                {
                    request.Country_Id = null;
                }
                else
                {
                    request.Country_Id = GetCountryID(dataContext, countryPrefix, captcha.Website_Id, out dummy);
                }
                logMessage = string.Format("About to InsertOnSubmit Request: RequestGUID={0}, Timestamp={1}, captchaId={2}, countryId={3}", guid, request.Timestamp, captchaIdForLog, (request.Country_Id==null ? "NULL" : request.Country_Id.ToString()));
                LogIt(logMessage, guid);

                // Insert new request.
                dataContext.TCS_REQUESTs.InsertOnSubmit(request);

                // Save changes.
                dataContext.SubmitChanges();

                // Get new request id.
                long requestId = request.Request_Id;

                // Set publisher settings.
                TCS_REQUEST_PUBLISHER publisher = new TCS_REQUEST_PUBLISHER();
                publisher.Request_Id = requestId;
                publisher.Publisher_Id = captcha.Publisher_Id;
                publisher.Website_Id = captcha.Website_Id;
                publisher.Captcha_Id = captcha.Captcha_Id;

                publisher.Revenue_Share_Pct = CalcRevShare(captcha.TP_PUBLISHER.Revenue_Share_Pct, captcha.TP_WEBSITE.Allow_Bonus, captcha.TP_WEBSITE.Total_Revenue, captcha.TP_WEBSITE.Bonus_Limit);
                publisher.TCS_REQUEST = request;

                // Set challenge.
                TCS_REQUEST_CHALLENGE challenge = new TCS_REQUEST_CHALLENGE();
                challenge.Request_Id = requestId;
                challenge.Security_Level_Id = captcha.Security_Level_Id;
                challenge.TCS_REQUEST = request;

                logMessage = string.Format("About to FindMatching for request: RequestID={0}, captchaId={1}, Revenue_Share_Pct={2}", requestId, captcha.Captcha_Id, publisher.Revenue_Share_Pct);
                LogIt(logMessage);

                // Find matching (and that the Captcha is not security).
                TA_AD ad = null;

                if (sessionId.Trim() != string.Empty)
                {
                    ad = FindMatching(dataContext, captcha, countryPrefix);
                }

               

                if (!(captcha.IsCommercial == null ? false : (bool)captcha.IsCommercial))
                {
                    ad = null;
                }



                if (ad != null)
                {
                    bool isRandom = false;
                    if (captcha.Type_Id == (int)CaptchaType.SlideToFit)
                    {
                        var adPrev = (from a in dataContext.TCS_REQUEST_ADVERTISERs
                                      join p in dataContext.TCS_REQUEST_PUBLISHERs
                                         on a.Request_Id equals p.Request_Id
                                         join r in dataContext.TCS_REQUESTs
                                         on a.Request_Id equals r.Request_Id
                                      where a.Ad_Id == ad.Ad_Id && p.Captcha_Id == captcha.Captcha_Id && r.Session_Id == sessionId && r.Is_Archived == 0
                                      select a).FirstOrDefault(); //dataContext.TCS_REQUEST_ADVERTISERs.Where(a => a.Ad_Id == a.Ad_Id).Select(aa=>aa.Request_Id).ToArray();

                        isRandom = (adPrev != null);
                    }

                    if (isRandom)
                    {
                        ad = null;
                    }
                }

                // If no matching found OR security only OR publisher/website/captcha is not running (=active).
                if (ad == null)
                {
                    #region NO Ad
                    LogIt("NO ad was found for Captcha_Id=" + captchaIdForLog);
                    if (captcha.Type_Id == (int)CaptchaType.Slider ||
                        captcha.Type_Id == (int)CaptchaType.RandomImage ||
                        captcha.Type_Id == (int)CaptchaType.SlideToFit
                        )
                    {
                        #region Get Security slider
                        // Set Captcha type as Random Image (security Slider).
                        publisher.Type_Id = (int)CaptchaType.RandomImage;
                        
                        // Set Captcha type as Slider.
                        //rd.Type = ApplicationConfiguration.REQUEST_TYPE_SLIDER;
                                                                        
                        // Generate image slide attributes.
                        challenge.Challenge_Code = GenerateSliderData(captcha.Max_Width, captcha.Max_Height, out rd.SliderData);
                        challenge.Params = rd.SliderData.Type + "," + rd.SliderData.Width + "," + rd.SliderData.Height + "," + rd.SliderData.X + "," + rd.SliderData.Y + "," + rd.SliderData.W + "," + rd.SliderData.H + "," + rd.SliderData.Min + "," + rd.SliderData.Max;
                        logMessage = string.Format("NO ad was found for Captcha_Id={0}. Showing security slider with challenge={1} and params={2}, country={3}", captchaIdForLog, challenge.Challenge_Code, challenge.Params, countryPrefix);
                        LogIt(logMessage);
                        #endregion
                    }
                    else
                    {
                        #region Get Security only
                        // Set Captcha type as Security Only.
                        publisher.Type_Id = (int)CaptchaType.SecurityOnly;

                        // Generate security only challenge.
                        challenge.Challenge_Code = GenerateCode((int)CaptchaType.SecurityOnly, captcha.Security_Level_Id);

                        logMessage = string.Format("NO ad was found for Captcha_Id={0}. Showing ***Security Only*** with challenge={1}, country={2}", captchaIdForLog, challenge.Challenge_Code, countryPrefix);
                        LogIt(logMessage);
                        #endregion
                    }

                    // Set Captcha dimensions.
                    rd.Width = captcha.Max_Width;
                    rd.Height = captcha.Max_Height;
                    #endregion
                }
                else
                {
                    #region Found an Ad
                   
                        logMessage = string.Format("FOUND Ad with Ad_Id={0} and Campaing_Id={1} for captchaId={2}, country={3}", ad.Ad_Id, ad.Campaign_Id, captchaIdForLog, countryPrefix);
                        LogIt(logMessage);
                        // Set advertiser settings.
                        TCS_REQUEST_ADVERTISER advertiser = new TCS_REQUEST_ADVERTISER();
                        advertiser.Request_Id = requestId;
                        advertiser.Advertiser_Id = ad.Advertiser_Id;
                        advertiser.Campaign_Id = ad.Campaign_Id;
                        advertiser.Ad_Id = ad.Ad_Id;
                        advertiser.Cpt_Bid = ad.Max_Cpt;
                        advertiser.Type_Id = ad.Type_Id;
                        advertiser.TCS_REQUEST = request;

                        switch (ad.Type_Id)
                        {
                            case (int)AdType.SloganOnly:
                                break;
                            case (int)AdType.SloganAndImage:
                            case (int)AdType.Slide2Fit:
                                rd.Ad = ad.Ad_Image;
                                rd.Link = (string.IsNullOrEmpty(ad.Ad_Url) ? null : ad.Ad_Url);
                                break;
                            case (int)AdType.SloganAndVideo:
                                rd.Ad = ad.Ad_Video;
                                break;
                            case (int)AdType.YbrantBanner:
                                rd.Banner = true;
                                break;
                        }

                        if (ad.Rtl.GetValueOrDefault()) rd.ResponseDirection = "rtl";

                        logMessage = string.Format("Before InsertOnSubmit to advertiser of requestId={0}, captchaId={1}, CampaignId={2}, ad_Id={3}, ad_type={4}", requestId, captchaIdForLog, ad.Campaign_Id, ad.Ad_Id, ad.Type_Id);
                        LogIt(logMessage);

                        // Insert advertiser settings.
                        dataContext.TCS_REQUEST_ADVERTISERs.InsertOnSubmit(advertiser);

                        // Check if ad includes link.
                        if (!string.IsNullOrEmpty(rd.Link))
                        {
                            // Set click settings.
                            TCS_REQUEST_CLICK click = new TCS_REQUEST_CLICK();
                            click.Request_Id = requestId;
                            click.Link_Url = rd.Link;
                            click.Timestamp = DateTime.Now;
                            click.Is_Clicked = 0;

                            // Insert click settings.
                            dataContext.TCS_REQUEST_CLICKs.InsertOnSubmit(click);
                        }

                        // Generate slogan challenge.
                        switch (captcha.Type_Id)
                        {
                            // Pay-Per-Type captcha.
                            case (int)CaptchaType.PayPerType:
                                publisher.Type_Id = (int)CaptchaType.PayPerType;
                                //rd.Type = ApplicationConfiguration.REQUEST_TYPE_PPT;
                                challenge.Challenge_Code = (ad.Ad_Slogan + " " + GenerateCode(captcha.Type_Id, captcha.Security_Level_Id)).Trim();
                                break;

                            // Slider captcha.
                            case (int)CaptchaType.Slider:
                                publisher.Type_Id = (int)CaptchaType.Slider;
                                //rd.Type = ApplicationConfiguration.REQUEST_TYPE_SLIDER;
                                challenge.Challenge_Code = GenerateSliderData(captcha.Max_Width, captcha.Max_Height, out rd.SliderData);
                                challenge.Params = rd.SliderData.Type + "," + rd.SliderData.Width + "," + rd.SliderData.Height + "," + rd.SliderData.X + "," + rd.SliderData.Y + "," + rd.SliderData.W + "," + rd.SliderData.H + "," + rd.SliderData.Min + "," + rd.SliderData.Max;
                                break;
                            // Type words commercial captcha.
                            case (int)CaptchaType.TypeWords:
                                if (captcha.IsCommercial == null ? false : (bool)captcha.IsCommercial)
                                {
                                    publisher.Type_Id = (int)CaptchaType.TypeWords;
                                    //rd.Type = ApplicationConfiguration.REQUEST_TYPE_PPT;
                                    challenge.Challenge_Code = (ad.Ad_Slogan + " " + GenerateCode(captcha.Type_Id, captcha.Security_Level_Id)).Trim();
                                }
                                break;
                            // Slider captcha.
                            case (int)CaptchaType.SlideToFit:
                                challenge.Challenge_Code = GenerateSliderData(captcha.Max_Width, captcha.Max_Height, out rd.SliderData);
                                challenge.Params = rd.SliderData.Type + "," + rd.SliderData.Width + "," + rd.SliderData.Height + "," + rd.SliderData.X + "," + rd.SliderData.Y + "," + rd.SliderData.W + "," + rd.SliderData.H + "," + rd.SliderData.Min + "," + rd.SliderData.Max;

                                if (captcha.IsCommercial == null ? false : (bool)captcha.IsCommercial)
                                {
                                    publisher.Type_Id = (int)CaptchaType.SlideToFit;
                                    //rd.Type = ApplicationConfiguration.REQUEST_TYPE_SLIDER;
                                }

                                break;
                        }

                        // Set ad dimensions.
                        rd.Width = (ad.Width == null || ad.Type_Id == (int)AdType.SloganOnly) ? captcha.Max_Width : ad.Width;
                        rd.Height = (ad.Height == null || ad.Type_Id == (int)AdType.SloganOnly) ? captcha.Max_Height : ad.Height;
                        //logMessage = string.ContentType("InsertOnSubmit challenge with: captchaId={0}, requestId={1}, type={2}, challenge={3}", captchaIdForLog, requestId, rd.Type, challenge.Challenge_Code);
                        LogIt(logMessage);
                    
                    #endregion
                }

                // Insert challenge settings.
                dataContext.TCS_REQUEST_CHALLENGEs.InsertOnSubmit(challenge);

                // Insert publisher settings.
                dataContext.TCS_REQUEST_PUBLISHERs.InsertOnSubmit(publisher);

                // Check if developer exists.
                Nullable<int> developerId = captcha.TP_PUBLISHER.Developer_Id;

                if (developerId != null)
                {
                    // Set publisher settings.
                    TCS_REQUEST_DEVELOPER developer = new TCS_REQUEST_DEVELOPER();
                    developer.Request_Id = requestId;
                    developer.Developer_Id = (int)developerId;
                    developer.Publisher_Id = captcha.Publisher_Id;
                    developer.Website_Id = captcha.Website_Id;
                    developer.Captcha_Id = captcha.Captcha_Id;
                    developer.Type_Id = publisher.Type_Id;
                    int developerRevenueSharePct = DeveloperBLL.GetDeveloper((int)developerId).Revenue_Share_Pct;
                    developer.Revenue_Share_Pct = developerRevenueSharePct;
                    developer.TCS_REQUEST = request;

                    // Insert developer settings.
                    dataContext.TCS_REQUEST_DEVELOPERs.InsertOnSubmit(developer);
                }

                rd.SecurityLevel = captcha.Security_Level_Id;
                rd.Challenge = challenge.Challenge_Code;
                rd.Theme = DictionaryBLL.GetThemeById(captcha.Theme_Id).Folder;

                // Check if popup.
                if (captcha.Allow_Popup == 1)
                    rd.Popup = true;

                LogIt("About to submit ALL changes for captchaId=" + captchaIdForLog);
                // Save changes.
                dataContext.SubmitChanges();

                // Save GUID & ID
                rd.GUID = guid;
                rd.Request_Id = request.Request_Id;
                logMessage = string.Format("SUBMITTED all changes for captchaId={0}, guid={1}, requestId={2}", captchaIdForLog, guid, rd.Request_Id);
                LogIt(logMessage);
            }

            return rd;
        }

        [Obsolete("Not in use", true)]
        public static RequestData NewSecurityRequest(TP_CAPTCHA captcha, string sessionId, string ipAddress, string referrerUrl, string countryPrefix)
        {
            #region logging for debug
            int captchaIdForLog = captcha == null ? -99 : captcha.Captcha_Id;
            int typeIdForLog = captcha == null ? -99 : captcha.Type_Id;
            string logMessage = string.Format("About to start NewSecurityRequest for captchaId={0}, typeId={1}, sessionId={2}, ipAddress={3}, reffereUrl={4}, countryPrefix={5}", captchaIdForLog, typeIdForLog, sessionId, ipAddress, referrerUrl, countryPrefix);
            LogIt(logMessage, string.Empty);
            #endregion

            RequestData rd = new RequestData();

            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Generate random GUID string.
                string guid = GenerateGuid();

                #region Create new captcha request.
                TCS_REQUEST request = new TCS_REQUEST();
                request.Request_Guid = guid;
                request.Timestamp = DateTime.Now;
                request.Session_Id = sessionId;
                request.IP_Address = ipAddress;
                request.Referrer_Url = referrerUrl;
                if (string.IsNullOrEmpty(countryPrefix))
                {
                    request.Country_Id = null;
                }
                else
                {
                    bool dummy;
                    request.Country_Id = GetCountryID(dataContext, countryPrefix, captcha.Website_Id, out dummy);
                }
                logMessage = string.Format("About to InsertOnSubmit New security request was created for captchaId={0}: guid={1}, timestamp={2}, ipAddress={3}, country_Id={4}", captchaIdForLog, guid, request.Timestamp, ipAddress, (request.Country_Id == null ? "NULL" : request.Country_Id.ToString()));
                LogIt(logMessage, guid);

                // TODO: Catch duplicate GUID exception?
                #endregion

                // Insert new request.
                dataContext.TCS_REQUESTs.InsertOnSubmit(request);

                // Save changes.
                dataContext.SubmitChanges();
                LogIt("Submitted changes for security captcha with captchaId=" + captchaIdForLog);

                // Get new request id.
                long requestId = request.Request_Id;

                // Set publisher settings.
                TCS_REQUEST_PUBLISHER publisher = new TCS_REQUEST_PUBLISHER();
                publisher.Request_Id = requestId;
                publisher.Publisher_Id = captcha.Publisher_Id;
                publisher.Website_Id = captcha.Website_Id;
                publisher.Captcha_Id = captcha.Captcha_Id;
                publisher.Revenue_Share_Pct = captcha.TP_PUBLISHER.Revenue_Share_Pct;
                publisher.TCS_REQUEST = request;

                // Check if popup.
                if (captcha.Allow_Popup == 1)
                    rd.Popup = true;

                // Set challenge.
                TCS_REQUEST_CHALLENGE challenge = new TCS_REQUEST_CHALLENGE();
                challenge.Request_Id = requestId;
                challenge.Security_Level_Id = captcha.Security_Level_Id;
                challenge.TCS_REQUEST = request;

                // Generate security only challenge.
                challenge.Challenge_Code = GenerateCode((int)CaptchaType.SecurityOnly, captcha.Security_Level_Id);

                // Set Captcha type as Security Only.
                publisher.Type_Id = (int)CaptchaType.SecurityOnly;

                // Set ad dimensions.
                rd.Width = captcha.Max_Width;
                rd.Height = captcha.Max_Height;

                logMessage = string.Format("About to InsertOnSubmit Publisher/Challenge details for captchaId={0}: requestId={1}, challengeCode={2} adWidth={3}, adHeight={4}, Type_Id={5}", captcha.Captcha_Id, requestId, challenge.Challenge_Code, rd.Width, rd.Height, publisher.Type_Id);
                LogIt(logMessage);

                // Insert challenge settings.
                dataContext.TCS_REQUEST_CHALLENGEs.InsertOnSubmit(challenge);

                // Insert publisher settings.
                dataContext.TCS_REQUEST_PUBLISHERs.InsertOnSubmit(publisher);

                #region Manage Developer details
                // Check if developer exists.
                Nullable<int> developerId = captcha.TP_PUBLISHER.Developer_Id;

                if (developerId != null)
                {
                    // Set publisher settings.
                    TCS_REQUEST_DEVELOPER developer = new TCS_REQUEST_DEVELOPER();
                    developer.Request_Id = requestId;
                    developer.Developer_Id = (int)developerId;
                    developer.Publisher_Id = captcha.Publisher_Id;
                    developer.Website_Id = captcha.Website_Id;
                    developer.Captcha_Id = captcha.Captcha_Id;
                    developer.Type_Id = publisher.Type_Id;
                    developer.Revenue_Share_Pct = DeveloperBLL.GetDeveloper((int)developerId).Revenue_Share_Pct;
                    developer.TCS_REQUEST = request;

                    // Insert developer settings.
                    dataContext.TCS_REQUEST_DEVELOPERs.InsertOnSubmit(developer);
                }
                #endregion

                rd.SecurityLevel = captcha.Security_Level_Id;
                rd.Challenge = challenge.Challenge_Code;
                //rd.Type = ApplicationConfiguration.REQUEST_TYPE_SECURITY;
                rd.Theme = DictionaryBLL.GetThemeById(captcha.Theme_Id).Folder;

                LogIt("About to submit changes for security capthca with CaptchaId=" + captchaIdForLog);

                // Save changes.
                dataContext.SubmitChanges();

                // Save GUID.
                rd.GUID = guid;
                rd.Request_Id = request.Request_Id;

                logMessage = string.Format("Security captcha was submitted for captchaId={0}, guid={1}, requestId={2}", captchaIdForLog, guid, rd.Request_Id);
                LogIt(logMessage);
            }

            return rd;
        }

        [Obsolete("Not in use", true)]
        public static RequestData NewSimpleRequest(int captchaId)
        {
            RequestData rd = new RequestData();

            // Generate random GUID string.
            string requestGuid = General.GenerateGuid() + "-aws";

            // Generate security only challenge.
            int securityLevel = (int)Constants.SecurityLevel.Medium;
            string challenge = GenerateCode((int)CaptchaType.SecurityOnly, securityLevel);

            //AmazonAWS aws = new AmazonAWS();
            //aws.NewRequest(captchaId, requestGuid, challenge);

            rd.Width = 200;
            rd.Height = 30;
            //rd.SecurityLevel = securityLevel;
            rd.Challenge = challenge;
            rd.GUID = requestGuid;

            return rd;
        }

        /// <summary>
        /// Indicates request as flagged content.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <returns>Whether opperation succeeded or not.</returns>
        public static bool Flag(string requestGuid)
        {
            // Check if request exists (by GUID).
            if (IsExist(requestGuid))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get request.
                    TCS_REQUEST request = dataContext.TCS_REQUESTs.SingleOrDefault(req => req.Request_Guid == requestGuid);

                    // Check if request is already archived. If so, "ignore".
                    if (request.Is_Archived == 1)
                        return false;

                    // Set flagged indicator.
                    request.Is_Flagged = 1;

                    // Save changes.
                    dataContext.SubmitChanges();

                    return true;
                }
            }
            else
            {
                // TODO: Request does not exists
                return false;
            }
        }

        /// <summary>
        /// Indicates request as resended.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <returns>Whether opperation succeeded or not.</returns>
        public static bool Resend(string requestGuid, string reason)
        {
            // Check if request exists (by GUID).
            if (IsExist(requestGuid))
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get request.
                    TCS_REQUEST request = dataContext.TCS_REQUESTs.SingleOrDefault(req => req.Request_Guid == requestGuid);

                    // Check if request is already archived. If so, "ignore".
                    if (request.Is_Archived == 1)
                        return false;

                    if (reason.ToLower() == "timeout")
                    {
                        // Set timeout indicator.
                        request.Is_Timed_Out = 1;
                    }
                    else
                    {
                        // Set resend indicator.
                        request.Is_Resend = 1;
                    }

                    // Save changes.
                    dataContext.SubmitChanges();

                    return true;
                }
            }
            else
            {
                // TODO: Request does not exists
                return false;
            }
        }

        /// <summary>
        /// Indicates request was viewed.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <returns>Whether opperation succeeded or not.</returns>
        public static bool Viewed(string requestGuid)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get request.
                    TCS_REQUEST request = dataContext.TCS_REQUESTs.SingleOrDefault(req => req.Request_Guid == requestGuid);

                    // Check if request is already archived. If so, "ignore".
                    if (request.Is_Viewed == 0)
                    {
                        // Set viewed indicator.
                        request.Is_Viewed = 1;

                        // Save changes.
                        dataContext.SubmitChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Set user's answer to the Captcha challenge.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="answer">User's Captcha challenge answer.</param>
        [Obsolete("Not in use", true)]
        public static string Answer(string requestGuid, string answer)
        {
            int successRate = 0;

            if (requestGuid.EndsWith("-aws"))
            {
                //AmazonAWS aws = new AmazonAWS();
                //string challengeCode = aws.GetChallangeCode(requestGuid);

                // Calculate answer success rate.
                //successRate = CheckSuccessRate(challengeCode.ToLower(), answer.ToLower());

                //aws.SetUserAnswer(requestGuid, answer, successRate);
            }
            else
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Get request.
                    TCS_REQUEST request = dataContext.TCS_REQUESTs.FirstOrDefault(req => req.Request_Guid == requestGuid);

                    // Check if request exists (by GUID).
                    if (request == null)
                    {
                        return "challenge-not-set";
                    }
                    else
                    {
                        //// Check if answer already checked or answered before.
                        //if (request.Is_Checked == 1 || request.User_Answer != null)
                        //{
                        //    return "already-checked";
                        //}

                        // Set checked indicator.
                        request.Is_Checked = 1;

                        // Set typed indicator.
                        request.Is_Typed = 1;

                        // TODO: Microsoft bug... Fixed in .Net 4.0?
                        // http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=351358
                        // request.TCS_REQUEST_CHALLENGE.User_Answer = answer;
                        // Get request challenge.
                        // TCS_REQUEST_CHALLENGE challenge = dataContext.TCS_REQUEST_CHALLENGEs.SingleOrDefault(req => req.Request_Id == request.Request_Id);
                        // challenge.User_Answer = answer;

                        // Set user's answer.
                        request.User_Answer = answer;

                        // Calculate answer success rate.
                        switch (request.TCS_REQUEST_PUBLISHER.Type_Id)
                        {
                            case (int)CaptchaType.RandomImage:
                            case (int)CaptchaType.Slider:
                            case (int)CaptchaType.SlideToFit:
                                decimal challengeValue = Convert.ToDecimal(request.TCS_REQUEST_CHALLENGE.Challenge_Code);
                                decimal userAnswer = Convert.ToDecimal(answer);
                                string[] sliderParams = request.TCS_REQUEST_CHALLENGE.Params.Split(',');
                                int minValue = Convert.ToInt32(sliderParams[7]);
                                
                                int maxValue = Convert.ToInt32(sliderParams[8]);
                                successRate = CheckSliderSuccessRate(challengeValue, userAnswer, minValue, maxValue);

                                if (request.TCS_REQUEST_PUBLISHER.TP_CAPTCHA.Type_Id == (int)CaptchaType.SlideToFit)
                                {
                                    decimal challengeValueSlider = Convert.ToDecimal(request.TCS_REQUEST_CHALLENGE.Challenge_Code);
                                    decimal userAnswerSlider = Convert.ToDecimal(answer);
                                    int maxValueSlider = 30;
                                    //int diff = Math.Abs(Convert.ToInt32(challengeValue) - Convert.ToInt32(userAnswer));
                                    //successRate = 100 - Convert.ToInt32((double)diff / (double)maxValue * 100.0);

                                    successRate = Math.Abs(challengeValueSlider - userAnswerSlider) == 0 ? 100 : 100 - (int)((double)Math.Abs(challengeValueSlider - userAnswerSlider) / (double)maxValueSlider * 100.0 * 1.1);
                                    //successRate = preRate;//CheckSliderSuccessRate(challengeValue, userAnswer, minValue, maxValue);

                                }

                            

                                break;
                            case (int)CaptchaType.SecurityOnly:
                            case (int)CaptchaType.PayPerType:
                            case (int)CaptchaType.TypeWords:
                            default:
                                successRate = CheckSuccessRate(request.TCS_REQUEST_CHALLENGE.Challenge_Code.ToLower(), answer.ToLower());

                                break;
                        }
                        request.Success_Rate = successRate;

                        /*
                        // Update campaigns daily used budget and advertiser balance.
                        if (request.TCS_REQUEST_PUBLISHER.Type_Id != (int)CaptchaType.SecurityOnly)
                        {
                            // Update used budget.
                            CampaignBLL.UpdateUsedBudget(dataContext, request.TCS_REQUEST_ADVERTISER.Advertiser_Id, request.TCS_REQUEST_ADVERTISER.Campaign_Id, request.TCS_REQUEST_ADVERTISER.Cpt_Bid);

                            // Update current balance.
                            AdvertiserBLL.UpdateCurrBalance(dataContext, request.TCS_REQUEST_ADVERTISER.Advertiser_Id, request.TCS_REQUEST_ADVERTISER.Cpt_Bid);
                        }
                        */
                        try
                        {
                            // Save changes.
                            dataContext.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            //General.WriteServiceError(requestGuid, "Answer", ApplicationConfiguration.ErrorType.Warning, ex);
                        }
                    }
                }
            }

            // Check if user answered correctly.
            //if (successRate >= ApplicationConfiguration.MIN_SUCCESS_RATE)
            {
                return "true";
            }
            //else
            {
                return "false";
            }
        }

        /// <summary>
        /// Click URL.
        /// </summary>
        /// <param name="requestGuid">Request GUID.</param>
        /// <param name="sessionId">User session id.</param>
        /// <param name="ipAddress">User IP.</param>
        /// <param name="referrerUrl">Reffrer URL.</param>
        /// <returns>Ad URL (to redirect to).</returns>
        public static string Click(string requestGuid, string sessionId, string ipAddress, string referrerUrl, string countryPrefix)
        {            
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                // Get request.
                TCS_REQUEST request = dataContext.TCS_REQUESTs.SingleOrDefault(r => r.Request_Guid == requestGuid);

                // Check if request exists (by GUID).
                if (request == null)
                    return null;

                // Get click data.
                long requestId = request.Request_Id;
                TCS_REQUEST_CLICK click = dataContext.TCS_REQUEST_CLICKs.SingleOrDefault(rc => rc.Request_Id == requestId);

                // Check if click exists.
                if (click == null)
                    return null;

                string linkURL = click.Link_Url;

                // Check if link URL starts with "http".
                if (!linkURL.ToLower().StartsWith("http"))
                    linkURL = "http://" + linkURL;

                // Check if link already clicked before.
                if (click.Is_Clicked == 1)
                {
                    // Get click url.
                    return linkURL;
                }

                // Set clicked indicator.                
                click.Is_Clicked = 1;

                // Set user info.               
                click.Session_Id = sessionId;
                click.Timestamp = DateTime.Now;
                click.IP_Address = ipAddress;
                click.Referrer_Url = referrerUrl;
                if (string.IsNullOrEmpty(countryPrefix))
                {
                    click.Country_Id = null;
                }
                else
                {
                    click.Country_Id = CountryPrefix2CountryId(countryPrefix);
                }
                // Save changes.
                dataContext.SubmitChanges();

                // Get click url.
                return linkURL;
            }
        }

        /// <summary>
        /// Fetch all un-archived and timed out Captcha's requests logs and archive them.
        /// Archive means: 
        /// * Flagged - Log flagged Captcha details.
        /// * Resend - Log resend Captcha details.
        /// * Un-types - Set as timed out.
        /// * Typed - Charge advertiser, credit publisher & get profit.
        /// </summary>
        public static void ArchiveTypes()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                dataContext.Process_ArchiveCaptchaRequests();
            }
        }

        /// <summary>
        /// Run finance process.
        /// </summary>
        public static void FinanceProcess()
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                dataContext.Process_CalcAdvertiserBalance();
            }
        }

        /// <summary>
        /// Check if user agent supports sliders.
        /// </summary>
        /// <param name="userAgent">User agent details.</param>
        /// <returns>Whether sliders available or not.</returns>
        public static bool IsSliderSupport(string userAgent)
        {
            try
            {
                if (string.IsNullOrEmpty(userAgent))
                {
                    return false;
                }
                else
                {
                    userAgent = userAgent.ToLower();

                    // Return true if OS=Windows and browser=Chrome or FireFox.
                    return (userAgent.Contains("windows") && (userAgent.Contains("chrome") || userAgent.Contains("firefox")));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region IP and Country Management

        /// <summary>
        /// Convert IP address to country prefix
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns>country prefix if found, otherwise - empty string</returns>
        public static string Ip2country(string ipAdress)
        {
            try
            {
                string countryCode = IP2Country.Convert(ipAdress);
                if (!string.IsNullOrEmpty(countryCode) && !countryCode.ToUpper().Equals("RD"))
                {
                    return countryCode;
                }
                NLogManager.logger.Warn("Failed to detect the country from the IP:" + ipAdress);
            }
            catch (Exception exp)
            {
                NLogManager.logger.ErrorException("Error in country detection from the IP:" + ipAdress, exp);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the first IP in the context of IP detection.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string ChooseFirstIP(string ip)
        {
            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    return ip;
                }
                else
                {
                    if (ip.Contains(','))
                    {
                        return ip.Split(',')[0];
                    }
                    else
                    {
                        return ip;
                    }
                }
            }
            catch (Exception exp)
            {
                NLogManager.logger.WarnException("Cannot detect IP in multiple IPs string, IP=" + ip, exp);
            }
            return ip;
        }

        #endregion

        #region Collect user data

        public static bool AddUserVisit(long request_Id, UserDataSource userDataSource, string user_Guid)
        {
            try
            {
                using (AdsCaptchaUsersDataDataContext dataContext = new AdsCaptchaUsersDataDataContext())
                {
                    TU_USER_VISIT userVisit = new TU_USER_VISIT();                    
                    userVisit.Request_Id = request_Id;
                    userVisit.Request_Type = userDataSource.ToString();
                    userVisit.User_GUID = user_Guid;

                    // Insert new request.
                    dataContext.TU_USER_VISITs.InsertOnSubmit(userVisit);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
                return true;
            }
            catch (Exception exp)
            {
                NLogManager.logger.ErrorException("Can't save user data into db", exp);
                return false;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if request exist by GUID.
        /// </summary>
        /// <param name="requestGuid">Request GUID to look for.</param>
        /// <returns>Returns whether request exists or not.</returns>
        private static bool IsExist(string requestGuid)
        {
            using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
            {
                return (dataContext.TCS_REQUESTs.Where(item => item.Request_Guid == requestGuid).Count() == 0) ? false : true;
            }
        }

        /// <summary>
        /// Find ads which match to requested Captcha, according to their attributes.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <returns>Returns list of ads which match attributes to requested Captcha.</returns>
        [Obsolete("Not in use")]
        private static TA_AD FindMatching(AdsCaptchaDataContext dataContext, TP_CAPTCHA captcha, string countryPrefix)
        {
            #region Debug - Force matching

            bool debugForceMatching= false;
            int debugForceMatchingForCaptchaId=-1;
            string debugForceMatchingAdIDs=string.Empty;
            try
            {
                debugForceMatching = Convert.ToBoolean(ConfigurationSettings.AppSettings["DebugForceMatching"]);
                debugForceMatchingForCaptchaId = Convert.ToInt32(ConfigurationSettings.AppSettings["DebugForceMatchingForCaptchaId"]);
                debugForceMatchingAdIDs = Convert.ToString(ConfigurationSettings.AppSettings["DebugForceMatchingAdId"]);

                string[] AdIDsArr = debugForceMatchingAdIDs.Split(',');
                List<int> adIDsList = new List<int>();
                foreach (string adID in AdIDsArr)
                {
                    adIDsList.Add(int.Parse(adID));
                }

                if (debugForceMatching && captcha.Captcha_Id == debugForceMatchingForCaptchaId)
                {
                    List<TA_AD> adsList = (from ad in dataContext.GetTable<TA_AD>()
                                           where adIDsList.Contains(ad.Ad_Id)
                                           select ad).ToList<TA_AD>();
                    // If no matching ads were found, then act like it's security only.
                    if (adsList == null || adsList.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        Random random = new Random();
                        int index = random.Next(adsList.Count);
                        return adsList[index];
                    }
                }
            }
            catch (Exception exp)
            {
                NLogManager.logger.ErrorException("Can't force matching",exp);
            }
            #endregion

            // Check if publisher, website and captcha are all running (=active) and Captcha is not security.
            bool isValid = captcha.TP_PUBLISHER.Status_Id == (int)Status.Running &&
                           captcha.TP_WEBSITE.Status_Id == (int)Status.Running &&
                           captcha.Status_Id == (int)Status.Running &&
                           captcha.Type_Id != (int)CaptchaType.SecurityOnly &&
                           captcha.Type_Id != (int)CaptchaType.RandomImage;

            string logMessage = string.Format("FindMatching: captchaId={0}, countryPrefix={1}, captchaType={2}, isValid={3}", captcha.Captcha_Id, countryPrefix, captcha.Type_Id, isValid);
            LogIt(logMessage);

            // Check if captcha exists.
            if (!isValid)
            {
                #region Captcha does NOT exist
                LogIt("Captcha is NOT valid for captchaId=" + captcha.Captcha_Id);
                return null;
                #endregion
            }
            else
            {
                #region Captcha Valid & Exists
                // Check if website can get bonus ads.
                TP_WEBSITE website = captcha.TP_WEBSITE;
                bool allowBonus = website.Allow_Bonus == 1 && website.Total_Revenue < website.Bonus_Limit;

                bool targetByLanguage = true;
                int selectedCountryId = GetCountryID(dataContext, countryPrefix, captcha.Website_Id, out targetByLanguage);

                // Get cache duration.
                int cacheDuration = Convert.ToInt16(ConfigurationSettings.AppSettings["cacheMatchingDuration"]);

                logMessage = string.Format("FindMatching: CaptchaId={0} is VALID, allowBonus={1}, selectedCountryId={2}, cacheDuration={3}", captcha.Captcha_Id, allowBonus, selectedCountryId, cacheDuration);
                LogIt(logMessage);

                // Set general matching query.
                var matching = (from ad in dataContext.GetTable<TA_AD>()
                                from ct in dataContext.GetTable<TA_CAMPAIGN_CATEGORY>()
                                from cn in dataContext.GetTable<TA_CAMPAIGN_COUNTRY>()
                                from cl in dataContext.GetTable<TA_CAMPAIGN_LANGUAGE>()
                                from wt in dataContext.GetTable<TP_WEBSITE_CATEGORY>().Where(w => w.Website_Id == captcha.Website_Id)
                                from wl in dataContext.GetTable<TP_WEBSITE_LANGUAGE>().Where(w => w.Website_Id == captcha.Website_Id)
                                where ad.Status_Id == (int)Status.Running
                                where ad.TA_CAMPAIGN.Status_Id == (int)Status.Running
                                where ad.TA_ADVERTISER.Status_Id == (int)Status.Running
                                where ad.Campaign_Id == ct.Campaign_Id
                                where ad.Campaign_Id == cn.Campaign_Id
                                where ad.Campaign_Id == cl.Campaign_Id
                                where (ct.Category_Id == 0 || wt.Category_Id == ct.Category_Id)
                                where (cl.Language_Id == 0 || ((targetByLanguage ? wl.Language_Id : cl.Language_Id) == cl.Language_Id)) //roei: disable language when using ip detect
                                where (cn.Country_Id == 0 || selectedCountryId == cn.Country_Id) //nati: country mating support
                                where ad.TA_CAMPAIGN.Is_Bonus == (allowBonus ? ad.TA_CAMPAIGN.Is_Bonus : 0)
                                where (decimal)0.9 * ad.TA_CAMPAIGN.Used_Budget + ad.Max_Cpt <= ad.TA_CAMPAIGN.Daily_Budget
                                where (ad.TA_CAMPAIGN.Schedule_Limit == 0 ? true : (ad.TA_CAMPAIGN.Schedule_End_Date >= DateTime.Today ? true : false))
                                where (ad.TA_CAMPAIGN.Schedule_Limit == 0 ? true : (ad.TA_CAMPAIGN.Schedule_Start_Date <= DateTime.Today ? true : false))
                                where (ad.TA_ADVERTISER.Payment_Method_Id != (int)PaymentMethod.CreditCard || (ad.TA_ADVERTISER.Payment_Method_Id == (int)PaymentMethod.CreditCard && ad.TA_ADVERTISER.TA_ADVERTISER_BILLING.Card_Id != "0")) 
                                where (ad.TA_ADVERTISER.Billing_Method_Id == (int)BillingMethod.Postpay || (ad.TA_ADVERTISER.Billing_Method_Id == (int)BillingMethod.Prepay && (decimal)0.9 * ad.TA_ADVERTISER.Curr_Balance >= ad.Max_Cpt))
                                select ad);
                LogIt("FindMatching: Finished 'general matching query' for captchaId " + captcha.Captcha_Id);

                var tempmatch = matching.Distinct().FromCache(cacheDuration).ToList<TA_AD>();

                // Get matching ads.
                switch (captcha.Type_Id)
                {
                    // Pay-Per-Type captcha.
                    case (int)CaptchaType.PayPerType:
                    case (int)CaptchaType.TypeWords:
                        matching = (from ad in matching
                                    where ( (captcha.Allow_Click == 1 && (ad.Type_Id == (int)AdType.YbrantBanner || ad.Ad_Url != null)) ||
                                            (ad.Ad_Url == null && ad.Type_Id != (int)AdType.YbrantBanner) )
                                    where ad.Type_Id != (int)AdType.Slide2Fit
                                    where ( (captcha.Allow_Slogan == 1 && ad.Type_Id == (int)AdType.SloganOnly ? true : false) ||
                                            (captcha.Allow_Image == 1 && (ad.Type_Id == (int)AdType.SloganAndImage || ad.Type_Id == (int)AdType.YbrantBanner) ? true : false) ||
                                            (captcha.Allow_Video == 1 && ad.Type_Id == (int)AdType.SloganAndVideo ? true : false) )
                                    where ( (ad.Width == captcha.Max_Width && ad.Height == captcha.Max_Height) ||
                                             ad.Type_Id == (int)AdType.SloganOnly || ad.Type_Id == (int)AdType.YbrantBanner) // check dims if NOT slogan or banner
                                    select ad);
                        LogIt("FindMatching: Updated matching query for PayPerType captchaId=" + captcha.Captcha_Id);
                        break;

                    // Slider captcha.
                    case (int)CaptchaType.Slider:
                    case (int)CaptchaType.SlideToFit:
                        matching = (from ad in matching
                                    where ad.Type_Id == (int)AdType.Slide2Fit
                                    where ad.Width == captcha.Max_Width
                                    where ad.Height == captcha.Max_Height
                                    select ad);
                        LogIt("FindMatching: Updated matching query for Slider captchaId=" + captcha.Captcha_Id);
                        break;
                }

                // Get final matching ads list.
                List<TA_AD> adsList = new List<TA_AD>();
                adsList = matching.Distinct().FromCache(cacheDuration).ToList<TA_AD>();

                // If no matching ads were found, then act like it's security only.
                if (adsList == null || adsList.Count == 0)
                {
                    LogIt("FindMatching: No adsList for captchaId = "+captcha.Captcha_Id);
                    return null;
                }

                // Get max cpt.
                decimal maxCpt = adsList.Max(i => i.Max_Cpt);

                // Take top ads to randomize.
                int numOfMatchAds = Convert.ToInt16(ConfigurationSettings.AppSettings["numOfMatchAds"]);

                logMessage = string.Format("FindMatching: Final Matching ad list for captchaId={0} contains {1} ads, with maxCpt={2} and numOfMatchAds={3}", captcha.Captcha_Id, adsList.Count, maxCpt, numOfMatchAds);
                LogIt(logMessage);

                // Calculate rank.
                var rank1 = (from ad in adsList 
                             select new { Ad = ad, Rank = 
                                                (ad.Max_Cpt == 0 ? 0 : Math.Round(ad.Max_Cpt / maxCpt * 100, 0) * (decimal)0.85) +
                                                ((ad.Add_Date - DateTime.Today).Hours < 24 ? 100 : 0) * (decimal)0.15
                             }).OrderByDescending(i => i.Rank).Take(numOfMatchAds);

                // Get total rank.
                decimal totalRank = rank1.Sum(i => i.Rank);

                int fromValue = 0;
                var rank2 = (from rnk in rank1
                             select new { Ad = rnk.Ad,                                               
                                          From = fromValue,
                                          To = (fromValue += (int)(rnk.Rank / totalRank * 100))
                                        } ).ToList();

                // Get max value.
                int minValue = rank2.Min(i => i.From);
                int maxValue = rank2.Max(i => i.To);

                // Randomize index.
                int idx = random.Next(minValue, maxValue);
                
                TA_AD selectedAd = rank2.Where(i => i.From <= idx && i.To > idx).SingleOrDefault().Ad;

                // Return matching ad.
                logMessage = string.Format("FindMatching RESULT: For captchaId={0}, totalRank={1}, selectedAd={2}", captcha.Captcha_Id, totalRank, (selectedAd == null ? "NULL" : selectedAd.Ad_Id.ToString()));
                LogIt(logMessage);

                return selectedAd;
                #endregion
            }
        }

        /// <summary>
        /// Calculates success rate.
        /// </summary>
        /// <param name="challengeText">Captcha challenge text.</param>
        /// <param name="userText">User answer text.</param>
        /// <returns>Success rate (0-100).</returns>
        public static int CheckSuccessRate(string challengeText, string userText)
        {
            try
            {
                // TODO: Calculate actual rate.
                challengeText = RemoveChars(challengeText);
                userText = RemoveChars(userText);

                if (userText.Equals(challengeText))
                {
                    return 100;
                }
                else
                {
                    int correctChars = 0;
                    for (int i = 0; i < userText.Length; i++)
                    {
                        string userChar = userText.Substring(i, 1);
                        string challengeChar = challengeText.Substring(i, 1);

                        if (userChar == challengeChar)
                            correctChars++;
                        else
                            break;
                    }
                    for (int i = 0; i < userText.Length && i < challengeText.Length; i++)
                    {
                        string userChar = userText.Substring(userText.Length - (i + 1), 1);
                        string challengeChar = challengeText.Substring(challengeText.Length - (i + 1), 1);

                        if (userChar == challengeChar)
                            correctChars++;
                        else
                            break;
                    }

                    // Check answer accuracity.
                    int successRate = (int)(100 * correctChars / challengeText.Length);
                    return successRate;
                }
            }
            catch
            {
                if (userText.Equals(challengeText))
                    return 100;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Calculates success rate (for sliders).
        /// </summary>
        /// <param name="challengeValue">Captcha challenge value.</param>
        /// <param name="userAnswer">User answer value.</param>
        /// <param name="minValue">Min value.</param>
        /// <param name="maxValue">Max value.</param>
        /// <returns></returns>
        [Obsolete("Moved to  AdsCaptcha.Managers.CaptchasManager")]
        public static int CheckSliderSuccessRate(decimal challengeValue, decimal userAnswer, int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            decimal a = challengeValue + userAnswer;
            
            decimal diff = Math.Min(Math.Abs(a - minValue), Math.Abs(maxValue - a));
            decimal failurePCT = (100 * diff / range);

            decimal allowedThreshold = Convert.ToDecimal(ConfigurationSettings.AppSettings["sliderThreshold"]);
            decimal successRate = (diff <= allowedThreshold ? 100 - failurePCT : 0);

            return (int)successRate;
        }

        /// <summary>
        /// Remove "ignorable" chars.
        /// </summary>
        /// <param name="text">Text to be maniulated.</param>
        /// <returns></returns>
        private static string RemoveChars(string text)
        {
            string ignoredChars = " !?'.,@$&%-";

            for (int i = 0; i < ignoredChars.Length; i++)
            {
                string curr = ignoredChars.Substring(i, 1);
                text = text.Replace(curr, "");
            }
            text = text.ToLower();

            return text;
        }

        /// <summary>
        /// Charge advertiser.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="requestId">Request id.</param>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="adType">Ad type.</param>
        /// <param name="date">Type date.</param>
        /// <param name="chargeAmmount">Ammount to charge.</param>
        private static void ChargeAdvertiser(AdsCaptchaDataContext dataContext, long requestId, int advertiserId, int campaignId, int adId, int adType, DateTime date, decimal chargeAmmount)
        {
            TA_CHARGE charge = new TA_CHARGE();

            // Create new charge record.
            charge.Request_Id = requestId;
            charge.Advertiser_Id = advertiserId;
            charge.Campaign_Id = campaignId;
            charge.Ad_Id = adId;
            charge.Ad_Type = adType;
            charge.Date = date;
            charge.Types = 1;
            charge.Charges = chargeAmmount;

            // Insert record to charge stats table.
            dataContext.TA_CHARGEs.InsertOnSubmit(charge);
        }

        /// <summary>
        /// Credit publisher.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="requestId">Request id.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="captchaType">Captcha type.</param>
        /// <param name="date">Type date.</param>
        /// <param name="creditAmmount">Ammount to credit.</param>
        private static void CreditPublisher(AdsCaptchaDataContext dataContext, long requestId, int publisherId, int websiteId, int captchaId, int captchaType, DateTime date, decimal creditAmmount)
        {
            TP_EARNING earning = new TP_EARNING();

            // Create new earning record.
            earning.Request_Id = requestId;
            earning.Publisher_Id = publisherId;
            earning.Website_Id = websiteId;
            earning.Captcha_Id = captchaId;
            earning.Captcha_Type_Id = captchaType;
            earning.Date = date;
            earning.Types = 1;
            earning.Earnings = creditAmmount;

            // Insert record to earnings table.
            dataContext.TP_EARNINGs.InsertOnSubmit(earning);
        }

        /// <summary>
        /// Credit developer.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="requestId">Request id.</param>
        /// <param name="developerId">developer id.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="captchaType">Captcha type.</param>
        /// <param name="date">Type date.</param>
        /// <param name="creditAmmount">Ammount to credit.</param>
        private static void CreditDeveloper(AdsCaptchaDataContext dataContext, long requestId, int developerId, int publisherId, int websiteId, int captchaId, int captchaType, DateTime date, decimal creditAmmount)
        {
            TD_EARNING earning = new TD_EARNING();

            // Create new earning record.
            earning.Request_Id = requestId;
            earning.Developer_Id = developerId;
            earning.Publisher_Id = publisherId;
            earning.Website_Id = websiteId;
            earning.Captcha_Id = captchaId;
            earning.Captcha_Type_Id = captchaType;
            earning.Date = date;
            earning.Types = 1;
            earning.Earnings = creditAmmount;

            // Insert record to earnings table.
            dataContext.TD_EARNINGs.InsertOnSubmit(earning);
        }

        /// <summary>
        /// Log AdsCaptcha profit.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="requestId">Request id.</param>
        /// <param name="date">Type date.</param>
        /// <param name="profitAmmount">Profit ammount.</param>
        private static void SetProfit(AdsCaptchaDataContext dataContext, long requestId, DateTime date, decimal profitAmmount)
        {
            // TODO: Log profit
        }

        /// <summary>
        /// Generates random GUID string.
        /// </summary>
        /// <returns>GUID string.</returns>
        private static string GenerateGuid()
        {
            // Generate GUID.
            string guid = System.Guid.NewGuid().ToString();

            return guid;
        }

        /// <summary>
        /// Generates random challenge code according to security level.
        /// </summary>
        /// <param name="securityLevel">Security level.</param>
        /// <returns>Random challenge code.</returns>
        [Obsolete("Not in use", true)]
        public static string GenerateCode(int typeId, int securityLevel)
        {
            int numOfDigits;
            securityLevel = securityLevel - (int)DecodeTables.SecurityLevel * 1000;

            // Check type.
            switch (typeId)
            {
                case (int)CaptchaType.PayPerType:
                
                    numOfDigits = securityLevel - 1;
                    break;
                case (int)CaptchaType.TypeWords:
                case (int)CaptchaType.SecurityOnly:
                    numOfDigits = securityLevel + 3;
                    break;
                default:
                    numOfDigits = 5;
                    break;
            }

            string s = "";
            Random random = new Random(DateTime.Now.Millisecond);

            // Generate random text.
            string chars = "";
            chars += "0123456789";
            //chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //chars += "abcdefghijklmnopqrstuvwxyz";
            char[] array = chars.ToCharArray();

            int index;
            for (int i = 0; i < numOfDigits; i++)
            {
                index = random.Next(array.Length - 1);
                s += array[index].ToString();
            }

            return s;
        }

        /// <summary>
        /// Get the correct country ID from auto detection or from Site Owner's definition
        /// </summary>
        private static int GetCountryID(AdsCaptchaDataContext dataContext, string countryPrefix, int websiteId, out bool targetByLanguage)
        {
            T_COUNTRY country = ((CountryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Country)).GetCountry(countryPrefix);

            // If country was not detected automatically - use language targeting
            bool countryDetectedByIP = country != null;
            targetByLanguage = (countryDetectedByIP ? false : true);

            int? selectedCountryId = country == null ? dataContext.GetTable<TP_WEBSITE_COUNTRY>().Where(w => w.Website_Id == websiteId).SingleOrDefault().Country_Id : country.Country_Id;
            if (selectedCountryId == null)
            {
                //NLogManager.logger.Warn("Country was not detected neither by IP detection nor by site owner. websiteID=" + websiteId);
                return -1;
            }
            else
            {
                return (int)selectedCountryId;
            }
        }

        private static int? CountryPrefix2CountryId(string countryPrefix)
        {
            T_COUNTRY country = ((CountryCacheManager)CacheCoreManager.GetInstanse().GetCacheManager(CacheTypes.Country)).GetCountry(countryPrefix);

            if (country == null)
            {
                //NLogManager.logger.Warn("CountryPrefix2CountryId: Country was not detected neither by IP detection nor by site owner.");
                return null;
            }
            else
            {
                return country.Country_Id;
            }
        }

        /// <summary>
        /// Generate slider data and image attributes.
        /// </summary>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="data">Slider data (output).</param>
        /// <returns>Challange random puzzle solution.</returns>
        [Obsolete("Moved to AdsCaptcha.dll")]
        public static string GenerateSliderData(int width, int height, out SliderData data)
        {
            // Random game type.
            int gameType = random.Next(1, 3);
            int tmp;
            int minDelta;
            int rndChallenge = 0;

            data = new SliderData();

            data.Width = width;
            data.Height = height;

            switch (gameType)
            {
                case 2:
                    data.Type = 2;
                    tmp = random.Next(-15, 15);
                    data.X = 0;
                    data.Y = height / 3 + tmp;
                    data.W = width;
                    data.H = height / 3 + tmp;
                    data.Min = 0;
                    data.Max = width;
                    minDelta = (int)(data.Max * 0.1);
                    rndChallenge = random.Next(data.Min + minDelta, data.Max - minDelta);
                    break;
                case 1:
                default:
                    data.Type = 1;
                    tmp = random.Next(15, 30);
                    data.X = tmp;
                    data.Y = tmp;
                    data.W = width - (tmp * 2);
                    data.H = height - (tmp * 2);
                    data.Min = 0;
                    data.Max = 360;
                    minDelta = (int)(data.Max * 0.1);
                    rndChallenge = random.Next(data.Min + minDelta, data.Max - minDelta);
                    break;
            }

            return rndChallenge.ToString();
        }

        /// <summary>
        /// Calculate rev. share according to bonus.
        /// </summary>
        /// <param name="revShare">Rev. share.</param>
        /// <param name="allowBonus">Allow bonus indicator.</param>
        /// <param name="toalRevenue">Total (all-time) revenue.</param>
        /// <param name="bonusLimit">Bonus limitation.</param>
        /// <returns>Calculated rev. share.</returns>
        private static int CalcRevShare(int revShare, int allowBonus, decimal toalRevenue, int bonusLimit)
        {
            int revenueSharePct;

            if (allowBonus == 1 && toalRevenue <= bonusLimit)
            {
                revenueSharePct = (toalRevenue < bonusLimit / 2 ? 100 : 75);
            }
            else
            {
                revenueSharePct = revShare;
            }

            return revenueSharePct;
        }

        #endregion

        #region Helper Methods
        private static bool LogIt(string message, string logUniqueId)
        {
            loggingUniqueId = logUniqueId;
            return LogIt(message);
        }

        private static bool LogIt(string message)
        {
            return NLogManager.LogDebug(message, "CaptchaServerBLL", loggingUniqueId);
        }
        #endregion
    }

    [Obsolete("Not in use", true)]
    public class ChallengeData
    {
        public string Challenge;
        public Nullable<int> SecurityLevel;
        public Constants.Direction Direction;
        public string BackColor;
        public string ForeColor;

        [Obsolete("Not In Use", true)]
        public ChallengeData()
        {
            Challenge = null;
            SecurityLevel = null;
            Direction = Constants.Direction.LeftToRight;
            BackColor = "#FFFFFF";
            ForeColor = "#707070";
        }
    }
}