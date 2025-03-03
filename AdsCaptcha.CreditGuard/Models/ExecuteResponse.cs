using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace CreditGuard
{
    public class ExecuteResponse : ResponseBase, IXmlSerializable
    {
        #region Properties

        public string CardID { get; set; }
        public string AuthorizationNumber { get; set; }

        #endregion

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
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
                            TransID = Convert.ToInt32(reader.Value);
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
                        case "cardId":
                            reader.Read();
                            CardID = reader.Value;
                            break;
                        case "authNumber":
                            reader.Read();
                            AuthorizationNumber = reader.Value;
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
