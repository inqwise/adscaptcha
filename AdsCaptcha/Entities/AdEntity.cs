using Inqwise.AdsCaptcha.Common;
using System;
namespace Inqwise.AdsCaptcha.Entities
{
    public class AdEntity : IAd
    {
        public AdEntity(System.Data.IDataReader reader)
        {
            this.Id = Convert.ToInt64(reader["Ad_Id"]);
            try
            {
                this.AdvertiserId = reader.ReadInt("Advertiser_Id");
                this.CampaignId = reader.ReadInt("Campaign_Id");
                this.Name = reader.ReadString("Ad_Name");
                this.ClickUrl = reader.ReadString("Ad_Url");
                this.LikeUrl = reader.ReadString("Ad_Like_Url");
                this.MaxBid = reader.ReadDecimal("Max_Cpt");
                this.Status = (Status)reader.ReadInt("Status_Id");
                this.ModifyDate = reader.ReadDateTime("Modify_Date");
                this.ExternalId = reader.ReadString("Ad_ExternalId");
                this.ImagePath = reader.ReadString("ImagePath");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Ad #" + Id, ex);
            }
        }

        public string ImagePath { get; private set; }

        public long Id
        {
            get; private set;
        }

        public int AdvertiserId
        {
            get; private set;
        }

        public int CampaignId
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public Status Status
        {
            get; private set;
        }

        public string ClickUrl
        {
            get; private set;
        }

        public decimal MaxBid
        {
            get; private set;
        }

        public DateTime ModifyDate
        {
            get; private set;
        }

        public string LikeUrl
        {
            get; private set;
        }

        public string ExternalId { get; private set; }
    }
}