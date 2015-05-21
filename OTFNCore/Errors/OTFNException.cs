using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class OTFNException : Exception
    {
        public OTFNException(string message) : base(message)
        {

        }
    }
}
