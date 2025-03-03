using System;
using System.Data;
using System.Linq;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Common.Data;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Entities;
using Inqwise.AdsCaptcha.Mails;
using NLog;

namespace Inqwise.AdsCaptcha.Managers
{
    public class WebsitesManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static AdsCaptchaOperationResult<INewWebsiteResult> Add(NewWebsiteArgs args)
        {
            AdsCaptchaOperationResult<INewWebsiteResult> result = null;
            try
            {
                if (null == args.PublisherId)
                {
                    result = AdsCaptchaOperationResult<INewWebsiteResult>.ToError(AdsCaptchaErrors.PublisheridNotSet);
                }

                if (null == result && null == args.NewCaptcha)
                {
                    result = AdsCaptchaOperationResult<INewWebsiteResult>.ToError(AdsCaptchaErrors.InvalidArgument);
                }

                if (null == result && null == args.PrivateKey)
                {
                    result = AdsCaptchaOperationResult<INewWebsiteResult>.ToError(AdsCaptchaErrors.PrivatekeyNotSet);
                }

                if (null == result && null == args.PublicKey)
                {
                    result = AdsCaptchaOperationResult<INewWebsiteResult>.ToError(AdsCaptchaErrors.PublickeyNotSet);
                }


                if (null == result)
                {
                    int websiteId;
                    int captchaId;
                    WebsitesDataAccess.Insert(args, out websiteId, out captchaId);
                    args.WebsiteId = websiteId;
                    args.NewCaptcha.CaptchaId = captchaId;
                    result = args;
                }

                // Send notifier to administrator.
                try
                {
                    if (args.SendAdminEmail)
                    {
                        SendNewWebsiteAdminMail(args); 
                    }

                    if (args.SendPublisherEmail)
                    {
                        SendNewWebsitePublisherMail(args);
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorException("Add: Failed to send Mails", ex);
                }

            }
            catch (Exception ex)
            {
                Log.ErrorException("Add: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<INewWebsiteResult>.ToError(AdsCaptchaErrors.GeneralError,
                                                                 description: ex.ToString());
            }

            return result;
        }

        private struct NewWebsitePublisherMailArgs : INewWebsitePublisherMailArgs
        {
            public string WebsiteUrl { get; set; }
            public int PublisherId { get; set; }
        }

        private static void SendNewWebsitePublisherMail(NewWebsiteArgs args)
        {
            AdsCaptchaOperationResult result = null;

            if (null == result)
            {
                if (null == args.PublisherId) throw new ArgumentNullException("args.PublisherId");
            }

            if (null == result)
            {
                // Send mail to administrator.
                MailBuilder.GetInstance(new NewWebsitePublisherMailArgs
                {
                    PublisherId = args.PublisherId.Value,
                    WebsiteUrl = args.WebsiteUrl,
                }).Build().Send();
            }
        }

        private struct NewWebsiteAdminMailArgs : INewWebsiteAdminMailArgs
        {
            public int PublisherId { get; set; }
            public int WebsiteId { get; set; }
            public string WebsiteUrl { get; set; }
        }

        private static void SendNewWebsiteAdminMail(NewWebsiteArgs args)
        {
            AdsCaptchaOperationResult result = null;

            if (null == result)
            {
                if (null == args.WebsiteId) throw new ArgumentNullException("args.WebsiteId");
                if (null == args.PublisherId) throw new ArgumentNullException("args.PublisherId");
            }

            if (null == result)
            {
                // Send mail to administrator.
                MailBuilder.GetInstance(new NewWebsiteAdminMailArgs
                {
                    PublisherId = args.PublisherId.Value,
                    WebsiteId = args.WebsiteId.Value,
                    WebsiteUrl = args.WebsiteUrl,
                }).Build().Send();
            }
        }

        public static AdsCaptchaOperationResult<IWebsite> Get(int websiteId, int? publisherId = null)
        {
            AdsCaptchaOperationResult<IWebsite> result;
            try
            {
                using (IDataReader reader = WebsitesDataAccess.GetReader(websiteId, publisherId))
                {
                    if (reader.Read())
                    {
                        result = new WebsiteEntity(reader);
                    }
                    else
                    {
                        result = AdsCaptchaOperationResult<IWebsite>.ToError(AdsCaptchaErrors.NoResults);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("Get: Unexpected Error Occured", ex);
                result = AdsCaptchaOperationResult<IWebsite>.ToError(AdsCaptchaErrors.GeneralError,
                                                                              description: ex.ToString());
            }

            return result;
        }
    }
}