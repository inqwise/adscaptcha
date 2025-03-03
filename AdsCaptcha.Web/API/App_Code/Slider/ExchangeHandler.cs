using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using API.Handlers;
using Jayrock.Json;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.SystemFramework;
using Nest;

namespace Inqwise.AdsCaptcha.API.Slider
{
    public class ExchangeHandler : BaseHandler<JsonObject>, System.Web.SessionState.IRequiresSessionState
    {
        private const string DEFAULT_ELASTIC_SEARCH_ENDPOINT = "http://192.168.1.53:9200";
        private static Lazy<string> ElasticSearchEndpointUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["ElasticSearch.Endpoint"] ?? DEFAULT_ELASTIC_SEARCH_ENDPOINT);
        private class Image
        {
            public string Id { get; set; }
            public string Url { get; set; }
        }

        protected override JsonObject Process(HttpContext context)
        {
            var output = new JsonObject();
            HttpRequest request = context.Request;
            string imageUrlBase64 = request["adId"];
            if (null != imageUrlBase64)
            {
                string url = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(imageUrlBase64));
                var client = new ElasticClient(new ConnectionSettings(new Uri(ElasticSearchEndpointUrl.Value)));
                var ad = new  Image {Url = url, Id = url.GetHashMd5()};
                var result = client.IndexAsync(ad, "images", "exchange", ad.Id, new IndexParameters {OpType = OpType.Create}).ContinueWith(
                    (t) =>
                        {
                            if (t.Result.OK)
                            {
                                //CacheBuilder.GetPublisher().AddAsync(ad.Id, "exchangeImage", null);
                                //Inform by Redis
                            }            
                        });
                
            }

            return GetJsonOk();
        }
    }
}