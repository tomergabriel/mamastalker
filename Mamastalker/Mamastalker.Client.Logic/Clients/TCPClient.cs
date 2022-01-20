using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
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

        private void Listen()
        {
            using var streamReader = new StreamReader(_tcpClient.GetStream());

            while (_tcpClient.Connected)
            {
                var recivedData = streamReader.ReadToEnd();

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

            var byteData = _byteArrayStringify.Parse(_stringify.Stringify(data));

            using var streamWriter = new StreamWriter(_tcpClient.GetStream());

            streamWriter.WriteLine(byteData);
            streamWriter.Flush();
        }

        public void Connect(IPEndPoint endPoint)
        {
            _tcpClient.Connect(endPoint);

            Task.Run(() => Listen());
        }
    }
}
