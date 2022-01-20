using Mamastalker.Common;
using Mamastalker.Common.ConsolePresentation;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using Mamastalker.Server.Logic.Servers;
using Mamastalker.Server.Logic.Servers.Abstract;
using Mamastalker.Server.Presentation.ResponseHandlers;

namespace Mamastalker.Server
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
