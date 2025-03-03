using System;
using System.Configuration;
using System.Linq;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Dal.ElasticSearch;
using Inqwise.AdsCaptcha.Entities;
using Inqwise.AdsCaptcha.Mails;

namespace Inqwise.AdsCaptcha.Managers
{
    public class CaptchasManager
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private static Lazy<ICache> _cachedCaptchas =
            new Lazy<ICache>(() => CacheBuilder.GetCache());
        
        public static AdsCaptchaOperationResult<ICaptcha> Get(long captchaId, int? publisherId = null, bool fromCache = false, string publicKey = null, bool defaultNotExist = false)
        {
            ICaptcha captcha;

            if (fromCache)
            {
                captcha = _cachedCaptchas.Value.GetOrAdd(captchaId.ToString(), ()=> (CaptchaEntity)GetCaptcha(captchaId));
            }
            else
            {
                captcha = GetCaptcha(captchaId);
            }

            if (null != captcha)
            {
                if (null != publicKey && captcha.PublicKey != publicKey)
	            {
		            Log.Warn("Get: Captchas PublicKey not equal. captchaId: '{0}', publicKey: '{1}', expected-PublicKey: '{2}'", captchaId, publicKey, captcha.PublicKey);
                    captcha = null;
	            }

                if (null != publisherId && captcha.PublisherId != publisherId)
                {
                    Log.Warn("Get: Captchas PublisherId not equal. captchaId: '{0}', publisherId: '{1}', expected-PublisherId: '{2}'", captchaId, publisherId, captcha.PublisherId);
                    captcha = null;
                }
                
            }

            if (null == captcha && defaultNotExist)
            {
                captcha = DefaultCaptcha;
            }

            return AdsCaptchaOperationResult<ICaptcha>.ToValueOrNotExist(captcha);
        }

        private static ICaptcha GetCaptcha(long captchaId, bool throwNoExist = false)
        {
            using (var reader = CaptchasDataAccess.GetDataReader(captchaId))
            {
                if (reader.Read())
                {
                    return new CaptchaEntity(reader);
                }

                if (throwNoExist) throw new ArgumentOutOfRangeException("Not found captcha #" + captchaId);
                return null;
            } 
        }

        private static ICaptcha _defaultCaptcha = null;

        public static ICaptcha DefaultCaptcha
        {
            get
            {
                if (null == _defaultCaptcha)
                {
                    lock (typeof(CaptchasManager))
                    {
                        if (null == _defaultCaptcha)
                        {
                            string[] defaultCaptcha = ConfigurationManager.AppSettings["DefaultCaptcha"].Split(';');
                            int captchaId = Convert.ToInt32(defaultCaptcha[0]);
                            _defaultCaptcha = GetCaptcha(captchaId, throwNoExist: true);
                        }
                    }
                }

                return _defaultCaptcha;
            }
        }

