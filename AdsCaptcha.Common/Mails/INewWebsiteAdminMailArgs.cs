namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface INewWebsiteAdminMailArgs : IMailBuilderArgs
    {
        int PublisherId { get; }
        int WebsiteId { get; }
        string WebsiteUrl { get; }
    }
}