using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class StrategyNotFoundException : NotFoundException
    {
        public StrategyNotFoundException(string message)
            : base(message)
        {

        }
    }
}
