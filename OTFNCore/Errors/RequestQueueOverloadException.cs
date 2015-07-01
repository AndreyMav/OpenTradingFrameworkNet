using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Errors
{
    public class RequestQueueOverloadException : OTFNException
    {
        public RequestQueueOverloadException(string message)
            : base(message)
        {

        }

        public RequestQueueOverloadException()
        {

        }

    }
}
