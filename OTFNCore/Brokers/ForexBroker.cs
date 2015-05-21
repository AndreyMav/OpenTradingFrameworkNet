using OTFN.Core.Brokers;
using OTFN.Core.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Terminals
{
    public class ForexBroker : Broker
    {
        private string uid;

        public override string UID
        {
            get { return uid; }
        }
        private void AddSymbolSimple(string symbolName)
        {
            symbolByName.Add(symbolName, Symbol.Create(symbolName, this, symbolName));
        }

        public ForexBroker(string uid)
        {
            this.uid = uid;
            AddSymbolSimple(ForexSymbolNames.EURUSD);
            AddSymbolSimple(ForexSymbolNames.EURGBP);
            AddSymbolSimple(ForexSymbolNames.GBPUSD);
            AddSymbolSimple(ForexSymbolNames.USDCAD);
            AddSymbolSimple(ForexSymbolNames.EURCAD);
            AddSymbolSimple(ForexSymbolNames.EURJPY);
            AddSymbolSimple(ForexSymbolNames.USDJPY);
            AddSymbolSimple(ForexSymbolNames.GBPJPY);
            AddSymbolSimple(ForexSymbolNames.GBPCAD);
            AddSymbolSimple(ForexSymbolNames.EURAUD);
            AddSymbolSimple(ForexSymbolNames.AUDCAD);
            AddSymbolSimple(ForexSymbolNames.AUDUSD);
            AddSymbolSimple(ForexSymbolNames.CADJPY);
            AddSymbolSimple(ForexSymbolNames.XAUUSD);
            AddSymbolSimple(ForexSymbolNames.XAGUSD);
            AddSymbolSimple(ForexSymbolNames.AUDNZD);
        }
    }
}
