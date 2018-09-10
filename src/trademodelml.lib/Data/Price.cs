using Microsoft.ML.Runtime.Api;

namespace trademodelml.lib.Data
{
    public class Price
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
        

    }
}