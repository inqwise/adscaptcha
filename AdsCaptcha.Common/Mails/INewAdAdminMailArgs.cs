namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface INewAdAdminMailArgs : IMailBuilderArgs
    {
        int AdvertiserId { get; }
        string CampaignName { get; }
        string AdName { get; }
        long AdId { get; }
        int CampaignId { get; }
        long? ImageId { get; }
    }
}
