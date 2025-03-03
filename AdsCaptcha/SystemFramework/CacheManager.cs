using System;
using System.Runtime.Caching;

namespace Inqwise.AdsCaptcha.SystemFramework
{
    public enum ExpirationType
    {
        Sliding,
        Absolute,
    }

    public class CacheManager<T>
    {
        private readonly ObjectCache _cache;

        private readonly System.Threading.ReaderWriterLockSlim _lock;
        public ExpirationType ExpirationType { get; private set; }
        public TimeSpan Expiration { get; private set; }
        public event Action<T> Removed;

        protected virtual void OnRemovedCallback(T obj)
        {
            var handler = Removed;
            if (handler != null) handler(obj);
        }


        private void OnRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            OnRemovedCallback((T)arguments.CacheItem.Value);
        }

        public CacheManager() : this(TimeSpan.FromSeconds(10))
        {
        }

        public CacheManager(TimeSpan expiration, ExpirationType expirationType = ExpirationType.Sliding, Action<T> removedCallback = null)
        {
            _cache = new MemoryCache(Guid.NewGuid().ToString());
            _lock = new System.Threading.ReaderWriterLockSlim();
            ExpirationType = expirationType;
            Expiration = expiration;
            if (null != removedCallback)
            {
                Removed += removedCallback;
            }
        }

        public T AddToCache(String cacheKeyName, T cacheItem)
        {
            return AddToCache(cacheKeyName, cacheItem, Expiration, ExpirationType);
        }

        private T AddToCache(String cacheKeyName, Tuple<T, TimeSpan> cacheItemPackage)
        {
            return AddToCache(cacheKeyName, cacheItemPackage.Item1, cacheItemPackage.Item2);
        }

        public T AddToCache(String cacheKeyName, T cacheItem, TimeSpan expiration, ExpirationType expirationType = ExpirationType.Sliding)
        {
            if (Equals(default(T), cacheItem))
            {
                return cacheItem;
            }
            else
            {
                _lock.EnterWriteLock();
                try
                {
                    var policy = new CacheItemPolicy
                    {
                        Priority = CacheItemPriority.Default,
                        SlidingExpiration = expiration,
                        RemovedCallback = OnRemovedCallback,
                    };

                    if (expirationType == ExpirationType.Sliding)
                    {
                        policy.SlidingExpiration = expiration;
                    }
                    else
                    {
                        policy.SlidingExpiration = TimeSpan.Zero;
                        policy.AbsoluteExpiration = DateTimeOffset.Now + expiration;
                    }

                    // Insert inside cache 
                    _cache.Set(cacheKeyName, cacheItem, policy);

                    return cacheItem;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public T GetCachedItem(String cacheKeyName)
        {
            return (T)_cache[cacheKeyName];
        }

        public T GetOrAddCachedItem(string cacheKeyName, Func<T> addFunc)
        {
            return GetOrAddCachedItem(cacheKeyName, addFunc());
        }

        public T GetOrAddCachedItem(string cacheKeyName, T cacheValue)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                return (T)(_cache[cacheKeyName] ?? AddToCache(cacheKeyName, cacheValue));
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        public T GetOrAddCachedItem(string cacheKeyName, Func<Tuple<T, TimeSpan>> addFunc)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                var result = (T) _cache[cacheKeyName];
                if (Equals(default(T), result) && null != addFunc)
                {
                    result = AddToCache(cacheKeyName, addFunc());
                }
                return result;
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        public T RemoveCachedItem(String cacheKeyName)
        {
            return (T)(_cache.Remove(cacheKeyName) ?? default(T));
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }
    }
}