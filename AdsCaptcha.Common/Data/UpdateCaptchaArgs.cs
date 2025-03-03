using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class UpdateCaptchaArgs
    {
        public UpdateCaptchaArgs(JsonObject args)
        {
            CaptchaId = args.GetInt("captchaId");
            Name = args.GetString("captchaName");
            SourceType = (CaptchaSourceType)args.GetInt("captchaSourceTypeId");
            Width = args.GetInt("width");
            Height = args.GetInt("height");
            SecurityLevelId = args.OptInt("securityLevelId");
            AttackDetectionAutoChange = args.OptBool("autoDetect").GetValueOrDefault();
        }

        public UpdateCaptchaArgs(long captchaId)
        {
            CaptchaId = captchaId;
        }

        public int? PublisherId { get; set; }
        public long CaptchaId { get; private set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public CaptchaSourceType SourceType { get; set; }
        public Status? Status { get; set; }
        public int? SecurityLevelId { get; set; }
        public bool? AttackDetectionAutoChange { get; set; }
    }
}