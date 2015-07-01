using Newtonsoft.Json.Linq;
using OTFN.Core.Brokers;
using OTFN.Core.Endpoints.JSON;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints
{
    public abstract class JSONTradingInterface : ITradingInterface
    {
        public const string KeyRequestId = "id";
        public const string KeyCommand = "cmd";
        public const string KeyData = "data";
        public const string KeyError = "error";
        public const string KeyErrorCode = "errorCode";

        public const string CommandGetOrders = "orders";
        public const string CommandGetAccountInfo = "accountInfo";
        public const string CommandGetQuotes = "quotes";

        public abstract Task<JObject> SendQuery(JObject query);

        protected IControllerInterface Controller;

        private JObject CreateQueryObject(String command)
        {
            return CreateQueryObject(command, null);
        }

        private JObject CreateQueryObject(String command, JObject data)
        {
            JObject obj = new JObject();
            obj.Add(KeyCommand, command);
            if (data != null)
            {
                obj.Add(KeyData, data);
            }
            return obj;
        }

        public async Task<IList<Order>> GetOrders()
        {
            JObject query = CreateQueryObject(CommandGetOrders);
            JObject response = await SendQuery(query);
            JArray arr = (JArray)response[KeyData];
            return arr.Select(o => new Order().FromJSONObject(o)).ToList();
        }

        public async Task<AccountInfo> GetAccountInfo()
        {
            JObject query = CreateQueryObject(CommandGetAccountInfo);
            JObject response = await SendQuery(query);
            return (AccountInfo)response.ToObject<AccountInfo>();
        }


        public async Task<List<Quote>> RequestQuotes(DateTime startTime, DateTime endTime)
        {
            JObject query = CreateQueryObject(CommandGetQuotes, new Timespan(startTime, endTime).ToJSONObject());
            JObject response = await SendQuery(query);
            JArray arr = (JArray)response[KeyData];
            return arr.Select(o => new Quote().FromJSONObject(o)).ToList();
        }

        public void SetController(IControllerInterface controller)
        {
            Controller = controller;
        }

        public event EventHandler Connected;

        public event EventHandler Disconnected;

        public event EventHandler<TickEventArgs> Tick;
    }
}
