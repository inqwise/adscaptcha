using System;
using NLog;
using System.Configuration;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class NLogManager
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool isWriteToLog = false;

        static NLogManager ()
        {
            try
            {
                isWriteToLog = (ConfigurationManager.AppSettings["DebugLogWriting"] != null ? ConfigurationManager.AppSettings["DebugLogWriting"] == "true" : false);
                if (isWriteToLog)
                {
                    logger.Debug("Logger is enabled");
                }
            }
            catch 
            {
                isWriteToLog = false;
            }
        }

        public static bool LogDebug(string message, string component, string uniqueId)
        {
            string updatedMessage = string.Format("[{0} - {1}]: {2}", component, uniqueId, message);
            return LogDebug(updatedMessage);
        }

        public static bool LogDebug (string message)
        {
            if (isWriteToLog)
            {
                logger.Debug(message);
                return true;
            }
            return false;
        }
    }
}