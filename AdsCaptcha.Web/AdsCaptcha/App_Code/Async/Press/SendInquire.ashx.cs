using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.Async.Press
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SendInquire : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string firstName = context.Request["firstname"];
            string lastName = context.Request["lastname"];
            string email = context.Request["email"];
            string company = context.Request["company"];
            string message = context.Request["message"];

            string result = "success";
            try
            {
                AdsCaptcha_DevEntities1 ent = new AdsCaptcha_DevEntities1();

                T_PRESS_INQUIRES inquire = new T_PRESS_INQUIRES();

                inquire.FirstName = firstName;
                inquire.LastName = lastName;
                inquire.Email = email;
                inquire.Company = company;
                inquire.Message = message;
                inquire.InsertDate = DateTime.Now;

                ent.AddToT_PRESS_INQUIRES(inquire);
                ent.SaveChanges();
            }
            catch
            {
                result = "Some error occurs, please try again later";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
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
