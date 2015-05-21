using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class AccountOfflineException : OTFNException
    {
        public AccountOfflineException(string message)
            : base(message)
        {

        }
    }
}
