using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public enum Currency
    {
        /// <summary>
        /// New Israeli Shekel.
        /// </summary>
        ILS,

        /// <summary>
        /// United States Dollar.
        /// </summary>
        USD,

        /// <summary>
        /// Great Britain Pound.
        /// </summary>
        GBP,

        /// <summary>
        /// New Israeli Shekel USD linked.
        /// </summary>
        IlsByUsd,

        /// <summary>
        /// Hong Kong Dollar
        /// </summary>
        HKD,

        /// <summary>
        /// Japanese Yen
        /// </summary>
        JPY,

        /// <summary>
        /// European currency unit.
        /// </summary>
        EUR,

        /// <summary>
        /// New Israeli Shekel index linked.
        /// </summary>
        IlsbyIndex
    }
}
