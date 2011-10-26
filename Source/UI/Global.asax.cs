using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using FabrikamWidgets.UI;
using Microsoft.Practices.ServiceLocation;
using Microsoft.ServiceModel.WebSockets;
using Ninject;
using Ninject.Extensions.Conventions;
using NinjectAdapter;

namespace UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly WebSocketsHost<ActivitySocket> activitySocketHost = new WebSocketsHost<ActivitySocket>();
        private readonly TcpListener flashSocketPolicyListener = new TcpListener(IPAddress.Any, 843);

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(CreateKernel()));

            // http://haacked.com/archive/2011/10/16/the-dangers-of-implementing-recurring-background-tasks-in-asp-net.aspx
            activitySocketHost.AddWebSocketsEndpoint("ws://localhost:4502/subscribe");
            activitySocketHost.Open();

            flashSocketPolicyListener.Start();
            flashSocketPolicyListener.BeginAcceptTcpClient(ar =>
            {
                using (var client = flashSocketPolicyListener.EndAcceptTcpClient(ar))
                {
                    var stream = client.GetStream();
                    // Loop to receive all the data sent by the client.
                    byte[] bytes = new Byte[256];
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var data = Encoding.ASCII.GetString(bytes, 0, i).ToUpper();
                        if (data.Contains("POLICY-FILE-REQUEST"))
                        {
                            var msg = Encoding.ASCII.GetBytes("<cross-domain-policy><allow-access-from domain=\"*\" to-ports=\"*\" /></cross-domain-policy>");
                            stream.Write(msg, 0, msg.Length);
                        }
                    }
                }
            }, null);
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Scan(x => x.WhereTypeIsInNamespace("FabrikamWidgets.Core.Services"));

            return kernel;
        }

        protected void Application_End()
        {
            flashSocketPolicyListener.Stop();
            activitySocketHost.Close();
        }
    }
}