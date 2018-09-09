using System;
using System.Collections.Generic;
using System.Linq;
using trademodelml.lib.Data;

namespace trademodelml.lib.Technicals
{
    public static class DirectionalIndexNegative
    {
        public static SortedList<DateTime,double> Calculate(SortedList<DateTime,Price> prices, int periods )
        {
            var returnValues = new SortedList<DateTime,double>();

            var atr = AverageTrueRange.Calculate(prices, periods);

            double[] downMoves = new double[prices.Count];
            downMoves[0] = 0;
            for (int i = 1; i < prices.Values.Count; i++)
            {
                var upMove = prices.Values[i].High - prices.Values[i - 1].High;
                var downMove = prices.Values[i - 1].Low - prices.Values[i].Low;
                if (downMove > upMove && downMove > 0)
                {
                    downMoves[i] = downMove;
                }
                else
                {
                    downMoves[i] = 0;
                }
            }

            var sma = SimpleMovingAverage.Calculate(downMoves, periods);                        

            returnValues[prices.Keys[0]] = 0;
            for (int i = 1; i < prices.Count; i++)
            {
                if (atr.Values[i] == 0)
                {
                    returnValues[prices.Keys[i]] = 0;
                    continue;
                }
                var calc = (sma[i]*100)/atr.Values[i];
                returnValues[prices.Keys[i]] = calc;
            }

            return returnValues;
        }
    }
}