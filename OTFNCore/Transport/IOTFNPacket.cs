using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Transport
{
    public interface IOTFNPacket
    {
        int RequestId { get; }
        string Operation { get; }
        IOTFNPacketPayload Payload { get; }
    }
}
