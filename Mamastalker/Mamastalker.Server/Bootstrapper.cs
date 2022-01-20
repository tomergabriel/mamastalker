using Mamastalker.Common.FormsPresentation.ResponseHandlers;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using Mamastalker.Server.Logic.Servers;
using Mamastalker.Server.Logic.Servers.Abstract;
using System.Drawing;

namespace Mamastalker.Server
{
    public class Bootstrapper
    {
        public ScreenshotOnTimerResponseHandler<string> OnDataHandler { get; private set; }

        public IServer BootstrapSever()
        {
            var byteArrayStringify = new ByteArrayStringify();

            var bitmapStringify = new GenericJsonStringify<Bitmap>();

            OnDataHandler = new ScreenshotOnTimerResponseHandler<string>(bitmapStringify, byteArrayStringify);

            var server = new TCPServer(OnDataHandler);

            return server;
        }

    }
}
