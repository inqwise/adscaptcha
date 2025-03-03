using System;
using System.Web;
using Inqwise.AdsCaptcha.BLL;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Managers;
using NLog;

namespace Inqwise.AdsCaptcha.Publisher
{
    public partial class PublisherAccount : System.Web.UI.MasterPage
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public string BaseUrl = "/";


        private readonly Lazy<int?> _publisherId = new Lazy<int?>(() =>
            { var obj = HttpContext.Current.Session["PublisherId"];
                try
                {
                    return Convert.ToInt32(obj);
                }
                catch (Exception)
                {
                    return null;
                }
            });
        protected int? PublisherId
        {
            get { return _publisherId.Value; }
        }

        private IPublisher _publisher;
        protected IPublisher Publisher {
            get
            {
                if (null == _publisher && PublisherId.HasValue)
                {
                    var publisherResult = PublishersManager.Get(PublisherId.Value);
                    if (publisherResult.HasValue)
                    {
                        _publisher = publisherResult.Value;
                    }
                }

                return _publisher;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == PublisherId)
            {
                Response.Redirect("Login.aspx");
            }
            
            try
            {
                if (Publisher == null)
                {
                    // TODO: Handle publisher not exists
                    throw new Exception("Publisher not exists");
                }

                if ((this.MainContent.Page is IPublisherManageWebsites) ||
                    (this.MainContent.Page is IPublisherNewCaptcha) ||
                    (this.MainContent.Page is IPublisherManageCaptchas) ||
                    (this.MainContent.Page is IPublisherEditWebsite) ||
                    (this.MainContent.Page is IPublisherEditCaptcha) ||
                    (this.MainContent.Page is IPublisherSignUp2))
                {
                    aMenuManageWebsites.Attributes["class"] = "activeitem";
                }
                else if ((this.MainContent.Page is IPublisherEarningsSummary) ||
                    (this.MainContent.Page is IPublisherPaymentHistory))
                {
                    aMenuEarningsSummary.Attributes["class"] = "activeitem";
                }
                else if ((this.MainContent.Page is IPublisherAccountPreferences) ||
                    (this.MainContent.Page is IPublisherPaymentPreferences) ||
                    (this.MainContent.Page is IPublisherChangePassword))
                {
                    aMenuAccountPreferences.Attributes["class"] = "activeitem";
                }
                else if ((this.MainContent.Page is IPublisherNewWebsite))
                {
                    aMenuNewWebsite.Attributes["class"] = "activeitem";
                }

                decimal totalEarnings = PublisherBLL.GetTotalEarnings(PublisherId.Value);
                labelEarningsSum.Text = String.Format("${0:#,##0.00}", totalEarnings);
            }
            catch(Exception ex)
            {
                Response.Redirect("Login.aspx");
            }

           
        }
    }

    
}
