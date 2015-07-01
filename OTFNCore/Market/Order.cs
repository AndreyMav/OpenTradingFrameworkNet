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

        #region Events

        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler Closed;

        #endregion Events


        public override bool Equals(object obj)
        {
            if(obj is Order)
            {
                return ((Order)obj).Ticket == Ticket;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Ticket.GetHashCode();
        }

        internal void OnClosed()
        {
            if (Closed != null)
                Closed(this, null);
        }
    }
}
