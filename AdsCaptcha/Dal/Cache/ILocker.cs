using System;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    public interface ILocker
    {
        IDisposable AcquireLock(string key, TimeSpan timeout);
        IDisposable AcquireLock(string key);
    }
}