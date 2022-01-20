using Mamastalker.Client.Logic.Clients;
using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Client.Presentation.OnDataHandlers;
using Mamastalker.Common;
using Mamastalker.Common.ConsolePresentation;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using System.Net.Sockets;

namespace Mamastalker.Client
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
