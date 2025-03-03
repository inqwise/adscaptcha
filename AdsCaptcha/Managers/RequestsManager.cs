using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoreWrapper;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.Entities;
using Inqwise.AdsCaptcha.SystemFramework;
using Inqwise.AdsCaptcha.Dal.ElasticSearch;
using Inqwise.AdsCaptcha.Workflows;

namespace Inqwise.AdsCaptcha.Managers
{
    public class RequestsManager
    {
        public const int MIN_CORRECT_INDEX_MARGIN = 3;
        private const int DEFAULT_MAX_NUMBER_OF_FRAMES = 40;
        private const int DEFAULT_MIN_NUMBER_OF_FRAMES = 30;

        private static readonly Lazy<int> _spriteMaxFrames = new Lazy<int>(() =>
        {
            int maxFrames;
            if (!int.TryParse(ConfigurationManager.AppSettings["Sprite.MaxFrames"], out maxFrames))
            {
                maxFrames = DEFAULT_MAX_NUMBER_OF_FRAMES;
            }
            return maxFrames;
        });

        private static readonly Lazy<int> _spriteMinFrames = new Lazy<int>(() =>
        {
            int maxFrames;
            if (!int.TryParse(ConfigurationManager.AppSettings["Sprite.MinFrames"], out maxFrames))
            {
                maxFrames = DEFAULT_MIN_NUMBER_OF_FRAMES;
            }
            return maxFrames;
        });

        private static readonly Lazy<int?> _effectTypeId = new Lazy<int?>(() =>
        {
            int effectTypeId;
            if (!int.TryParse(ConfigurationManager.AppSettings["Sprite.EffectTypeId"], out effectTypeId))
            {
                return null;
            }
            return effectTypeId;
        });

        private static readonly Lazy<int> _useHistoryAfterMinutes = new Lazy<int>(() =>
        {
            int useHistoryAfterMinutes;
            if (!int.TryParse(ConfigurationManager.AppSettings["Sprite.UseHistoryAfterMinutes"], out useHistoryAfterMinutes))
            {
                useHistoryAfterMinutes = Randomizer.Instance.Random.Next(5, 10);
            }
            return useHistoryAfterMinutes;
        });

        private static readonly Lazy<TimeSpan> _requestCacheTimeoutSec = new Lazy<TimeSpan>(() =>
        {
            int requestCacheTimeoutSec;
            if (!int.TryParse(ConfigurationManager.AppSettings["RequestCacheTimeoutSec"], out requestCacheTimeoutSec))
            {
                requestCacheTimeoutSec = DEFAULT_REQUEST_CACHE_TIMEOUT_SEC;
            }
            return TimeSpan.FromSeconds(requestCacheTimeoutSec);
        });

        public static int MaxNumberOfFrames
        {
            get { return _spriteMaxFrames.Value; }
        }

        public static int MinNumberOfFrames
        {
            get { return _spriteMinFrames.Value; }
        }

        public static int UseHistoryAfterMinutes
        {
            get { return _useHistoryAfterMinutes.Value; }
        }

        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private static readonly Lazy<CacheManager<IRequestBase>> _cachedRequests =
            new Lazy<CacheManager<IRequestBase>>(
                () => new CacheManager<IRequestBase>(_requestCacheTimeoutSec.Value, ExpirationType.Absolute, CachedRequestRemoved));
        
        private static void CachedRequestRemoved(IRequestBase request)
        {
            request.Dispose();
        }

        private static readonly int DEFAULT_REQUEST_CACHE_TIMEOUT_SEC = 30;


