using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints.JSON
{
    public class Timespan
    {
        private const string KeyFrom = "from";
        private const string KeyTo= "to";

        public DateTime From;
        public DateTime To;

        public Timespan(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public JObject ToJSONObject()
        {
            JObject result = new JObject();
            result.Add(KeyFrom, From);
            result.Add(KeyTo, To);

            return result;
        }
    }
}
