using System;
using System.Collections.Generic;
using System.Linq;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class NewWebsiteArgs : INewWebsiteResult
    {
        private int? _publisherId;
        private int? _websiteId;
        private NewCaptchaArgs _newCaptcha;
        private const string CATEGORIES_PARAM_NAME = "categories";

        public NewWebsiteArgs(JsonObject args)
            : this()
        {
            CategoriesList = args.GetMenyInt(CATEGORIES_PARAM_NAME).ToList();
            WebsiteUrl = args.OptString("websiteUrl");
            NewCaptcha = new NewCaptchaArgs(args["captcha"] as JsonObject);
            CampaignId = args.OptInt("campaignId");
        }

        public NewWebsiteArgs()
        {
            PublicKey = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            PrivateKey = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
        }

        public int? WebsiteId
        {
            get { return _websiteId; }
            set             
            {
                _websiteId = value;
                if (null != NewCaptcha)
                {
                    NewCaptcha.WebsiteId = value;
                }
            }
        }

        long INewWebsiteResult.CaptchaId
        {
            get { return NewCaptcha.CaptchaId.Value; }
        }

        public string WebsiteUrl { get; set; }
        public List<int> CategoriesList { get; set; }
        public int? PublisherId
        {
            get { return _publisherId; }
            set 
            {
                _publisherId = value;
                if (null != NewCaptcha)
                {
                    NewCaptcha.PublisherId = value;
                }
            }
        }

        public NewCaptchaArgs NewCaptcha
        {
            get { return _newCaptcha; }
            set
            {
                _newCaptcha = value;
                _newCaptcha.PublisherId = PublisherId;
                _newCaptcha.WebsiteId = WebsiteId;
            }
        }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public Status? Status { get; set; }

        public bool SendAdminEmail { get; set; }

        public bool SendPublisherEmail { get; set; }

        int INewWebsiteResult.WebsiteId
        {
            get { return WebsiteId.Value; }
        }

        public int? CampaignId { get; set; }
    }
}