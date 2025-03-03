using System;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    public enum CacheName
    {
        Default = 12,
        Locker = 13,
        Captchas = 15,
        AttacksIndicators = 11,
    }

    public interface ICache
    {
        TValue Get<TValue>(object key, TimeSpan? slidingExpiry = null, CacheName cacheName = CacheName.Default);
        TValue GetOrAdd<TValue>(object key, Func<TValue> getFunc, TimeSpan? expiry = null, CacheName cacheName = CacheName.Default);
        TValue Add<TValue>(object key, TValue value, TimeSpan? expiry = null, CacheName cacheName = CacheName.Default);
        bool AddIfNotExists<TValue>(object key, TValue value, TimeSpan? expiry = null, CacheName cacheName = CacheName.Default);
        bool Remove(object key, CacheName cacheName = CacheName.Default);
        void AddAsync<TValue>(object key, TValue value, Action<TValue> callback, TimeSpan? expiry = null, CacheName cacheName = CacheName.Default);
    }
}