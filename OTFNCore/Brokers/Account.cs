using OTFN.Core.Endpoints;
using OTFN.Core.Errors;
using OTFN.Core.Market;
using OTFN.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Brokers
{
    public class Account
    {
        #region Constants

        private const int OrderUpdateThrottlePeriodMS = 100;

        #endregion Constants


        #region Events

        public class OrderEventArgs : EventArgs
        {
            public Order Order;
            public OrderEventArgs(Order order)
            {
                Order = order;
            }
        }


        public delegate void OrderOpenedHandler(object sender, OrderEventArgs e);
        public event OrderOpenedHandler OrderOpened;

        public delegate void OrderClosedHandler(object sender, OrderEventArgs e);
        public event OrderClosedHandler OrderClosed;


        #endregion Events

        private string accountId;

        public string AccountId
        {
            get { return accountId; }
        }

        public Account(string id)
        {
            accountId = id;
        }

        public override int GetHashCode()
        {
            return accountId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Account)
            {
                return ((Account)obj).accountId == accountId;
            }
            return false;
        }

#region Orders

        private List<Order> orders = new List<Order>();
        ThrottledOperation ordersUpdateOperation;

        public Task UpdateOrders()
        {
            lock(this)
            {
                if(ordersUpdateOperation == null)
                {
                    ordersUpdateOperation = new ThrottledOperation(OrderUpdateThrottlePeriodMS, async () =>
                    {
                        Endpoint endpoint = EndpointRegistry.FindFirstByAccount(this);
                        if (endpoint == null)
                            throw new AccountOfflineException(accountId);

                        IList<Order> newOrders = await endpoint.GetOrders();

                        IEnumerable<Order> closed = orders.Except(newOrders);
                        IEnumerable<Order> opened = newOrders.Except(orders);

                        orders.RemoveAll(order => closed.Contains(order));
                        foreach(Order order in opened)
                        {
                            orders.Add(order);
                        }

                        if (OrderOpened != null)
                        {
                            foreach (Order order in opened)
                            {
                                OrderOpened(this, new OrderEventArgs(order));
                            }
                        }

                        foreach (Order order in closed)
                        {
                            order.OnClosed();
                            if (OrderClosed != null)
                            {
                                OrderClosed(this, new OrderEventArgs(order));
                            }
                        }

                        
                    });
                }
            }

            return ordersUpdateOperation.Run();
        }

#endregion Orders

    }
}
