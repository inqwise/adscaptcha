<%@ WebHandler Language="C#" Class="SubmitFeedback" %>

using System;
using System.Web;
using System.Configuration;
using System.Net.Mail;

public class SubmitFeedback : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string sResult = string.Empty;
        try
        {
            string name = context.Request["name"];
            string email = context.Request["email"];
            string message = context.Request["message"];
            string referrer = context.Request["referrer"];

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
            smtp.Send(mm);
            
                        
            sResult ="0";
        }
        catch (Exception exc)
        {
            sResult = exc.ToString();
        }
        context.Response.Write(sResult);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}