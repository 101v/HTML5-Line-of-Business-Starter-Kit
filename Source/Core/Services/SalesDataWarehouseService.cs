using System;
using System.Collections.Generic;

namespace FabrikamWidgets.Core.Services
{
    /// <remarks>
    /// These methods should pull data from a read data store (CQRS) or data
    /// warehouse (OLAP). For demo purposes it will generate random data.
    /// </remarks>
    public class SalesDataWarehouseService
    {
        private readonly static Random random = new Random();

        public Dictionary<DateTime, int> Revenue(DateTime fromDate, DateTime toDate)
        {

            var result = new Dictionary<DateTime, int>();
            for (double day = fromDate.Subtract(toDate).TotalDays; day >= 0; day--)
            {
                result.Add(DateTime.Today.AddDays(day * -1), random.Next(300));
            }
            return result;
        }

        public Dictionary<DateTime, int> OrderVolume(DateTime fromDate, DateTime toDate)
        {
            var result = new Dictionary<DateTime, int>();
            for (double day = fromDate.Subtract(toDate).TotalDays; day >= 0; day--)
            {
                result.Add(DateTime.Today.AddDays(day * -1), random.Next(30));
            }
            return result;
        }

        public Dictionary<DateTime, double> AverageOrderValue(DateTime fromDate, DateTime toDate)
        {
            var result = new Dictionary<DateTime, double>();
            for (double day = fromDate.Subtract(toDate).TotalDays; day >= 0; day--)
            {
                result.Add(DateTime.Today.AddDays(day * -1), Math.Round(random.Next(500) * random.NextDouble(), 2));
            }
            return result;
        }

        public Dictionary<DateTime, Tuple<int, int>> UniqueCustomers(DateTime fromDate, DateTime toDate)
        {
            var result = new Dictionary<DateTime, Tuple<int, int>>();
            for (double day = fromDate.Subtract(toDate).TotalDays; day >= 0; day--)
            {
                var uniqueCustomers = random.Next(500);
                var newCustomers = random.Next(uniqueCustomers);
                result.Add(DateTime.Today.AddDays(day * -1), new Tuple<int, int>(uniqueCustomers, newCustomers));
            }
            return result;
        }

        public Dictionary<DateTime, int> UniqueSiteVisitors(DateTime fromDate, DateTime toDate)
        {
            var result = new Dictionary<DateTime, int>();
            for (double day = fromDate.Subtract(toDate).TotalDays; day >= 0; day--)
            {
                result.Add(DateTime.Today.AddDays(day * -1), random.Next(30));
            }
            return result;
        }

        public CustomerSnapshot RetrieveCustomerSnapshot(string id)
        {
            return new CustomerSnapshot
            {
                FirstName = id.Split(' ')[0],
                LastName = id.Split(' ')[1],
                EmailAddress = String.Format("{0}.{1}@foo.com", id.Split(' ')[0], id.Split(' ')[1]),
                PhoneNumber = "(416) 999-6738",
                MonthlyOrders = new List<int>
                {
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                    random.Next(300),
                }
            };
        }
    }
}
