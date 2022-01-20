using Mamastalker.Client.Data.OnDataHandlers;
using Mamastalker.Client.Logic.Clients;
using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Common.Logic.DataConverters.Stringifies;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mamastalker.Client
{
    public class Bootstrapper
    {
        public IClient<string> BootstrapClient()
        {
            var binaryFormatter = new BinaryFormatter();

            var tcpClient = new TcpClient();

            var byteArrayStringify = new ByteArrayStringify();

            var stringStringify = new StringStringify();

            var bitmapStringify = new GenericBinaryFormatterStringify<Bitmap>(binaryFormatter, byteArrayStringify);

            var client = new TCPClient<string>(stringStringify, byteArrayStringify, tcpClient);

            var onDataHandler = new BitmapSaveToFileOnDataHandler<string>(bitmapStringify);

            client.OnReciveDataEvent += (recivedObject) => onDataHandler.OnDataEventHandler(recivedObject.ToString());

            return client;
        }
    }
}
