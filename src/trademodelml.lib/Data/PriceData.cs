using System;
using System.Collections.Generic;

namespace trademodelml.lib.Data
{
    public class PriceData
    {
        /// <summary>
        /// Get or sets the instrument that the prices belong to.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        /// Gets or sets the internal SortedList of prices
        /// </summary>
        public SortedList<DateTime, Price> Prices { get; set; }
    }
}