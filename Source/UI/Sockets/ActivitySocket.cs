using System;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using Microsoft.ServiceModel.WebSockets;

namespace FabrikamWidgets.UI
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ActivitySocket : WebSocketsService
    {
        private bool disposed;
        private string[] subscription;
        private Timer timer;
        private Companies symbols = new Companies(
            new CompanyInfo { StockSymbol = "msft", StockPrice = 25 },
            new CompanyInfo { StockSymbol = "appl", StockPrice = 90 },
            new CompanyInfo { StockSymbol = "ibm", StockPrice = 130 },
            new CompanyInfo { StockSymbol = "orcl", StockPrice = 26 });

        public override void OnOpen()
        {
            subscription = HttpRequestUri.Query.Split(new char[] { '?', '=', '+' }, StringSplitOptions.RemoveEmptyEntries);
            if (subscription.Length > 1)
            {
                timer = new Timer(TimerCallback, null, 0, 1000);
            }

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

                    if (this.symbols != null)
                    {
                        this.symbols.Dispose();
                    }
                }
                disposed = true;
            }

            base.Dispose(disposing);
        }

        private void TimerCallback(object state)
        {
            string result = string.Empty;
            this.symbols.UpdateValues();
            for (var cnt = 1; cnt < this.subscription.Length; cnt++)
            {
                CompanyInfo info;
                if (this.symbols.TryGetValue(this.subscription[cnt], out info))
                {
                    result += string.Format(CultureInfo.InvariantCulture, "{0} ${1:0.00} ", subscription[cnt].ToUpperInvariant(), info.StockPrice);
                }
                else
                {
                    result += string.Format(CultureInfo.InvariantCulture, "{0} not found ", subscription[cnt].ToUpperInvariant());
                }
            }

            this.SendMessage(result);
        }
    }
}
