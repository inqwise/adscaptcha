using System;

namespace Inqwise.AdsCaptcha.Common
{
    public enum AdType
    {
        SloganOnly = 16001,
        SloganAndImage = 16002,
        SloganAndVideo = 16003,
        SloganAndCoupon = 16004,
        Slide2Fit = 16005,
        YbrantBanner = 16006
    }

    public interface IAd
    {
        long Id { get; }
        int AdvertiserId { get; }
        int CampaignId { get; }
        string Name { get; }
        Status Status { get; }
        string ClickUrl { get; }
        decimal MaxBid { get; }
        DateTime ModifyDate { get; }
        string LikeUrl { get; }
        string ExternalId { get; }
        string ImagePath { get; }
    }
}