        public static AdsCaptchaOperationResult Update(UpdateCaptchaArgs args)
        {
            AdsCaptchaOperationResult result = null;
            try
            {
                CaptchasDataAccess.Update(args);
                _cachedCaptchas.Value.Remove(args.CaptchaId.ToString(), CacheName.Captchas);
                result = AdsCaptchaOperationResult.Ok;
            }
            catch (Exception ex)
            {
                Log.ErrorException("Update: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult<long> Add(NewCaptchaArgs args)
        {
            AdsCaptchaOperationResult<long> result = null;
            long? captchaId = null;
            try
            {
                if (null == args.WebsiteId)
                {
                    result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.WebsiteidNotSet);
                }

                if (null == result && null == args.PublisherId)
                {
                    result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.PublisheridNotSet);
                }

                if (null == result)
                {
                    captchaId = CaptchasDataAccess.Insert(args);
                    args.CaptchaId = captchaId;
                }

                if (null == result)
                {
                    result = captchaId;
                    try
                    {
                        if (args.SendAdminEmail)
                        {
                            SendNewCaptchaAdminMail(args, null); 
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorException("Add: Failed to send mail", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Add: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.GeneralError,
                                                                 description: ex.ToString());
            }

            return result;
        }

        private struct NewACaptchaAdminMailArgs : INewCaptchaAdminMailArgs
        {
            public long CaptchaId { get; set; }
            public string WebsiteUrl { get; set; }
            public int WebsiteId { get; set; }
            public int PublisherId { get; set; }
        }

        internal static void SendNewCaptchaAdminMail(NewCaptchaArgs args, string websiteUrl)
        {
            AdsCaptchaOperationResult result = null;

            if (null == result)
            {
                if (null == args.WebsiteId) throw new ArgumentNullException("args.WebsiteId");
                if (null == args.CaptchaId) throw new ArgumentNullException("args.CaptchaId");
                if (null == args.PublisherId) throw new ArgumentNullException("args.PublisherId");
            }

            if (null == result && null == websiteUrl)
            {
                IWebsite website = null;
                AdsCaptchaOperationResult<IWebsite> websiteResult = WebsitesManager.Get(args.WebsiteId.Value, null);
                if (websiteResult.HasError)
                {
                    result = websiteResult;
                }
                else
                {
                    website = websiteResult.Value;
                    websiteUrl = website.Url;
                }
            }

            if (null == result)
            {
                // Send mail to administrator.
                MailBuilder.GetInstance(new NewACaptchaAdminMailArgs
                {
                    CaptchaId = args.CaptchaId.Value,
                    PublisherId = args.PublisherId.Value,
                    WebsiteId = args.WebsiteId.Value,
                    WebsiteUrl = websiteUrl,
                }).Build().Send();
            }
        }

        public static CaptchaStatistics GetStatistics(long captchaId)
        {
            var statistics = new CaptchaStatistics();

            var now = DateTime.Now;
                
            var q = new
            {
                size = 0,
                aggs = new
                {
                    countOfVisits = new
                    {
                        filter = new
                        {
                            and = new object[]
                                                {
                                                    new
                                                        {
                                                            term = new
                                                                {
                                                                    captchaId = captchaId
                                                                }
                                                        },
                                                    new
                                                        {
                                                            range = new
                                                                {
                                                                    insertDate = new
                                                                        {
                                                                            from = now.AddDays(-10)
                                                                        }
                                                                }
                                                        }
                                                }
                        },
                        aggs = new
                        {
                            lastMinute = new
                            {
                                filter = new
                                {
                                    range = new
                                    {
                                        insertDate = new
                                        {
                                            from = now.AddMinutes(-1)
                                        }
                                    }
                                },
                                aggs = new
                                {
                                    tenSeconds = new
                                    {
                                        date_histogram = new
                                        {
                                            field = "insertDate",
                                            interval = "10s",
                                            format = "HH:mm:ss"
                                        }
                                    },
                                }
                            },
                            lastTenMinutes = new
                            {
                                filter = new
                                {
                                    range = new
                                    {
                                        insertDate = new
                                        {
                                            from = now.AddMinutes(-11),
                                            to = now.AddMinutes(-1),
                                        }
                                    }
                                },
                                aggs = new
                                {
                                    oneMinute = new
                                    {
                                        date_histogram = new
                                        {
                                            field = "insertDate",
                                            interval = "1m",
                                            format = "HH:mm:ss"
                                        }
                                    }
                                }
                            },
                            lastHour = new
                            {
                                filter = new
                                {
                                    range = new
                                    {
                                        insertDate = new
                                        {
                                            from = now.AddHours(-1),
                                            to = now.AddMinutes(-1),
                                        }
                                    }
                                },
                                aggs = new
                                {
                                    tenMinutes = new
                                    {
                                        date_histogram = new
                                        {
                                            field = "insertDate",
                                            interval = "10m",
                                            format = "HH:mm:ss"
                                        }
                                    }
                                }
                            },
                            lastDay = new
                            {
                                filter = new
                                {
                                    range = new
                                    {
                                        insertDate = new
                                        {
                                            from = now.AddDays(-1),
                                            to = now.AddMinutes(-1),
                                        }
                                    }
                                },
                                aggs = new
                                {
                                    oneHour = new
                                    {
                                        date_histogram = new
                                        {
                                            field = "insertDate",
                                            interval = "1H",
                                            format = "HH:mm:ss"
                                        }
                                    }
                                }
                            },
                            lastTenDays = new
                            {
                                filter = new
                                {
                                    range = new
                                    {
                                        insertDate = new
                                        {
                                            from = now.AddHours(-1),
                                            to = now.AddMinutes(-1),
                                        }
                                    }
                                },
                                aggs = new
                                {
                                    oneDay = new
                                    {
                                        date_histogram = new
                                        {
                                            field = "insertDate",
                                            interval = "1d"
                                        }
                                    }
                                }
                            },
                        }
                    }
                }
            };




            var qJson = JsonConvert.ExportToString(q);
            var searchResult = ElasticSearchFactory.GetElasticClient("visitors", t => t.Name.ToLower().Replace("args", string.Empty)).SearchRaw<VisitorArgs, Object>(qJson);
            if (searchResult.IsValid)
            {
                var aggs = (JsonObject)JsonConvert.Import(searchResult.ConnectionStatus.Result);
                var countOfVisits = aggs.GetJObject("aggregations").GetJObject("countOfVisits");
                var lastMinByTenSecList = countOfVisits.GetJObject("lastMinute").GetJObject("tenSeconds").GetJArray("buckets");
                var lastTenMinByMinList = countOfVisits.GetJObject("lastTenMinutes").GetJObject("oneMinute").GetJArray("buckets");
                var lastHourByTenMinList = countOfVisits.GetJObject("lastHour").GetJObject("tenMinutes").GetJArray("buckets");
                var lastDayByHourList = countOfVisits.GetJObject("lastDay").GetJObject("oneHour").GetJArray("buckets");
                var lastTenDaysByDayList = countOfVisits.GetJObject("lastTenDays").GetJObject("oneDay").GetJArray("buckets");

                statistics.LastMinByTenSecAvg = (lastMinByTenSecList.Count == 0) ? 0 : lastMinByTenSecList.Sum(o => ((JsonObject)o).GetLong("doc_count"))/6;
                statistics.LastMinByTenSecMax = (lastMinByTenSecList.Count == 0) ? 0 : lastMinByTenSecList.Max(o => ((JsonObject)o).GetLong("doc_count"));

                statistics.LastTenMinByMinAvg = (lastTenMinByMinList.Count == 0) ? 0 : lastTenMinByMinList.Sum(o => ((JsonObject)o).GetLong("doc_count"))/10;
                statistics.LastTenMinByMinMax = (lastTenMinByMinList.Count == 0) ? 0 : lastTenMinByMinList.Max(o => ((JsonObject)o).GetLong("doc_count"));
                statistics.LastTenMinByMinCountOfZero = (lastTenMinByMinList.Count == 0) ? null : (10 - lastTenMinByMinList.Count()) as int?;


                statistics.LastHourByTenMinAvg = (lastHourByTenMinList.Count == 0) ? 0 : lastHourByTenMinList.Sum(o => ((JsonObject)o).GetLong("doc_count"))/6;
                statistics.LastHourByTenMinMax = (lastHourByTenMinList.Count == 0) ? 0 : lastHourByTenMinList.Max(o => ((JsonObject)o).GetLong("doc_count"));
                statistics.LastHourByTenMinCountOfZero = (lastHourByTenMinList.Count == 0) ? null : (6 - lastHourByTenMinList.Count()) as int?;

                statistics.LastDayByHourAvg = (lastDayByHourList.Count == 0) ? 0 : lastDayByHourList.Sum(o => ((JsonObject)o).GetLong("doc_count"))/24;
                statistics.LastDayByHourMax = (lastDayByHourList.Count == 0) ? 0 : lastDayByHourList.Max(o => ((JsonObject)o).GetLong("doc_count"));
                statistics.LastDayByHourCountOfZero = (lastDayByHourList.Count == 0) ? null : (24 - lastDayByHourList.Count()) as int?;

                statistics.LastTenDaysByDayAvg = (lastTenDaysByDayList.Count == 0) ? 0 : lastTenDaysByDayList.Sum(o => ((JsonObject)o).GetLong("doc_count"))/10;
                statistics.LastTenDaysByDayMax = (lastTenDaysByDayList.Count == 0) ? 0 : lastTenDaysByDayList.Max(o => ((JsonObject)o).GetLong("doc_count"));
                statistics.LastTenDaysByDayCountOfZero = (lastTenDaysByDayList.Count == 0) ? null : (10 - lastTenDaysByDayList.Count()) as int?;
            }

            return statistics;
        }

        public static void Index(IRequest request, CaptchaStatistics statistics)
        {
            ElasticSearchFactory.ElasticClient.IndexAsync(new VisitorArgs { CaptchaId = request.CaptchaId.GetValueOrDefault(), InsertDate = DateTime.Now, IpAddress = request.ClientIp, CountryCode = request.CountryCode, CountOfTouches = request.CountOfTouches, VisitorUid = request.VisitorUid, DifficultyLevelId = request.DifficultyLevelId.GetValueOrDefault(), Statistics = statistics }, "visitors", "visitor");
        }
    }
}