using PingPong.Client.Logic.Clients;
using PingPong.Client.Logic.Clients.Abstract;
using PingPong.Client.Presentation.OnDataHandlers;
using PingPong.Common;
using PingPong.Common.ConsolePresentation;
using PingPong.Common.Logic.DataConverters.Stringifies;
using System.Net.Sockets;

namespace PingPong.Client
{
    public class Bootstrapper
    {
        public IClient<Person> BootstrapClient()
        {
            var tcpClient = new TcpClient();

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            var byteArrayStringify = new ByteArrayStringify();

            var stringStringify = new StringStringify();

            var genericJsonStringify = new GenericJsonStringify<Person>();

            var client = new TCPClient<Person>(genericJsonStringify, byteArrayStringify, tcpClient);

            var output = new ConsoleOutput<string>();

            var onDataHandler = new OutputOnDataHandler<string, string>(stringStringify, output);

            client.OnReciveDataEvent += (recivedObject) => onDataHandler.OnDataEventHandler(recivedObject.ToString());

            return client;
        }
    }
}
