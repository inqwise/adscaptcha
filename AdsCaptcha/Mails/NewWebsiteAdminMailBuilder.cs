using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Mails
{
    public class NewWebsiteAdminMailBuilder : MailBuilder<INewWebsiteAdminMailArgs>, INewWebsiteAdminMailArgs
    {
        public IPublisher Publisher { get; private set; }

        protected override string TemplateGroupRelativePath
        {
            get { return @"Website\NewWebsiteAdminEmail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            SetRecipients(ApplicationConfiguration.AdminEmail.Value);
            Publisher = PublishersManager.Get(Args.PublisherId).Value;
            base.OnBeforeBuild();
        }

        public int PublisherId
        {
            get { return Args.PublisherId; }
        }

        public int WebsiteId
        {
            get { return Args.WebsiteId; }
        }

        public string WebsiteUrl
        {
            get { return Args.WebsiteUrl; }
        }

        /*
             string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditWebsite.aspx?PublisherId=" + publisherId.ToString() + "&WebsiteId=" + websiteId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Website (#" + websiteId + ")";
            string body = @"
            The site owner <a href='mailto:" + email + "'>" + email + @"</a> has just added new website.<br /><br />
            Please review the website in order to catalogue it and approve/reject it.
            <table>
            <tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
            <tr><td><b>URL:</b></td><td><a href='" + url + "'>" + url + @"</a></td></tr>
            </table>
            <br/><br/>
            Click <a href='" + link + @"'>here</a> to view and edit details.
            ";
             */
    }
}