using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CreditGuard
{
    public class ExecuteRequest : RequestBase, IXmlSerializable
    {
        #region Constructors

        public ExecuteRequest(ActionType actionType, string requestID, string terminalNumber, string version, bool debug, string card, string cardExpiration, int total, TransactionType transType, CreditType creditType,
            Currency currency, TransactionCode transCode, Validation validation, string authorizationNumber)
            : this(requestID, terminalNumber, version, debug, cardExpiration, total, transType, creditType, currency, transCode, validation, authorizationNumber)
        {
            if (actionType == ActionType.CardID)
            {
                this.CardID = card;
            }
            else
            {
                this.CardNumber = card;
            }
        }

        protected ExecuteRequest(string requestID, string terminalNumber, string version, bool debug, string cardExpiration, int total, TransactionType transType, CreditType creditType,
            Currency currency, TransactionCode transCode, Validation validation, string authorizationNumber)
            : base(requestID, version, debug)
        {
            this.RequestID = requestID;
            this.TerminalNumber = terminalNumber;
            this.Version = version;
            this.Debug = Debug;
            this.CardExpiration = cardExpiration;
            this.Total = total;
            this.TransType = transType;
            this.CreditType = creditType;
            this.Currency = currency;
            this.TransCode = transCode;
            this.Validation = validation;
            this.AuthorizationNumber = authorizationNumber;
        }

        #endregion

        #region Properties

        public string TerminalNumber { get; set; }
        public int Total { get; set; }
        public TransactionType TransType { get; set; }
        public CreditType CreditType { get; set; }
        public Currency Currency { get; set; }
        public TransactionCode TransCode { get; set; }
        public Validation Validation { get; set; }
        public String CardNumber { get; set; }
        public String HoderID { get; set; }
        public string AuthorizationNumber { get; set; }
        
        /// <summary>
        /// Example: 0812
        /// </summary>
        public string CardExpiration { get; set; }
        public string CardID { get; set; }

        #endregion

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("ashrait");
            writer.WriteStartElement("request");

            writer.WriteElementString("command", Command);
            writer.WriteElementString("requestId", RequestID);
            writer.WriteElementString("version", Version);
            
            writer.WriteStartElement("doDeal");

            writer.WriteElementString("terminalNumber", TerminalNumber.ToString());
            
            if (!String.IsNullOrEmpty(CardID))
            {
                writer.WriteElementString("cardId", CardID.ToString());
            }
            else
            {
                writer.WriteElementString("cardNo", CardNumber);
            }
            
            writer.WriteElementString("cardExpiration", CardExpiration);
            writer.WriteElementString("transactionType", TransType.ToString());
            writer.WriteElementString("creditType", CreditType.ToString());
            writer.WriteElementString("currency", Currency.ToString());
            writer.WriteElementString("transactionCode", TransCode.ToString());
            writer.WriteElementString("total", Total.ToString());

            //if (Debug)
            //{
            //    writer.WriteElementString("authNumber", "6789393");
            //}
            writer.WriteElementString("authNumber", "6789393");


            string validation;
            switch (Validation)
            {
                case Validation.CreditLimit:
                    validation = "CreditLimit";
                    break;
                case Validation.NoComm:
                    validation = "NoComm";
                    break;
                case Validation.Normal:
                    validation = "Normal";
                    break;
                case Validation.Verify:
                    validation = "Verify";
                    break;
                case Validation.AutoComm:
                default:
                    validation = "AutoComm";
                    break;
            }
            writer.WriteElementString("validation", validation);

            writer.WriteEndElement(); //doDeal
            writer.WriteEndElement(); //request
            writer.WriteEndElement(); //ashrait
        }

        #endregion        
    }
}
