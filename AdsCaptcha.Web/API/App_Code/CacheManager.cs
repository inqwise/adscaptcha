using System;
using System.Runtime.Caching;
using Inqwise.AdsCaptcha.DAL;

/// <summary>
/// Summary description for CacheManager
/// </summary>
public class CacheManager<T>
{
    private readonly ObjectCache _cache;

    private readonly System.Threading.ReaderWriterLockSlim _lock;

    public CacheManager()
    {
        _cache = new MemoryCache(Guid.NewGuid().ToString());
        _lock = new System.Threading.ReaderWriterLockSlim();
    }

    private T AddToCache(String cacheKeyName, Tuple<T, TimeSpan> cacheItemPackage)
    {
        return AddToCache(cacheKeyName, cacheItemPackage.Item1, cacheItemPackage.Item2);
    }

    public T AddToCache(String cacheKeyName, T cacheItem, TimeSpan slidingExpiration)
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
                        SlidingExpiration = slidingExpiration,
                    };

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

    public T GetOrAddCachedItem(string cacheKeyName, Func<Tuple<T, TimeSpan>> addFunc)
    {
        _lock.EnterUpgradeableReadLock();
        try
        {
            return (T)(_cache[cacheKeyName] ?? AddToCache(cacheKeyName, addFunc()));
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

public static class CacheManager
{
    public static readonly TimeSpan DefaultRequestsCacheExpiration = TimeSpan.FromSeconds(300);
    public static readonly TimeSpan DefaultImageIdsCacheExpiration = TimeSpan.FromSeconds(300);

    private static Lazy<CacheManager<T_REQUESTS>> _requestsCache = new Lazy<CacheManager<T_REQUESTS>>();

    public static CacheManager<T_REQUESTS> RequestsCache
    {
        get { return _requestsCache.Value; }
    }

    private static Lazy<CacheManager<Tuple<int, int>>> _imageIdsCache = new Lazy<CacheManager<Tuple<int, int>>>();
    public static CacheManager<Tuple<int, int>> ImageIdsCache
    {
        get { return _imageIdsCache.Value; }
    }
}