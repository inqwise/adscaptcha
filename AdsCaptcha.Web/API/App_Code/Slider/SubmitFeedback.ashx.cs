using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Net.Mail;
using System.Configuration;

namespace Inqwise.AdsCaptcha.API.Slider
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SubmitFeedback : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string sResult = string.Empty;
            try
            {
                string name = context.Request["name"];
                string email = context.Request["email"];
                string message = context.Request["message"];
                string referrer = context.Request["referrer"];

                if (string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(message) ||
                    string.IsNullOrEmpty(referrer))
                {
                    sResult = "One or more fields were empty";
                }
                else
                {

                    string[] receipients = ConfigurationManager.AppSettings["FeedbackRecepients"].Split(';');

                    MailMessage mm = new MailMessage();
                    mm.Subject = "Inqwise Captcha feedback";
                    mm.IsBodyHtml = true;
                    string body = "<table><tr><td>Name: </td><td>" + name + "</td></tr>";

                    body += "<tr><td>Email: </td><td>" + email + "</td></tr>";
                    body += "<tr><td>Message: </td><td>" + message + "</td></tr>";
                    body += "<tr><td>From Page: </td><td>" + referrer + "</td></tr>";
                    body += "</table>";

                    mm.Body = body;


                    foreach (string to in receipients)
                    {
                        mm.To.Add(to);
                    }

                    SmtpClient smtp = new SmtpClient();
                    smtp.EnableSsl = true;
                    smtp.Send(mm);


                    sResult = "0";
                }
            }
            catch (Exception exc)
            {
               // sResult = exc.ToString();
                sResult = "Unexpected error, try again later";
            }
            context.Response.Write(sResult);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
