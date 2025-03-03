using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public enum CreditType
    {
        /// <summary>
        /// Single payment debit.
        /// </summary>
        RegularCredit,

        /// <summary>
        /// "Isracredit", "AMEX credit", "Visa Adif/30+", "Diners Adif/30+" (local Israeli payment method).
        /// </summary>
        IsraCredit,

        /// <summary>
        /// Ad hock debit- "Hiyuv Miyadi" (local Israeli payment method).
        /// </summary>
        AdHock,

        /// <summary>
        /// Club deal (local Israeli payment method).
        /// </summary>
        ClubDeal,

        /// <summary>
        /// Special alpha – "super credit" (local Israeli payment method). Tag numberOfPayments is mandatory
        /// </summary>
        SpecialAlpha,

        /// <summary>
        /// Special credit - "credit"/"fixed payments credit" (local Israeli payment method). Tag numberOfPayments is mandatory
        /// </summary>
        SpecialCredit,

        /// <summary>
        ///  Multiple payments debit (installments). Tags numberOfPayments, periodicalPayment and firstPayment are mandatory according to the notes below
        /// </summary>
        Payments,

        /// <summary>
        ///  Payment club (local Israeli payment method).
        /// </summary>
        PaymentClub
    }
}
