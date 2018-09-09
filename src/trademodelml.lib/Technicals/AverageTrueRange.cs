using System;
using System.Collections.Generic;
using trademodelml.lib.Data;

namespace trademodelml.lib.Technicals
{
    public static class AverageTrueRange
    {
        /// <summary>
        /// Current ATR = [(Prior ATR x 13) + Current TR] / 14
        /// </summary>
        /// <param name="prices">The source price dataset.</param>
        /// <param name="periods">Lookback perods. Default should be 14</param>
        /// <returns>A sorted list arranged the same as the underlying data with the ATR.</returns>
        public static SortedList<DateTime,double> Calculate(this SortedList<DateTime, Price> prices, int periods)
        {
            if (periods > prices.Values.Count - 1) throw new ArgumentException("Lookback periods must not exceed length of reference data");
            var returnValues = new SortedList<DateTime,double>();

            // initialize ATR
            var trData = TrueRange.Calculate(prices);
            returnValues[prices.Keys[0]] = prices.Values[0].High - prices.Values[0].Low;
            double value = 0;
            for (int i = 1; i <= periods; i++)
            {
                var tr = trData.Values[i];
                returnValues[prices.Keys[i]] = tr;
                value += tr;
            }
            returnValues[prices.Keys[periods]] = value / periods;
            double previousAtr = value/periods;

            for (int i = periods + 1; i < prices.Values.Count; i++)
            {
                var tr = trData[prices.Keys[i]];
                var atr = ((previousAtr*(periods - 1)) + tr) / periods;
                returnValues[prices.Keys[i]] = atr;
                previousAtr = atr;
            }
            return returnValues;
        }
    }
}