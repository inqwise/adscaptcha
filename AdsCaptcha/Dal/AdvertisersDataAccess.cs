using System.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class AdvertisersDataAccess : DataAccess
    {
        private const string GET_ADVERTISER_SP_NAME = "USP_GetAdvertiserData";
        private const string ADVERTISER_ID_PARAM_NAME = "@AdvertiserId";

        public static IDataReader GetReader(int advertiserId)
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_ADVERTISER_SP_NAME))
            {
                db.AddInParameter(command, ADVERTISER_ID_PARAM_NAME, DbType.Int32, advertiserId);

                return db.ExecuteReader(command);
            }
        }
    }
}