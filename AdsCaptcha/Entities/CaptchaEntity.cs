using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.Entities
{
    public class CaptchaEntity : ICaptcha
    {
        public CaptchaEntity(IDataReader reader)
        {
            Id = Convert.ToInt64(reader["Captcha_Id"]);
            try
            {
                PublisherId = Convert.ToInt32(reader["Publisher_Id"]);
                WebsiteId = Convert.ToInt32(reader["Website_Id"]);
                Name = Convert.ToString(reader["Captcha_Name"]);
                SourceType = (CaptchaSourceType)Convert.ToInt32(reader["SourceTypeId"]);
                MaxWidth = Convert.ToInt32(reader["Max_Width"]);
                MaxHeight = Convert.ToInt32(reader["Max_Height"]);
                PublicKey = reader.ReadString("Public_Key");
                PrivateKey = reader.ReadString("Private_Key");
                Language = reader.ReadString("Language");
                LikeUrl = reader.ReadString("Like_Url");
                Status = (Status)reader.ReadInt("Status_Id");
                CampaignId = reader.OptInt("Campaign_Id");
                FeedbackId = reader.GetValueOrDefault<string>("Feedback_Id");
                SecurityLevel = (CaptchaSecurityLevel)reader.ReadInt("Security_Level_Id");
                AttackDetectionAutoChange = reader.ReadBool("AttackDetectionAutoChange");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Captcha #" + Id, ex);
            }
        }

        public CaptchaEntity()
        {    
        }

        public long Id { get; set; }
        public int PublisherId { get; set; }
        public int WebsiteId { get; set; }
        public string Name { get; set; }
        public CaptchaSourceType SourceType { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Language { get; set; }
        public string LikeUrl { get; set; }
        public Status Status { get; set; }
        public int? CampaignId { get; set; }
        public string FeedbackId { get; set; }
        public CaptchaSecurityLevel SecurityLevel { get; set; }
        public bool AttackDetectionAutoChange { get; set; }
    }
}