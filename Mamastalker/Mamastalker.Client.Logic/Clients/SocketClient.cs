using PingPong.Client.Logic.Clients.Abstract;
using PingPong.Common.Logic.DataConverters.Stringifies.Abstract;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Client.Logic.Clients
{
    public class SocketClient<TData> : IClient<TData>
    {
        private readonly Socket _socket;

        public Action<TData> OnReciveDataEvent { get; set; }

        private readonly IStringify<TData> _stringify;

        private readonly IStringify<byte[]> _byteArrayStringify;

        public SocketClient(Socket socket,
                            IStringify<TData> stringify,
                            IStringify<byte[]> byteArrayStringify)
        {
            _socket = socket;
            _stringify = stringify;
            _byteArrayStringify = byteArrayStringify;
        }

        ~SocketClient()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        private string ListenLoop()
        {
            var data = string.Empty;

            while (true)
            {
                var bytes = new byte[1024];
                var bytesReceived = _socket.Receive(bytes);
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
            while (_socket.Connected)
            {
                var recivedData = ListenLoop();

                var parsedRecivedData = _stringify.Parse(recivedData);

                OnReciveDataEvent?.Invoke(parsedRecivedData);
            }
        }

        public void SendData(TData data)
        {
            if (_socket is null)
            {
                return;
            }

            var stringifiedData = _stringify.Stringify(data);

            var byteData = _byteArrayStringify.Parse(stringifiedData + (char)4);

            _socket.Send(byteData);
        }

        public void Connect(IPEndPoint endPoint)
        {
            _socket.Connect(endPoint);

            Task.Run(() => Listen());
        }
    }
}
