using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Entities
{
    public class CampaignEntity : ICampaign
    {
        public int Id { get; private set; }
        public int AdvertiserId { get; private set; }
        public string Name { get; private set; }
        public Status Status { get; private set; }
        public CampaignPaymentType PaymentType { get; private set; }

        public CampaignEntity(IDataReader reader)
        {
            Id = reader.ReadInt("Campaign_Id");
            try
            {
                AdvertiserId = reader.ReadInt("Advertiser_Id");
                Name = reader.ReadString("Campaign_Name");
                Status = (Status)reader.ReadInt("Status_Id");
                PaymentType = (CampaignPaymentType)reader.ReadInt("CampaignPaymentType");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Campaign #" + Id, ex);
            }
        }
    }
}