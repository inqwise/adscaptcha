using System;
using System.Configuration;
using Inqwise.AdsCaptcha.BLL;

namespace Inqwise.AdsCaptcha.API
{
    public partial class Logger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string logMessage=string.Empty;
            try
            {
                // Get Log Message
                logMessage = Request.QueryString["logMessage"];
                string isWriteToLog = ConfigurationSettings.AppSettings["DebugLogWriting"];
                if (string.IsNullOrEmpty(isWriteToLog)==false && isWriteToLog.ToLower().Equals("true"))
                {
                    NLogManager.logger.Debug(logMessage);
                }
            }
            catch (Exception exp)
            {
                NLogManager.logger.ErrorException("Error writing to log: " + logMessage,exp);
            }
        }
    }
}
