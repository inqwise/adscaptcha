using System;
using System.Data;
using Inqwise.AdsCaptcha.Common.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class CampaignsDataAccess : DataAccess
    {
        private const string ADVERTISER_ID_PARAM_NAME = "@advertiserId";
        private const string CAMPAIGN_NAME_PARAM_NAME = "@campaignName";
        private const string COUNTRIES_PARAM_NAME = "@listOfCountryiDs";
        private const string CATEGORIES_PARAM_NAME = "@listOfCategoryIDs";
        private const string DAILY_BUDGET_PARAM_NAME = "@dailyBudget";
        private const string CAMPAIGN_PAYMENT_TYPE_ID_PARAM_NAME = "@campaignPaymentType";
        private const string START_DATE_PARAM_NAME = "@startDate";
        private const string END_DATE_PARAM_NAME = "@endDate";
        private const string AD_NAME_PARAM_NAME = "@adName";
        private const string WIDTH_PARAM_NAME = "@width";
        private const string HEIGHT_PARAM_NAME = "@height";
        private const string CLICK_URL_PARAM_NAME = "@adUrl";
        private const string LIKE_URL_PARAM_NAME = "@adLikeUrl";
        private const string MAX_BID_PARAM_NAME = "@maxCpt";
        private const string CAMPAIGN_ID_PARAM_NAME = "@CampaignId";
        private const string AD_ID_PARAM_NAME = "@Ad_Id";
        private const string BONUS_CAMPAIGN_PARAM_NAME = "@bonusCampaign";

        private const string INSERT_CAMPAIGN_SP_NAME = "USP_AddNewCampaign";
        private const string GET_CAMPAIGN_SP_NAME = "USP_GetCampaignData";
        

        public static void Insert(NewCampaignArgs args, out int campaignId, out int adId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_CAMPAIGN_SP_NAME))
            {
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, args.AdvertiserId);
                db.AddInParameter(command, CAMPAIGN_NAME_PARAM_NAME, DbType.String, args.CampaignName);
                if (null != args.NewAd.NewImage)
                {
                    db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.NewAd.NewImage.Width);
                    db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.NewAd.NewImage.Height);
                }
                db.AddInParameter(command, COUNTRIES_PARAM_NAME, DbType.String, String.Join(",", args.CountriesList));
                db.AddInParameter(command, CATEGORIES_PARAM_NAME, DbType.String, String.Join(",", args.CategoriesList));
                db.AddInParameter(command, DAILY_BUDGET_PARAM_NAME, DbType.Int32, args.DailyBudget);
                db.AddInParameter(command, CAMPAIGN_PAYMENT_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.CampaignPaymentType);
                db.AddInParameter(command, START_DATE_PARAM_NAME, DbType.DateTime2, args.FromDate);
                db.AddInParameter(command, END_DATE_PARAM_NAME, DbType.DateTime2, args.ToDate);
                db.AddInParameter(command, AD_NAME_PARAM_NAME, DbType.String, args.NewAd.AdName);
                db.AddInParameter(command, CLICK_URL_PARAM_NAME, DbType.String, args.NewAd.ClickUrl);
                db.AddInParameter(command, LIKE_URL_PARAM_NAME, DbType.String, args.NewAd.LikeUrl);
                db.AddInParameter(command, MAX_BID_PARAM_NAME, DbType.Decimal, args.NewAd.MaxBid);
                db.AddInParameter(command, BONUS_CAMPAIGN_PARAM_NAME, DbType.Boolean, args.IsBonus);
                
                // output
                db.AddOutParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, sizeof(int));
                db.AddOutParameter(command, AD_ID_PARAM_NAME, DbType.Int32, sizeof(int));

                db.ExecuteNonQuery(command);

                campaignId = Convert.ToInt32(db.GetParameterValue(command, CAMPAIGN_ID_PARAM_NAME));
                adId = Convert.ToInt32(db.GetParameterValue(command, AD_ID_PARAM_NAME));
            }
        }

        public static IDataReader GetReader(int campaignId, int? advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_CAMPAIGN_SP_NAME))
            {
                db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, campaignId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);

                return db.ExecuteReader(command);
            }
        }
    }
}