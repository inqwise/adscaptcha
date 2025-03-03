using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Entities
{
    public class AdvertiserEntity : IAdvertiser
    {
        public AdvertiserEntity(IDataReader reader)
        {
            Id = reader.ReadInt("Advertiser_Id");
            try
            {
                Email = reader.ReadString("Email");
                JoinDate = reader.ReadDateTime("Join_Date");
                Status = (Status)reader.ReadInt("Status_Id");
                BillingMethod = (BillingMethod)reader.ReadInt("Billing_Method_Id");
                PaymentMethod = (PaymentMethod)reader.ReadInt("Payment_Method_Id");
                EmailAnnouncements = Convert.ToBoolean(reader["Get_Email_Announcements"]);
                EmailNewsletters = Convert.ToBoolean(reader["Get_Email_Newsletters"]);
                MinimumBillingAmount = Convert.ToDecimal(reader["Min_Billing_Amount"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Advertiser #" + Id, ex);
            }
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public DateTime JoinDate { get; private set; }
        public Status Status { get; private set; }
        public BillingMethod BillingMethod { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public bool EmailAnnouncements { get; private set; }
        public bool EmailNewsletters { get; private set; }
        public decimal MinimumBillingAmount { get; private set; }
    }
}