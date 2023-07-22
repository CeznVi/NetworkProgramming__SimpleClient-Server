using MessageServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientForTest
{
    class ClientState
    {
        public Socket Socket { get; set; }
        public string Message { get; set; }

        public byte[] ByteMessageResponce { get; set; }

    }
}
