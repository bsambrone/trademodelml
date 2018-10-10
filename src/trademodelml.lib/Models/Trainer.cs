using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.FastTree;
using Microsoft.ML.Runtime.Learners;
using Microsoft.ML.Runtime.Model;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using trademodelml.lib.Data;

namespace trademodelml.lib.Models
{
    public class Trainer
    {
        public void TrainAndEvaluate(string dataPath, string testPath)
        {
            // setup the data reader            
            var reader = TextLoader.CreateReader(EnvironmentContainer.Instance, c => 
            (
                features: c.LoadFloat(0, 7),
                label: c.LoadFloat(8)
            ));

            // make the estimator and predictor
            FastTreeRegressionPredictor pred = null;            
            var ctx = new RegressionContext(EnvironmentContainer.Instance);
            var est = reader.MakeNewEstimator()
                .Append(r => (r.label, score: ctx.Trainers.FastTree(r.label, r.features,
                    numTrees: 10,
                    numLeaves: 5,
                    onFit: (p) => { pred = p; })));
            
            // make the learning pipe
            var pipe = reader.Append(est);

            // make the model
            var dataSource = new MultiFileSource(dataPath);
            var model = pipe.Fit(dataSource);
            var data = model.Read(dataSource);

            // get 
            var metrics = ctx.Evaluate(data, r => r.label, r => r.score, new PoissonLoss());

        }
    }
}