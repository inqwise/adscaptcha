using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class CaptchasDataAccess : DataAccess
    {
        private const string STATUS_ID_PARAM_NAME = "@StatusId";
        private const string PUBLISHER_ID_PARAM_NAME = "@PublisherId";
        private const string WEBSITE_ID_PARAM_NAME = "@WebsiteId";
        private const string WIDTH_PARAM_NAME = "@Width";
        private const string HEIGHT_PARAM_NAME = "@Height";
        private const string CAPTCHA_NAME_PARAM_NAME = "@CaptchaName";
        private const string CAPTCHA_SOURCE_TYPE_ID_PARAM_NAME = "@SourceTypeId";
        private const string CAPTCHA_ID_PARAM_NAME = "@Captcha_Id";
        private const string CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME = "@SecurityLevelId";
        private const string ATTACK_DETECTION_AUTO_CHANGE_PARAM_NAME = "@AttackDetectionAutoChange";

        private const string GET_CAPTCHA_SP_NAME = "USP_GetCaptchaById";
        private const string INSERT_CAPTCHA_SP_NAME = "USP_AddNewCaptcha";
        private const string UPDATE_CAPTCHA_SP_NAME = "USP_UpdateCaptcha";

        public static IDataReader GetDataReader(long captchaId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_CAPTCHA_SP_NAME))
            {
                db.AddInParameter(command, CAPTCHA_ID_PARAM_NAME, DbType.Int64, captchaId);
                return db.ExecuteReader(command);
            }
        }

        public static long Insert(NewCaptchaArgs args)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_CAPTCHA_SP_NAME))
            {
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, args.PublisherId);
                db.AddInParameter(command, WEBSITE_ID_PARAM_NAME, DbType.Int32, args.WebsiteId);
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.Width);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.Height);
                db.AddInParameter(command, CAPTCHA_NAME_PARAM_NAME, DbType.String, args.Name);
                db.AddInParameter(command, CAPTCHA_SOURCE_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.SourceType);
                db.AddInParameter(command, CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME, DbType.Int32, args.SecurityLevelId);
                // output
                db.AddOutParameter(command, CAPTCHA_ID_PARAM_NAME, DbType.Int32, sizeof(int));
                db.ExecuteNonQuery(command);

                return Convert.ToInt64(db.GetParameterValue(command, CAPTCHA_ID_PARAM_NAME));
            }
        }

        public static void Update(UpdateCaptchaArgs args)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(UPDATE_CAPTCHA_SP_NAME))
            {
                db.AddInParameter(command, CAPTCHA_ID_PARAM_NAME, DbType.Int32, args.CaptchaId);
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, args.PublisherId);
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.Width);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.Height);
                db.AddInParameter(command, CAPTCHA_NAME_PARAM_NAME, DbType.String, args.Name);
                db.AddInParameter(command, CAPTCHA_SOURCE_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.SourceType);
                db.AddInParameter(command, STATUS_ID_PARAM_NAME, DbType.Int32, args.Status);
                db.AddInParameter(command, CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME, DbType.Int32, args.SecurityLevelId);
                if (args.AttackDetectionAutoChange.HasValue)
                {
                    db.AddInParameter(command, ATTACK_DETECTION_AUTO_CHANGE_PARAM_NAME, DbType.Boolean,
                                      args.AttackDetectionAutoChange);
                }

                db.ExecuteNonQuery(command);
            }
        }
    }
}