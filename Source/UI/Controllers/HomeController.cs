using System;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using FabrikamWidgets.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FabrikamWidgets.UI.Controllers
{
    public class HomeController : Controller
    {
        private const int SalesHistoryDayRange = 30;
        private readonly SalesDataWarehouseService salesDataWarehouseService;

        public HomeController()
            : this(ServiceLocator.Current.GetInstance<SalesDataWarehouseService>())
        { }

        public HomeController(SalesDataWarehouseService salesDataWarehouseService)
        {
            this.salesDataWarehouseService = salesDataWarehouseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalesHistory(DateTime date)
        {
            var data = salesDataWarehouseService.Revenue(date, date.Subtract(TimeSpan.FromDays(SalesHistoryDayRange)));
            return new JsonNetResult()
            {
                Data = data.ToList().ConvertAll(item => new object[] { item.Key.ToJavascriptTimestamp(), item.Value }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult ActivityStream(DateTime? date)
        {
            throw new NotImplementedException();
            // http://activitystrea.ms/
        }

        public ActionResult KeyPerformanceIndicators(DateTime date)
        {
            dynamic data = new ExpandoObject();
            var orderVolume = salesDataWarehouseService.OrderVolume(date, date.Subtract(TimeSpan.FromDays(SalesHistoryDayRange)));
            data.RecentOrders = new ExpandoObject();
            data.RecentOrders.CurrentValue = orderVolume.Values.LastOrDefault();
            data.RecentOrders.HistoricalValues = orderVolume.Values.ToArray();
            var averageOrderValue = salesDataWarehouseService.AverageOrderValue(date, date.Subtract(TimeSpan.FromDays(SalesHistoryDayRange)));
            data.AverageOrderValue = new ExpandoObject();
            data.AverageOrderValue.CurrentValue = averageOrderValue.Values.LastOrDefault();
            data.AverageOrderValue.HistoricalValues = averageOrderValue.Values.ToArray();
            var uniqueCustomers = salesDataWarehouseService.UniqueCustomers(date, date.Subtract(TimeSpan.FromDays(SalesHistoryDayRange)));
            data.NewCustomerRatio = new ExpandoObject();
            data.NewCustomerRatio.CurrentValue = uniqueCustomers.Values.LastOrDefault().Item2;
            data.NewCustomerRatio.HistoricalValues = uniqueCustomers.Values.ToList().ConvertAll(item => item.Item1).ToArray();
            data.NewCustomerRatio.HistoricalUniqueCustomerValues = uniqueCustomers.Values.ToList().ConvertAll(item => item.Item2).ToArray();
            var uniqueSiteVisitors = salesDataWarehouseService.UniqueSiteVisitors(date, date.Subtract(TimeSpan.FromDays(SalesHistoryDayRange)));
            data.UniqueSiteVisitors = new ExpandoObject();
            data.UniqueSiteVisitors.CurrentValue = uniqueSiteVisitors.Values.LastOrDefault();
            data.UniqueSiteVisitors.HistoricalValues = uniqueSiteVisitors.Values.ToArray();
            return new JsonNetResult() { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
