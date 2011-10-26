using System.Web.Mvc;
using FabrikamWidgets.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FabrikamWidgets.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SalesDataWarehouseService salesDataWarehouseService;

        public CustomersController()
            : this(ServiceLocator.Current.GetInstance<SalesDataWarehouseService>())
        { }

        public CustomersController(SalesDataWarehouseService salesDataWarehouseService)
        {
            this.salesDataWarehouseService = salesDataWarehouseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Preview(string id)
        {
            var data = salesDataWarehouseService.RetrieveCustomerSnapshot(id);
            return new JsonNetResult()
            {
                Data = data,
                HttpStatusCode = (int)System.Net.HttpStatusCode.OK,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
