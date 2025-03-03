using System;
using System.Security.Policy;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Mails
{
    public class CaptchaAttackNotificationMailBuilder : MailBuilder<ICaptchaAttackNotificationMailArgs>,
                                                        ICaptchaAttackNotificationMailArgs
    {
        public IPublisher Publisher { get; private set; }

        protected override string TemplateGroupRelativePath
        {
            get { return @"Captcha\CaptchaAttackNotificationMail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            Publisher = PublishersManager.Get(Args.PublisherId).Value;
            SetRecipients(Publisher.Email);
            base.OnBeforeBuild();
        }

        public DateTime AttackDate {
            get { return Args.AttackDate; }
        }
        public CaptchaSecurityLevel PreviousSecurityLevel {
            get { return Args.PreviousSecurityLevel; }
        }

        public CaptchaSecurityLevel CurrentSecurityLevel {
            get { return Args.CurrentSecurityLevel; }
        }
        public int PublisherId {
            get { return Args.PublisherId; }
        }
        public long CaptchaId {
            get { return Args.CaptchaId; }
        }

        public int WebsiteId {
            get { return Args.WebsiteId; }
        }
    }
}