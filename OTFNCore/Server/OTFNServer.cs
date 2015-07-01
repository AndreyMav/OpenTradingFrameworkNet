using OTFN.Core.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Server
{
    public class OTFNServer
    {
        private static OTFNServer instance;

        private bool IsInitialized = false;

        private OTFNServer()
        {

        }

        public static OTFNServer Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new OTFNServer();
                }
                return instance;
            }
        }


        private IOTFNPort Port;
        public void SetPort(IOTFNPort port)
        {
            if (IsInitialized)
                throw new Exception("Cannot set port when server is already initialized!");

            port.OnPacket += port_OnPacket;
            Port = port;
        }

        void port_OnPacket(object sender, PacketEventArgs e)
        {
            throw new NotImplementedException();
        }


    }
}
