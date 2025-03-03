using System;
using System.Collections.Generic;
using Inqwise.AdsCaptcha.Common;

namespace Advertiser
{
    [Serializable]
    public class NewCampaignData
    {
        public string CampaignName;
        public List<int> LanguagesList;
        public List<int> CountriesList;
        public List<int> CategoriesList;
        public List<string> KeywordsList;
        public int DailyBudget;
        public List<DateTime> ScheduleDatesList;

        public string AdName;
        public int AdType;
        public Nullable<int> Width;
        public Nullable<int> Height;
        public string AdSlogan;
        public bool AdRtl;
        public string AdImage;
        public string AdVideo;
        public string AdUrl;
        public string AdLikeUrl;
        public decimal MaxCpt;
        public int CampaignPaymentType;

        public NewCampaignData()
        {
            CampaignName = null;
            LanguagesList = null;
            CountriesList = null;
            CategoriesList = null;
            KeywordsList = null;
            DailyBudget = 0;
            ScheduleDatesList = null;
            AdName = null;
            AdType = 0;
            Width = null;
            Height = null;
            AdSlogan = null;
            AdRtl = false;
            AdImage = null;
            AdVideo = null;
            AdUrl = null;
            AdLikeUrl = null;
            MaxCpt = 0;
            CampaignPaymentType = (int) Inqwise.AdsCaptcha.Common.CampaignPaymentType.Fit;
        }
    }
}