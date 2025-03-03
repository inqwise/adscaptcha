using System;
using System.Collections;
using System.Linq;
using Inqwise.AdsCaptcha.BLL.Common;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class ReportsBLL
    {
        #region Public Methods

        public static IEnumerable SiteOwners(int publisherId, int publisherStatusId, string publisherEmail,
                                             int websiteId, int websiteStatusId, int websiteCategoryId, string websiteUrl, int addDateDiff,
                                             int captchaId, int captchaStatusId, int captchaTypeId,
                                             int developerId, int developerStatusId, string developerEmail, 
                                             bool checkFromDate, DateTime fromDate, bool checkToDate, DateTime toDate,
                                             int countryId, 
                                             string orderBy, bool isDesc, int numOfRows)
        {
            using (var dataContext = new AdsCaptchaDataContext())
            {
                // Set timeout.
                dataContext.CommandTimeout = 3600;

                // Get all tables (before filtering and joining).
                var developers = (from d in dataContext.TD_DEVELOPERs select d);
                var publishers = (from p in dataContext.TP_PUBLISHERs select p);
                var websites = (from w in dataContext.TP_WEBSITEs select w);
                var captchas = (from c in dataContext.TP_CAPTCHAs select c);
                
                var prestats = (from s in dataContext.TCS_STATs
                                join c in captchas
                                on s.Captcha_Id equals c.Captcha_Id
                                where ((countryId <= 0) || (s.Country_Id == countryId))
                                group s by new
                                {
                                    s.Date,
                                    s.Developer_Id,
                                    s.Publisher_Id,
                                    s.Website_Id,
                                    s.Captcha_Id,
                                    //s.Captcha_Type_Id,
                                    //s.Security_Level_Id,
                                    c.IsCommercial
                                } into st
                                select new
                                {
                                    st.Key.Date,
                                    st.Key.Developer_Id,
                                    st.Key.Publisher_Id,
                                    st.Key.Website_Id,
                                    st.Key.Captcha_Id,
                                    //st.Key.Captcha_Type_Id,
                                    //st.Key.Security_Level_Id,
                                    SecurityOnly = st.Where(w => w.Captcha_Type_Id == (int)CaptchaType.SecurityOnly ||
                                                                w.Captcha_Type_Id == (int)CaptchaType.RandomImage ||
                                                                (bool)st.Key.IsCommercial).Sum(s => (int?)s.Types),
                                    Clickable = st.Sum(s => (int?)s.Clickable),
                                    Types = st.Sum(s => (int?)s.Types),
                                    Correct = st.Sum(s => (int?)s.Correct),
                                    Served = st.Sum(s => (int?)s.Served),
                                    Publisher_Earnings = st.Sum(s => (int?)s.Publisher_Earnings),
                                    Developer_Earnings = st.Sum(s => (int?)s.Developer_Earnings),
                                    Advertiser_Charges = st.Sum(s => (int?)s.Advertiser_Charges),
                                    Profits = st.Sum(s => (int?)s.Profits),
                                    Views = st.Sum(s => (int?)s.Views)
                                });

                var stats = (from s in prestats
                             join r in dataContext.T_REPORTS_PUBLISHER_REPORTMAINs
                             on new { s.Publisher_Id, s.Website_Id, s.Captcha_Id, s.Date } equals new { Publisher_Id = r.PublisherId, Website_Id = r.WebsiteId, Captcha_Id = r.CaptchaId, Date = r.ReportDate }

                             select new
                             {
                                 //s.Date,
                                 ////s.Served,
                                 ////s.Types,
                                 ////s.Correct,
                                 //s.Clickable,
                                 ////s.Clicks,
                                 //s.Country_Id,
                                 //s.Developer_Id,
                                 //s.Publisher_Id,
                                 //s.Website_Id,
                                 //s.Captcha_Id,
                                 //s.Captcha_Type_Id,
                                 //s.Security_Level_Id,
                                 //s.Advertiser_Id,
                                 //s.Campaign_Id,
                                 //s.Ad_Id,
                                 //s.Ad_Type_Id,
                                 ////s.Publisher_Earnings,
                                 //s.Developer_Earnings,
                                 //s.Advertiser_Charges,
                                 //s.Profits,
                                 //s.Views,

                                 s.Date,
                                 s.Clickable,
                                 s.Developer_Id,
                                 s.Publisher_Id,
                                 s.Website_Id,
                                 s.Captcha_Id,
                                 //s.Captcha_Type_Id,
                                 //s.Security_Level_Id,
                                 s.SecurityOnly,
                                 s.Developer_Earnings,
                                 s.Advertiser_Charges,
                                 s.Profits,
                                 s.Views,

                                 r.Served,
                                 Clicks = r.Clicked,
                                 Correct = r.Fitted,
                                 Publisher_Earnings = r.Earning,
                                 r.AverageSuccessRate,
                                 Types = r.Typed
                             });
                var reports_pub = (from r in dataContext.T_REPORTS_PUBLISHER_REPORTMAINs select r);

                // Filter by publisher ID.
                if (publisherId > 0)
                {
                    publishers = from p in publishers
                                 where p.Publisher_Id == publisherId
                                 select p;
                }
                // Filter by publisher status.
                if (publisherStatusId > 0)
                {
                    publishers = from p in publishers
                                 where p.Status_Id == publisherStatusId
                                 select p;
                }
                // Filter by publisher email.
                if (!String.IsNullOrEmpty(publisherEmail))
                {
                    publishers = from p in publishers
                                 where p.Email.ToLower().Contains(publisherEmail.ToLower())
                                 select p;
                }
                // Filter by website ID.
                if (websiteId > 0)
                {
                    websites = from w in websites
                               where w.Website_Id == websiteId
                               select w;
                }
                // Filter by website status.
                if (websiteStatusId > 0)
                {
                    websites = from w in websites
                               where w.Status_Id == websiteStatusId
                               select w;
                }
                // Filter by website category.
                if (websiteCategoryId > 0)
                {
                    websites = from w in websites
                               join t in dataContext.TP_WEBSITE_CATEGORies on w.Website_Id equals t.Website_Id
                               where t.Category_Id == websiteCategoryId
                               select w;
                }
                // Filter by website url.
                if (!String.IsNullOrEmpty(websiteUrl))
                {
                    //System.Text.Encoding.ASCII
                    websites = from w in websites
                               where w.Url.ToLower().Contains(websiteUrl.ToLower())
                               select w;


                }
                // Filter by website added day.
                if (addDateDiff > 0)
                {
                    websites = from w in websites
                               where w.Add_Date >= DateTime.Today.AddDays(-addDateDiff)
                               select w;
                }
                // Filter by Captcha ID.
                if (captchaId > 0)
                {
                    captchas = from c in captchas
                               where c.Captcha_Id == captchaId
                               select c;
                }
                // Filter by Captcha status.
                if (captchaStatusId > 0)
                {
                    captchas = from c in captchas
                               where c.Status_Id == captchaStatusId
                               select c;
                }
                // Filter by Captcha type.
                if (captchaTypeId > 0)
                {
                    captchas = from c in captchas
                               where c.Type_Id == captchaTypeId
                               select c;
                }
                // Filter by developer ID.
                if (developerId > 0)
                {
                    publishers = from p in publishers 
                                 where p.Developer_Id == developerId
                                 select p;
                }
                // Filter by developer status.
                if (developerStatusId > 0)
                {
                    publishers = from p in publishers
                                 from d in (from d in developers where d.Developer_Id == p.Developer_Id && d.Status_Id == developerStatusId select d)
                                 select p;
                }
                // Filter by developer email.
                if (!String.IsNullOrEmpty(developerEmail))
                {
                    publishers = from p in publishers
                                 from d in (from d in developers where d.Developer_Id == p.Developer_Id && d.Email.Contains(developerEmail) select d)
                                 select p;
                }
                // Filter by (from) date.
                if (checkFromDate)
                {
                    stats = from s in stats
                            where s.Date >= fromDate
                            select s;
                }
                // Filter by (to) date.
                if (checkToDate)
                {
                    stats = from s in stats
                            where s.Date <= toDate
                            select s;
                }
                // Filter by country.
                //if (countryId > 0)
                //{
                //    stats = from s in stats
                //            where s.Country_Id == countryId
                //            select s;
                //}

                // Get meta data.
                var data = from p in publishers
                           join w in websites on p.Publisher_Id equals w.Publisher_Id
                           join c in captchas on w.Website_Id equals c.Website_Id
                           from d in (from d in developers where p.Developer_Id == d.Developer_Id select d).DefaultIfEmpty()
                           from pm in (from pm in dataContext.T_DECODEs where p.Credit_Method_Id == pm.Item_Id select pm).DefaultIfEmpty()
                           join ct in dataContext.T_DECODEs on c.Type_Id equals ct.Item_Id
                           let Publisher_Name = (p.First_Name == null ? "" : p.First_Name) + " " + (p.Last_Name == null ? "" : p.Last_Name)
                           let Is_Bonus = (w.Allow_Bonus == 1 && w.Bonus_Limit < w.Total_Revenue) ? "Yes" : null
                           let Dimensions = c.Max_Width.ToString() + 'x' + c.Max_Height.ToString()
                           let Pop_Under = c.Allow_Popup == 1 ? "Yes" : null
                           let Security_Level = c.Security_Level_Id - (int)DecodeTables.SecurityLevel * 1000
                           let Developer = d == null ? null : "Yes"
                           let Developer_Id = (int?)d.Developer_Id
                           let Developer_Name = (d == null ? null : d.First_Name + ' ' + d.Last_Name)
                           let Developer_Rev_Share = (int?)d.Revenue_Share_Pct
                           select new
                           {
                               p.Publisher_Id,
                               Publisher_Email = p.Email,
                               Publisher_Name,
                               Publisher_Status_Id = p.Status_Id,
                               Publisher_Rev_Share = p.Revenue_Share_Pct,
                               Payment_Method = pm.Item_Desc,
                               w.Website_Id,
                               w.Url,
                               Website_Status_Id = w.Status_Id,
                               Is_Bonus,
                               w.Add_Date,
                               c.Captcha_Id,
                               Captcha_Status_Id = c.Status_Id,
                               Captcha_Type = ct.Item_Desc,
                               Pop_Under,
                               Dimensions,
                               Security_Level,
                               Developer,
                               Developer_Id,
                               Developer_Email = d.Email,
                               Developer_Name,
                               Developer_Rev_Share
                           };

                // Get and group by statistics data.
                var grp = from s in stats
                          join p in publishers on s.Publisher_Id equals p.Publisher_Id
                          join w in websites on s.Website_Id equals w.Website_Id
                          join c in captchas on s.Captcha_Id equals c.Captcha_Id
                          group s by new { s.Publisher_Id, s.Website_Id, s.Captcha_Id } into g
                          select new
                          {
                              g.Key.Publisher_Id,
                              g.Key.Website_Id,
                              g.Key.Captcha_Id,
                              Served = g.Sum(s => (int?)s.Served),
                              TotalTyped = g.Sum(s => (int?)s.Types),
                              Security = g.Sum(s => (int?)s.SecurityOnly), //g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.SecurityOnly || w.Captcha_Type_Id == (int)CaptchaType.RandomImage).Sum(s => (int?)s.Types),
                              Typed = g.Sum(s => (int?)s.Types),//g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.PayPerType).Sum(s => (int?)s.Types),
                              Fit = g.Sum(s => (int?)s.Correct), //g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.Slider).Sum(s => (int?)s.Types),
                              Clickable = g.Sum(s => (int?)s.Clickable),
                              Clicks = g.Sum(s => (int?)s.Clicks),
                              Publisher_Earnings = g.Sum(s => (decimal?)s.Publisher_Earnings),
                              Developer_Earnings = g.Sum(s => (decimal?)s.Developer_Earnings),
                              Profits = g.Sum(s => (decimal?)s.Profits)
                          };

                // Build final query.
                var query = from d in data
                            from g in (from g in grp where d.Publisher_Id == g.Publisher_Id && d.Website_Id == g.Website_Id && d.Captcha_Id == g.Captcha_Id select g).DefaultIfEmpty()
                            let Served = g.Served == null ? 0 : g.Served
                            let TotalTyped = g.TotalTyped == null ? 0 : g.TotalTyped
                            let Security = g.Security == null ? 0 : g.Security
                            let Typed = g.Typed == null ? 0 : g.Typed
                            let Fits = g.Fit == null ? 0 : g.Fit
                            let Clickable = g.Clickable == null ? 0 : g.Clickable
                            let Clicked = g.Clicks == null ? 0 : g.Clicks
                            let CTR = g == null ? 0 : (Clickable == 0 ? 0 : Clicked / Clickable)
                            let CTTR = g == null ? 0 : (TotalTyped == 0 ? 0 : Clicked / TotalTyped)
                            let eCTTR = g == null ? 0 : (Served == 0 || TotalTyped == 0 ? 0 : CTTR / Served / TotalTyped)
                            let eCPM = g == null ? 0 : (Served == 0 ? 0 : g.Publisher_Earnings / Served * 1000)
                            let Publisher_Earnings = g == null ? 0 : g.Publisher_Earnings
                            let Developer_Earnings = g == null ? 0 : g.Developer_Earnings
                            let Profits = g == null ? 0 : g.Profits
                            select new
                            {
                                d.Publisher_Id,
                                d.Publisher_Status_Id,
                                d.Publisher_Email,
                                d.Publisher_Name,
                                d.Publisher_Rev_Share,
                                d.Payment_Method,
                                d.Website_Id,
                                d.Website_Status_Id,
                                d.Url,
                                d.Is_Bonus,
                                d.Add_Date,
                                d.Captcha_Id,
                                d.Captcha_Status_Id,
                                d.Captcha_Type,
                                d.Pop_Under,
                                d.Dimensions,
                                d.Security_Level,
                                d.Developer,
                                d.Developer_Id,
                                d.Developer_Email,
                                d.Developer_Name,
                                d.Developer_Rev_Share,
                                Served,
                                Security,
                                Typed,
                                Fits,
                                Clickable,
                                Clicked,
                                CTR,
                                CTTR,
                                eCTTR,
                                eCPM,
                                Publisher_Earnings,
                                Developer_Earnings,
                                Profits
                            };

                // Sort query.
                query = OrderExtension.OrderBy(query, orderBy, isDesc);

                // Take limited num. of rows.
                if (numOfRows > 0)
                    query = query.Take(numOfRows);

                return query.ToList();
            }
        }

        public static IEnumerable Advertisers(int advertiserId, int advertiserStatusId, string advertiserEmail,
                                              int campaignId, int campaignStatusId, int campaignCategoryId, int addDateDiff,
                                              int adId, int adStatusId, int adTypeId,
                                              bool checkFromDate, DateTime fromDate, bool checkToDate, DateTime toDate,
                                              int countryId,
                                              string orderBy, bool isDesc, int numOfRows)
        {
            using (var dataContext = new AdsCaptchaDataContext())
            {
                // Set timeout.
                dataContext.CommandTimeout = 3600;

                // Get all tables (before filtering and joining).
                var advertisers = (from a in dataContext.TA_ADVERTISERs select a);
                var campaigns = (from c in dataContext.TA_CAMPAIGNs select c);
                var ads = (from d in dataContext.TA_ADs select d);
                var stats = (from s in dataContext.TCS_STATs select s);

                // Filter by advertiser ID.
                if (advertiserId > 0)
                {
                    advertisers = from a in advertisers
                                  where a.Advertiser_Id == advertiserId
                                  select a;
                }
                // Filter by advertiser status.
                if (advertiserStatusId > 0)
                {
                    advertisers = from a in advertisers
                                  where a.Status_Id == advertiserStatusId
                                  select a;
                }
                // Filter by advertiser email.
                if (!String.IsNullOrEmpty(advertiserEmail))
                {
                    advertisers = from a in advertisers
                                  where a.Email.Contains(advertiserEmail)
                                  select a;
                }
                // Filter by campaign ID.
                if (campaignId > 0)
                {
                    campaigns = from c in campaigns
                                where c.Campaign_Id == campaignId
                                select c;
                }
                // Filter by campaign status.
                if (campaignStatusId > 0)
                {
                    campaigns = from c in campaigns
                                where c.Status_Id == campaignStatusId
                                select c;
                }
                // Filter by campaign category.
                if (campaignCategoryId > 0)
                {
                    campaigns = from c in campaigns
                                join t in dataContext.TA_CAMPAIGN_CATEGORies on c.Campaign_Id equals t.Campaign_Id
                                where t.Category_Id == campaignCategoryId
                                select c;
                }
                // Filter by campaign added day.
                if (addDateDiff > 0)
                {
                    campaigns = from c in campaigns
                                where c.Add_Date >= DateTime.Today.AddDays(-addDateDiff)
                                select c;
                }
                // Filter by ad ID.
                if (adId > 0)
                {
                    ads = from d in ads
                          where d.Ad_Id == adId
                          select d;
                }
                // Filter by ad status.
                if (adStatusId > 0)
                {
                    ads = from d in ads
                          where d.Status_Id == adStatusId
                          select d;
                }
                // Filter by ad type.
                if (adTypeId > 0)
                {
                    ads = from d in ads
                          where d.Type_Id == adTypeId
                          select d;
                }
                // Filter by (from) date.
                if (checkFromDate)
                {
                    stats = from s in stats
                            where s.Date >= fromDate
                            select s;
                }
                // Filter by (to) date.
                if (checkToDate)
                {
                    stats = from s in stats
                            where s.Date <= toDate
                            select s;
                }
                // Filter by country.
                if (countryId > 0)
                {
                    stats = from s in stats
                            where s.Country_Id == countryId
                            select s;
                }

                // Get meta data.
                var data = from a in advertisers
                           join c in campaigns on a.Advertiser_Id equals c.Advertiser_Id
                           join d in ads on c.Campaign_Id equals d.Campaign_Id
                           join pm in dataContext.T_DECODEs on a.Payment_Method_Id equals pm.Item_Id
                           join at in dataContext.T_DECODEs on d.Type_Id equals at.Item_Id
                           let Is_Bonus = c.Is_Bonus == 1 ? "Yes" : null
                           let Ad_Click = d.Ad_Url != null ? "Yes" : null
                           let Ad_Dimensions = d.Width.ToString() + 'x' + d.Height.ToString()
                           select new
                           {
                               a.Advertiser_Id,
                               Advertiser_Email = a.Email,
                               Advertiser_Status_Id = a.Status_Id,
                               Payment_Method = pm.Item_Desc,
                               c.Campaign_Id,
                               c.Campaign_Name,
                               Campaign_Status_Id = c.Status_Id,
                               Is_Bonus,
                               c.Add_Date,
                               d.Ad_Id,
                               Ad_Status_Id = d.Status_Id,
                               Ad_Type = at.Item_Desc,
                               d.Ad_Image,
                               d.Ad_Slogan,
                               Ad_Bid = d.Max_Cpt,
                               Ad_Click,
                               Ad_Dimensions
                           };

                // Get and group by statistics data.
                var grp = from s in stats
                          join a in advertisers on s.Advertiser_Id equals a.Advertiser_Id
                          join c in campaigns on s.Campaign_Id equals c.Campaign_Id 
                          join d in ads on s.Ad_Id equals d.Ad_Id
                          group s by new { s.Advertiser_Id, s.Campaign_Id, s.Ad_Id } into g
                          select new
                          {
                              g.Key.Advertiser_Id,
                              g.Key.Campaign_Id,
                              g.Key.Ad_Id,
                              Served = g.Sum(s => (int?)s.Served),
                              TotalTyped = g.Sum(s => (int?)s.Types),
                              Typed = g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.PayPerType).Sum(s => (int?)s.Types),
                              Fit = g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.Slider).Sum(s => (int?)s.Types),
                              Clickable = g.Sum(s => (int?)s.Clickable),
                              Clicks = g.Sum(s => (int?)s.Clicks),
                              Advertiser_Charges = g.Sum(s => (decimal?)s.Advertiser_Charges)
                          };

                // Build final query.
                var query = from d in data
                            from g in
                                (from g in grp where d.Advertiser_Id == g.Advertiser_Id && d.Campaign_Id == g.Campaign_Id && d.Ad_Id == g.Ad_Id select g).DefaultIfEmpty()
                            let Served = g.Served == null ? 0 : g.Served
                            let TotalTyped = g.TotalTyped == null ? 0 : g.TotalTyped
                            let Typed = g.Typed == null ? 0 : g.Typed
                            let Fits = g.Fit == null ? 0 : g.Fit
                            let Clickable = g.Clickable == null ? 0 : g.Clickable
                            let Clicked = g.Clicks == null ? 0 : g.Clicks
                            let CTR = g == null ? 0 : (Clickable == 0 ? 0 : Clicked / Clickable)
                            let CTTR = g == null ? 0 : (TotalTyped == 0 ? 0 : Clicked / TotalTyped)
                            let eCTTR = g == null ? 0 : (Served == 0 || TotalTyped == 0 ? 0 : CTTR / Served / TotalTyped)
                            let eCPM = 0
                            let Advertiser_Charges = g == null ? 0 : g.Advertiser_Charges
                            select new 
                            {
                                d.Advertiser_Id,
                                d.Advertiser_Status_Id,
                                d.Advertiser_Email,
                                d.Payment_Method,
                                d.Campaign_Id,
                                d.Campaign_Status_Id,
                                d.Campaign_Name,
                                d.Is_Bonus,
                                d.Add_Date,
                                d.Ad_Id,
                                d.Ad_Status_Id,
                                d.Ad_Type,
                                d.Ad_Image,
                                d.Ad_Slogan,
                                d.Ad_Click,
                                d.Ad_Dimensions,
                                d.Ad_Bid,
                                Served,
                                Typed,
                                Fits,
                                Clickable,
                                Clicked,
                                CTR,
                                CTTR,
                                eCTTR,
                                eCPM,
                                Advertiser_Charges
                            };

                // Sort query.
                query = OrderExtension.OrderBy(query, orderBy, isDesc);

                // Take limited num. of rows.
                if (numOfRows > 0)
                    query = query.Take(numOfRows);

                return query.ToList();
            }
        }

        public static IEnumerable Countries(bool checkFromDate, DateTime fromDate, bool checkToDate, DateTime toDate,
                                            int countryId,
                                            string orderBy, bool isDesc, int numOfRows)
        {
            using (var dataContext = new AdsCaptchaDataContext())
            {
                // Set timeout.
                dataContext.CommandTimeout = 3600;

                // Get all tables (before filtering and joining).
                var stats = (from s in dataContext.TCS_STATs select s);

                // Filter by (from) date.
                if (checkFromDate)
                {
                    stats = from s in stats
                            where s.Date >= fromDate
                            select s;
                }
                // Filter by (to) date.
                if (checkToDate)
                {
                    stats = from s in stats
                            where s.Date <= toDate
                            select s;
                }
                // Filter by country.
                if (countryId > 0)
                {
                    stats = from s in stats
                            where s.Country_Id == countryId
                            select s;
                }

                // Get and group by statistics data.
                var grp = from s in stats
                          group s by new { s.Country_Id } into g
                          select new
                          {
                              g.Key.Country_Id,
                              Served = g.Sum(s => (int?)s.Served),
                              TotalTyped = g.Sum(s => (int?)s.Types),
                              Security = g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.SecurityOnly || w.Captcha_Type_Id == (int)CaptchaType.RandomImage).Sum(s => (int?)s.Types),
                              Typed = g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.PayPerType).Sum(s => (int?)s.Types),
                              Fit = g.Where(w => w.Captcha_Type_Id == (int)CaptchaType.Slider).Sum(s => (int?)s.Types),
                              Clickable = g.Sum(s => (int?)s.Clickable),
                              Clicks = g.Sum(s => (int?)s.Clicks),
                              Publisher_Earnings = g.Sum(s => (decimal?)s.Publisher_Earnings),
                              Developer_Earnings = g.Sum(s => (decimal?)s.Developer_Earnings),
                              Profits = g.Sum(s => (decimal?)s.Profits)
                          };

                // Build final query.
                var query = from c in dataContext.T_COUNTRies where !c.Is_Deleted
                            from g in grp 
                            where c.Country_Id == g.Country_Id 
                            let Served = g.Served == null ? 0 : g.Served
                            let TotalTyped = g.TotalTyped == null ? 0 : g.TotalTyped
                            let Security = g.Security == null ? 0 : g.Security
                            let Typed = g.Typed == null ? 0 : g.Typed
                            let Fits = g.Fit == null ? 0 : g.Fit
                            let Clickable = g.Clickable == null ? 0 : g.Clickable
                            let Clicked = g.Clicks == null ? 0 : g.Clicks
                            let CTR = g == null ? 0 : (Clickable == 0 ? 0 : Clicked / Clickable)
                            let CTTR = g == null ? 0 : (TotalTyped == 0 ? 0 : Clicked / TotalTyped)
                            let eCTTR = g == null ? 0 : (Served == 0 || TotalTyped == 0 ? 0 : CTTR / Served / TotalTyped)
                            let eCPM = 0
                            let Publisher_Earnings = g == null ? 0 : g.Publisher_Earnings
                            let Developer_Earnings = g == null ? 0 : g.Developer_Earnings
                            let Profits = g == null ? 0 : g.Profits
                            select new
                            {
                                c.Country_Id,
                                c.Country_Prefix,
                                c.Country_Name,
                                Served,
                                Security,
                                Typed,
                                Fits,
                                Clickable,
                                Clicked,
                                CTR,
                                CTTR,
                                eCTTR,
                                eCPM,
                                Publisher_Earnings,
                                Developer_Earnings,
                                Profits
                            };

                // Sort query.
                query = OrderExtension.OrderBy(query, orderBy, isDesc);

                // Take limited num. of rows.
                if (numOfRows > 0)
                    query = query.Take(numOfRows);

                return query.ToList();
            }
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}
