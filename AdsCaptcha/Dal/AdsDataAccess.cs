using System;
using System.Data;
using Inqwise.AdsCaptcha.Common.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class AdsDataAccess : DataAccess
    {
        private const string GET_ADS_SP_NAME = "[USP_GetCampaignAds]";
        private const string GET_LAST_AD_SP_NAME = "[USP_GetLastAdData]";
        private const string GET_AD_SP_NAME = "[USP_GetAdData]";
        private const string UPDATE_AD_SP_NAME = "[USP_UpdateAd]";
        private const string ADVERTISER_ID_PARAM_NAME = "@AdvertiserId";
        private const string INSERT_AD_SP_NAME = "[USP_AddNewAd]";
        private const string AD_NAME_PARAM_NAME = "@adName";
        private const string WIDTH_PARAM_NAME = "@width";
        private const string HEIGHT_PARAM_NAME = "@height";
        private const string CLICK_URL_PARAM_NAME = "@adUrl";
        private const string LIKE_URL_PARAM_NAME = "@adLikeUrl";
        private const string MAX_BID_PARAM_NAME = "@maxCpt";
        private const string CAMPAIGN_ID_PARAM_NAME = "@CampaignId";
        private const string AD_ID_PARAM_NAME = "@Ad_Id";
        private const string STATUS_ID_PARAM_NAME = "@StatusId";
        private const string AD_EXTERNAL_ID_PARAM_NAME = "@Ad_ExternalId";
        private const string DELETE_AD_SP_NAME = "[USP_DeleteAd]";

        public static long Insert(NewAdArgs args)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_AD_SP_NAME))
            {
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, args.AdvertiserId);
                db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, args.CampaignId);
                if (null != args.NewImage)
                {
                    db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.NewImage.Width);
                    db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.NewImage.Height);
                }
                db.AddInParameter(command, AD_NAME_PARAM_NAME, DbType.String, args.AdName);
                db.AddInParameter(command, CLICK_URL_PARAM_NAME, DbType.String, args.ClickUrl);
                db.AddInParameter(command, LIKE_URL_PARAM_NAME, DbType.String, args.LikeUrl);
                db.AddInParameter(command, MAX_BID_PARAM_NAME, DbType.Decimal, args.MaxBid);
                
                // output
                db.AddOutParameter(command, AD_ID_PARAM_NAME, DbType.Int32, sizeof(int));
                db.ExecuteNonQuery(command);

                return Convert.ToInt64(db.GetParameterValue(command, AD_ID_PARAM_NAME));
            }
        }

        public static void Update(UpdateAdArgs args)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(UPDATE_AD_SP_NAME))
            {
                db.AddInParameter(command, AD_ID_PARAM_NAME, DbType.Int32, args.AdId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, args.AdvertiserId);
                if (null != args.NewImage)
                {
                    db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.NewImage.Width);
                    db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.NewImage.Height);
                }
                db.AddInParameter(command, AD_NAME_PARAM_NAME, DbType.String, args.AdName);
                db.AddInParameter(command, CLICK_URL_PARAM_NAME, DbType.String, args.ClickUrl);
                db.AddInParameter(command, LIKE_URL_PARAM_NAME, DbType.String, args.LikeUrl);
                db.AddInParameter(command, MAX_BID_PARAM_NAME, DbType.Decimal, args.MaxBid);
                db.AddInParameter(command, STATUS_ID_PARAM_NAME, DbType.Int32, (int)args.Status); 
                
                db.ExecuteNonQuery(command);
            }
        }

        public static IDataReader GetReader(long adId, int? advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_AD_SP_NAME))
            {
                db.AddInParameter(command, AD_ID_PARAM_NAME, DbType.Int32, adId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);
                
                return db.ExecuteReader(command);
            }
        }

        public static IDataReader GetReader(string adExternalId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_AD_SP_NAME))
            {
                db.AddInParameter(command, AD_EXTERNAL_ID_PARAM_NAME, DbType.String, adExternalId);

                return db.ExecuteReader(command);
            }
        }

        public static IDataReader GetLastReader(int campaignId, int? advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_LAST_AD_SP_NAME))
            {
                db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, campaignId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);

                return db.ExecuteReader(command);
            }
        }

        public static DsAds GetMeny(long campaignId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_ADS_SP_NAME))
            {
                db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, campaignId);

                var ds = new DsAds();
                db.LoadDataSet(command, ds, ds.Ads.TableName);

                return ds;
            }
        }

        public static void Delete(long adId, int? advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(DELETE_AD_SP_NAME))
            {
                db.AddInParameter(command, AD_ID_PARAM_NAME, DbType.Int64, adId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);

                db.ExecuteNonQuery(command);
            }
        }
    }
}