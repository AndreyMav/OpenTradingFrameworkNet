using OTFN.Core.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Market
{
    public class Symbol
    {
        private string name;
        private string brokerSpecificName;
        private Broker broker;

        private static Dictionary<string, Symbol> allSymbols = new Dictionary<string,Symbol>();

        public Broker Broker
        {
            get { return broker; }
        }

        public string BrokerSpecificName
        {
            get { return brokerSpecificName; }
        }

        public string Name
        {
            get { return name; }
        }

        private Symbol(string name, Broker broker, string brokerName)
        {
            this.name = name;
            this.brokerSpecificName = brokerName;
            this.broker = broker;
        }

        public static Symbol Create(string name, Broker broker, string brokerSpecificName)
        {
            Symbol symbol;

            string key = name + "|" + brokerSpecificName;

            if(!allSymbols.TryGetValue(key, out symbol))
            {
                symbol = new Symbol(name, broker, brokerSpecificName);
                allSymbols[key] = symbol;
            }

            return symbol;
        }

    }
}
