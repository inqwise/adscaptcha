using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Inqwise.AdsCaptcha.BLL;
using System.Configuration;

namespace Inqwise.AdsCaptcha
{
    public partial class AdsCaptcha : System.Web.UI.MasterPage
    {
        public string BaseUrl = System.Configuration.ConfigurationManager.AppSettings["Url"];
        public string APIUrl = System.Configuration.ConfigurationManager.AppSettings["API"];

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (IsCountryIPValid())
                return;
            */
        }

        void Page_Init()
        {
            this.ID = "MasterPage";
        }

        /// <summary>
        /// Check if IP country is valid and allowed.
        /// </summary>
        /// <returns>Whether IP is valid or not.</returns>
        private bool IsCountryIPValid()
        {
            string IP_COUNTRY_URL = "http://ipinfodb.com/ip_query_country.php?ip=";

            try
            {
                bool returnValue = false;
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strIPCountryCheckURL = IP_COUNTRY_URL + ip + "&output=xml";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strIPCountryCheckURL);
                req.Method = "POST";
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string strResponse = reader.ReadToEnd();
                reader.Close();
                res.Close();
                XmlDocument xmlResponse = new XmlDocument();
                xmlResponse.LoadXml(strResponse);

                // Get country name.
                string countryName = xmlResponse.SelectSingleNode("Response/CountryName").InnerText;

                if (DictionaryBLL.GetCountryBlockedList().Contains(countryName.ToLowerInvariant()))
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }

                return returnValue;
            }
            catch
            {
                return true;
            }
        }
    }
}
