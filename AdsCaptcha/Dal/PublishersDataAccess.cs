using System;
using System.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class PublishersDataAccess : DataAccess
    {
        //PARAMS
        private const string PUBLISHER_ID_PARAM_NAME = "@PublisherId";
        private const string ADVERTISER_ID_PARAM_NAME = "@AdvertiserId";

        //SP
        private const string GET_PUBLISHER_SP_NAME = "USP_GetPublisherData";
        private const string INSERT_ADVERTISER_SP_NAME = "USP_PublisherAddAdvertiser";
        
        public static IDataReader GetReader(int publisherId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_PUBLISHER_SP_NAME))
            {
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, publisherId);

                return db.ExecuteReader(command);
            }
        }

        public static int InsertAdvertiser(int publisherId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(INSERT_ADVERTISER_SP_NAME))
            {
                db.AddInParameter(command, PUBLISHER_ID_PARAM_NAME, DbType.Int32, publisherId);
                db.AddOutParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, sizeof(int));

                db.ExecuteNonQuery(command);

                return Convert.ToInt32(db.GetParameterValue(command, ADVERTISER_ID_PARAM_NAME));
            }
        }
    }
}