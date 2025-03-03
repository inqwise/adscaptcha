namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface INewWebsitePublisherMailArgs : IMailBuilderArgs
    {
        string WebsiteUrl { get; }
        int PublisherId { get; }
    }
}