using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Brokers
{
    public class Account
    {
        private string accountId;

        public string AccountId
        {
            get { return accountId; }
        }

        public Account(string id)
        {
            accountId = id;
        }
    }
}
