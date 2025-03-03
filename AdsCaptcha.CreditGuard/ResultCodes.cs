using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public static class ResultCodes
    {
        public const string SUCCESS                                       = "000";
        public const string CARD_IS_BLOCKED                               = "001";
        public const string CARD_IS_STOLEN                                = "002";
        public const string CONTACT_CREDIT_COMPANY                        = "003";
        public const string REFUSAL_CREDIT_COMPANY                        = "004";
        public const string CARD_IS_FORGED                                = "005";
        public const string INCORRECT_CVV_OR_ID                           = "006";
        public const string INCORRECT_CAVV                                = "007";
        public const string NO_COMMUNICATION                              = "009";
        public const string OLD_TRANSACTION                               = "032";
        public const string DEFECTIVE_CARD                                = "033";
        public const string EXPIRED_CARD                                  = "036";
        public const string INCORRECT_CONTROL_NUMBER                      = "039";
        public const string ID_NUMBER_IS_REQUIRED                         = "057";
        public const string CVV_IS_REQUIRED                               = "058";
        public const string ID_NUMBER_AND_CVV_ARE_REQUIRED                = "059";
        public const string CARD_NOT_PERMITTED_TO_EXECUTE_IMMEDIATE_DEBIT = "154";
        public const string PAYMENT_AMOUNT_TOO_LOW                        = "155";
        public const string AMOUNT_IS_ABOVE_PERMITTED                     = "338";
        public const string INACTIVE_CARD                                 = "450";
    }
}
