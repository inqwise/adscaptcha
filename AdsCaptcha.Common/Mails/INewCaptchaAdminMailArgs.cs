namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface INewCaptchaAdminMailArgs : IMailBuilderArgs
    {
        long CaptchaId { get; }
        string WebsiteUrl { get; }
        int WebsiteId { get; }
        int PublisherId { get; }
    }
}