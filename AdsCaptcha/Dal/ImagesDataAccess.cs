using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Dal
{
    public class ImagesDataAccess : DataAccess
    {
        private const string CHANGE_IMAGE_STATUS_SP_NAME = "USP_ChangeImageStatus";
        private const string GET_SPRITE_SP_NAME = "USP_Request_GetSpriteData";
        private const string IS_DELETED_PARAM_NAME = "@ShowDeleted";
        private const string IMAGE_TYPES_PARAM_NAME = "@ImageTypeIds";
        private const string AD_ID_PARAM_NAME = "@Ad_Id";
        private const string CAMPAIGN_ID_PARAM_NAME = "@CampaignId";
        private const string ADVERTISER_ID_PARAM_NAME = "@AdvertiserId";
        private const string CONTENT_TYPE_PARAM_NAME = "@ContentType";
        private const string FILE_EXTENTION_PARAM_NAME = "@Extention";
        private const string INSERT_IMAGE_SP_NAME = "[dbo].[USP_InsertImage]";
        private const string FILE_PATH_PARAM_NAME = "@ImagePath";
        private const string WIDTH_PARAM_NAME = "@Width";
        private const string HEIGHT_PARAM_NAME = "@Height";
        private const string IMAGE_TYPE_ID_PARAM_NAME = "@ImageTypeId";
        private const string FROM_DATE_PARAM_NAME = "@FromDate";
        private const string TO_DATE_PARAM_NAME = "@ToDate";
        private const string GET_IMAGES_SP_NAME = "USP_GetImagesData";
        private const string DELETE_IMAGE_SP_NAME = "USP_DeleteImage";
        private const string IMAGE_ID_PARAM_NAME = "@ImageId";
        private const string GET_IMAGE_SP_NAME = "[dbo].[USP_GetImageData]";
        private const string INSERT_SPRITE_SP_NAME = "USP_SpriteRequests_InsertData";
        private const string ORIG_REQUEST_ID_PARAM_NAME = "@OrigRequestId";
        private const string REQUEST_ID_PARAM_NAME = "@Request_Id";
        private const string EFFECT_TYPE_ID_PARAM_NAME = "@EffectTypeId";
        private const string SPRITE_URL_PARAM_NAME = "@SpriteURL";
        private const string SPRITE_BASE64_PARAM_NAME = "@SpriteBase64";
        private const string CORRECT_FRAME_INDEX_PARAM_NAME = "@CorrectFrameIndex";
        private const string FRAMES_COUNT_PARAM_NAME = "@FramesCount";
        private const string CLIENT_IP_PARAM_NAME = "@ClientIP";
        private const string SPRITE_ID_PARAM_NAME = "@SpriteId";
        private const string IMAGE_STATUS_ID_PARAM_NAME = "@StatusId";
        private const string CAPTCHA_DIFFICULTY_LEVEL_ID_PARAM_NAME = "@DifficultyLevelId";

        private static Lazy<MsgPackSerializer> _serializer = new Lazy<MsgPackSerializer>();
        
        public static void Delete(long id)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(DELETE_IMAGE_SP_NAME))
            {
                db.AddInParameter(command, IMAGE_ID_PARAM_NAME, DbType.Int64, id);
                db.ExecuteNonQuery(command);
            }
        }

        public static DsImages GetMeny(DateTime? fromDate, DateTime? toDate, IEnumerable<ImageType> types, bool deleted, ImageStatus? status)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_IMAGES_SP_NAME))
            {
                var ds = new DsImages();
                db.AddInParameter(command, FROM_DATE_PARAM_NAME, DbType.DateTime2, fromDate);
                db.AddInParameter(command, TO_DATE_PARAM_NAME, DbType.DateTime2, toDate);
                if (null != types)
                {
                    db.AddInParameter(command, IMAGE_TYPES_PARAM_NAME, DbType.String, string.Join(",", types.Cast<int>()));
                }
                db.AddInParameter(command, IS_DELETED_PARAM_NAME, DbType.Boolean, deleted);
                if (status.HasValue)
                {
                    db.AddInParameter(command, IMAGE_STATUS_ID_PARAM_NAME, DbType.Int32, (int)status.Value); 
                }
                db.LoadDataSet(command, ds, ds.Images.TableName);

                return ds;
            }
        }

        public static long Insert(string filePath, int width, int height, int imageTypeId, string extension, string contentType, int? advertiserId, long? adId, int? campaignId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            
            using (var command = db.GetStoredProcCommand(INSERT_IMAGE_SP_NAME))
            {
                db.AddInParameter(command, FILE_PATH_PARAM_NAME, DbType.String, filePath);
                db.AddInParameter(command, FILE_EXTENTION_PARAM_NAME, DbType.String, Path.GetExtension(filePath));
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, width);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, height);
                db.AddInParameter(command, IMAGE_TYPE_ID_PARAM_NAME, DbType.Int32, imageTypeId);
                db.AddInParameter(command, CONTENT_TYPE_PARAM_NAME, DbType.String, contentType);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);
                db.AddInParameter(command, AD_ID_PARAM_NAME, DbType.Int32, adId);
                db.AddInParameter(command, CAMPAIGN_ID_PARAM_NAME, DbType.Int32, campaignId);

                return Convert.ToInt64(db.ExecuteScalar(command));
            }
        }

        public static IDataReader GetReader(long? imageId, long? adId, long? advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_IMAGE_SP_NAME))
            {
                db.AddInParameter(command, IMAGE_ID_PARAM_NAME, DbType.Int64, imageId);
                db.AddInParameter(command, AD_ID_PARAM_NAME, DbType.Int64, adId);
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);
                return db.ExecuteReader(command);
            }
        }

        internal static long InsertSprite(long requestId, int effectTypeId, string spriteUrl, string spriteBase64, long imageId, int correctFrameIndex, int framesCount, string clientIp, int width, int height, int? difficultyLevelId)
        {
            Task.Run(() => { CacheBuilder.GetCache(ExpirationType.Sliding).Add("sp_" + requestId.ToString(), spriteBase64); });

            var db = Factory.Create(CAPTCHA_DB_NAME);

            using (var command = db.GetStoredProcCommand(INSERT_SPRITE_SP_NAME))
            {
                db.AddInParameter(command, ORIG_REQUEST_ID_PARAM_NAME, DbType.Int64, requestId);
                db.AddInParameter(command, EFFECT_TYPE_ID_PARAM_NAME, DbType.Int32, effectTypeId);
                db.AddInParameter(command, SPRITE_URL_PARAM_NAME, DbType.String, spriteUrl);
                db.AddInParameter(command, SPRITE_BASE64_PARAM_NAME, DbType.String, spriteBase64);
                db.AddInParameter(command, IMAGE_ID_PARAM_NAME, DbType.Int64, imageId);
                db.AddInParameter(command, CORRECT_FRAME_INDEX_PARAM_NAME, DbType.Int16, correctFrameIndex);
                db.AddInParameter(command, FRAMES_COUNT_PARAM_NAME, DbType.Int16, framesCount);
                db.AddInParameter(command, CLIENT_IP_PARAM_NAME, DbType.String, clientIp);
                db.AddInParameter(command, WIDTH_PARAM_NAME, DbType.Int32, width);
                db.AddInParameter(command, HEIGHT_PARAM_NAME, DbType.Int32, height);
                if (null != difficultyLevelId)
                {
                    db.AddInParameter(command, CAPTCHA_DIFFICULTY_LEVEL_ID_PARAM_NAME, DbType.Int32, difficultyLevelId);
                }
                db.AddOutParameter(command, SPRITE_ID_PARAM_NAME, DbType.Int64, sizeof(long));

                db.ExecuteNonQuery(command);

                return (long) db.GetParameterValue(command, SPRITE_ID_PARAM_NAME);
            }
        }

        internal static string GetSpriteBase64(long requestId)
        {
            return CacheBuilder.GetCache(ExpirationType.Sliding).GetOrAdd("sp_" + requestId.ToString(), () => InternalGetSpriteBase64(requestId));
        }

        private static string InternalGetSpriteBase64(long requestId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);

            using (var command = db.GetStoredProcCommand(GET_SPRITE_SP_NAME))
            {
                db.AddInParameter(command, REQUEST_ID_PARAM_NAME, DbType.Int64, requestId);

                object objResult = db.ExecuteScalar(command);

                return objResult as string;
            }
        }

        public static void ChangeStatus(long imageId, int imageStatusId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);

            using (var command = db.GetStoredProcCommand(CHANGE_IMAGE_STATUS_SP_NAME))
            {
                db.AddInParameter(command, IMAGE_ID_PARAM_NAME, DbType.Int64, imageId);
                db.AddInParameter(command, IMAGE_STATUS_ID_PARAM_NAME, DbType.Int32, imageStatusId);

                db.ExecuteNonQuery(command);
            }
        }
    }
}