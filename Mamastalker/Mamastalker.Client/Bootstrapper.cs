using Mamastalker.Client.Data.OnDataHandlers;
using Mamastalker.Client.Logic.Clients;
using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using System.Drawing;
using System.Net.Sockets;

namespace Mamastalker.Client
{
    public class Bootstrapper
    {
        public IClient<string> BootstrapClient()
        {
            var folderName = "clientScreenshots";
            
            var tcpClient = new TcpClient();

            var byteArrayStringify = new ByteArrayStringify();

            var stringStringify = new StringStringify();

            var genericJsonStringify = new GenericJsonStringify<Bitmap>();

            var client = new TCPClient<string>(stringStringify, byteArrayStringify, tcpClient);

            var onDataHandler = new BitmapSaveToFileOnDataHandler<string>(folderName, genericJsonStringify);

            client.OnReciveDataEvent += (recivedObject) => onDataHandler.OnDataEventHandler(recivedObject.ToString());

            return client;
        }
    }
}
