namespace Inqwise.AdsCaptcha.Common.Data
{
    public interface INewCampaignResult
    {
        int CampaignId { get; }
        long AdId { get; }
        long? ImageId { get; }
    }
}