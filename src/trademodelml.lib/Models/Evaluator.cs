using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using trademodelml.lib.Data;

namespace trademodelml.lib.Models
{
    public class Evaluator
    {
        public RegressionMetrics Evaluate(string testCsvPath, PredictionModel<Price, ClosePricePrediction> model)
        {
            var testData = new TextLoader(testCsvPath).CreateFrom<Price>(useHeader: true, separator: ',');
            var evaluator = new RegressionEvaluator();
            var metrics = evaluator.Evaluate(model, testData); 
            return metrics;           
        }
    }
}