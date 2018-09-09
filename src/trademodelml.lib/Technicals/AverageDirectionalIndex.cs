using System;
using System.Collections.Generic;
using System.Linq;
using trademodelml.lib.Data;

namespace trademodelml.lib.Technicals
{
    public static class AverageDirectionalIndex
    {
        /// <summary>
        /// First ADX14 = 14 period Average of DX Second ADX14 = ((First ADX14 x 13) + Current DX Value)/14 Subsequent ADX14 = ((Prior ADX14 x 13) + Current DX Value)/14
        /// </summary>
        /// <param name="prices">The source price dataset.</param>
        /// <param name="periods">Lookback periods. Default is 20</param>
        /// <returns></returns>
        public static SortedList<DateTime,double> Calculate(this SortedList<DateTime, Price> prices, int periods)
        {
            if (periods > prices.Count - 1) throw new ArgumentException("Lookback periods must not exceed length of reference data");
            var returnValues = new SortedList<DateTime,double>();

            var diPlus = DirectionalIndexPositive.Calculate(prices, periods);
            var diNeg = DirectionalIndexNegative.Calculate(prices, periods);

            double[] absDiff = new double[prices.Count];
            for (int i = 0; i < prices.Count; i++)
            {
                var abs = Math.Abs(diPlus.Values[i] - diNeg.Values[i]);
                absDiff[i] = abs;
            }

            var sma = SimpleMovingAverage.Calculate(absDiff, periods);

            for (int i = 0; i < prices.Count; i++)
            {
                if (diPlus.Values[i] + diNeg.Values[i] == 0)
                {
                    returnValues[prices.Keys[i]] = 0;
                    continue;
                }
                var adx = 100 * (sma[i]/(diPlus.Values[i] + diNeg.Values[i]));
                returnValues[prices.Keys[i]] = adx;
            }

            return returnValues;
        }
    }
}