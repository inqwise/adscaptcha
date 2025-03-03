using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Inqwise.AdsCaptcha.ExchangeWCF
{
    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
    [ServiceContract]
    public interface IExchangeService
    {

        [OperationContract]
        ExchangeData GetAd(string visitorIP, string[] previousAds, string country, int exchange);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ExchangeData
    {
        string imgID = string.Empty;
        string imgUrl = string.Empty;
        string clickUrl = string.Empty;
        string error = string.Empty;

        [DataMember]
        public string ImageID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        [DataMember]
        public string ImageUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        [DataMember]
        public string ClickUrl
        {
            get { return clickUrl; }
            set { clickUrl = value; }
        }

        [DataMember]
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
    }
}
