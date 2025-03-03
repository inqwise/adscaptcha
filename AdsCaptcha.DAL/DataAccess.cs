using System.Configuration;

namespace Inqwise.AdsCaptcha.DAL
{
    public abstract class DataAccess
    {
        protected static string GetConnectionStringByName(string name)
        {
            // Return null on failure.
            string returnValue = null;

            // Get the collection of connection strings.
            ConnectionStringSettingsCollection settings =
            ConfigurationManager.ConnectionStrings;

            // Walk through the collection and return the first
            // connection string matching the name.
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.Name == name)
                        returnValue = cs.ConnectionString;
                    break;
                }
            }
            return returnValue;
        }
    }
}
