using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Transport.Direct
{
    public class DirectMT4Port : IOTFNPort
    {
        public Task SendPacket(IOTFNPacket packet)
        {
            OpenTradingFrameworkMTLib.EnqueueRequest();
        }

        public event EventHandler<PacketEventArgs> OnPacket;
    }
}
