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

        private async Task Listen()
        {
            using var streamReader = new StreamReader(_tcpClient.GetStream());

            while (_tcpClient.Connected)
            {
                var recivedData = await streamReader.ReadToEndAsync();

                var parsedRecivedData = _stringify.Parse(recivedData);

                OnReciveDataEvent?.Invoke(parsedRecivedData);
            }
        }

        public async Task SendData(TData data)
        {
            if (_tcpClient is null)
            {
                return;
            }

            var message = _stringify.Stringify(data);

            using var streamWriter = new StreamWriter(_tcpClient.GetStream());

            await streamWriter.WriteLineAsync(message);
            await streamWriter.FlushAsync();
        }

        public async Task Connect(IPEndPoint endPoint)
        {
            _tcpClient.Connect(endPoint);

            await Task.Run(() => Listen());
        }
    }
}
