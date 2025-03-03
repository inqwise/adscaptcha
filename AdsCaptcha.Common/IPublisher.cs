using System;

namespace Inqwise.AdsCaptcha.Common
{
    public interface IPublisher
    {
        int Id { get; }
        string Email { get; }
        Status Status { get; }
        DateTime JoinDate { get; }
        int? AdvertiserId { get; }
        AdsCaptchaOperationResult<int> CreateAdvertiser();
        string FirstName { get; }
    }
}