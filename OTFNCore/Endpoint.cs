using OTFN.Core.Brokers;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core
{
    public abstract class Endpoint
    {
        public Endpoint(Broker broker, Account account, Symbol symbol, Timeframe timeframe)
        {
            Broker = broker;
            Account = account;
            Symbol = symbol;
            Timeframe = timeframe;
        }

        public Broker Broker;
        public Account Account;
        public Symbol Symbol;
        public Timeframe Timeframe;

        protected abstract Task<List<Order>> UpdateOrders();
    }
}
