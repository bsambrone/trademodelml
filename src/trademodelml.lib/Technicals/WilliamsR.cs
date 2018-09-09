using System;
using System.Collections.Generic;
using System.Linq;
using trademodelml.lib.Data;

namespace trademodelml.lib.Technicals
{
    public static class WilliamsR
    {
        /// <summary>
        /// %R = (Highest High - Close)/(Highest High - Lowest Low) * -100
        /// </summary>
        /// <param name="prices">The source price dataset.</param>
        /// <param name="periods">Lookback periods. Default is 14</param>
        /// <returns>A sorted list arranged the same as the underlying data with the ATR.</returns>
        public static SortedList<DateTime,double> CalculateWilliamsR(this SortedList<DateTime, Price> prices, int periods)
        {
            if (periods > prices.Values.Count - 1) throw new ArgumentException("Lookback periods must not exceed length of reference data");
            var returnValues = new SortedList<DateTime, double>();

            Queue<double> highes = new Queue<double>();
            Queue<double> lows = new Queue<double>();
            for (int i = 0; i < prices.Values.Count; i++)
            {
                highes.Enqueue(prices.Values[i].High);
                lows.Enqueue(prices.Values[i].Low);
                if (highes.Count > periods) highes.Dequeue();
                if (lows.Count > periods) lows.Dequeue();

                var highestHigh = highes.Max();
                var lowestLow = lows.Min();

                var value = (highestHigh - prices.Values[i].Close)/(highestHigh - lowestLow)*-100;
                returnValues[prices.Keys[i]] = value;
            }
            return returnValues;
        }
    }
}