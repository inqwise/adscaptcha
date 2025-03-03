using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CreditGuard
{
    public static class HttpHelper
    {
        public static string SendHttpRequest(string requestURI)
        {
            ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();

            WebRequest httpWebRequest = WebRequest.Create(requestURI);

            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            WebResponse objResponse = httpWebRequest.GetResponse();
            StreamReader tr = new StreamReader(objResponse.GetResponseStream());
            return tr.ReadToEnd();
        }
    }

    public class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate cert, WebRequest request, int certificateProblem)
        {
            //Implements ICertificatePolicy.CheckValidationResult        
            // Return true to force the certificate to be accepted
            return true;
        }
    }
}
