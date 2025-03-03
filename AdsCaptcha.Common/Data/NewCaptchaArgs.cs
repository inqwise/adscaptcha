using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class NewCaptchaArgs
    {
        public NewCaptchaArgs()
        {
        }

        public NewCaptchaArgs(JsonObject args)
        {
            Name = args.GetString("captchaName");
            SourceType = (CaptchaSourceType)args.GetInt("captchaSourceTypeId");
            Width = args.GetInt("width");
            Height = args.GetInt("height");
            WebsiteId = args.OptInt("websiteId");
            SecurityLevelId = args.OptInt("securityLevelId", (int)CaptchaSecurityLevel.Low).GetValueOrDefault();
        }

        public string Name { get; set; }
        public CaptchaSourceType SourceType { get; set; }
        public int? PublisherId { get; set; }
        public int? WebsiteId { get; set; }
        public long? CaptchaId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool SendAdminEmail { get; set; }
        public int SecurityLevelId { get; set; }
    }
}