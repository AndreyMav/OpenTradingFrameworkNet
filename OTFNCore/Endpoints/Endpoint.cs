using OTFN.Core.Brokers;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints
{
    public class Endpoint
    {
        public delegate void EndpointRefreshHandler(object sender, EventArgs e);

        public event EndpointRefreshHandler Refreshed;

        private ITradingInterface TradingInterface;

        internal Endpoint(ITradingInterface tradingInterface, Broker broker, Account account, Symbol symbol, Timeframe timeframe)
        {
            TradingInterface = tradingInterface;
            Broker = broker;
            Account = account;
            Symbol = symbol;
            Timeframe = timeframe;
        }

        public Broker Broker;
        public Account Account;
        public Symbol Symbol;
        public Timeframe Timeframe;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Endpoint)
            {
                Endpoint ep = (Endpoint)obj;
                if (ep.Account == Account
                    && ep.Broker.UID == Broker.UID
                    && ep.Symbol == Symbol
                    && ep.Timeframe == Timeframe)
                    return true;
            }
            return false;
        }

        internal void Refresh()
        {
            if(Refreshed != null)
            {
                Refreshed(this, null);
            }
        }


        ////////////////////////////////////////////////////

        public Task<List<Order>> GetOrders()
        {
            return TradingInterface.GetOrders();
        }
    }
}
