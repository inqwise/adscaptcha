using System;

namespace Inqwise.AdsCaptcha.Common
{
    public enum ImageType
    {
        Undefined = 0,
        Random = 1,
        Commercial = 2,
        HouseAd = 10,
        Exchange = 101,
        Demo = 5,
        Temp = 6,
        Ad = 7,
    }

    public enum ImageStatus
    {
        Active = 0,
        Deleted = 1,
        Pending = 2,
    }

    public interface IImage
    {
        long Id { get; }
        ImageType ImageType { get; }
        DateTime InsertDate { get; }
        int? AdvertiserId { get; }
        int? CampaignId { get; }
        int? AdId { get; }
        int Width { get; }
        int Height { get; }
        string Path { get; }
        string ContentType { get; }
    }
}