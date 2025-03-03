using System;
using System.Data;
using Inqwise.AdsCaptcha.Common.Data;
using System.Linq;

namespace Inqwise.AdsCaptcha.Dal
{
    public class WebsitesDataAccess : DataAccess
    {
        //PARAMS
        private const string STATUS_ID_PARAM_NAME = "@StatusId";
        private const string PUBLIC_KEY_PARAM_NAME = "@PublicKey";
        private const string PRIVATE_KEY_PARAM_NAME = "@PrivateKey";
        private const string CAPTCHA_ID_PARAM_NAME = "@CaptchaId";
        private const string WEBSITE_ID_PARAM_NAME = "@WebsiteId";
        private const string CAPTCHA_NAME_PARAM_NAME = "@CaptchaName";
        private const string SOURCE_TYPE_ID_PARAM_NAME = "@CaptchaSourceTypeId";
        private const string WIDTH_PARAM_NAME = "@Width";
        private const string HEIGHT_PARAM_NAME = "@Height";
        private const string WEBSITE_URL_PARAM_NAME = "@WebsiteUrl";
        private const string LANGUAGE_ID_PARAM_NAME = "@LanguageId";
        private const string CATEGORIES_PARAM_NAME = "@CategoriesList";
        private const string PUBLISHER_ID_PARAM_NAME = "@PublisherId";
        private const string CAMPAIGN_NAME_PARAM_NAME = "@campaignName";
        private const string CAMPAIGN_ID_PARAM_NAME = "@CampaignId";
        private const string CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME = "@CaptchaSecurityLevelId";

        //SP
        private const string INSERT_WEBSITE_SP_NAME = "USP_AddNewWebsite";
        private const string GET_WEBSITE_SP_NAME = "USP_GetWebsiteData";
        private const string INSERT_CAMPAIGN_SP_NAME = "USP_WebsiteAddNewCampaign";

        public static void Insert(NewWebsiteArgs args, out int websiteId, out int captchaId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_WEBSITE_SP_NAME))
            {
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, args.PublisherId);
                db.AddInParameter(command, CATEGORIES_PARAM_NAME, DbType.String, String.Join(",", args.CategoriesList));
                db.AddInParameter(command, LANGUAGE_ID_PARAM_NAME, DbType.Int32, 0);

                db.AddInParameter(command, WEBSITE_URL_PARAM_NAME, DbType.String, args.WebsiteUrl);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.NewCaptcha.Height);
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.NewCaptcha.Width);
                db.AddInParameter(command, SOURCE_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.NewCaptcha.SourceType);
                db.AddInParameter(command, CAPTCHA_NAME_PARAM_NAME, DbType.String, args.NewCaptcha.Name);

                db.AddInParameter(command, PUBLIC_KEY_PARAM_NAME, DbType.String, args.PublicKey);
                db.AddInParameter(command, PRIVATE_KEY_PARAM_NAME, DbType.String, args.PrivateKey);

                if (args.Status.HasValue)
                {
                    db.AddInParameter(command, STATUS_ID_PARAM_NAME, DbType.Int32, (int)args.Status); 
                }

                if (args.CampaignId.HasValue)
                {
                    db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, args.CampaignId); 
                }

                db.AddInParameter(command, CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME, DbType.Int32, args.NewCaptcha.SecurityLevelId);

                // output
                db.AddOutParameter(command, WEBSITE_ID_PARAM_NAME, DbType.Int32, sizeof(int));
                db.AddOutParameter(command, CAPTCHA_ID_PARAM_NAME, DbType.Int32, sizeof(int));

                

                db.ExecuteNonQuery(command);

                websiteId = Convert.ToInt32(db.GetParameterValue(command, WEBSITE_ID_PARAM_NAME));
                captchaId = Convert.ToInt32(db.GetParameterValue(command, CAPTCHA_ID_PARAM_NAME));
            }
        }

        public static IDataReader GetReader(int websiteId, int? publisherId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_WEBSITE_SP_NAME))
            {
                db.AddInParameter(command, WEBSITE_ID_PARAM_NAME, DbType.Int32, websiteId);
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, publisherId);

                return db.ExecuteReader(command);
            }
        }

        public static int InsertCampaign(int? websiteId, int publisherId, string campaignName)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_CAMPAIGN_SP_NAME))
            {
                db.AddInParameter(command, WEBSITE_ID_PARAM_NAME, DbType.Int32, websiteId);
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, publisherId);
                db.AddInParameter(command, CAMPAIGN_NAME_PARAM_NAME, DbType.String, campaignName);
                db.AddOutParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, sizeof(Int32));

                db.ExecuteNonQuery(command);

                return Convert.ToInt32(db.GetParameterValue(command, CAMPAIGN_ID_PARAM_NAME));
            }
        }

    }
}