using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public enum Validation
    {
        /// <summary>
        /// Verifies card locally.
        /// If the card is ok and the total amount of the deal is under the ceiling, a debit is made without communication to Shva.
        /// If it's above the ceiling, an error occurs.
        /// </summary>
        NoComm,

        /// <summary>
        /// A local check on the CG Gateway for the validity of the credit card number and if it exist in the blocked cards list. No actual debit occurs.
        /// </summary>
        Normal,

        /// <summary>
        /// Same as J2 (Normal). It also returns ceiling limit in the total field. for Israeli cards Only
        /// </summary>
        CreditLimit,

        /// <summary>
        /// Verifies card locally or in credit company; depends on ceiling ZFL terminal parameters
        /// A positive response results in actual settlement.
        /// </summary>
        AutoComm,

        /// <summary>
        /// Verifies card by credit company regardless of the ceiling ZFL terminal parameters. No settlement is performed; the amount of verify without settlement is held in card holder's obligor (usually 48 hours). (This is used for authorization purposes only.)
        /// Available only when the credit card company allows it on the terminal
        /// </summary>
        Verify
    }
}
