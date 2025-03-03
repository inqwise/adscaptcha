using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Managers;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.Mails
{
    internal class NewAdAdminMailBuilder : MailBuilder<INewAdAdminMailArgs>, INewAdAdminMailArgs
    {
        protected override string TemplateGroupRelativePath
        {
            get { return @"Ad\NewAdAdminEmail.stg"; }
        }

        protected override void OnBeforeBuild()
        {
            SetRecipients(ApplicationConfiguration.AdminEmail.Value);
            Advertiser = AdvertisersManager.Get(Args.AdvertiserId).Value;
            base.OnBeforeBuild();
        }

        public IAdvertiser Advertiser { get; private set; }

        public int AdvertiserId
        {
            get { return Args.AdvertiserId; }
        }

        public string CampaignName
        {
            get { return Args.CampaignName; }
        }

        public string AdName
        {
            get { return Args.AdName; }
        }

        public long AdId
        {
            get { return Args.AdId; }
        }

        public int CampaignId
        {
            get { return Args.CampaignId; }
        }

        public long? ImageId
        {
            get { return Args.ImageId; }
        }
    }
}
