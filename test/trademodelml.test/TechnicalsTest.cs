using System;
using System.Collections.Generic;
using trademodelml.lib.Data;
using trademodelml.lib.Technicals;
using Xunit;

namespace trademodelml.test
{
    public class TechnicalsTest
    {
        [Fact]
        public void WilliamsRTest()
        {
            // setup
            int periods = 5;
            var model = new PriceData();
            model.Prices = new SortedList<DateTime, Price>();
            model.Prices.Add(new DateTime(2018,1,1), new Price { Open = 1, High = 2, Low = 1, Close = 1.5 });
            model.Prices.Add(new DateTime(2018,1,2), new Price { Open = 1.5, High = 4, Low = 1, Close = 3 });
            model.Prices.Add(new DateTime(2018,1,3), new Price { Open = 3, High = 5, Low = 0.7, Close = 4 });
            model.Prices.Add(new DateTime(2018,1,4), new Price { Open = 4, High = 4.4, Low = 1.2, Close = 2.7 });
            model.Prices.Add(new DateTime(2018,1,5), new Price { Open = 2.7, High = 3, Low = 1.8, Close = 1.9 });
            model.Prices.Add(new DateTime(2018,1,6), new Price { Open = 1.9, High = 2, Low = 0.4, Close = 1.2 });
            model.Prices.Add(new DateTime(2018,1,7), new Price { Open = 1.2, High = 2.5, Low = 1, Close = 1.5 });
            model.Prices.Add(new DateTime(2018,1,8), new Price { Open = 1.5, High = 3.2, Low = 1.9, Close = 2.8 });
            model.Prices.Add(new DateTime(2018,1,9), new Price { Open = 2.8, High = 5, Low = 2.5, Close = 4.1 });
            model.Prices.Add(new DateTime(2018,1,10), new Price { Open = 4.1, High = 4.2, Low = 3.2, Close = 3.8 });

            // test
            var calculated = model.Prices.CalculateWilliamsR(periods);
            
            // assert            
            Assert.NotEmpty(calculated);
            // TODO - assert the correct value
        }
    }
}