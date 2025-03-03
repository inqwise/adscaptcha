using System;
using System.Collections.Generic;
using System.Linq;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class NewCampaignArgs : INewCampaignResult
    {
        private int? _advertiserId;
        private const string CAMPAIGN_PAYMENT_TYPE_ID_PARAM_NAME = "campaignPaymentTypeId";
        private const string CAMPAIGN_NAME_PARAM_NAME = "campaignName";
        private const string AD_PARAM_NAME = "ad";
        private const string GUID_PARAM_NAME = "guid";
        private const string TIMESTAMP_PARAM_NAME = "timestamp";
        private const string COUNTRIES_PARAM_NAME = "countries";
        private const string CATEGORIES_PARAM_NAME = "categories";
        private const string DAILY_BUDGET_PARAM_NAME = "dailyBudget";
        private const string FROM_DATE_PARAM_NAME = "fromDate";
        private const string TO_DATE_PARAM_NAME = "toDate";

        public Guid Guid { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string CampaignName { get; set; }
        public List<int> CountriesList { get; private set; }
        public List<int> CategoriesList { get; private set; }
        public int DailyBudget { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public CampaignPaymentType CampaignPaymentType { get; set; }

        public NewCampaignArgs(JsonObject args)
        {
            Guid = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            NewAd = new NewAdArgs(args[AD_PARAM_NAME] as JsonObject);
            CountriesList = args.GetMenyInt(COUNTRIES_PARAM_NAME).ToList();
            CategoriesList = args.GetMenyInt(CATEGORIES_PARAM_NAME).ToList();
            CampaignName = args.GetString(CAMPAIGN_NAME_PARAM_NAME);
            DailyBudget = args.GetInt(DAILY_BUDGET_PARAM_NAME);
            FromDate = args.OptDate(FROM_DATE_PARAM_NAME);
            ToDate = args.OptDate(TO_DATE_PARAM_NAME);
            CampaignPaymentType = (CampaignPaymentType) args.GetInt(CAMPAIGN_PAYMENT_TYPE_ID_PARAM_NAME);

        }

        public NewAdArgs NewAd { get; private set; }

        public int? AdvertiserId
        {
            get { return _advertiserId; }
            set { NewAd.AdvertiserId = _advertiserId = value; }

        }

        public JsonObject ToJson()
        {
            var jo = new JsonObject();
            jo.Put(GUID_PARAM_NAME, Guid.ToString());
            jo.Put(TIMESTAMP_PARAM_NAME, TimeStamp.Ticks);
            jo.Put(AD_PARAM_NAME, NewAd.ToJson());
            jo.Put(CAMPAIGN_NAME_PARAM_NAME, CampaignName);
            jo.Put(COUNTRIES_PARAM_NAME, CountriesList);
            jo.Put(CATEGORIES_PARAM_NAME, CategoriesList);
            jo.Put(DAILY_BUDGET_PARAM_NAME, DailyBudget);
            jo.PutDate(FROM_DATE_PARAM_NAME, FromDate);
            jo.PutDate(TO_DATE_PARAM_NAME, ToDate);
            jo.Put(CAMPAIGN_PAYMENT_TYPE_ID_PARAM_NAME, (int) CampaignPaymentType);

            return jo;
        }

        public bool IsBonus { get; set; }

        int INewCampaignResult.CampaignId
        {
            get { return NewAd.CampaignId.Value; }
        }

        long INewCampaignResult.AdId
        {
            get { return NewAd.AdId.Value; }
        }

        long? INewCampaignResult.ImageId
        {
            get { return (null == NewAd.NewImage ? default(long?) : NewAd.NewImage.ImageId); }
        }
    }
}
