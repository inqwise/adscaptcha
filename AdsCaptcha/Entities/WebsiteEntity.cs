using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;

namespace Inqwise.AdsCaptcha.Entities
{
    public class WebsiteEntity : IWebsite
    {
        private List<int> _categories;
        IEnumerable<int> IWebsite.Categories
        {
            get { return _categories; }
        }

        public WebsiteEntity(IDataReader reader)
        {
            Id = reader.ReadInt("Website_Id");
            try
            {
                PublisherId = reader.ReadInt("Publisher_Id");
                Url = reader.ReadString("Url");
                Status = (Status)reader.ReadInt("Status_Id");
                PublicKey = reader.ReadString("Public_Key");
                PrivateKey = reader.ReadString("Private_Key");
                ModifyDate = reader.ReadDateTime("Modify_Date");
                CampaignId = reader.OptInt("Campaign_Id");
                _categories = new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Website #" + Id, ex);
            }
        }

        public int Id { get; private set; }
        public int PublisherId { get; private set; }
        public string Url { get; private set; }
        public Status Status { get; private set; }
        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }
        public DateTime ModifyDate { get; private set; }
        public int TotalRevenue { get; private set; }
        
        public void AddCategoryId(int categoryId)
        {
            _categories.Add(categoryId);
        }

        public int? CampaignId { get; private set; }
        public AdsCaptchaOperationResult<int> CreateCampaign(string campaignName)
        {
            AdsCaptchaOperationResult<int> result;
            if (CampaignId.HasValue)
            {
                result = AdsCaptchaOperationResult<int>.ToError(AdsCaptchaErrors.InvalidOperation,
                                                                description: "CampaignId already exist");
            }
            else
            {
                result = CampaignId = WebsitesDataAccess.InsertCampaign(Id, PublisherId, campaignName ?? Url);
            }

            return result;
        }
    }
}