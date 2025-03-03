using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;

namespace CreditGuard
{
    public class CreditGuard
    {
        CreditGuardConfiguration _conf;

        public CreditGuard()
        {
            _conf = CreditGuardConfiguration.Get();
        }

        public ExecuteResponse Execute(string requestID, string cardNumber, string cardExpiration, int total, TransactionType transactionType, CreditType creditType, Currency currency,
            TransactionCode transCode, Validation validation, string authorizationNumber)
        {
            var terminalNum = _conf.TerminalNumber.Value;
            var debug = (_conf.Debug.Value == "true");

            var request = new ExecuteRequest(ActionType.CardNumber, requestID, terminalNum, _conf.Version.Value, debug, cardNumber, cardExpiration, total, transactionType,
                creditType, currency, transCode, validation, authorizationNumber);

            return Execute(request);
        }

        public ExecuteResponse Execute(string requestID, string cardID, string cardExpiration, TransactionType transactionType, CreditType creditType, int total, Currency currency,
            TransactionCode transCode, Validation validation, string authorizationNumber)
        {
            var terminalNum = _conf.TerminalNumber.Value;
            var debug = (_conf.Debug.Value == "true");

            var request = new ExecuteRequest(ActionType.CardID, requestID, terminalNum, _conf.Version.Value, debug, cardID, cardExpiration, total, transactionType, creditType,
                currency, transCode, validation, authorizationNumber);

            return Execute(request);
        }

        protected ExecuteResponse Execute(ExecuteRequest request)
        {
            XmlTextWriter writer = null;
            XmlTextReader reader = null;

            try
            {
                StringWriter sw = new StringWriter();
                writer = new XmlTextWriter(sw);
                request.WriteXml(writer);

                string paramsGet = "user=" + _conf.TerminalUser.Value + "&password=" + _conf.TerminalPass.Value + "&int_in=" + HttpUtility.UrlEncode(sw.ToString());
                var xmlResponse = HttpHelper.SendHttpRequest(_conf.TerminalURI.Value + "?" + paramsGet);

                var error = ErrorResponse.ToError(xmlResponse);

                if (error == null)
                {
                    reader = new XmlTextReader(new StringReader(xmlResponse));
                    var ExecuteResponse = new ExecuteResponse();
                    ExecuteResponse.ReadXml(reader);
                    return ExecuteResponse;
                }
                else
                {
                    throw new ExecuteException(error);
                }
            }
            catch (ExecuteException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
