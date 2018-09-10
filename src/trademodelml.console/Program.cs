using System;
using trademodelml.lib.Data;
using trademodelml.lib.Models;

namespace trademodelml.console
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2010,1,1);
            DateTime endDate = new DateTime(2014,1,1);
            string symbol = "AAPL";
            var loader = new DataLoader();
            var dataSet = loader.LoadData(symbol, startDate, endDate);

            var testingSet = dataSet.Prices.TakeTestingSet(20);
            var trainingSet = dataSet.Prices.TakeTrainingSet(80);
            
            var writer = new CsvGenerator();
            var dataPath = writer.CreateCsv(trainingSet);
            var testPath = writer.CreateCsv(testingSet);

            var trainer = new Trainer();
            var model = trainer.Train(dataPath);

            var evaluator = new Evaluator();
            var metrics = evaluator.Evaluate(testPath, model);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
