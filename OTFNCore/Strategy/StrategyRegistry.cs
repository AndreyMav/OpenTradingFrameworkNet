using OTFN.Core.Endpoints;
using OTFN.Core.Market;
using OTFN.Core.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Strategy
{
    public class StrategyRegistry
    {
        private static Dictionary<String, IStrategyFactory> strategies = new Dictionary<string,IStrategyFactory>();

        public static IStrategy CreateStrategyInstance(string strategyName, Endpoint endpoint)
        {
            IStrategyFactory factory;
            lock (strategies)
            {
                if (!strategies.TryGetValue(strategyName, out factory))
                {
                    return null;
                }

                return factory.CreateInstance(endpoint);
            }
        }

        public static bool HasStrategy(string strategyName)
        {
            lock(strategies)
            {
                return strategies.ContainsKey(strategyName);
            }
        }
    }
}
