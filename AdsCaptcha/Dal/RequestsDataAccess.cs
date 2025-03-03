using System;
using System.Data;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class RequestsDataAccess : DataAccess
    {
        private const string USE_HISTORY_AFTER_MINUTES_PARAM_NAME = "@UseHistoryAfterMinutes";
        private const string UPDATE_CLICKED_SP_NAME = "[dbo].[USP_UpdateRequestClicked]";
        private const string LIKE_URL_PARAM_NAME = "@FacebookLikeUrl";
        private const string IMAGE_TYPE_ID_PARAM_NAME = "@ImageTypeId";
        private const string EFFECT_TYPE_ID_PARAM_NAME = "@EffectTypeId";
        private const string WIDTH_PARAM_NAME = "@Width";
        private const string HEIGHT_PARAM_NAME = "@Height";
        private const string CLIENT_IP_PARAM_NAME = "@IP_Address";
        private const string PUBLIC_KEY_PARAM_NAME = "@PublicKey";
        private const string SESSION_ID_PARAM_NAME = "@Session_Id";
        private const string REFERRER_URL_PARAM_NAME = "@Referrer_Url";
        private const string PREVIOUS_REQUEST_GUID_PARAM_NAME = "@RefRequest_Guid";
        private const string CAPTCHA_ID_PARAM_NAME = "@Captcha_Id";
        private const string COUNTRY_ID_PARAM_NAME = "@Country_Id";
        private const string CORRECT_IMAGE_INDEX_PARAM_NAME = "@CorrectFrameIndex";
        private const string REQUEST_GUID_PARAM_NAME = "@Request_Guid";
        private const string REQUEST_ID_PARAM_NAME = "@Request_Id";
        private const string GET_REQUEST_SP_NAME = "USP_Request_GetData";
        private const string INSERT_REQUEST_SP_NAME = "[dbo].[USP_Requests_Insert]";
        private const string COUNT_OF_FRAMES_PARAM_NAME = "@FramesCount";
        private const string INSERT_REQUEST_TAKES_SP_NAME = "USP_RequestTakesAddNew";
        private const string SELECTED_INDEX_PARAM_NAME = "@SelectedFrameIndex";
        private const string SUCCESS_RATE_PARAM_NAME = "@SucessRate";
        private const string LINK_URL_PARAM_NAME = "@Link_Url";
        private const string VISITOR_UID_PARAM_NAME = "@VisitorUid";
        private const string CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME = "@SecurityLevelId";
        private const string CAPTCHA_DIFFICULTY_LEVEL_ID_PARAM_NAME = "@DifficultyLevelId";

        public static IDataReader GetDataReader(long? id, string guid)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_REQUEST_SP_NAME))
            {
                if (null != guid)
                {
                    db.AddInParameter(command, REQUEST_GUID_PARAM_NAME, DbType.String, guid); 
                }
                if (null != id)
                {
                    db.AddInParameter(command, REQUEST_ID_PARAM_NAME, DbType.Int64, id); 
                }
                return db.ExecuteReader(command);
            }
        }

        public static long Insert(NewRequestArgs args, int useHistoryAfterMiinutes)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_REQUEST_SP_NAME))
            {
                db.AddInParameter(command, REQUEST_GUID_PARAM_NAME, DbType.String, args.RequestGuid.ToString());
                db.AddInParameter(command, CORRECT_IMAGE_INDEX_PARAM_NAME, DbType.Int32, args.CorrectIndex);
                db.AddInParameter(command, COUNTRY_ID_PARAM_NAME, DbType.Int32, args.CountryId);
                db.AddInParameter(command, CLIENT_IP_PARAM_NAME, DbType.String, args.ClientIp);
                db.AddInParameter(command, PUBLIC_KEY_PARAM_NAME, DbType.String, args.PublicKey);
                db.AddInParameter(command, SESSION_ID_PARAM_NAME, DbType.String, args.SessionId);
                db.AddInParameter(command, REFERRER_URL_PARAM_NAME, DbType.String, args.ReferrerUrl);
                db.AddInParameter(command, PREVIOUS_REQUEST_GUID_PARAM_NAME, DbType.String, args.PreviousRequestGuid);
                db.AddInParameter(command, CAPTCHA_ID_PARAM_NAME, DbType.Int64, args.CaptchaId);
                db.AddInParameter(command, COUNT_OF_FRAMES_PARAM_NAME, DbType.Int32, args.CountOfFrames);
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, args.Width);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, args.Height);
                if (args.ImageType != ImageType.Undefined)
                {
                    db.AddInParameter(command, IMAGE_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.ImageType);  
                }
                db.AddInParameter(command, EFFECT_TYPE_ID_PARAM_NAME, DbType.Int32, (int)args.EffectType); 
                db.AddInParameter(command, LINK_URL_PARAM_NAME, DbType.String, args.ClickUrl);
                db.AddInParameter(command, LIKE_URL_PARAM_NAME, DbType.String, args.LikeUrl);
                db.AddInParameter(command, USE_HISTORY_AFTER_MINUTES_PARAM_NAME, DbType.Int32, useHistoryAfterMiinutes);

                if (args.SecurityLevel != CaptchaSecurityLevel.Undefined)
                {
                    db.AddInParameter(command, CAPTCHA_SECURITY_LEVEL_ID_PARAM_NAME, DbType.Int32, (int)args.SecurityLevel);
                }

                if (null != args.DifficultyLevelId)
                {
                    db.AddInParameter(command, CAPTCHA_DIFFICULTY_LEVEL_ID_PARAM_NAME, DbType.Int32, args.DifficultyLevelId);
                }

                var objRequestId = db.ExecuteScalar(command);

                return Convert.ToInt64(objRequestId);
            }
        }

        public static int Touch(long requestId, int? selectedIndex, int? successRate, string clientIp, string visitorUid)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_REQUEST_TAKES_SP_NAME))
            {
                db.AddInParameter(command, REQUEST_ID_PARAM_NAME, DbType.Int64, requestId);
                db.AddInParameter(command, SELECTED_INDEX_PARAM_NAME, DbType.Int32, selectedIndex);
                db.AddInParameter(command, SUCCESS_RATE_PARAM_NAME, DbType.Int32, successRate);
                db.AddInParameter(command, CLIENT_IP_PARAM_NAME, DbType.String, clientIp);
                db.AddInParameter(command, VISITOR_UID_PARAM_NAME, DbType.String, visitorUid);

                return Convert.ToInt32(db.ExecuteScalar(command));
            }
        }

        public static void UpdateClicked(string requestGuid)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(UPDATE_CLICKED_SP_NAME))
            {
                db.AddInParameter(command, REQUEST_GUID_PARAM_NAME, DbType.String, requestGuid);
                db.ExecuteNonQuery(command);
            }
        }
    }
}