using System.Collections.Generic;
using System.Linq;

namespace trademodelml.lib.Technicals
{
    public static class SimpleMovingAverage
    {
        public static double[] CalculateSimpleMovingAverage(this double[] data, int periods)
        {
            double[] returnValues = new double[data.Length];

            List<double> intermediates = new List<double>();
            Queue<double> trailing = new Queue<double>();
            for (int i = 0; i < periods; i++)
            {
                intermediates.Add(data[i]);
                trailing.Enqueue(data[i]);
                returnValues[i] = intermediates.Sum()/intermediates.Count;
            }
            
            for (int i = periods; i < data.Length; i++)
            {
                trailing.Enqueue(data[i]);
                trailing.Dequeue();
                returnValues[i] = trailing.Average();
            }

            return returnValues;
        }
    }
}