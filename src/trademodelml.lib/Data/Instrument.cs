namespace trademodelml.lib.Data
{
    public class Instrument
    {
        /// <summary>
        /// Gets or sets the sector that contains this symbol's industry.
        /// </summary>
        public string Sector { get; set; }

        /// <summary>
        /// Gets or sets the industry that contains this symbol.
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// Gets or sets the stock symbol associated with this dataset.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the exchange where this instrument is traded.
        /// </summary>
        public string Exchange { get; set; }
    }
}