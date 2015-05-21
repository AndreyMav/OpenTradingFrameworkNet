using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class SymbolNotFoundException : NotFoundException
    {
        public SymbolNotFoundException(string message)
            : base(message)
        {

        }
    }
}
