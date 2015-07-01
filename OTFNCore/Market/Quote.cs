using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Market
{
    public class Quote
    {
        public double Open;
        public double High;
        public double Low;
        public double Close;
        public double Volume;
        public double RealVolume;
        public uint Timestamp;

        public enum Price
        {
            Close,
            Open,
            High,
            Low
        }
    }
}
