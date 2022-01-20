using Mamastalker.Common.FormsPresentation.ResponseHandlers;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using Mamastalker.Server.Logic.Servers;
using Mamastalker.Server.Logic.Servers.Abstract;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mamastalker.Server
{
    public class Bootstrapper
    {
        public ScreenshotOnTimerResponseHandler<string> OnDataHandler { get; private set; }

        public IServer BootstrapSever()
        {
            var binaryFormatter = new BinaryFormatter();

            var byteArrayStringify = new ByteArrayStringify();

            var bitmapStringify = new GenericBinaryFormatterStringify<Bitmap>(binaryFormatter, byteArrayStringify);
            //var bitmapStringify = new GenericJsonStringify<Bitmap>();

            OnDataHandler = new ScreenshotOnTimerResponseHandler<string>(bitmapStringify, byteArrayStringify);

            var server = new TCPServer(OnDataHandler);

            return server;
        }

    }
}
