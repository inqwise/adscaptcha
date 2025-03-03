using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;

namespace Inqwise.AdsCaptcha.Mails
{
    public class NewWebsitePublisherMailBuilder : MailBuilder<INewWebsitePublisherMailArgs>, INewWebsitePublisherMailArgs
    {
        public IPublisher Publisher { get; private set; }

        protected override string TemplateGroupRelativePath
        {
            get { return @"Website\NewWebsitePublisherEmail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            Publisher = PublishersManager.Get(Args.PublisherId).Value;
            SetRecipients(Publisher.Email);
            base.OnBeforeBuild();
        }

        public int PublisherId
        {
            get { return Args.PublisherId; }
        }

        public string WebsiteUrl
        {
            get { return Args.WebsiteUrl; }
        }

        /*
             string server = ConfigurationSettings.AppSettings["URL"];
            string resources = server + "Resources.aspx";
            string contact = server + "ContactUs.aspx";

            string subject = "Your Inqwise new website";

            string body =
                "Hello," + "<br /><br />" +
                "Congratulations on joining Inqwise! You have successfully registered " + website + " with us." + "<br />" +
                "Approval of the above website is on hold, pending evaluation of its content." + "<br />" +
                "The evaluation period will take up to 72 hours, after which you'll receive notification via email." + "<br /><br />" +
                "Please note:" + "<br />" +
                "You can already embed the captcha codes into your website. Until the evaluation is complete, the website will display a 'Security Only' captcha (without the ads)." + "<br />" +
                "Also, you may be interested to know that larger and visually-richer Inqwise's captchas are more lucrative to you as a publisher, as they generate more $$$." + "<br /><br />" +
                "To find out more, please visit our website." + "<br /><br />" +
                "Have a great day," + "<br />" +
                "The <b>Inqwise</b> team";
             */
    }
}