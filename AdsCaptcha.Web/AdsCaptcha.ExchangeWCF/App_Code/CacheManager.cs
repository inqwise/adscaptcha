using System.Runtime.Caching;
using System;

/// <summary>
/// Summary description for Cache
/// </summary>
public class CacheManager<T>
{
    private readonly ObjectCache _cache;  

    private readonly System.Threading.ReaderWriterLockSlim _lock;

    public CacheManager()
    {
        _cache = MemoryCache.Default;
        _lock = new System.Threading.ReaderWriterLockSlim();
    }

    private T AddToCache(String cacheKeyName, Tuple<T, TimeSpan> cacheItemPackage)
    {
        return AddToCache(cacheKeyName, cacheItemPackage.Item1, cacheItemPackage.Item2);
    }

    public T AddToCache(String cacheKeyName, T cacheItem, TimeSpan slidingExpiration) 
    {
        _lock.EnterWriteLock();
        try
        {
            var policy = new CacheItemPolicy
                {
                    Priority = CacheItemPriority.Default,
                    SlidingExpiration = slidingExpiration,
                };

            // Add inside cache 
            _cache.Set(cacheKeyName, cacheItem, policy);

            return cacheItem;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public T GetCachedItem(String cacheKeyName) 
    { 
        return (T)_cache[cacheKeyName]; 
    }

    public T GetOrAddCachedItem(string cacheKeyName, Func<Tuple<T, TimeSpan>> addFunc)
    {
        _lock.EnterUpgradeableReadLock();
        try
        {
            return (T) (_cache[cacheKeyName] ?? AddToCache(cacheKeyName, addFunc()));
        }
        finally
        {
            _lock.ExitUpgradeableReadLock();
        }
    }

    public T RemoveCachedItem(String cacheKeyName) 
    {
        return (T) (_cache.Remove(cacheKeyName) ?? default(T));    
    }


    public bool Contains(string key)
    {
        return _cache.Contains(key);
    }
} 
