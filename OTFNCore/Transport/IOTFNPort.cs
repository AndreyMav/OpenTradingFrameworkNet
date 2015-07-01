using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Transport
{

    public class PacketEventArgs : EventArgs
    {
        public IOTFNPacket Packet;
    }

    public interface IOTFNPort
    {
        Task SendPacket(IOTFNPacket packet);
        event EventHandler<PacketEventArgs> OnPacket;
    }
}
