using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public enum TransactionCode
    {
        /// <summary>
        /// Swiping of magnetic card.
        /// </summary>
        Regular,

        /// <summary>
        /// Self service.
        /// </summary>
        SelfService,

        /// <summary>
        /// Fuel self service.
        /// </summary>
        FuelSelfService,

        /// <summary>
        /// Transaction through Internet/phone with card number.
        /// </summary>
        Phone,

        /// <summary>
        /// Card holder is present, however card is not swiped.
        /// </summary>
        Signature
    }
}
