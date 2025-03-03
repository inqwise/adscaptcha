using System;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Entities
{
    public class ImageEntity : IImage
    {
        public ImageEntity(System.Data.IDataReader reader)
        {
            Id = Convert.ToInt64(reader["ImageID"]);
            try
            {
                ImageType = (ImageType)reader.ReadInt("ImageTypeId");
                InsertDate = reader.ReadDateTime("InsertDate");
                AdvertiserId = reader.OptInt("AdvertiserId");
                CampaignId = reader.OptInt("CampaignId");
                AdId = reader.OptInt("AdId");
                Width = reader.ReadInt("Width");
                Height = reader.ReadInt("Height");
                Path = reader.ReadString("ImagePath");
                ContentType = reader.ReadString("ContentType");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Image #" + Id, ex);
            }
        }

        public long Id { get; private set; }
        public ImageType ImageType { get; private set; }
        public DateTime InsertDate { get; private set; }
        public int? AdvertiserId { get; private set; }
        public int? CampaignId { get; private set; }
        public int? AdId { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Path { get; private set; }
        public string ContentType { get; private set; }
    }
}