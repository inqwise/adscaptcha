using System;
using System.Diagnostics;
using System.Net;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.BLL
{
    public static class General
    {
        #region Security

        /// <summary>
        /// MD5 encryption.
        /// </summary>
        /// <param name="str">String to encrypt.</param>
        /// <returns>Encrypted (MD5) string.</returns>
        public static string GenerateMD5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        /// <summary>
        /// Generates new randomized password.
        /// </summary>
        /// <returns>Randomized password.</returns>
        public static string GeneratePassword()
        {
            string s = "";
            Random random = new Random(DateTime.Now.Millisecond);

            // Generate random text.
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            char[] array = chars.ToCharArray();

            int index;
            int length = 8;
            
            for (int i = 0; i < length; i++)
            {
                index = random.Next(array.Length - 1);
                s += array[index].ToString();
            }

            return s;
        }

        /// <summary>
        /// Generates random GUID string.
        /// </summary>
        /// <returns>GUID string.</returns>
        public static string GenerateGuid()
        {
            // Generate GUID.
            string guid = System.Guid.NewGuid().ToString();

            return guid;
        }

        #endregion

        #region Error and Logging

        /// <summary>
        /// Write general error to DB.
        /// </summary>
        /// <param name="requestGuid">Request guid.</param>
        /// <param name="func">Function name.</param>
        /// <param name="ex">Exception.</param>
        [Obsolete("Not in use", true)]
        public static bool WriteServiceError(string requestGuid, string module, object errorType, Exception ex)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create error.
                    TCS_ERROR error = new TCS_ERROR();
                    error.Request_Guid = requestGuid;
                    error.Function = module;
                    error.Timestamp = DateTime.Now;
                    error.Message = ex.Message;
                    error.Stack_Trace = ex.StackTrace;
                    error.Source = ex.Source;                    
                    error.Error_Type = (int)errorType;

                    // Add error to DB.
                    dataContext.TCS_ERRORs.InsertOnSubmit(error);

                    // Save changes.
                    dataContext.SubmitChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                // TODO: Handle service exception.
                return false;
            }
        }

        /// <summary>
        /// Write general error to DB.
        /// </summary>
        /// <param name="module">Module name.</param>
        /// <param name="description">Error description.</param>
        public static void WriteGeneralError(string module, string description)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    // Create error.
                    T_ERROR error = new T_ERROR();
                    error.Error_Date = DateTime.Today;
                    error.Module = module;
                    error.Description = description;

                    // Add error to DB.
                    dataContext.T_ERRORs.InsertOnSubmit(error);

                    // Save changes.
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    LogException(ex.Message);
                    System.Diagnostics.Debug.Write("BLL.General.DAL.WriteError: " + ex.Message);
                }
                catch { }
            }
        }
        
        /// <summary>
        /// Write log exception to EventLog.
        /// </summary>
        /// <param name="error">Error message.</param>
        private static void LogException(string error)
        {
            try
            {
                EventLog el = new EventLog();
                el.Source = "AdsCaptcha";
                el.WriteEntry(error);
            }
            catch { }
        }

        #endregion

        #region Misc

        /// <summary>
        /// Round up.
        /// </summary>
        /// <param name="valueToRound"></param>
        /// <returns>Rounded up number.</returns>
        public static double RoundUp(double valueToRound)
        {
            return (Math.Floor(valueToRound + 0.5));
        }

        public static bool IsUrlExists(string url)
        {
            Uri urlCheck = new Uri(url);
            WebRequest request = WebRequest.Create(urlCheck);
            request.Timeout = 10000;

            WebResponse response;
            try
            {
                response = request.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
