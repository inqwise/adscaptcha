using System;
using System.Collections.Concurrent;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    public class CacheBuilder
    {
        //private const string DEFAULT_CACHE_NAME = "Default";
        private readonly static Lazy<ConcurrentDictionary<String, ICache>> CachesSet =
            new Lazy<ConcurrentDictionary<String, ICache>>();

        private static readonly Lazy<ILocker> Locker = new Lazy<ILocker>(()=>new RedisCache());

        private static readonly Lazy<IPublisher> Publisher = new Lazy<IPublisher>(() => new RedisCache());
     
        public static ICache GetCache(ExpirationType expirationType = ExpirationType.Absolute)
        {
            ICache cache = CachesSet.Value.GetOrAdd(expirationType.ToString(),
                                                                  (n) => new RedisCache(expirationType));

            return cache;
        }

        public static ILocker GetLocker()
        {
            return Locker.Value;
        }

        public static IPublisher GetPublisher()
        {
            return Publisher.Value;
        }
    }
}