using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Inqwise.AdsCaptcha.Common.Mails;
using NLog;
using Inqwise.AdsCaptcha.Common;

namespace Inqwise.AdsCaptcha.SystemFramework
{
    public class Emailer : IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Emailer> Instance = new Lazy<Emailer>(() => new Emailer());
        private ConcurrentQueue<IMailArgs> _queue = new ConcurrentQueue<IMailArgs>();
        public string Server { get; private set; }
        public int Port { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public Boolean Ssl { get; private set; }
        public string AuditEmail { get; private set; }
        private readonly SmtpClient _client;

        private Emailer()
        {
            Server = ConfigurationManager.AppSettings["SmtpServer"];
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
            Ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpSSL"].ToLower());
            User = ConfigurationManager.AppSettings["SmtpUser"];
            Password = ConfigurationManager.AppSettings["SmtpPass"];
            AuditEmail = ConfigurationManager.AppSettings["AuditEmail"];

            _queue = new ConcurrentQueue<IMailArgs>();
            Task.Run(new Action(ConsumeReadThread));
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;

            _client = new SmtpClient(Server, Port)
                {
                    EnableSsl = Ssl,
                    Timeout = 10000,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(User, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
        }

        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            if (Instance.IsValueCreated)
            {
                lock (Instance)
                {
                    if (Instance.IsValueCreated)
                    {
                        Instance.Value.Dispose();
                    }
                }
            }
        }

        private void ConsumeReadThread()
        {
            while (null != _queue)
            {
                try
                {
                    while (null != _queue && _queue.IsEmpty)
                    {
                        lock (Instance)
                        {
                            if (_queue.IsEmpty)
                            {
                                Monitor.Wait(Instance);
                            } 
                        }
                    }

                    if (null != _queue)
                    {
                        IMailArgs message = null;
                        while (_queue.TryDequeue(out message))
                        {
                            InternalSend(message);
                        } 
                    }
                }
                catch(Exception ex)
                {
                    Log.ErrorException("ConsumeReadThread: Unexpected error occured", ex);
                }
            }

            _client.Dispose();
        }

        private void InternalSend(IMailArgs args)
        {
            try
            {
                var message = new MailMessage
                    {
                        From = new MailAddress(args.From ?? User),
                        IsBodyHtml = true,
                        Body = args.Body,
                        Subject = args.Subject,
                    };

                foreach (string recipient in args.Recipients.Split(';', ',',' ')) message.To.Add(recipient);

                if (null != AuditEmail)
                {
                    foreach (string recipient in AuditEmail.Split(';', ',', ' ')) message.Bcc.Add(recipient);
                }

                _client.Send(message);
            }
            catch(Exception ex)
            {
                Log.ErrorException("InternalSend: Unexpected error occured", ex);
            }
        }

        public static void Send(IMailArgs args)
        {
            if (null != Instance.Value._queue)
            {
                lock (Instance)
                {
                    if (null != Instance.Value._queue)
                    {
                        Instance.Value._queue.Enqueue(args);
                        Monitor.Pulse(Instance); 
                    }
                }  
            }            
        }

        public void Dispose()
        {
            if (null != _queue)
            {
                lock (Instance)
                {
                    if (null != _queue)
                    {
                        _queue = null;
                        Monitor.Pulse(Instance);
                    }
                } 
            }
        }
    }
}