namespace Inqwise.AdsCaptcha.Common
{
    public enum CaptchaType
    {
        PayPerType = 13001,
        SecurityOnly = 13002,
        Slider = 13003,
        RandomImage = 13004,
        SlideToFit = 13005,
        TypeWords = 13006,
        SliderIn = 13007
    }

    public enum CaptchaSourceType
    {
        NonCommercial = 0,
        Commercial = 1,
    }

    public enum CaptchaSecurityLevel
    {
        Undefined = 0,
        VeryLow = 11001,
        Low = 11002,
        Medium = 11003,
        High = 11004,
        VeryHigh = 11005
    }

    public interface ICaptcha
    {
        long Id { get; }
        int PublisherId { get; }
        int WebsiteId { get; }
        string Name { get; }
        CaptchaSourceType SourceType { get; }
        int MaxWidth { get; }
        int MaxHeight { get; }
        string PublicKey { get; }
        string PrivateKey { get; }
        string Language { get; }
        string LikeUrl { get; }
        Status Status { get; }
        int? CampaignId { get; }
        string FeedbackId { get; }
        CaptchaSecurityLevel SecurityLevel { get; }
        bool AttackDetectionAutoChange { get; }
    }
}