using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace trademodelml.lib.Data
{
    /// <summary>
    /// Responsible for creating a CSV of a model so it can be read by ML.Net
    /// </summary>
    public class CsvGenerator
    {
        /// <summary>
        /// Creates a CSV with a given dataset for use in ML.
        /// </summary>
        /// <param name="prices">Source prices to create a CSV from.</param>
        /// <returns>A string with the path to the generated CSV.</returns>
        public string CreateCsv(SortedList<DateTime,Price> prices)
        {
            var path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            var sr = new StreamWriter(path);
            using (var writer = new CsvWriter(sr))
            {
                //writer.WriteHeader<Price>();
                writer.WriteRecords(prices.Values);
            }
            return path;
        }
    }
}