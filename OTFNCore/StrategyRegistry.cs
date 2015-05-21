using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core
{
    public class StrategyRegistry
    {
        private static Dictionary<String, IStrategyFactory> strategies = new Dictionary<string,IStrategyFactory>();

        public static IStrategy CreateStrategyInstance(string strategyName, Endpoint endpoint)
        {
            IStrategyFactory factory;
            if(!strategies.TryGetValue(strategyName, out factory))
            {
                return null;
            }

            return factory.CreateInstance(endpoint);
        }
    }
}
