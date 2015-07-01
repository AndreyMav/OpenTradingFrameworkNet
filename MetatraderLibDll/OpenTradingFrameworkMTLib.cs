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
using Newtonsoft.Json.Linq;
using OTFN.Core.Endpoints.JSON;

namespace MetatraderLibDll
{
    public class OpenTradingFrameworkMTLib
    {
        private const int MaxQueueSize = 100000;

        static OpenTradingFrameworkMTLib()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static ILog Log = LogManager.GetLogger(typeof(OpenTradingFrameworkMTLib).Name);

        private static Dictionary<int, IStrategy> instances = new Dictionary<int,IStrategy>();

        private static Queue<MT4Request> requestQueue = new Queue<MT4Request>();

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

        private static JObject CreateErrorResponse(string error, int errorCode, int requestId)
        {
            JObject resp = new JObject();
            resp[JSONTradingInterface.KeyRequestId] = requestId;
            resp[JSONTradingInterface.KeyError] = error;
            resp[JSONTradingInterface.KeyErrorCode] = errorCode;
            return resp;
        }

        private static JObject CreateResponse(int requestId, JToken data)
        {
            JObject json = new JObject();
            json[JSONTradingInterface.KeyRequestId] = requestId;
            json[JSONTradingInterface.KeyData] = data;
            return json;
        }

        internal static async Task<JObject> ProcessRequest(JObject req)
        {
            string command = (string)req[JSONTradingInterface.KeyCommand];
            int requestId = (int)req[JSONTradingInterface.KeyRequestId];
            JToken data = (JToken)req[JSONTradingInterface.KeyData];
            switch(command)
            {
                case JSONTradingInterface.CommandGetOrders:
                    return await ReqGetOrders(requestId, data);    
                default:
                    return CreateErrorResponse("Command not implemented", (int)ErrorCodes.NotImplemented, requestId);
            }
        }

        private static async Task<JObject> ReqGetOrders(int requestId, JToken data)
        {
            MT4Response resp = await EnqueueRequest(new MT4Request(MT4Request.GetOrders));
            if(resp.ErrorCode == 0)
            {
                JArray orders = new JArray();

                foreach(Order o in resp.Objects)
                {
                    orders.Add(((Order)o).ToJSONObject());
                }
                return CreateResponse(requestId, orders);
            }

            return CreateErrorResponse("Failed to get Orders", resp.ErrorCode, requestId);
        }

        private static Task<MT4Response> EnqueueRequest(MT4Request req)
        {
            lock(requestQueue)
            {
                requestQueue.Enqueue(req);
                if (requestQueue.Count >= MaxQueueSize)
                    throw new RequestQueueOverloadException();
            }
            return req.WaitTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="doubles"></param>
        /// <param name="strings"></param>
        /// <param name="requestId"></param>
        /// <returns>Command ID or negative value if there is no more commands pending</returns>
        public static int OTFN_GetNextRequest(int instanceId, out double[] doubles, out string[] strings, out int requestId)
        {
            doubles = null;
            strings = null;
            requestId = 0;
            MT4Request req;
            lock(requestQueue)
            {
                if (requestQueue.Count == 0)
                    return -1;

                req = requestQueue.Dequeue();
            }

            doubles = req.Doubles;
            strings = req.Strings;
            requestId = req.Id;

            return req.Command;
        }

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
        public static int OTFN_Init(string brokerName, string accountId, string strategyName, int magic, string symbol, int timeframe, int cookie)
        {
            
            if(strategy == null)
                throw new StrategyNotFoundException(strategyName);

            instances[strategy.InstanceId] = strategy;

            return strategy.InstanceId;
        }


        public static int OTFN_Orders(int requestId, OrderInfo[] orders, int ordersCount)
        {
            IStrategy strategy;
            if (!instances.TryGetValue(requestId, out strategy))
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
