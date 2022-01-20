using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mamastalker.Client.Logic.Clients
{
    public class TCPClient<TData> : IClient<TData>
    {
        private readonly IStringify<TData> _stringify;

        private readonly IStringify<byte[]> _byteArrayStringify;

        private readonly TcpClient _tcpClient;

        public Action<TData> OnReciveDataEvent { get; set; }

        public TCPClient(IStringify<TData> stringify,
                         IStringify<byte[]> byteArrayStringify,
                         TcpClient tcpClient)
        {
            _stringify = stringify;
            _byteArrayStringify = byteArrayStringify;
            _tcpClient = tcpClient;
        }

        ~TCPClient()
        {
            _tcpClient.Close();
        }

        private string ListenLoop()
        {
            var data = string.Empty;

            var networkStream = _tcpClient.GetStream();

            while (true)
            {
                var bytes = new byte[1024];

                var bytesReceived = networkStream.Read(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                if (data.Contains((char)4))
                {
                    data = data[0..^1];
                    break;
                }
            }

            return data;
        }

        private void Listen()
        {
            while (_tcpClient.Connected)
            {
                var recivedData = ListenLoop();

                var parsedRecivedData = _stringify.Parse(recivedData);

                OnReciveDataEvent?.Invoke(parsedRecivedData);
            }
        }

        public void SendData(TData data)
        {
            if (_tcpClient is null)
            {
                return;
            }

            var stringifiedData = _stringify.Stringify(data);

            var byteData = _byteArrayStringify.Parse(stringifiedData + (char)4);

            var networkStream = _tcpClient.GetStream();

            networkStream.Write(byteData);
        }

        public void Connect(IPEndPoint endPoint)
        {
            _tcpClient.Connect(endPoint);

            Task.Run(() => Listen());
        }
    }
}
