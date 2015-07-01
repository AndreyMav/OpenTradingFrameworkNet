using OTFN.Core.Brokers;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints
{
    public class TickEventArgs : EventArgs
    {
        public TickEventArgs(Tick tick)
        {
            Tick = tick;
        }

        public Tick Tick;
    }

    public class OnTesterEventArgs : EventArgs
    {
    }

    public interface ITradingInterface
    {
        void SetController(IControllerInterface controller);
        Task<IList<Order>> GetOrders();
        Task<AccountInfo> GetAccountInfo();
        Task<List<Quote>> RequestQuotes(DateTime startTime, DateTime endTime);

        event EventHandler Connected;
        event EventHandler Disconnected;
        event EventHandler<TickEventArgs> Tick;
    }
}
