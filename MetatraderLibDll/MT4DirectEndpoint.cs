using OTFN.Core;
using OTFN.Core.Brokers;
using OTFN.Core.Endpoints;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    class MT4DirectEndpoint : ITradingInterface
    {

        public Task<List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }

}
