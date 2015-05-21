using OTFN.Core.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTFN.Core.Strategy
{
    public interface IStrategyFactory
    {
        string StrategyName { get; }
        IStrategy CreateInstance(Endpoint endpoint);
    }
}
