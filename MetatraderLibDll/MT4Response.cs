using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetatraderLibDll
{
    class MT4Response
    {
        public int ErrorCode;
        public string[] Strings;
        public double[] Doubles;
        public object[] Objects;
    }
}
