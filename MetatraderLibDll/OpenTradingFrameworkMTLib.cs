using OTFN.Core;
using OTFN.Core.Brokers;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using OTFN.Core.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using OTFN.Core.Errors;
using OTFN.Core.Strategy;
using OTFN.Core.Endpoints;

namespace MetatraderLibDll
{
    public class OpenTradingFrameworkMTLib
    {
        static OpenTradingFrameworkMTLib()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static ILog Log = LogManager.GetLogger(typeof(OpenTradingFrameworkMTLib).Name);

        private static Dictionary<int, IStrategy> instances = new Dictionary<int,IStrategy>();

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct MqlTick
        {
            public Int64 Time;
            public Double Bid;
            public Double Ask;
            public Double Last;
            public UInt64 Volume;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct OrderInfo
        {
            public Int32 Ticket;
            public Int32 Type;
            public Double Lots;
            public Double OpenPrice;
            public Double ClosePrice;
            public Double TakeProfit;
            public Double StopLoss;
            public UInt64 Expiration;
            public UInt64 OpenTime;
            public UInt64 CloseTime;
            public Int32 Magic;
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brokerName"></param>
        /// <param name="accountId"></param>
        /// <param name="strategyName"></param>
        /// <param name="magic"></param>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <returns>InstanceId of this particular expert or negative value on error</returns>
        public static int Init(string brokerName, string accountId, string strategyName, int magic, string symbol, int timeframe)
        {
            Log.Info(String.Format("Init: broker: {0}, account: {1}, strategy: {2}, magic: {3}, symbol: {4}, timeframe: {5}", brokerName, accountId, strategyName, magic, symbol, timeframe));

            ForexBroker broker = new ForexBroker(brokerName);
            broker = (ForexBroker)BrokerRegistry.RegisterBroker(broker);
            Symbol brokerSymbol = broker.GetSymbolByName(symbol);

            if (brokerSymbol == null)
                throw new SymbolNotFoundException(symbol);

            Timeframe tf = Timeframe.GetFromMinutes(timeframe);
            Account account = broker.GetAccountById(accountId);

            Endpoint endpoint = EndpointRegistry.RefreshEndpoint(new MT4DirectEndpoint(), broker, account, brokerSymbol, tf);

            IStrategy strategy = StrategyRegistry.CreateStrategyInstance(strategyName, endpoint);

            if(strategy == null)
                throw new StrategyNotFoundException(strategyName);

            instances[strategy.InstanceId] = strategy;

            return strategy.InstanceId;
        }


        public static int SendOrders(int expertId, OrderInfo[] orders, int ordersCount)
        {
            IStrategy strategy;
            if (!instances.TryGetValue(expertId, out strategy))
                return -1;

            List<Order> orderList = new List<Order>();

            foreach(OrderInfo orderInfo in orders)
            {
                Order order = new Order();
                order.Ticket = orderInfo.Ticket;

                order.Ticket = orderInfo.Ticket;
                order.Type = orderInfo.Type;
                order.Lots = orderInfo.Lots;
                order.OpenPrice = orderInfo.OpenPrice;
                order.TakeProfit = orderInfo.ClosePrice;
                order.ClosePrice = orderInfo.TakeProfit;
                order.StopLoss = orderInfo.StopLoss;
                order.Expiration = TimeUtil.UnixTimeStampToDateTime(orderInfo.Expiration);
                order.OpenTime = TimeUtil.UnixTimeStampToDateTime(orderInfo.OpenTime);
                order.CloseTime = TimeUtil.UnixTimeStampToDateTime(orderInfo.CloseTime);
                order.Magic = orderInfo.Magic;

                orderList.Add(order);
            }

            strategy.UpdateOrders(orderList);

            return 0;
        }
    }
}
