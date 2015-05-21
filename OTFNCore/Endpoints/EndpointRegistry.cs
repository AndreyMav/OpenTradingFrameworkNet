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
    public class EndpointRegistry
    {
        private static List<Endpoint> endpoints = new List<Endpoint>();

        public static Endpoint RefreshEndpoint(ITradingInterface tradingInterface, Broker broker, Account account, Symbol symbol, Timeframe timeframe)
        {
            lock(endpoints)
            {
                Endpoint endpoint = new Endpoint(tradingInterface, broker, account, symbol, timeframe);
                Endpoint existing = FindExistingEndpoint(endpoint);
                if (existing == null)
                {
                    endpoints.Add(endpoint);
                    existing = endpoint;
                }
                existing.Refresh();
                return existing;
            }
        }

        public static Endpoint FindExistingEndpoint(Endpoint endpoint)
        {
            lock(endpoints)
            {
                int idx = endpoints.IndexOf(endpoint);
                if(idx == -1)
                {
                    return null;
                }
                return endpoints[idx];
            }
        }

        public static Endpoint FindFirstByAccount(Account account)
        {
            lock(endpoints)
            {
                int idx = endpoints.FindIndex(0, endpoint => endpoint.Account == account);
                if (idx == -1)
                    return null;
                return endpoints[idx];
            }
        }
    }
}
