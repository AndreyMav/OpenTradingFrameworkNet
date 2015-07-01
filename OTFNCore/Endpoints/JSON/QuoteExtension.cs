using Newtonsoft.Json.Linq;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints.JSON
{
    public static class QuoteExtension
    {
        private const string KeyOpen = "o";
        private const string KeyHigh = "h";
        private const string KeyLow = "l";
        private const string KeyClose = "c";
        private const string KeyVolume = "v";
        private const string KeyRealVolume = "V";
        private const string KeyTimestamp = "t";

        public static JToken ToJSONObject(this Quote quote)
        {
            JArray obj = new JArray();
            
            obj.Add(quote.Open);
            obj.Add(quote.High);
            obj.Add(quote.Low);
            obj.Add(quote.Close);
            obj.Add(quote.Volume);
            obj.Add(quote.RealVolume);
            obj.Add(quote.Timestamp);
            return obj;
        }

        public static Quote FromJSONObject(this Quote quote, JToken jsonObj)
        {
            JArray arr = (JArray)jsonObj;
            quote.Open = (double)arr[0];
            quote.High = (double)arr[1];
            quote.Low = (double)arr[2];
            quote.Close = (double)arr[3];
            quote.Volume = (double)arr[4];
            quote.RealVolume = (double)arr[5];
            quote.Timestamp = (uint)arr[6];
            return quote;
        }
    }
}
