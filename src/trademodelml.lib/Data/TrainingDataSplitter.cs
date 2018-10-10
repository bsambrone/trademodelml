using System;
using System.Collections.Generic;

namespace trademodelml.lib.Data
{
    public static class TrainingDataSplitter
    {
        public static SortedList<DateTime, PriceComputed> TakeTestingSet(this SortedList<DateTime, PriceComputed> source, int trainingPercent)
        {
            int dataSize = source.Count;
            int trainingAmount = (int)Math.Round(dataSize * 0.2, 0);
            var outputList = new SortedList<DateTime, PriceComputed>();
            for (int i = 0; i < trainingAmount; i++)
            {
                outputList[source.Keys[i]] = source.Values[i];
            }
            return outputList;
        }

        public static SortedList<DateTime, PriceComputed> TakeTrainingSet(this SortedList<DateTime, PriceComputed> source, int dataPercent)
        {
            int dataSize = source.Count;
            int trainingAmount = (int)Math.Round(dataSize * 0.2, 0);
            var outputList = new SortedList<DateTime, PriceComputed>();
            for (int i = trainingAmount; i < dataSize; i++)
            {
                outputList[source.Keys[i]] = source.Values[i];
            }
            return outputList;
        }        
    }
}