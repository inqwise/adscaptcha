using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Mails
{
    public class NewCampaignAdminMailBuilder : MailBuilder<INewCampaignAdminMailArgs>, INewCampaignAdminMailArgs
    {
        public IAdvertiser Advertiser { get; private set; }

        protected override string TemplateGroupRelativePath
        {
            get { return @"Campaign\NewCampaignAdminEmail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            SetRecipients(ApplicationConfiguration.AdminEmail.Value);
            Advertiser = AdvertisersManager.Get(Args.AdvertiserId).Value;
            base.OnBeforeBuild();
        }

        public int AdvertiserId {
            get { return Args.AdvertiserId; }
        }

        public int CampaignId
        {
            get { return Args.CampaignId; }
        }

        public string CampaignName
        {
            get { return Args.CampaignName; }
        }
    }
}