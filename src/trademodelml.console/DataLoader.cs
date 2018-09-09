using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using trademodelml.lib.Data;

namespace trademodelml.console
{
    public class DataLoader
    {
        // this is for my local machine to mess around with data. Use your own data loader for your own console app.
        private const string Connstring = "Server=BILL-PC;Database=Finance;Trusted_Connection=True;";
        
        public PriceData LoadData(string symbol, DateTime start, DateTime end)
        {
            var dataSet = new PriceData();
            dataSet.Prices = GetPrices(symbol, start, end);
            dataSet.Instrument = GetInstrument(symbol);
            return dataSet;
        }
        
        public SortedList<DateTime, Price> GetPrices(string symbol, DateTime start, DateTime end)
        {            
            var data = new SortedList<DateTime, Price>();
            using (var conn = new SqlConnection(Connstring))
            {
                conn.Open();
                string cmdtext = "SELECT OpenPrice,HighPrice,LowPrice,ClosePrice,Volume,CandleStart FROM Prices WHERE InstrumentID = (SELECT InstrumentID FROM Instruments WHERE Symbol = @symbol) AND PriceStart >= @start AND PriceEnd <= @end ORDER BY PriceStart";
                var cmd = new SqlCommand(cmdtext,conn);
                cmd.Parameters.Add(new SqlParameter("symbol", symbol));
                cmd.Parameters.Add(new SqlParameter("start", start));
                cmd.Parameters.Add(new SqlParameter("end", end));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var candle = new Price
                    {
                        Open = reader.GetDouble(0),
                        High = reader.GetDouble(1),
                        Low = reader.GetDouble(2),
                        Close = reader.GetDouble(3),
                        Volume = reader.GetInt32(4)
                    };

                    data.Add(reader.GetDateTime(5), candle);
                }

                conn.Close();
            }
            return data;
        }

        public Instrument GetInstrument(string symbol)
        {
            var instrument = new Instrument();
            using (var conn = new SqlConnection(Connstring))
            {
                conn.Open();
                string cmdtext = "SELECT e.Exchange, s.Sector, i.Industry FROM Instruments p JOIN Industries i ON i.IndustryId = p.IndustryId JOIN Sectors s ON s.SectorID = i.SectorId JOIN Exchanges e ON e.ExchangeID = p.ExchangeID WHERE p.Symbol = @symbol";
                var cmd = new SqlCommand(cmdtext, conn);
                cmd.Parameters.Add(new SqlParameter("symbol", symbol));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    instrument.Symbol = symbol;
                    instrument.Exchange = reader.GetValue(0).ToString();
                    instrument.Sector = reader.GetValue(1).ToString();
                    instrument.Industry = reader.GetValue(2).ToString();
                }
                reader.Close();

                conn.Close();
            }
            return instrument;
        }
    }
}