using System;
using System.Collections.Generic;
using trademodelml.lib.Data;

namespace trademodelml.lib.Technicals
{
    public static class TrueRange
    {
        public static SortedList<DateTime, double> Calculate(this SortedList<DateTime, Price> prices)
        {
            var returnValues = new SortedList<DateTime, double>();
            returnValues[prices.Keys[0]] = prices.Values[0].High - prices.Values[0].Low;
            for (int i = 1; i < prices.Values.Count; i++)
            {
                double method1 = prices.Values[i].High - prices.Values[i].Low;
                double method2 = Math.Abs(prices.Values[i].High - prices.Values[i - 1].Close);
                double method3 = Math.Abs(prices.Values[i].Low - prices.Values[i - 1].Close);
                double interimMax = Math.Max(method1, method2);
                var tr = Math.Max(interimMax, method3);
                returnValues[prices.Keys[i]] = tr;
            }
            return returnValues;
        }        
    }
}