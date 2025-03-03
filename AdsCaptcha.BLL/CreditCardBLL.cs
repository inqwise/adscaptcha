using System;
using CreditGuard;

namespace AdsCaptcha.BLL
{
    public enum CreditMethod
    {
        BankWire = 23001,
        Check = 23002,
        Manual = 23003,
        PayPal = 23004,
        Later = 23005
    }

    public static class CreditCardBLL
    {
        public class CreditCardResult
        {
            public string CardID { get; set; }
            public string CardNumber { get; set; }
            public string TransID { get; set; }
            public string AuthorizationNumber { get; set; }
            public bool Status { get; set; }
            public string ErrorMessage { get; set; }
        }

        //Normal היא בדיקת j2
        //בדיקת j2 הינה בדיקה המופעלת ברמת המסוף בלבד. כלומר, אין יציאה לחברת האשראי
        //בדיקה זו בודקת את ולידיות כרטיס האשראי והתוקף, חשוב להדגיש שהיא אינה בודקת קשר בין השניים
        //לדוגמא, אם תעבירו כרטיס 4580458045804580, כרטיס שפועל לא קיים, אך הינו ולידי (מספר הכרטיס מכיל בין 8-16 ספרות) תתקבל תשובה תקינה
        //בדיקת תוקף ב- j2 בודקת שלא פג תוקפו

        // AutoComm הינו הפרמטר היחיד המשמש לחיוב הלקוח. מבצע יציאה לחברת האשראי, בדיקת תקינות כרטיס האשראי אל מול התוקף, (בודק גם מול ת.ז ו-cvv, אבל רק במקרה ובדיקה זו מוגדרת בחברות האשראי על המסוף שלך
        // סוגר את הסכום שהוזן במסגרת האשראי של הלקוח ולבסוף מחייב

        // Verify – מתבצעת בדיקת תקינות לכרטיס, וסגירת הסכום שהוזן במסגרת האשראי של הלקוח, ללא חיוב בפועל
        
        #region Public Methods
        
        public static CreditCardResult IsValid(string cardNumber, string cardExpiration, decimal amount)
        {
            CreditCardResult result = new CreditCardResult();

            string requestID = DateTime.UtcNow.Ticks.ToString();
            string authNumber = null;
            int total = Convert.ToInt32(Math.Round(amount * 100));

            try
            {
                CreditGuard.CreditGuard creditGuard = new CreditGuard.CreditGuard();
                CreditGuard.ExecuteResponse response = creditGuard.Execute(requestID, cardNumber, cardExpiration, total, TransactionType.Debit, CreditType.RegularCredit, Currency.USD, TransactionCode.Phone, Validation.Verify, authNumber);
                result.Status = true;
                result.CardID = response.CardID;
                response.AuthorizationNumber = response.AuthorizationNumber;
            }
            catch (CreditGuard.ExecuteException ex)
            {
                result.CardID = "0";
                result.Status = false;
                result.ErrorMessage = GetErrorMessage(ex);
            }

            return result;
        }

        public static CreditCardResult Debit(string cardId, string cardExpiration, decimal amount)
        {
            CreditCardResult result = new CreditCardResult();

            string requestID = DateTime.UtcNow.Ticks.ToString();
            string authNumber = null;
            int total = Convert.ToInt32(Math.Round(amount * 100));

            try
            {
                CreditGuard.CreditGuard creditGuard = new CreditGuard.CreditGuard();
                CreditGuard.ExecuteResponse response = creditGuard.Execute(requestID, cardId, cardExpiration, TransactionType.Debit, CreditType.RegularCredit, total, Currency.USD, TransactionCode.Phone, Validation.Normal, authNumber);
                result.Status = true;
                result.CardID = response.CardID;
                result.AuthorizationNumber = response.AuthorizationNumber;
                result.TransID = response.TransID.ToString();

                // Send admin notification.
                Mail.SendCreditCardPaymentAdminMail(result.TransID, total);
            }
            catch (CreditGuard.ExecuteException ex)
            {
                result.Status = false;
                result.ErrorMessage = GetErrorMessage(ex);
            }
                        
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static string GetErrorMessage(CreditGuard.ExecuteException ex)
        {
            string errorMessage;

            // Check error code.
            switch (ex.Error.Result)
            {                
                case CreditGuard.ResultCodes.CARD_IS_BLOCKED:
                    errorMessage = "The card is blocked, confiscate it";
                    break;
                case CreditGuard.ResultCodes.REFUSAL_CREDIT_COMPANY:
                    errorMessage = "Refusal by credit company";
                    break;
                case CreditGuard.ResultCodes.INCORRECT_CAVV:
                    errorMessage = "Incorrect CAVV/ECI/UCAF";
                    break;
                case CreditGuard.ResultCodes.INCORRECT_CVV_OR_ID:
                    errorMessage = "Incorrect CVV/ID";
                    break;
                case CreditGuard.ResultCodes.NO_COMMUNICATION:
                    errorMessage = "No communication. Please try again later";
                    break;
                case CreditGuard.ResultCodes.DEFECTIVE_CARD:
                    errorMessage = "Defective card";
                    break;
                case CreditGuard.ResultCodes.EXPIRED_CARD:
                    errorMessage = "Expired card";
                    break;
                case CreditGuard.ResultCodes.INCORRECT_CONTROL_NUMBER:
                    errorMessage = "Incorrect credit card number";
                    break;
                case CreditGuard.ResultCodes.ID_NUMBER_IS_REQUIRED:
                    errorMessage = "ID number is required";
                    break;
                case CreditGuard.ResultCodes.CVV_IS_REQUIRED:
                    errorMessage = "CVV is required";
                    break;
                case CreditGuard.ResultCodes.ID_NUMBER_AND_CVV_ARE_REQUIRED:
                    errorMessage = "ID number and CVV are required";
                    break;
                case CreditGuard.ResultCodes.CARD_NOT_PERMITTED_TO_EXECUTE_IMMEDIATE_DEBIT:
                    errorMessage = "The card is not permitted to execute immediate debit transactions";
                    break;
                case CreditGuard.ResultCodes.PAYMENT_AMOUNT_TOO_LOW:
                    errorMessage = "The payment amount is too low for credit transactions";
                    break;
                case CreditGuard.ResultCodes.AMOUNT_IS_ABOVE_PERMITTED:
                    errorMessage = "Amount for this card is above permitted per day";
                    break;
                case CreditGuard.ResultCodes.INACTIVE_CARD:
                    errorMessage = "Inactive card";
                    break;
                case CreditGuard.ResultCodes.OLD_TRANSACTION:
                    errorMessage = "Sorry, our credit card gateway is under maintenance. Please try again later";
                    break;
                default:
                    //errorMessage = ex.Error.Message + " (" + ex.Error.Result + ")";
                    errorMessage = "Please check your credit card details and try again";
                    break;                
            }

            /*
            // Log error message.
            string errorInfo =
                "Credit card action error " + " | " +
                "RequestID: " + ex.Error.RequestID + " | " +
                "ErrorCode: " + ex.Error.Result + " | " +
                "ErrorMessage: " + ex.Error.Message + " | " +
                "TransID: " + ex.Error.TransID;
            NLogManager.logger.Error(errorInfo);
            */

            // Send admin notification.
            Mail.SendCreditCardTransErrorAdminMail(ex.Error.TransID.ToString(), ex.Error.RequestID, ex.Error.Result, ex.Error.Message);            

            return errorMessage;
        }

        #endregion Private Methods
    }
}
