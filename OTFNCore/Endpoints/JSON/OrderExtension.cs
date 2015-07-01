using Newtonsoft.Json.Linq;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints.JSON
{
    public static class OrderExtension
    {
        private const string KeyOrderId = "id";
        private const string KeyOrderType = "type";
        private const string KeyOrderLots = "lots";
        private const string KeyOrderOpenPrice = "open";
        private const string KeyOrderClosePrice = "close";
        private const string KeyOrderTakeProfit = "tp";
        private const string KeyOrderStopLoss = "sl";
        private const string KeyOrderExpiration = "expTime";
        private const string KeyOrderOpenTime = "openTime";
        private const string KeyOrderCloseTime = "closeTime";
        private const string KeyOrderMagic = "magic";

        public static Order FromJSONObject(this Order order, JToken obj)
        {
            order.Ticket = (int)obj[KeyOrderId];
            order.Type = (int)obj[KeyOrderType];
            order.Lots = (double)obj[KeyOrderLots];
            order.OpenPrice = (double)obj[KeyOrderOpenPrice];
            order.ClosePrice = (double)obj[KeyOrderClosePrice];
            order.TakeProfit = (double)obj[KeyOrderTakeProfit];
            order.StopLoss = (double)obj[KeyOrderStopLoss];
            order.Expiration = (DateTime)obj[KeyOrderExpiration];
            order.OpenTime = (DateTime)obj[KeyOrderOpenTime];
            order.CloseTime = (DateTime)obj[KeyOrderCloseTime];
            order.Magic = (int)obj[KeyOrderMagic];
            return order;
        }

        public static JObject ToJSONObject(this Order order)
        {
            JObject obj = new JObject();
            obj[KeyOrderId] = order.Ticket;
            obj[KeyOrderType] = order.Type;
            obj[KeyOrderLots] = order.Lots;
            obj[KeyOrderOpenPrice] = order.OpenPrice;
            obj[KeyOrderClosePrice] = order.ClosePrice;
            obj[KeyOrderTakeProfit] = order.TakeProfit;
            obj[KeyOrderStopLoss] = order.StopLoss;
            obj[KeyOrderExpiration] = order.Expiration;
            obj[KeyOrderOpenTime] = order.OpenTime;
            obj[KeyOrderCloseTime] = order.CloseTime;
            obj[KeyOrderMagic] = order.Magic;
            return obj;
        }
    }
}
