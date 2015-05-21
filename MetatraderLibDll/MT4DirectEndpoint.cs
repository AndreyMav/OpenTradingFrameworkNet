using OTFN.Core;
using OTFN.Core.Brokers;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    class MT4DirectEndpoint : Endpoint
    {
        public MT4DirectEndpoint(Broker broker, Account account, Symbol symbol, Timeframe timeframe)
            : base(broker, account, symbol, timeframe)
        {

        }

        protected override Task<List<Order>> UpdateOrders()
        {
            return null;
        }
    }

}
