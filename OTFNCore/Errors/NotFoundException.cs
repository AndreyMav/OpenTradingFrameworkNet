using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class NotFoundException : OTFNException
    {
        public NotFoundException(string message)
            : base(message)
        {

        }
    }
}
