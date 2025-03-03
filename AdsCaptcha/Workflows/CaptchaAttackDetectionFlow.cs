using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Dal.ElasticSearch;
using Inqwise.AdsCaptcha.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Workflows
{
    public class CaptchaAttackDetectionFlow
    {
        private const double MAX_DAILY_DIFF = 2;
        private const double MAX_HOURLY_DIFF = 3;
        private const double MAX_TEN_MIN_DIFF = 3;
        private const double MAX_MIN_DIFF = 4;
        private const double MAX_TEN_SEC_DIFF = 4;

        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        
        private IRequest request;

        private static Lazy<ICache> _cachedCaptchasAttacksIndicators =
            new Lazy<ICache>(() => CacheBuilder.GetCache(ExpirationType.Sliding));
        

        public CaptchaAttackDetectionFlow(IRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            this.request = request;
        }

        private enum Periods
        {
            TenDaysByDay,
            DayByHour,
            HourByTenMin,
            MinByTenSec,
            TenMinByMin
        }

        private readonly Dictionary<Periods, double> _indicators = new Dictionary<Periods, double>();
        
        public AdsCaptchaOperationResult Process()
        {
            return AdsCaptchaOperationResult.Ok;
            try
            {
                //Stopwatch sw = Stopwatch.StartNew();
                AdsCaptchaOperationResult result = null;

                var stats = CaptchasManager.GetStatistics(request.CaptchaId.GetValueOrDefault());
                CaptchasManager.Index(request, stats);

                if (stats.DiffTenDaysByDay >= MAX_DAILY_DIFF)
                    _indicators.Add(Periods.TenDaysByDay, stats.DiffTenDaysByDay);
                if (stats.DiffDayByHour >= MAX_HOURLY_DIFF) _indicators.Add(Periods.DayByHour, stats.DiffDayByHour);
                if (stats.DiffHourByTenMin >= MAX_TEN_MIN_DIFF)
                    _indicators.Add(Periods.HourByTenMin, stats.DiffHourByTenMin);
                if (stats.DiffLastTenMinByMin >= MAX_MIN_DIFF)
                    _indicators.Add(Periods.TenMinByMin, stats.DiffLastTenMinByMin);
                if (stats.DiffLastMinByTenSec >= MAX_TEN_SEC_DIFF)
                    _indicators.Add(Periods.MinByTenSec, stats.DiffLastMinByTenSec);

                if (_indicators.Count > 0)
                {
                    if (_cachedCaptchasAttacksIndicators.Value.AddIfNotExists(request.CaptchaId.ToString(),
                                                                              stats.DiffLastMinByTenSec,
                                                                              TimeSpan.FromMinutes(5),
                                                                              CacheName.AttacksIndicators))
                    {

                        ElasticSearchFactory.ElasticClient.IndexAsync(
                            new
                                {
                                    CaptchaId = request.CaptchaId.GetValueOrDefault(),
                                    InsertDate = DateTime.Now,
                                    Statistics = stats,
                                    ActiveIndicators = string.Join("|", _indicators.Select(kv => string.Format("'{0}'_'{1}'", kv.Key.ToString(), kv.Value)).ToArray()),
                                }, "visitors", "attack");
                        Log.Error("Captcha #{0} - Pick detected.", request.CaptchaId);

                        var captcha = CaptchasManager.Get(request.CaptchaId.GetValueOrDefault(), null, true);
                        if (captcha.HasValue && captcha.Value.AttackDetectionAutoChange &&
                            ((int) captcha.Value.SecurityLevel) < (int) CaptchaSecurityLevel.High)
                        {
                            CaptchaSecurityLevel newSecurityLevel = IncreaseCaptchaSecurityLevel(captcha.Value);
                            SendCaptchasOwnerNotification(captcha.Value, newSecurityLevel);
                        }
                    }
                }
                /*
                sw.Stop();
                if (sw.ElapsedMilliseconds > 50)
                {
                    Log.Error("CaptchaAttackDetectionFlow: {0}", sw.ElapsedMilliseconds);
                }
                 */
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return AdsCaptchaOperationResult.Ok;
        }

        private CaptchaSecurityLevel IncreaseCaptchaSecurityLevel(ICaptcha captcha)
        {
            var newSecurityLevel = CaptchaSecurityLevel.High;
            //if ((int) captcha.SecurityLevel > (int) CaptchaSecurityLevel.Low)
            //{
            //    newSecurityLevel = CaptchaSecurityLevel.High;
            //}

            CaptchasManager.Update(new UpdateCaptchaArgs(captcha.Id){Name = captcha.Name, SecurityLevelId = (int)newSecurityLevel, Height = captcha.MaxHeight, Width = captcha.MaxWidth, SourceType = captcha.SourceType});

            return newSecurityLevel;
        }

        private struct CaptchaAttackNotificationMailArgs : ICaptchaAttackNotificationMailArgs
        {
            public DateTime AttackDate { get; set; }
            public CaptchaSecurityLevel PreviousSecurityLevel { get; set; }
            public CaptchaSecurityLevel CurrentSecurityLevel { get; set; }
            public int PublisherId { get; set; }
            public long CaptchaId { get; set; }
            public int WebsiteId { get; set; }
        }

        private void SendCaptchasOwnerNotification(ICaptcha captcha, CaptchaSecurityLevel currentSecurityLevel)
        {
            try
            {
                var publisherResult = PublishersManager.Get(captcha.PublisherId);

                // Send mail to publisher.
                MailBuilder.GetInstance(new CaptchaAttackNotificationMailArgs
                    {
                        AttackDate = request.Timestamp,
                        CurrentSecurityLevel = currentSecurityLevel,
                        PreviousSecurityLevel = captcha.SecurityLevel,
                        PublisherId = captcha.PublisherId,
                        CaptchaId = captcha.Id,
                        WebsiteId = captcha.WebsiteId,

                    }).Build().Send();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private TimeSpan GetExpirationByPeriod(Periods period)
        {
            switch (period)
            {
                case Periods.TenDaysByDay:
                    return TimeSpan.FromDays(10);
                case Periods.DayByHour:
                    return TimeSpan.FromDays(1);
                case Periods.HourByTenMin:
                    return TimeSpan.FromHours(1);
                case Periods.MinByTenSec:
                    return TimeSpan.FromMinutes(1);
                default:
                    throw new ArgumentOutOfRangeException("period");
            }
        }
    }
}