using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.Configuration;

namespace Inqwise.AdsCaptcha.Dal.ElasticSearch
{
    public class ElasticSearchFactory
    {
        private static readonly Lazy<string> _elasticSearchUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["ElasticSearch.Url"]);

        public static ElasticClient ElasticClient
        {
            get
            {
                try
                {
                    return GetElasticClient("images");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public static ElasticClient GetElasticClient(string index, Func<Type, string> typeNameInferrer = null)
        {
            try
            {
                var uriString = _elasticSearchUrl.Value;
                var searchBoxUri = new Uri(uriString);
                var settings = new ConnectionSettings(searchBoxUri);
                settings.SetDefaultIndex(index);
                if(null != typeNameInferrer){
                    settings.SetDefaultTypeNameInferrer(typeNameInferrer);
                }
                return new ElasticClient(settings);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
