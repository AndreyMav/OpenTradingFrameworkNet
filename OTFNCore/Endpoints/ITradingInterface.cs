using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Endpoints
{
    public interface ITradingInterface
    {
        Task<List<Order>> GetOrders();
    }
}
