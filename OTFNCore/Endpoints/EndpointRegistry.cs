using log4net;
using OTFN.Core.Brokers;
using OTFN.Core.Errors;
using OTFN.Core.Market;
using OTFN.Core.Strategy;
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
        private static ILog Log = LogManager.GetLogger(typeof(EndpointRegistry));

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

        public static Endpoint RegisterEndpoint(ITradingInterface tradingInterface, string brokerName, string accountId, string strategyName, int magic, string symbol, int timeframe)
        {
            Log.Info(String.Format("Init: broker: {0}, account: {1}, strategy: {2}, magic: {3}, symbol: {4}, timeframe: {5}", brokerName, accountId, strategyName, magic, symbol, timeframe));

            if(!StrategyRegistry.HasStrategy(strategyName))
            {
                Log.Error("Invalid strategy");
                throw new StrategyNotFoundException(strategyName);
            }

            ForexBroker broker = new ForexBroker(brokerName);
            broker = (ForexBroker)BrokerRegistry.RegisterBroker(broker);
            Symbol brokerSymbol = broker.GetSymbolByName(symbol);

            if (brokerSymbol == null)
                throw new SymbolNotFoundException(symbol);

            Timeframe tf = Timeframe.GetFromMinutes(timeframe);
            Account account = broker.GetAccountById(accountId);

            Endpoint endpoint = EndpointRegistry.RefreshEndpoint(tradingInterface, broker, account, brokerSymbol, tf);

            endpoint.BindStrategy(strategyName, magic);

            return endpoint;
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
