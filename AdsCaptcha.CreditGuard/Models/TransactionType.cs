using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public enum TransactionType
    {
        /// <summary>
        /// Card holder is charged.
        /// </summary>
        Debit,
        
        /// <summary>
        /// Card holder is credited.
        /// </summary>
        Credit
    }
}
