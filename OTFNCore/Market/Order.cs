using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Market
{
    public class Order
    {
        public int Ticket;
        public int Type;
        public double Lots;
        public double OpenPrice;
        public double ClosePrice;
        public double TakeProfit;
        public double StopLoss;
        public DateTime Expiration;
        public DateTime OpenTime;
        public DateTime CloseTime;
        public int Magic;

    }
}
