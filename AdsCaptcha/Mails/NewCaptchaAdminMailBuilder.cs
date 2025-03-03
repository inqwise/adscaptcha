using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Mails
{
    public class NewCaptchaAdminMailBuilder : MailBuilder<INewCaptchaAdminMailArgs>, INewCaptchaAdminMailArgs
    {
        public IPublisher Publisher { get; private set; }

        protected override string TemplateGroupRelativePath
        {
            get { return @"Captcha\NewCaptchaAdminEmail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            SetRecipients(ApplicationConfiguration.AdminEmail.Value);
            Publisher = PublishersManager.Get(Args.PublisherId).Value;
            base.OnBeforeBuild();
        }

        /*
             string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditCaptcha.aspx?PublisherId=" + publisherId.ToString() + "&WebsiteId=" + websiteId.ToString() + "&CaptchaId=" + captchaId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Captcha (#" + captchaId + ")";
            string body = @"
The site owner <a href='mailto:" + email + "'>" + email + @"</a> has just added new CAPTCHA.<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
<tr><td><b>URL:</b></td><td><a href='" + url + "'>" + url + @"</a></td></tr>
<tr><td><b>Type:</b></td><td>" + DictionaryBLL.GetNameById(type) + @"</td></tr>
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";
             */

        public long CaptchaId
        {
            get { return Args.CaptchaId; }
        }

        public string WebsiteUrl
        {
            get { return Args.WebsiteUrl; }
        }

        public int WebsiteId
        {
            get { return Args.WebsiteId; }
        }

        public int PublisherId
        {
            get { return Args.PublisherId; }
        }
    }
}