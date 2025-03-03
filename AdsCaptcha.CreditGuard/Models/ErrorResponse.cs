using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace CreditGuard
{
    public class ErrorResponse : ResponseBase, IXmlSerializable
    {
        internal static ErrorResponse ToError(string xmlResponse)
        {
            Regex isError = new Regex("<result>(.*)</result>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            var match = isError.Match(xmlResponse);

            if (match.Groups.Count == 2 && match.Groups[1].Value == "000")
                return null;

            ErrorResponse response = new ErrorResponse();

            XmlTextReader reader = new XmlTextReader(new StringReader(xmlResponse));
            response.ReadXml(reader);
           
            return response;
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "requestId":
                            reader.Read();
                            RequestID = reader.Value;
                            break;
                        case "version":
                            reader.Read();
                            Version = reader.Value;
                            break;
                        case "tranId":
                            reader.Read();
                            TransID = String.IsNullOrEmpty(reader.Value) ? -1 : Convert.ToInt32(reader.Value);
                            break;
                        case "result":
                            reader.Read();
                            Result = reader.Value;
                            break;
                        case "message":
                            reader.Read();
                            Message = reader.Value;
                            break;
                        case "additionalInfo":
                            reader.Read();
                            AdditionalInfo = reader.Value;
                            break;
                        case "language":
                            reader.Read();
                            Language = reader.Value;
                            break;
                    }
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
