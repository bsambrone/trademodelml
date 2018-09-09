using System;

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


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
