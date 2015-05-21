using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Terminals
{
    public class BrokerRegistry
    {
        private static Dictionary<String, Broker> brokers = new Dictionary<string, Broker>();

        public static ForexBroker GenericForexBroker
        {
            get
            {
                return (ForexBroker)RegisterBroker(new ForexBroker("genericForex"));
            }
        }

        public static Broker GetBrokerByUID(string uid)
        {
            Broker broker = null;
            brokers.TryGetValue(uid, out broker);
            return broker;
        }

        public static Broker RegisterBroker(Broker broker)
        {
            Broker existingBroker;
            if(!brokers.TryGetValue(broker.UID, out existingBroker))
            {
                existingBroker = broker;
                brokers.Add(broker.UID, broker);
            }

            return existingBroker;
        }
    }
}
