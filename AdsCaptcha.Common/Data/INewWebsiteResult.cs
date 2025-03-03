namespace Inqwise.AdsCaptcha.Common.Data
{
    public interface INewWebsiteResult
    {
        int WebsiteId { get; }
        long CaptchaId { get; }
    }
}