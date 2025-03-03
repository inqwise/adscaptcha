using System.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public class CountriesDataAccess : DataAccess
    {
        private const string GET_COUNTRIES_SP_NAME = "USP_GetCountries";

        public static IDataReader GetDataReader()
        {
            var db = Factory.Create(CAPTCHA_DB_NAME);
            using (var command = db.GetStoredProcCommand(GET_COUNTRIES_SP_NAME))
            {
                return db.ExecuteReader(command);
            }
        }
    }
}