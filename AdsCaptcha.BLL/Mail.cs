using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using Inqwise.AdsCaptcha.DAL;
using Inqwise.AdsCaptcha.SystemFramework;

namespace Inqwise.AdsCaptcha.BLL
{
    public enum Modules
    {
        Publisher = 1,
        Advertiser = 2,
        Admin = 3,
        Developer = 4
    }

    public static class Mail
    {
        #region Entities

        private class EmailMessage
        {
            public EmailMessage(string from, string recipients, string subject, string body)
            {
                this.From = from;
                this.Recipients = recipients;
                this.Subject = subject;
                this.Body = body;
            }

            public string From { get; set; }
            public string Recipients { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }

        #endregion Entities

        #region Private Methods

        private static bool SendMailDB(EmailMessage message)
        {
            try
            {
                using (AdsCaptchaDataContext dataContext = new AdsCaptchaDataContext())
                {
                    int result = dataContext.SendMail(message.Recipients, message.Subject, message.Body);

                    return (result == 0 ? true : false);
                }
            }
            catch
            {
                return false;
            }
        }

        private static bool SendMailSMTP(EmailMessage message)
        {
            try
            {
                string smtpServer = ConfigurationSettings.AppSettings["SmtpServer"];
                int smtpPort = Convert.ToInt32(ConfigurationSettings.AppSettings["SmtpPort"]);
                string smtpSSL = ConfigurationSettings.AppSettings["SmtpSSL"].ToLower();
                string smtpUser = ConfigurationSettings.AppSettings["SmtpUser"];
                string smtpPass = ConfigurationSettings.AppSettings["SmtpPass"];

                MailMessage mailMessage = new MailMessage();

                foreach (string recipient in message.Recipients.Split(';')) mailMessage.To.Add(recipient);
                mailMessage.Subject = message.Subject;
                
                // roei: cancel send mail from developer mail
                /*
                if (message.From != null)
                    mailMessage.From = new MailAddress(message.From);
                else
                */
                mailMessage.From = new MailAddress(smtpUser);
                
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message.Body;

                //SmtpClient smtp = new SmtpClient("mail.com", 26);
                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                smtp.EnableSsl = (smtpSSL == "true" ? true : false);
                smtp.Timeout = 10000;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void SendMailThread(Object msg)
        {
            try
            {
                EmailMessage message = (EmailMessage)msg;
                //string mailMethod = ConfigurationSettings.AppSettings["MailMethod"].ToUpper();

                //bool sent = false;
                
                //if (String.IsNullOrEmpty(message.From))
                //    sent = SendMailDB(message);
                
                //if (!sent)
                    SendMailSMTP(message);
            }
            catch
            {
                // TODO: Log exception.
            }
        }

        #endregion Private Methods

        #region Public Methods

        public static void SendMail(string from, string recipients, string subject, string body)
        {
            try
            {
                string env = ConfigurationSettings.AppSettings["Environment"];
                string server = ConfigurationSettings.AppSettings["URL"];

                string newBody = body;
                string newSubject = subject;

                if (env != "Prod")
                    newSubject += (" | " + env);

                newBody = "<div style='display:block;margin-bottom:20px;'>" +
                          "<a href='" + server + "'><img src='" + server + "images/logo_mail.png' width='181' height='43' border='0'></a>" +
                          "</div>" +
                          "<font style='font-size:14px;'>" +
                          body +
                          "</font>";

                EmailMessage emailMessage = new EmailMessage(from, recipients, newSubject, newBody);
                ThreadPool.QueueUserWorkItem(SendMailThread, emailMessage);
            }
            catch (Exception ex)
            {
                // TODO: Log exception.
            }
        }

        public static void SendMail(string recipients, string subject, string body)
        {
            SendMail(null, recipients, subject, body);
        }

        #endregion Public Methods

        #region Mails
        /// <summary>
        /// Send invation mail to publisher (from developer).
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="password">New password.</param>
        /// <param name="developer">Developer name.</param>
        /// <param name="url">Website url.</param>
        public static void SendInviteNewPublisherMail(string publisherEmail, string password, string developerEmail, string developerName, string url)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "Publisher" + "/StartPage.aspx";

            string subject = "Welcome to Inqwise.com";
            string body =
                "Hello," + "<br /><br />" +
                developerName + " has just signed up your website [" + "<a href='" + url + "'>" + url + "</a>" + "] to Inqwise!" + "<br /><br />" +
                "Your new account details:" + "<br />" +
                "Username: " + "<b>" + publisherEmail + "</b>" + "<br />" +
                "Password: " + "<b>" + password + "</b>" + "<br /><br />" +
                "Please click " + "<a href='" + link + "'>here</a>" + " to login to your account." + "<br /><br />" +
                "We highly recommend that you change your password upon your first login." + "<br /><br />" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(developerEmail, publisherEmail, subject, body);
        }

        /// <summary>
        /// Send website change (by developer) notification to publisher.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="developer">Developer name.</param>
        public static void SendWebsiteChangeByDeveloper(string email, int websiteId, string url, string developer)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "Publisher" + "/EditWebsite.aspx?WebsiteId=" + websiteId.ToString();

            string subject = "Your website has been modified | Inqwise Notification";
            string body =
                "Hello," + "<br /><br />" +
                developer + " has modified your website's settings [" + "<a href='" + url + "'>" + url + "</a>" + "]." + "<br /><br />" +
                "To view/modify your website settings, please click " + "<a href='" + link + "'>here</a>" + "." + "<br /><br />" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send captcha change (by developer) notification to publisher.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="developer">Developer name.</param>
        public static void SendCaptchaChangeByDeveloper(string email, int captchaId, int websiteId, string developer)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "Publisher" + "/EditCaptcha.aspx?WebsiteId=" + websiteId.ToString() + "&CaptchaId=" + captchaId.ToString();

            string subject = "Your CAPTCHA c modified | Inqwise Notification";
            string body =
                "Hello," + "<br /><br />" +
                developer + " has modified your CAPTCHA's settings." + "<br /><br />" +
                "To view/modify your CAPTCHA settings, please click " + "<a href='" + link + "'>here</a>" + "." + "<br /><br />" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send activation mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="activationCode">Activation code.</param>
        public static void SendPublisherActivationMail(string email, string activationCode)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "Publisher" + "/Activate.aspx?c=" + activationCode;

            string subject = "Welcome to Inqwise";
            string body =
                "Thanks for joining Inqwise!" + "<br /><br />" +
                "To verify your Inqwise account, please click the link below, or copy and paste it to your internet browser, to verify your Inqwise account:" + "<br />" +
                "<a href='" + link + "'>" + link + "</a>" + "<br /><br />" +
                "If the link above does not work, please contact our support team: " +
                "<a href='mailto:" + ApplicationConfiguration.MAIL_SUPPORT + "'>" + ApplicationConfiguration.MAIL_SUPPORT + "</a>" + "." + "<br /><br />" +
                "As a verified Inqwise account holder, we recommend that you take the time to manage your account, define the websites that you own, and attach an Inqwise to each of your websites." + "<br /><br />" +
                "For any assistance, please contact our support service at " +
                "<a href='mailto:" + ApplicationConfiguration.MAIL_SUPPORT + "'>" + ApplicationConfiguration.MAIL_SUPPORT + "</a>" + "." + "<br /><br />" +
                "Welcome aboard," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send activation mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="activationCode">Activation code.</param>
        public static void SendDeveloperActivationMail(string email, string activationCode)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "Developer" + "/Activate.aspx?c=" + activationCode;

            string subject = "Welcome to Inqwise";
            string body =
                "Thanks for joining Inqwise!" + "<br /><br />" +
                "To verify your Inqwise account, please click the link below, or copy and paste it to your internet browser, to verify your Inqwise account:" + "<br />" +
                "<a href='" + link + "'>" + link + "</a>" + "<br /><br />" +
                "If you have trouble with the link above does not work, please contact our support team: " +
                "<a href='mailto:" + ApplicationConfiguration.MAIL_SUPPORT + "'>" + ApplicationConfiguration.MAIL_SUPPORT + "</a>" + "." + "<br /><br />" +
                "As a verified Inqwise developer account holder, we recommend that you take the time to manage your account, define the websites that you own, and attach an Inqwise to each of your websites." + "<br /><br />" +
                "For any assistance, please contact our support service at " +
                "<a href='mailto:" + ApplicationConfiguration.MAIL_SUPPORT + "'>" + ApplicationConfiguration.MAIL_SUPPORT + "</a>" + "." + "<br /><br />" +
                "Welcome aboard," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send new password mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="password">New password.</param>
        public static void SendNewPasswordMail(int module, string email, string password)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string dir = (module == (int)Modules.Publisher) ? "Publisher" : (module == (int)Modules.Advertiser) ? "Advertiser" : "Developer";
            //string link = server + dir + "/Login.aspx";
            string link = server + dir + "/ChangePassword.aspx";

            string subject = "Your Inqwise.com new password";
            string body =
                "Hello," + "<br /><br />" +
                "Your new password is: " + "<b>" + password + "</b>" + "." + "<br />" +
                "Please click " + "<a href='" + link + "'>here</a>" + " to login and change your password." + "<br /><br />" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send new website mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="website">Website url.</param>
        [Obsolete("Not In use", true)]
        public static void SendNewWebsiteSecurityOnlyMail(string email, string website)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string resources = server + "Resources.aspx";
            string contact = server + "ContactUs.aspx";

            string subject = "Your Inqwise new website";

            string body =
                "Hello," + "<br /><br />" +
                "Congratulations on joining Inqwise! You have successfully registered " + website + " with us." + "<br /><br />" +
                "You are now ready to set up our completely FREE Security-Only captcha. For detailed information on how to get started see the <a href='" + resources + "' target='_blank'>Resources Page</a> hosted on our website." + "<br /><br />" +
                "If you need any assistance, please do not hesitate to contact us from the <a href='" + contact + "' target='_blank'>“Contact Us”</a> link on our home page." + "<br /><br />" +
                "Have a great day," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send new website mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        /// <param name="website">Website url.</param>
        [Obsolete("Use AdsCaptcha.Managers.Websites.Add", true)]
        public static void SendNewWebsiteMail(string email, string website)
        {
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

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send website approval by admin.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="publisherEmail">Publisher email.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="websiteUrl">Website url.</param>
        public static void SendWebsiteSecurityOnlyApproveMail(string email, int publisherId, int websiteId, string websiteUrl)
        {
            string server = ConfigurationSettings.AppSettings["URL"];

            string subject = "Your Inqwise website has been approved";

            string body =
                "Hello," + "<br /><br />" +
                "Congratulations, approval of your website (" + websiteUrl + ") has been granted for our captcha platform that combines the security captcha with advertising. This service will allow you to continue to stop spam, and make money!" + "<br /><br />" +
                "The set up is easy, all you need to do is log in to your account and set your captcha type to “GET Paid Per Type”. Or you can decide to do nothing and continue enjoying our Free Security- Only captcha." + "<br /><br />" +
                "For more information, please visit our website at <a href='" + server + "' target='_blank'>www.Inqwise.com</a>." + "<br /><br />" +
                "The <b>Inqwise</b> team wishes you all the best with your site, and are always here to assist you with any questions, problems or suggestions that you may have." + "<br /><br />" +
                "Have a great day," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send website approval by admin.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="publisherEmail">Publisher email.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="websiteUrl">Website url.</param>
        public static void SendWebsiteApproveMail(string email, int publisherId, int websiteId, string websiteUrl)
        {
            string server = ConfigurationSettings.AppSettings["URL"];

            string subject = "Your Inqwise website has been approved";
            
            string body =
                "Hello," + "<br /><br />" +
                "Congratulations, your website (" + websiteUrl + ") has been approved." + "<br />" +
                "You can now stop spam, and make money!" + "<br /><br />" +
                "<br /><br /><b>Please note</b>:  we currently have <u>only</u> campaigns using our advance product:  the <b>Slider</b>  ( Slide To Fit  -  image size 300X250)" +
                "<br />In order to receive faster pay out we recommend you  to  update your captcha type  though your account with us." +
                "<br /><br />The <b>Inqwise</b> team wishes you all the best with your site, and are always here to assist you with any questions, problems or suggestions that you may have." + "<br /><br />" +
                "Have a great day," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send new advertiser mail.
        /// </summary>
        /// <param name="email">Recipient email address.</param>
        public static void SendNewAdvertiserMail(string email)
        {
            string subject = "Welcome to Inqwise";

            string body =
                "Thanks for joining Inqwise!" + "<br /><br />" +
                "You are now a registered advertiser." + "<br /><br />" +
                "For any assistance, please contact our support service at " +
                "<a href='mailto:" + ApplicationConfiguration.MAIL_SUPPORT + "'>" + ApplicationConfiguration.MAIL_SUPPORT + "</a>" + "." + "<br /><br />" +
                "Welcome aboard," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(email, subject, body);
        }

        /// <summary>
        /// Send credit card payment notification.
        /// </summary>
        public static void SendCreditCardPaymentMail(string advertiserEmail)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "ContactUs.aspx";

            string subject = "Your credit card payment has been received";
            string body =
                "Thank you! Your credit card payment has been received." + "<br/>" +
                "To review the transaction details, please check your account by logging in from our website." + "<br/><br/>" +
                "Please note that it may take a few hours for your balance to update." + "<br/><br/>" +
                "If you need any assistance, please do not hesitate to contact us from the <a href='" + link + "'>“Contact Us”</a> link on our home page." + "<br/><br/>" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";

            SendMail(advertiserEmail, subject, body);
        }

        /// <summary>
        /// Send PayPal payment notification.
        /// </summary>
        public static void SendPayPalPaymentMail(string advertiserEmail, string transactionId, decimal amount, string currency)
        {
            string server = ConfigurationSettings.AppSettings["URL"];
            string link = server + "ContactUs.aspx";

            string subject = "Your PayPal payment has been received";
            string body =
                "Thank you! Your PayPal payment has been received." + "<br/>" +
                "To review the transaction details, please check your account by logging in from our website." + "<br/><br/>" +
                "Please note that it may take a few hours for your balance to update." + "<br/><br/>" +
                "If you need any assistance, please do not hesitate to contact us from the <a href='" + link + "'>“Contact Us”</a> link on our home page." + "<br/><br/>" +
                "Thanks," + "<br />" +
                "The <b>Inqwise</b> team";
            /*
            "Payment details:" + "<br /><br />" +
            "<b>" + "Transaction ID : " + "</b>" + transactionId + "<br />" +
            "<b>" + "Amount         : " + "</b>" + amount.ToString() + " " + currency
            */

            SendMail(advertiserEmail, subject, body);
        }


        /// <summary>
        /// Send new developer admin notifier.
        /// </summary>
        /// <param name="developerId">Developer id.</param>
        /// <param name="developerEmail">Developer email.</param>
        public static void SendNewDeveloperAdminMail(int developerId, string email)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditDeveloper.aspx?DeveloperId=" + developerId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Developer (#" + developerId + ")";
            string body = @"
A new developer has just signed up.<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new publisher admin notifier.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="publisherEmail">Publisher email.</param>
        public static void SendNewPublisherAdminMail(int publisherId, string email)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditPublisher.aspx?PublisherId=" + publisherId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Site Owner (#" + publisherId + ")";
            string body = @"
A new site owner has just signed up.<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new website admin notifier.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="publisherEmail">Publisher email.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="websiteUrl">Website url.</param>
        [Obsolete("Use AdsCaptcha.Mails.MailBuilder", true)]
        public static void SendNewWebsiteAdminMail(int publisherId, string email, int websiteId, string url)
        {
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

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new captcha admin notifier.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="publisherEmail">Publisher email.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="websiteUrl">Website url.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="captchaType">Captcha type.</param>
        [Obsolete("Use AdsCaptcha.Mails.MailBuilder", true)]
        public static void SendNewCaptchaAdminMail(int publisherId, string email, int websiteId, string url, int captchaId, int type)
        {
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

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send Captcha type changed admin notifier.
        /// </summary>
        /// <param name="publisherId">Publisher id.</param>
        /// <param name="websiteId">Website id.</param>
        /// <param name="captchaId">Captcha id.</param>
        /// <param name="oldType">Old Captcha type.</param>
        /// <param name="newType">New Captcha type.</param>
        [Obsolete("Not in use", true)]
        public static void SendCaptchaTypeChangedAdminMail(int publisherId, int websiteId, int captchaId, int oldType, int newType)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditCaptcha.aspx?PublisherId=" + publisherId.ToString() + "&WebsiteId=" + websiteId.ToString() + "&CaptchaId=" + captchaId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | Captcha Type Changed (#" + captchaId + ")";
            string body = @"
Captcha's type changed from " + DictionaryBLL.GetNameById(oldType) + @" to " + DictionaryBLL.GetNameById(newType) + @".<br /><br />
Click <a href='" + link + @"'>here</a> to view and edit details.
";

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new advertiser admin notifier.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="advertiserEmail">Advertiser email.</param>
        public static void SendNewAdvertiserAdminMail(int advertiserId, string email)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditAdvertiser.aspx?AdvertiserId=" + advertiserId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Advertiser (#" + advertiserId + ")";
            string body = @"
A new advertiser has just signed up.<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";
            
            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new campaign admin notifier.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="advertiserEmail">Advertiser email.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="campaignName">Campaign name.</param>
        [Obsolete("Use AdsCaptcha.Mails.MailBuilder", true)]
        public static void SendNewCampaignAdminMail(int advertiserId, string email, int campaignId, string campaignName)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditCampaign.aspx?AdvertiserId=" + advertiserId.ToString() + "&CampaignId=" + campaignId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Campaign (#" + campaignId + ")";
            string body = @"
The advertiser <a href='mailto:" + email + "'>" + email + @"</a> has just added new campaign.<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
<tr><td><b>Campaign:</b></td><td>" + campaignName + @"</td></tr>
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new ad admin notifier.
        /// </summary>
        /// <param name="advertiserId">Advertiser id.</param>
        /// <param name="advertiserEmail">Advertiser email.</param>
        /// <param name="campaignId">Campaign id.</param>
        /// <param name="campaignName">Campaign name.</param>
        /// <param name="adId">Ad id.</param>
        /// <param name="adName">Ad name.</param>
        /// <param name="adType">Ad type.</param>
        [Obsolete("Use AdsCaptcha.Mails.MailBuilder", true)]
        public static void SendNewAdAdminMail(int advertiserId, string email, int campaignId, string campaignName, int adId, string adName, string imageUrl)
        {
            string server = ConfigurationSettings.AppSettings["Admin"];
            string link = server + "EditAd.aspx?AdvertiserId=" + advertiserId.ToString() + "&CampaignId=" + campaignId.ToString() + "&AdId=" + adId.ToString();

            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | New Ad (#" + adId + ")";
            string body = @"
The advertiser <a href='mailto:" + email + "'>" + email + @"</a> has just added new ad to the campaign " + campaignName + @".<br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
<tr><td><b>Campaign:</b></td><td>" + campaignName + @"</td></tr>
<tr><td><b>Ad:</b></td><td>" + adName + @"</td></tr>";
            body += "<tr><td><b>Preview:</b></td><td><img src='" + imageUrl + "' border='0'></td></tr>";
            body += @"
</table>
<br/><br/>
Click <a href='" + link + @"'>here</a> to view and edit details.
";

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send new request admin notifier.
        /// </summary>
        public static void SendNewRequestAdminMail(int messageId, string name, string email, string phone, int subjectId, string message)
        {
            string support = ConfigurationSettings.AppSettings["SupportEmail"];

            string subject = "Inqwise | Request #" + messageId.ToString();
            string body = @"
A new request has been sent by <b>" + name + @"</b><br /><br />
<table>
<tr><td><b>Email:</b></td><td><a href='mailto:" + email + "'>" + email + @"</a></td></tr>
<tr><td><b>Phone:</b></td><td>" + phone + @"</td></tr>
<tr><td><b>Subject:</b></td><td>" + DictionaryBLL.GetCrmSubjectList().Single(i => i.Item_Id == subjectId).Item_Desc + @"</td></tr>
<tr><td><b>Message:</b></td><td>" + message + @"</td></tr>
</table>
";

            SendMail(support, subject, body);
        }

        /// <summary>
        /// Send credit card payment notification.
        /// </summary>
        public static void SendCreditCardPaymentAdminMail(string transactionId, int amount)
        {
            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | Credit Card Payment Received";
            string body =
                "A credit card payment has received." +
                "<br /><br />" +
                "<b>" + "Transaction ID : " + "</b>" + transactionId + "<br />" +
                "<b>" + "Amount         : " + "</b>" + "$" + (amount / 100).ToString();

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send credit card transaction error notification.
        /// </summary>
        public static void SendCreditCardTransErrorAdminMail(string transactionId, string requestId, string errorCode, string errorMessage)
        {
            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | Credit Card Warning";
            string body =
                "A credit card trascation error has occured." +
                "<br /><br />" +
                "<b>" + "Transaction ID : " + "</b>" + transactionId + "<br />" +
                "<b>" + "Request ID     : " + "</b>" + requestId + "<br />" +
                "<b>" + "Error Code     : " + "</b>" + errorCode + "<br />" +
                "<b>" + "Error Message  : " + "</b>" + errorMessage;

            SendMail(admin, subject, body);
        }

        /// <summary>
        /// Send PayPal payment notification.
        /// </summary>
        public static void SendPayPalPaymentAdminMail(string type, string transactionId, string status, decimal amount, decimal fee, string currency, int advertiserId, string advertiserEmail, string paymentData)
        {
            string admin = ConfigurationSettings.AppSettings["AdminEmail"];

            string subject = "Inqwise | PayPal Payment " + type;
            string body =
                "A PayPal payment " + type.ToLower() + " received." +
                "<br /><br />" +
                "<b>" + "Transaction ID : " + "</b>" + transactionId + "<br />" +
                "<b>" + "Status         : " + "</b>" + status + "<br />" +
                "<b>" + "Amount         : " + "</b>" + amount.ToString() + " " + currency + "<br />" +
                "<b>" + "Fee            : " + "</b>" + fee.ToString() + " " + currency + "<br />" +
                "<br/>" +
                "<b>" + "Advertiser ID  : " + "</b>" + advertiserId.ToString() + "<br />" +
                "<b>" + "Email          : " + "</b>" + advertiserEmail +
                "<br/><br/>" + "-----------------------------------------------------------------------" + "<br/><br/>" +
                paymentData;

            SendMail(admin, subject, body);
        }
        
        #endregion Mails
    }
}
