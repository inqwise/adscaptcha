using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Inqwise.AdsCaptcha.Dal
{
    public abstract class DataAccess
    {
        protected const string CAPTCHA_DB_NAME = "ADS_CAPTCHA";
        protected static DatabaseProviderFactory Factory = new DatabaseProviderFactory(); 
    }
}