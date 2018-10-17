using System;
using trademodelml.lib;
using trademodelml.lib.Data;
using trademodelml.lib.Models;

namespace trademodelml.console
{
    class Program
    {
        static void Main(string[] args)
        {
            // load up our data
            DateTime startDate = new DateTime(2010,1,1);
            DateTime endDate = new DateTime(2014,1,1);
            string symbol = "AAPL";
            var loader = new DataLoader();
            var rawDataSet = loader.LoadData(symbol, startDate, endDate);
            var generator = new ComputedPriceGenerator();
            var computedPrices = generator.Generate(rawDataSet.Prices);

            var testingSet = computedPrices.TakeTestingSet(20);
            var trainingSet = computedPrices.TakeTrainingSet(80);
            
            var writer = new CsvGenerator();
            var dataPath = writer.CreateCsv(trainingSet);
            var testPath = writer.CreateCsv(testingSet);

            // setup modeling
            var trainer = new Trainer();
            trainer.TrainAndEvaluate(dataPath, testPath);

            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }
    }
}
