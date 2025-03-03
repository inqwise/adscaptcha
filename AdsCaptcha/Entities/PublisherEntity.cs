using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;

namespace Inqwise.AdsCaptcha.Entities
{
    public class PublisherEntity : IPublisher
    {
        public PublisherEntity(IDataReader reader)
        {
            Id = reader.ReadInt("Publisher_Id");
            try
            {
                Email = reader.ReadString("Email");
                Status = (Status)reader.ReadInt("Status_Id");
                JoinDate = reader.ReadDateTime("Join_Date");
                AdvertiserId = reader.OptInt("Advertiser_Id");
                FirstName = reader.SafeGetString("First_Name");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Publisher #" + Id, ex);
            }
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public Status Status { get; private set; }
        public DateTime JoinDate { get; private set; }
        public int? AdvertiserId { get; private set; }
        public string FirstName { get; private set; }

        public AdsCaptchaOperationResult<int> CreateAdvertiser()
        {
            AdsCaptchaOperationResult<int> result;

            if (AdvertiserId.HasValue)
            {
                result = AdsCaptchaOperationResult<int>.ToError(AdsCaptchaErrors.InvalidOperation,
                                                                description: "AdvertiserId already exist");
            }
            else
            {
                result = AdvertiserId = PublishersDataAccess.InsertAdvertiser(Id);
            }

            return result;
        }
    }
}