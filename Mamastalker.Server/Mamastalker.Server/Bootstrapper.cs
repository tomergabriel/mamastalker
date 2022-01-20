using Mamastalker.Server.Logic.Servers;
using Mamastalker.Server.Logic.DataConverters;
using Mamastalker.Server.Logic.ResponseHandlers;
using Mamastalker.Server.Logic.Servers.Abstract;
using System.Net.Sockets;

namespace PingPong
{
    public class Bootstrapper
    {
        public IServer BootstrapSever()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            var dataConverter = null;

            var onDataHandler = null;

            var server = new TCPServer(onDataHandler);

            return server;
        }

    }
}
