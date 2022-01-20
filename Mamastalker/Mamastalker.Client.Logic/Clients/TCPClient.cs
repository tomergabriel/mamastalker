using Mamastalker.Client.Logic.Clients.Abstract;
using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mamastalker.Client.Logic.Clients
{
    public class TCPClient<TData> : IClient<TData>
    {
        private readonly IStringify<TData> _stringify;

        private readonly TcpClient _tcpClient;

        public Action<TData> OnReciveDataEvent { get; set; }

        public TCPClient(IStringify<TData> stringify,
                         TcpClient tcpClient)
        {
            _stringify = stringify;
            _tcpClient = tcpClient;
        }

        ~TCPClient()
        {
            _tcpClient.Close();
        }

        private string ListenLoop(NetworkStream networkStream)
        {
            var data = string.Empty;

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

            Console.WriteLine("debug: recived data");

            return data;
        }

        private void Listen()
        {
            while (_tcpClient.Connected)
            {
                var recivedData = ListenLoop(_tcpClient.GetStream());

                var parsedRecivedData = _stringify.Parse(recivedData);

                OnReciveDataEvent?.Invoke(parsedRecivedData);
            }
        }

        public void SendData(TData data)
        {
            if (!_tcpClient.Connected)
            {
                return;
            }

            var message = _stringify.Stringify(data);

            using var streamWriter = new StreamWriter(_tcpClient.GetStream());

            Console.WriteLine("debug: send data");

            streamWriter.Write(message);
            streamWriter.Flush();
        }

        public void Connect(IPEndPoint endPoint)
        {
            _tcpClient.Connect(endPoint);

            Console.WriteLine("debug: connected");

            Task.Run(Listen);
        }
    }
}
