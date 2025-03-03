using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Net;
using NLog;
using System.Windows.Forms;
using System.Threading;

namespace Inqwise.AdsCaptcha.API.App_Code
{
    public class YBrantProcess
    {
        public YBrantResponce RunRequest(string url)
        {
            try
            {
                NLog.Logger nlog = NLog.LogManager.GetCurrentClassLogger();

                YBrantResponce y = new YBrantResponce();

                CookieContainer cookieJar = GetCookie();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["YBrantURLNew"]);

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                request.Accept = "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, */*";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US";
                request.Headers["Accept-Encoding"] = "gzip, deflate";

                //request.Headers["Cookie"] = @"uid=uid=ed7c7c62-6efa-11e2-bae1-1cc1de084954&_hmacv=1&_salt=3784398196&_keyid=k1&_hmac=f0f23501721005d09f068991f2a490febb8cedfc; BX=4s4kj7l8h00d9&b=3&s=01&t=34; RMBX=4s4kj7l8h00d9&b=3&s=01&t=34; ih=\"b!!!!,!AIvo!!!!#>'XL7!B:jT!!!!#>'Xb?!C%<t!!!!$>'XMs!C%>]!!!!$>'XN!!C%>w!!!!$>'XM#!C%>x!!!!$>'XM.!C%?!!!!!$>'XM1!C%?#!!!!#>'XO/!C6@4!!!!$>'R+K"; vuday1=!!!!#O8gb8!4fc!iNf=5; liday1=m.4eTah/j9.:cnc#aQz%X@+$s!4fc!TdC$R";

                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                request.AllowAutoRedirect = true;
                request.AllowWriteStreamBuffering = true;

                request.CookieContainer = cookieJar;

                using (WebResponse response = request.GetResponse())
                {

                    //StreamReader reader =  new StreamReader(data);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string s = reader.ReadToEnd();


                        nlog.Debug(s);

                        y = GetYBrant(s);
                    }




                    string headers = string.Empty;

                    System.Collections.Specialized.NameValueCollection headersnames = request.Headers;
                    for (int i = 0; i < headersnames.Count; i++)
                    {
                        string key = headersnames.GetKey(i);
                        string value = headersnames.Get(i);
                        headers += key + ": " + value + "; ";
                    }

                    headers += "coockie: " + cookieJar.Count.ToString() + "\n";

                    nlog.Debug(headers);

                    headers = string.Empty;

                    headersnames = response.Headers;
                    for (int i = 0; i < headersnames.Count; i++)
                    {
                        string key = headersnames.GetKey(i);
                        string value = headersnames.Get(i);
                        headers += key + ": " + value + "; ";
                    }


                    nlog.Debug(headers);


                }



                //WebBrowser wb = new WebBrowser();
                //wb.Navigate(ConfigurationManager.AppSettings["YBrantURLNew"]);

                //while (wb.ReadyState != WebBrowserReadyState.Complete)
                //{
                //    Thread.CurrentThread.Join(10);
                //    Application.DoEvents();
                //}

                //y = GetYBrant(wb.DocumentText);

                return y;
            }
            catch(Exception exc)
            {
                NLog.Logger nlog = NLog.LogManager.GetCurrentClassLogger();
                nlog.Error(exc.ToString());
                return new YBrantResponce();
            }
        }

        private YBrantResponce GetYBrant(string data)
        {
            YBrantResponce y = new YBrantResponce();

            data = data.Replace("\\\"", "\"");

            string temp = "href=\"";
            int startind = data.IndexOf(temp);
            int endind = -1;
            if (startind != -1)
            {
                endind = data.IndexOf("\"", startind + temp.Length);
                y.ClickUrl = data.Substring(startind + temp.Length, endind - startind - temp.Length);

                temp = "src=\"";
                startind = data.IndexOf(temp);
                endind = data.IndexOf("\"", startind + temp.Length);

                y.ImgUrl = data.Substring(startind + temp.Length, endind - startind - temp.Length);

                temp = "rm_data.creative_id = ";
                startind = data.IndexOf(temp);
                endind = data.IndexOf(";", startind + temp.Length);

                y.ID = data.Substring(startind + temp.Length, endind - startind - temp.Length).Trim();
            }

            return y;
        }

        private CookieContainer GetCookie()
        {
            string url = ConfigurationManager.AppSettings["YBrantURLNew"];

            CookieContainer cookieJar = new CookieContainer();
            CookieContainer cookieJar2 = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("bh", "\"\""));
            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("ih", "\"\""));
            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("BX", ""));
            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("RMBX", ""));
            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("vuday1", ""));
            cookieJar.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie("liday1", ""));


            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            request.Accept = "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, */*";
            request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
            request.Headers["Accept-Language"] = "en-US";
            request.Headers["Accept-Encoding"] = "gzip, deflate";

            //request.Headers["Cookie"] = @"uid=uid=ed7c7c62-6efa-11e2-bae1-1cc1de084954&_hmacv=1&_salt=3784398196&_keyid=k1&_hmac=f0f23501721005d09f068991f2a490febb8cedfc; BX=4s4kj7l8h00d9&b=3&s=01&t=34; RMBX=4s4kj7l8h00d9&b=3&s=01&t=34; ih=\"b!!!!,!AIvo!!!!#>'XL7!B:jT!!!!#>'Xb?!C%<t!!!!$>'XMs!C%>]!!!!$>'XN!!C%>w!!!!$>'XM#!C%>x!!!!$>'XM.!C%?!!!!!$>'XM1!C%?#!!!!#>'XO/!C6@4!!!!$>'R+K"; vuday1=!!!!#O8gb8!4fc!iNf=5; liday1=m.4eTah/j9.:cnc#aQz%X@+$s!4fc!TdC$R";

            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            request.AllowAutoRedirect = true;
            request.AllowWriteStreamBuffering = true;

            request.Method = "HEAD";

            request.CookieContainer = cookieJar;



            using (WebResponse response = request.GetResponse())
            {

                //StreamReader reader =  new StreamReader(data);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string[] cookies = response.Headers["Set-Cookie"].Split(new char[] { ';', ',' });


                    foreach (string c in cookies)
                    {
                        if ((c.IndexOf("expire") != -1) ||
                            (c.IndexOf("path") != -1) ||
                            (c.IndexOf("=") == -1))
                            continue;

                        string[] cc = c.Split('=');
                        string[] predefined = { "ih", "bh", "BX", "RMBX", "liday1", "vuday1" };
                        if (predefined.Contains(cc[0].Trim()))
                        {
                            cookieJar2.Add(new Uri(ConfigurationManager.AppSettings["YBrantURLNew"]), new Cookie(cc[0].Trim(), cc[1].Trim()));
                        }
                    }
                }
            }

            return cookieJar2;
        }
    }
}
