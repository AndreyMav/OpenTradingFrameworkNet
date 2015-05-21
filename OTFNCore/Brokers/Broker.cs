using OTFN.Core.Brokers;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Terminals
{
    public abstract class Broker
    {
        abstract public string UID
        {
            get;
        }

        virtual protected Account RegisterAccount(string accountId)
        {
            Account account = new Account(accountId);
            registeredAccounts.Add(accountId, account);
            return account;
        }

        protected Dictionary<string, Symbol> symbolByName = new Dictionary<string, Symbol>();
        protected Dictionary<string, Account> registeredAccounts = new Dictionary<string, Account>();

        public Account GetAccountById(string accountId)
        {
            Account account;
            if(!registeredAccounts.TryGetValue(accountId, out account))
            {
                account = RegisterAccount(accountId);
            }

            return account;
        }

        public ICollection<Symbol> GetSymbols()
        {
            return symbolByName.Values;
        }

        public Symbol GetSymbolByName(string name)
        {
            Symbol symbol = null;
            symbolByName.TryGetValue(name, out symbol);
            return symbol;
        }


    }
}
