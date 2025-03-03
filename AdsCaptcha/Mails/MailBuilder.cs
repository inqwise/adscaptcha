using System.Linq;
using Antlr4.StringTemplate;
using Inqwise.AdsCaptcha.Common.Mails;
using Inqwise.AdsCaptcha.SystemFramework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Inqwise.AdsCaptcha.Mails
{
    public abstract class MailBuilder
    {
        private static readonly Lazy<Dictionary<Type, Func<MailBuilder>>> BuildersMap =
            new Lazy<Dictionary<Type, Func<MailBuilder>>>(
                () =>
                new Dictionary<Type, Func<MailBuilder>>
                    {
                        // Ad
                        {typeof (INewAdAdminMailArgs), () => new NewAdAdminMailBuilder()},
                        // Campaign
                        {typeof (INewCampaignAdminMailArgs), () => new NewCampaignAdminMailBuilder()},
                        // Captcha
                        {typeof (INewCaptchaAdminMailArgs), () => new NewCaptchaAdminMailBuilder()},
                        {typeof (ICaptchaAttackNotificationMailArgs) , () => new CaptchaAttackNotificationMailBuilder()},
                        // Website
                        {typeof (INewWebsiteAdminMailArgs), () => new NewWebsiteAdminMailBuilder()},
                        {typeof (INewWebsitePublisherMailArgs), () => new NewWebsitePublisherMailBuilder()},
                        
                    });
        public abstract IMail Build();
        protected abstract MailBuilder SetArgs(IMailBuilderArgs args);
        public static MailBuilder GetInstance(IMailBuilderArgs args)
        {
            Func<MailBuilder> builderInstanciator = (from p in BuildersMap.Value where p.Key.IsInstanceOfType(args) select p.Value).FirstOrDefault();

            if (null == builderInstanciator)
            {
                throw new NotImplementedException();
            }

            return builderInstanciator().SetArgs(args);
        }

        public string From { get; private set; }
        public string Recipients { get; private set; }

        public MailBuilder SetFrom(string from)
        {
            From = from;
            return this;
        }

        public MailBuilder SetRecipients(string recepients)
        {
            Recipients = recepients;
            return this;
        }
    }

    public abstract class MailBuilder<T> : MailBuilder, IMail
    {
        public const string SUBJECT_KEY = "subject";
        public const string BODY_KEY = "body";
        private const string DATA_KEY = "data";

        public T Args { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public string StaticBaseUrl
        {
            get { return ApplicationConfiguration.StaticBaseUrl.Value; }
        }

        public string SiteUrl
        {
            get { return ApplicationConfiguration.SiteUrl.Value; }
        }

        public string AdminBaseUrl
        {
            get { return ApplicationConfiguration.AdminUrl.Value; }
        }

        public string Environment {
            get { return ApplicationConfiguration.Environment.Value; }
        }

        protected sealed override MailBuilder SetArgs(IMailBuilderArgs args)
        {
            Args = (T)args;
            return this;
        }

        protected static TemplateGroup GetTemplateGroup(string relativePath) 
        {
            string absolutePath = GetTemplateAbsolutePath(relativePath);
            return new TemplateGroupFile(absolutePath);
        }

        public static string GetTemplateAbsolutePath(string relativePath) 
        {
            return Path.Combine(ApplicationConfiguration.EmailTemplatesFolderPath.Value, relativePath);
        }

        protected abstract string TemplateGroupRelativePath { get; }

        protected virtual void OnBeforeBuild()
        {
            
        }

        public override IMail Build()
        {
            OnBeforeBuild();

            if (null == Recipients)
            {
                throw new ArgumentNullException("Recipients");
            }

            var group = GetTemplateGroup(TemplateGroupRelativePath);
            group.RegisterRenderer(typeof(DateTime), new DateRenderer());

            var subjectTemplate = group.GetInstanceOf(SUBJECT_KEY);
            var bodyTemplate = group.GetInstanceOf(BODY_KEY);
            subjectTemplate.Add(DATA_KEY, this);
            bodyTemplate.Add(DATA_KEY, this);
            
            Subject = subjectTemplate.Render();
            Body = bodyTemplate.Render();

            return this;
        }

        public void Send()
        {
            Emailer.Send(this);
        }
    }
}
