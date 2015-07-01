using Newtonsoft.Json.Linq;
using OTFN.Core;
using OTFN.Core.Brokers;
using OTFN.Core.Endpoints;
using OTFN.Core.Market;
using OTFN.Core.Strategy;
using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    class MT4DirectTradingInterface : JSONTradingInterface
    {
        public override Task<JObject> SendQuery(JObject query)
        {
            return OpenTradingFrameworkMTLib.ProcessRequest(query);
        }
    }

}
