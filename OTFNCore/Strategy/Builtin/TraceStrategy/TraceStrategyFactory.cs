using OTFN.Core.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Strategy.Builtin.TraceStrategy
{
    class TraceStrategyFactory : IStrategyFactory
    {
        public IStrategy CreateInstance(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        public string StrategyName
        {
            get { return "Trace"; }
        }
    }
}
