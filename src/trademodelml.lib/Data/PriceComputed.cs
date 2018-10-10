using Microsoft.ML.Runtime.Api;

namespace trademodelml.lib.Data
{
    /// <summary>
    /// Represents an extended version of Price that has technicals and desired predictions.
    /// </summary>
    public class PriceComputed
    {
        [Column("0")]
        public double Open { get; set; }

        [Column("1")]
        public double High { get; set; }

        [Column("2")]
        public double Low { get; set; }

        [Column("3")]
        public double Close { get; set; }

        [Column("4")]
        public double Volume { get; set; }

        [Column("5")]
        public double AverageDirectionalIndex { get; set; }

        [Column("6")]
        public double WilliamsR { get; set; }

        [Column("7")]
        public double AverageTrueRange { get; set; }        


        /// <summary>
        /// Gets or sets the close price you are trying to predict. This can be any number of days in the future, so long as you're consistent.
        /// </summary>
        [Column("8")]
        public double PredictedFutureClosePrice { get; set; }
    }
}