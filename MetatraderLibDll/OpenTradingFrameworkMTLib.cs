using OTFN.Core;
using OTFN.Core.Brokers;
using OTFN.Core.Market;
using OTFN.Core.Terminals;
using OTFN.Core.Utils;
using RGiesecke.DllExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    public class OpenTradingFrameworkMTLib
    {
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
        /// <param name="strategyName"></param>
        /// <param name="magic"></param>
        /// <param name="symbol"></param>
        /// <param name="timeframe"></param>
        /// <param name="accountId"></param>
        /// <returns>InstanceId of this particular expert or negative value on error</returns>
        [DllExport("OTFN_Init", CallingConvention = CallingConvention.StdCall)]
        public static int Init(string brokerName, string strategyName, int magic, string symbol, int timeframe, string accountId)
        {
            ForexBroker broker = new ForexBroker(brokerName);
            broker = (ForexBroker)BrokerRegistry.RegisterBroker(broker);
            Symbol brokerSymbol = broker.GetSymbolByName(symbol);
            
            if (brokerSymbol == null)
                return -1;

            Timeframe tf = Timeframe.GetFromMinutes(timeframe);
            Account account = broker.GetAccountById(accountId);

            Endpoint endpoint = new MT4DirectEndpoint(broker, account, brokerSymbol, tf);

            IStrategy strategy = StrategyRegistry.CreateStrategyInstance(strategyName, endpoint);
            if(strategy == null)
            {
                return -1;
            }

            instances[strategy.InstanceId] = strategy;

            return strategy.InstanceId;
        }


        [DllExport("OTFN_SendOrders", CallingConvention = CallingConvention.StdCall)]
        public static int SendOrders(int expertId, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] OrderInfo[] orders, int ordersCount)
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
