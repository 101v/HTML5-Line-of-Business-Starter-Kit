using System;
using System.ServiceModel;
using System.Threading;
using Microsoft.ServiceModel.WebSockets;

namespace FabrikamWidgets.UI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ActivitySocket : WebSocketsService
    {
        private bool disposed;
        private Timer timer;

        public override void OnOpen()
        {
            timer = new Timer(TimerCallback, null, 0, Convert.ToInt32(TimeSpan.FromSeconds(15).TotalMilliseconds));

            base.OnOpen();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (this.timer != null)
                    {
                        this.timer.Dispose();
                    }
                }
                disposed = true;
            }

            base.Dispose(disposing);
        }

        private void TimerCallback(object state)
        {
            // var activity = activityService.Retrieve(date);
            //var activity = new Activity
            //{
            //    Published = DateTime.UtcNow,
            //    Actor = new ActivityObject
            //    {
            //        Url = "http://foo.com",
            //        ObjectType = "Customer",
            //        Id = "1234",
            //        DisplayName = "Contoso Bank",
            //    },
            //    Verb = "created a new order",
            //    @Object = new ActivityObject
            //    {
            //        Url = "http://bar.com",
            //        ObjectType = "Order",
            //        Id = "4321",
            //        DisplayName = "Order 43212",
            //    }
            //};

            //using (var writer = new StringWriter())
            //{
            //    var settings = new JsonSerializerSettings()
            //    {
            //        ContractResolver = new CamelCasePropertyNamesContractResolver()
            //    };
            //    JsonSerializer.Create(settings).Serialize(writer, activity);
            //    SendMessage(writer.ToString());
            //}

            var result = String.Format(
                "<a href=\"#\">{0}</a> placed a new order for {1} widgets<br /><small>{2} · <a href=\"#\">View</a> · <a href=\"#\">Comment</a></small>",
                "Contoso Bank", 23, DateTime.Now);

            SendMessage(result);
        }
    }
}