using System;

namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface ICaptchaAttackNotificationMailArgs : IMailBuilderArgs
    {
        DateTime AttackDate { get; }
        CaptchaSecurityLevel PreviousSecurityLevel { get; }
        CaptchaSecurityLevel CurrentSecurityLevel { get; }
        int PublisherId { get; }
        long CaptchaId { get; }
        int WebsiteId { get; }
    }
}