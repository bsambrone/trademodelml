using Microsoft.ML.Runtime.Api;

namespace trademodelml.lib.Models
{
    public class ClosePricePrediction
    {
        [ColumnName("Score")]
        public float ClosePrice;
    }
}