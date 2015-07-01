using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Strategy.Builtin.TraceStrategy
{
    class TraceStrategy : IStrategy
    {
        private int instanceId;

        public int InstanceId
        {
            get { return instanceId; }
        }

        ///////////////////////////////////////////////////////////////

        public void UpdateOrders(ICollection<Order> orders)
        {
        }

        public void OnEndpointRefreshed()
        {
        }
    }
}
