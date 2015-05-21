using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTFN.Core
{
    public interface IStrategy
    {
        int InstanceId { get; }
        void UpdateOrders(ICollection<Order> orders);
    }
}
