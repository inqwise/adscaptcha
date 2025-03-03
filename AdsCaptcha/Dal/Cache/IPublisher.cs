using System;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    public interface IPublisher : ICache
    {
        void PublishAsync(string key, string value, Action callback);
        void Publish(string key, string value);
    }
}