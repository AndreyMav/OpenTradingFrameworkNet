using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BridgeClient
{
    class BridgeConnection
    {
        private NetMQContext context;
        private NetMQSocket socket;
        private Poller poller;

        public BridgeConnection()
        {
        }

        public void Start()
        {
            context = NetMQContext.Create();
            socket = context.CreateRequestSocket();
            poller = new Poller();
            poller.AddSocket(socket);

            socket.Connect("tcp://localhost:8000");
            socket.ReceiveReady += socket_ReceiveReady;
        }

        void socket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
        }
    }
}
