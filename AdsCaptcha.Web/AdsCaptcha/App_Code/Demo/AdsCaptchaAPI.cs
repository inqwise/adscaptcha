using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace Inqwise.AdsCaptchaAPI
{
    public static class CAPTCHA
    {
        private static string ADSCAPTCHA_API = ConfigurationSettings.AppSettings["API"];

        public static string GetCaptcha(int captchaId, 
                                        string publicKey,
                                        bool isSSL)
        {
            if (isSSL)
                ADSCAPTCHA_API = ADSCAPTCHA_API.Replace("http://", "https://");

            string urlGet = ADSCAPTCHA_API + "Get.aspx";
            string urlNoScript = ADSCAPTCHA_API + "NoScript.aspx";
            string dummy = DateTime.UtcNow.Ticks.ToString();
            string data = "?CaptchaId=" + captchaId.ToString() +
                          "&PublicKey=" + publicKey +
                          "&Dummy=" + dummy;

            return "<script src='" + urlGet + data + "' type='text/javascript'></script>" + 
                   "<noscript>" +
                       "<iframe src='" + urlNoScript + data + "' width='300' height='100' frameborder='0'></iframe>" +
                       "<table>" +
                       "<tr><td>Type challenge here:</td><td><input type='text' name='adscaptcha_response_field' value='' /></td></tr>" +
                       "<tr><td>Paste code here:</td><td><input type='text' name='adscaptcha_challenge_field' value='' /></td></tr>" +
                       "</table>" +
                   "</noscript>";
        }

        public static string ValidateCaptcha(int captchaId, 
                                             string privateKey, 
                                             string challengeValue, 
                                             string responseValue, 
                                             string remoteAddress,
                                             bool isSSL)
        {
            if (isSSL)
                ADSCAPTCHA_API = ADSCAPTCHA_API.Replace("http://", "https://");

            string url = ADSCAPTCHA_API + "Validate.aspx";
            string data = "CaptchaId=" + captchaId +
                          "&PrivateKey=" + privateKey +
                          "&ChallengeCode=" + challengeValue +
                          "&UserResponse=" + responseValue +
                          "&RemoteAddress=" + remoteAddress;

            string result = HttpPost(url, data);

            return result;
        }

        private static string HttpPost(string uri, string data)
        {
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            webRequest.ContentLength = bytes.Length;
            Stream ds = webRequest.GetRequestStream();
            ds.Write(bytes, 0, bytes.Length);
            ds.Close();

            WebResponse webResponse = webRequest.GetResponse();
            if (webResponse == null)
                return null;
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            return sr.ReadToEnd().Trim().ToLower();
        }
    }
}
