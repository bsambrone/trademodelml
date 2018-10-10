using System;
using System.Collections.Generic;
using trademodelml.lib.Technicals;

namespace trademodelml.lib.Data
{
    public class ComputedPriceGenerator
    {
        public SortedList<DateTime, PriceComputed> Generate(SortedList<DateTime, Price> inputPrices)
        {
            // hard code everything for local testing, need to make configurable
            var adx = AverageDirectionalIndex.Calculate(inputPrices, 20);
            var williams = WilliamsR.Calculate(inputPrices, 14);
            var atr = AverageTrueRange.Calculate(inputPrices, 14);

            var returnVals = new SortedList<DateTime, PriceComputed>();
            int iter = 0;            
            foreach (var item in inputPrices)
            {
                var computed = new PriceComputed();
                computed.Open = item.Value.Open;
                computed.High = item.Value.High;
                computed.Low = item.Value.Low;
                computed.Close = item.Value.Close;
                computed.Volume = item.Value.Volume;
                computed.AverageDirectionalIndex = adx[item.Key];
                computed.WilliamsR = williams[item.Key];
                computed.AverageTrueRange = atr[item.Key];                
                if (inputPrices.Count - 5 == iter)
                {
                    break;
                }
                else
                {                                        
                    // FIX ME, just for testing
                    computed.PredictedFutureClosePrice = inputPrices[item.Key].Close + 0.5;
                }                
                returnVals.Add(item.Key, computed);
                iter++;
            }

            return returnVals;
        }
    }
}