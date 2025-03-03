namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface INewCampaignAdminMailArgs : IMailBuilderArgs
    {
        int AdvertiserId { get; }
        int CampaignId { get; }
        string CampaignName { get; }
    }
}