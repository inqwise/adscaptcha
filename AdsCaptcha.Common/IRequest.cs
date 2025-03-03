using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Inqwise.AdsCaptcha.Common
{

    public enum EffectTypes
    {
        Undefined = 0,
        Swirl = 1,
        AccordionChameleon = 2,
    }

    public interface IRequest : IRequestBase
    {
        long RequestId { get; }
        DateTime Timestamp { get; }
        string SessionId { get; }
        int? AdId { get; }
        AdType? AdType { get; }
        int? CampaignId { get; }
        int? AdvertiserId { get; }
        long? CaptchaId { get; }
        string PublicKey { get; }
        string PrivateKey { get; }
        string ClientIp { get; }
        long ImageId { get; }
        bool IsDemo { get; }
        EffectTypes EffectType { get; }
        string ImageExternalId { get; }
        string ImageSource { get; }
        string CountryCode { get; }
    }
}