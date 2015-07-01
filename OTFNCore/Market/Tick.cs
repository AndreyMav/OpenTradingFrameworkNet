using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Market
{
    public class Tick
    {
        DateTime Timestamp;
        public double Bid;
        public double Ask;
        public double Last;
        public double Volume;
    }
}
