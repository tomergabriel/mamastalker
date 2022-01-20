using PingPong.Common;
using PingPong.Common.ConsolePresentation;
using PingPong.Common.Logic.DataConverters.Stringifies;
using PingPong.Server.Logic.Servers;
using PingPong.Server.Logic.Servers.Abstract;
using PingPong.Server.Presentation.ResponseHandlers;

namespace PingPong.Server
{
    public class Bootstrapper
    {
        public IServer BootstrapSocketSever()
        {
            var output = new ConsoleOutput<Person>();

            var stringToByteArrayDataParser = new ByteArrayStringify();

            var stringToPersonDataParser = new GenericJsonStringify<Person>();

            var onDataHandler = new OutputAndSendBackResponseHandler<string, Person>(output, stringToPersonDataParser, stringToByteArrayDataParser);

            var socketServer = new TCPServer(onDataHandler);

            return socketServer;
        }

    }
}
