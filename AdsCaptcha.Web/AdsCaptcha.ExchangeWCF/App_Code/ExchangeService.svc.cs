using System;
using System.Net;
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using Inqwise.AdsCaptcha.ExchangeWCF.Config;

namespace Inqwise.AdsCaptcha.ExchangeWCF
{
    // NOTE: If you change the class name "ExchangeService" here, you must also update the reference to "Service1" in Web.config and in the associated .svc file.
    public class ExchangeService : IExchangeService
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private static readonly CacheManager<CookieContainer> CountriesCookiesCache = new CacheManager<CookieContainer>();
        private static readonly TimeSpan DefaultSlidingCacheExpiration = TimeSpan.FromMinutes(20);
        public ExchangeData GetAd(string visitorIP, string[] previousAds, string country, int exchange)
        {
            ExchangeData data = new ExchangeData();

            try
            {
                var exchangeConfig = ExchangeConfig.GetExchange(country);

                if (exchangeConfig == null)
                {
                    return new ExchangeData();
                }
                else
                {
                    string requestUriString = "http://tags1.z5x.net:5280/?ad_type=ad&ad_size=300x250&site=1577054&section_code=Inqwise052713&pub_url=www.Inqwise.com";
                    var cookieJar = CountriesCookiesCache.GetOrAddCachedItem(country, () => new Tuple<CookieContainer, TimeSpan>(GetCookiesContainer(requestUriString), DefaultSlidingCacheExpiration));
                    
                    string url = exchangeConfig.ExchangePrimaryUrl;

                    string res = GetResponse(url, cookieJar, visitorIP, requestUriString);


                    string _saltText = "_salt=";
                    int ind = res.IndexOf(_saltText);

                    if (ind != -1)
                    {
                        int indEnd = res.IndexOf("\";", ind);
                        string salt = res.Substring(ind + _saltText.Length, indEnd - ind - _saltText.Length);

                        if (previousAds.Length > 0)
                        {
                            for (int i = 0; i < previousAds.Length; i++)
                            {
                                if (previousAds[i].Trim() != string.Empty)
                                    url += previousAds[i].Trim() + ",";
                            }
                            url = url.Substring(0, url.Length - 1);
                        }
                        else
                        {
                            url = url.Replace("&amp;X=", "");
                        }

                        url = exchangeConfig.ExchangeSecondaryUrl.Replace("XXX", salt);

                        res = GetResponse(url, new CookieContainer(), visitorIP, requestUriString);

                        data = GetYBrant(res);
                        Log.Info("GetAd: visitorIP: '{0}', previousAds: '{1}', country: '{2}', exchange: '{3}', url: '{4}', imageId: '{5}'", visitorIP, String.Join(",", previousAds), country, exchange, data.ImageUrl, data.ImageID);
                    }


                    //string salt = GetSalt(cookieJar);
                    //url = url.Replace("XXXXX", salt);

                   

                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


                    //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                    //request.Accept = "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, */*";
                    //request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                    //request.Headers["Accept-Language"] = "en-US";
                    //request.Headers["Accept-Encoding"] = "gzip, deflate";
                    //request.Headers.Add("X-Forwarded-For", visitorIP);


                    //request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                    //request.AllowAutoRedirect = true;
                    //request.AllowWriteStreamBuffering = true;

                    //request.CookieContainer = cookieJar;

                    //using (WebResponse response = request.GetResponse())
                    //{

                    //    //StreamReader reader =  new StreamReader(data);
                    //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    //    {
                    //        string s = reader.ReadToEnd();
                            
                    //        //data.Error = s;
                    //    }
                    //}
                }

            }
            catch (Exception exc)
            {
                data.Error = exc.ToString();
            }


            return data;
        }

        private CookieContainer GetCookiesContainer(string requestUriString)
        {
            var container = new CookieContainer();
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            request.CookieContainer = container;
            using (WebResponse response = request.GetResponse())
            {
                //StreamReader reader =  new StreamReader(data);
                //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                //{
                //    string s = reader.ReadToEnd();
                //    Log.Info("GetCookiesContainer: response '{0}'", s);
                //}
            }

            http://ad.z5x.net/st

            request = (HttpWebRequest)WebRequest.Create("http://ad.z5x.net/st" + request.RequestUri.Query);
            request.CookieContainer = container;
            using (WebResponse response = request.GetResponse()) ;

            Log.Info("GetCookiesContainer: cookie header: {0}", null == container ? "" : container.GetCookieHeader(request.RequestUri));

            return container;
        }

        private ExchangeData GetYBrant(string data)
        {
            ExchangeData y = new ExchangeData();

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

                y.ImageUrl = data.Substring(startind + temp.Length, endind - startind - temp.Length);

                temp = "rm_data.creative_id = ";
                startind = data.IndexOf(temp);
                endind = data.IndexOf(";", startind + temp.Length);

                y.ImageID = data.Substring(startind + temp.Length, endind - startind - temp.Length).Trim();
            }

            return y;
        }

        private string GetSalt(CookieContainer cookieJar)
        {
            string salt = string.Empty;

            try
            {

                string url = ConfigurationManager.AppSettings["YBrantURLScript"];

              
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                request.Accept = "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, */*";
                request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
                request.Headers["Accept-Language"] = "en-US";
                request.Headers["Accept-Encoding"] = "gzip, deflate";

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
                        //data = GetYBrant(s);
                        //data.Error = s;
                        string temp = "_salt=";
                        int startind = s.IndexOf(temp);
                        int endind = s.IndexOf("\"", startind + temp.Length);
                        salt = s.Substring(startind + temp.Length, endind - startind - temp.Length);

                    }
                }

            }
            catch (Exception exc)
            {
                salt = string.Empty;
            }

            return salt;
        }

        private string GetResponse(string url, CookieContainer cookieJar, string IP, string refererUrl)
        {
            string result = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            request.Accept = "image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-ms-xbap, */*";
            request.Headers["Accept-Charset"] = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
            //request.Headers["Accept-Language"] = "en-US";
            request.Headers["Accept-Encoding"] = "gzip, deflate";
            request.Headers["X-Forwarded-For"] = IP;

            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.CookieContainer = cookieJar;
            request.KeepAlive = true;
            request.Referer = refererUrl;


            using (WebResponse response = request.GetResponse())
            {
                //Log.Info("GetResponse: cookie header: {0}", cookieJar.GetCookieHeader(response.ResponseUri));
                //StreamReader reader =  new StreamReader(data);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }

                
            }

            return result;
        }
    }
}
