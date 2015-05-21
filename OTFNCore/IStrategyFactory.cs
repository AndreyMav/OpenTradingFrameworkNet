using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTFN.Core
{
    public interface IStrategyFactory
    {
        IStrategy CreateInstance(Endpoint endpoint);
    }
}
