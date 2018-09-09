using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using trademodelml.lib.Data;

namespace trademodelml.lib.Models
{
    public class Trainer
    {
        public PredictionModel<Price, ClosePricePrediction> Train(string csvPath)
        {
            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader(csvPath).CreateFrom<Price>(useHeader: true, separator: ','));
            pipeline.Add(new ColumnCopier(("CsvNeedsThisColumnAsTheAnswer", "Label")));
            pipeline.Add(new ColumnConcatenator("Features", "OpenPrice", "HighPrice", "LowPrice", "ClosePrice", "Volume"));
            pipeline.Add(new FastTreeRegressor());
            // TODO - add shuffler            
            var model = pipeline.Train<Price, ClosePricePrediction>();
            return model;
        }
    }
}