        public static AdsCaptchaOperationResult<IRequest> Get(string guid, bool onlyFromCache = true, bool removefromCache = false, bool isDemo = false)
        {
            AdsCaptchaOperationResult<IRequest> result = null;
            try
            {

                var cache = CacheBuilder.GetCache();
                result = AdsCaptchaOperationResult<IRequest>.ToValueOrNotExist(onlyFromCache ? cache.Get<RequestEntity>(guid).WithChangeCallback(RequestChanged) : cache.GetOrAdd(guid, () => GetRequest(null, guid)).WithChangeCallback(RequestChanged));
                
                if (result.HasValue && removefromCache)
                {
                    RemoveFromCache(guid);
                }

                if (result.HasValue && result.Value.IsDemo != isDemo)
                {
                    Log.Warn("Get : Found request #{0}, But expected IsDemo:{1}", result.Value.RequestId, isDemo);
                    result = AdsCaptchaOperationResult<IRequest>.ToError(AdsCaptchaErrors.NoResults);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected error occured", ex);
                result = AdsCaptchaOperationResult<IRequest>.ToError(AdsCaptchaErrors.GeneralError, description:ex.ToString());
            }

            return result;
        }

        
        public static AdsCaptchaOperationResult<long> Insert(NewRequestArgs args)
        {
            AdsCaptchaOperationResult<long> result;
            try
            {
                result = AdsCaptchaOperationResult<long>.ToValueOrNotExist(RequestsDataAccess.Insert(args, UseHistoryAfterMinutes));
            }
            catch (Exception ex)
            {
                Log.ErrorException("Insert: Unexpected error occured", ex);
                result = AdsCaptchaOperationResult<long>.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            return result;
        }

        public static AdsCaptchaOperationResult<IRequest> Get(long id)
        {
            AdsCaptchaOperationResult<IRequest> result;
            try
            {
                result = AdsCaptchaOperationResult < IRequest >.ToValueOrNotExist(GetRequest(id, null));
                if (result.HasValue)
                {
                    CacheBuilder.GetCache().AddAsync(result.Value.RequestGuid.ToString(), result.Value, null);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected error occured", ex);
                result = AdsCaptchaOperationResult<IRequest>.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            return result;
        }

        private static RequestEntity GetRequest(long? id, string guid)
        {
            RequestEntity result = null;
            if (null == id && null == guid)
            {
                throw new ArgumentNullException("GetRequest: RequestId or RequestGuid required");
            }

            Stopwatch sw = Stopwatch.StartNew();

            using (var reader = RequestsDataAccess.GetDataReader(id, guid))
            {
                if (reader.Read())
                {
                    result = new RequestEntity(reader).WithChangeCallback(RequestChanged);
                }
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 200)
            {
                Log.Warn("RequestsManager.GetRequest: {0}", sw.ElapsedMilliseconds);
            }

            return result;
        }

        private static void RequestChanged(IRequestBase request)
        {
            Task.Run(() => CacheBuilder.GetCache().Add(request.RequestGuid.ToString(), request));
        }

        public static AdsCaptchaOperationResult Touch(IRequestBase request, int? selectedIndex, int? successRate, string clientIp, int maxTouches = 2)
        {
            AdsCaptchaOperationResult result = AdsCaptchaOperationResult.Ok;
            try
            {
                int countOfTouches = request.Touch(selectedIndex, successRate, clientIp);

                if (countOfTouches > maxTouches)
                {
                    // Remove from cache
                    RemoveFromCache(request.RequestGuid.ToString());
                }

                var realRequest = request as IRequest;
                if (null != realRequest && !realRequest.IsDemo)
                {
                    Task.Run(() => { new CaptchaAttackDetectionFlow(realRequest).Process(); });
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Touch: Unexpected error occured", ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            return result;
        }

        public static int GenerateCountOfFrames()
        {
            return Randomizer.Instance.Random.Next(MinNumberOfFrames, MaxNumberOfFrames);
        }

        public static int GenerateCorrectIndex(int countOfFrames)
        {
            return Randomizer.Instance.Random.Next(MIN_CORRECT_INDEX_MARGIN, countOfFrames - MIN_CORRECT_INDEX_MARGIN) - 1; 
        }

        public static void RemoveFromCache(string requestGuid)
        {
            CacheBuilder.GetCache().Remove(requestGuid);
        }

        internal static IRequestBase InsertTemp(NewRequestArgs args)
        {
            CacheBuilder.GetCache().Add(args.RequestGuid.ToString(), args);
            args.SetChangeCallback(RequestChanged);
            return args;
        }

        public static AdsCaptchaOperationResult<IRequestBase> GetTemp(string requestGuid)
        {
            var requestBase = CacheBuilder.GetCache().Get<NewRequestArgs>(requestGuid);    
            
            return AdsCaptchaOperationResult<IRequestBase>.ToValueOrNotExist(requestBase);
        }

        public static AdsCaptchaOperationResult UpdateClicked(string requestGuid)
        {
            AdsCaptchaOperationResult result = AdsCaptchaOperationResult.Ok;
            try
            {
                RequestsDataAccess.UpdateClicked(requestGuid);
            }
            catch (Exception ex)
            {
                Log.ErrorException("UpdateClicked: Unexpected error occured", ex);
                result = AdsCaptchaOperationResult.ToError(AdsCaptchaErrors.GeneralError, description: ex.ToString());
            }

            return result;
        }


        public static EffectTypes GetEffectType(CaptchaSecurityLevel securityLevel)
        {
            EffectTypes result;
            switch (securityLevel)
            {
                case CaptchaSecurityLevel.High:
                case CaptchaSecurityLevel.VeryHigh:
                    result = EffectTypes.AccordionChameleon;
                    break;
                case CaptchaSecurityLevel.Medium:
                    result = (EffectTypes)Randomizer.Instance.Random.Next(1, 3);
                    break;
                default:
                    result = EffectTypes.Swirl;
                    break;
            }

            return result;
        }


        public static int? GetDifficultyLevelId(EffectTypes effectType, CaptchaSecurityLevel securityLevel)
        {
            if (effectType == EffectTypes.Swirl) return null;

            return (int) ManagedAccordionChameleon.Difficulty.EASY;
        }

    }
}