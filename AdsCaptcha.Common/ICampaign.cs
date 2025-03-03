namespace Inqwise.AdsCaptcha.Common
{

    public enum Status
    {
        Running = 10001,
        Paused = 10002,
        Pending = 10003,
        Rejected = 10004
    }

    public enum CampaignPaymentType
    {
        Fit = 24001,
        Click = 24002
    }

    public interface ICampaign
    {
        int Id { get; }
        int AdvertiserId { get; }
        string Name { get; }
        Status Status { get; }
        CampaignPaymentType PaymentType { get; }
    }
}