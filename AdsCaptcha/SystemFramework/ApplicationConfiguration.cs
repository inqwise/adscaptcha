using System;
using System.Collections.Generic;
using System.Configuration;

namespace Inqwise.AdsCaptcha.SystemFramework
{
    public class ApplicationConfiguration
    {
        //public static string SERVER = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        public static string MAIL_SUPPORT = System.Configuration.ConfigurationManager.AppSettings["SupportEmail"];

        //public static string UPLOADS_DIRECTORY = "Uploads//";

        public static int DEFAULT_MIN_BILLING_AMOUNT = 100;
        public static int DEFAULT_MIN_CHECK_AMOUNT = 150;
        public static int MIN_FUNDS_WARNING = 10;
        public static int MIN_PREPAY_FUNDS = 25;
        public static int DEFAULT_BONUS_LIMIT = 100;
        public static int DEFAULT_REVENUE_SHARE_PUBLISHER = 50;
        public static int DEFAULT_REVENUE_SHARE_DEVELOPER = 20;
        //public static int MIN_SUCCESS_RATE = 80;

        public static int MAX_SLOGAN_LENGTH = 20;

        public static int SECURITY_ONLY_WIDTH = 200;
        public static int SECURITY_ONLY_HEIGHT = 200;

        public static List<string> ALLOWED_IMAGE_TYPE_LIST = new List<string>() { "image/gif", "image/jpeg", "image/pjpeg" };
        public static string ALLOWED_IMAGE_TYPE = "GIF, JPG";
        public static int ALLOWED_IMAGE_SIZE = 204800; // 200KB // 102400=100KB
        //public static List<string> ALLOWED_VIDEO_TYPE_LIST = new List<string>() { };
        //public static string ALLOWED_VIDEO_TYPE = "";
        //public static int ALLOWED_VIDEO_SIZE = 1024000 * 3; // 3MB

        //public static string REQUEST_TYPE_SECURITY = "security";
        //public static string REQUEST_TYPE_PPT = "type";
        //public static string REQUEST_TYPE_SLIDER = "slider";
        public static string SESSION_PAYMENT_PREFS_CONTEXT = "PaymentPrefsContext";

        public static Lazy<string> AdminUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["Admin"]);
        public static Lazy<string> ApiUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["API"]);
        public static Lazy<string> SiteUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["Site"]);

        public static Lazy<string> TempFolderPath = new Lazy<string>(() => ConfigurationManager.AppSettings["TempUploadFolder"]);

        public static Lazy<string> EmailTemplatesFolderPath = new Lazy<string>(() =>
            {
                var value = ConfigurationManager.AppSettings["EmailTemplatesFolder"];
                if (null == value) throw new ArgumentNullException("EmailTemplatesFolder");
                return value;
            });

        public static Lazy<string> Environment = new Lazy<string>(() => ConfigurationManager.AppSettings["Environment"]);
        public static Lazy<string> AdminEmail = new Lazy<string>(() => ConfigurationManager.AppSettings["AdminEmail"]);
        public static Lazy<string> StaticBaseUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["Static"] ?? SiteUrl.Value);

        public static Lazy<string> GeoIpPath = new Lazy<string>(() => ConfigurationManager.AppSettings["GeoIpPath"]);
        public static Lazy<string> DefaultFeedbackId = new Lazy<string>(() => ConfigurationManager.AppSettings["Inqwise.DefaultGuid"]);
    }
}