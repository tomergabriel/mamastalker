using PingPong.Server.Logic.ResponseHandlers.Abstract;
using PingPong.Server.Logic.Servers.Abstract;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Server.Logic.Servers
{
    public class SocketServer : IServer
    {
        private readonly Socket _socket;

        private readonly IResponseHandler<string> _onDataHandler;

        public SocketServer(IResponseHandler<string> onDataHandler, Socket socket)
        {
            _socket = socket;
            _onDataHandler = onDataHandler;
        }

        ~SocketServer()
        {
            if (!(_socket is null))
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }

        private void Reply(byte[] message, Socket handlerSocket)
        {
            var finalizedMessage = new byte[message.Length + 1];

            message.CopyTo(finalizedMessage, 0);
            finalizedMessage[^1] = 4;

            handlerSocket.Send(finalizedMessage);
        }

        private string ListenLoop(Socket handlerSocket)
        {
            var data = string.Empty;

            while (true)
            {
                var bytes = new byte[1024];
                var bytesReceived = handlerSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                if (data.Contains((char)4))
                {
                    data = data[0..^1];
                    break;
                }
            }

            return data;
        }

        private void Listen(Socket handlerSocket)
        {
            while (handlerSocket.Connected)
            {
                var data = ListenLoop(handlerSocket);

                _onDataHandler.HandleData(data, (replyMessage) => Reply(replyMessage, handlerSocket));
            }

            handlerSocket.Shutdown(SocketShutdown.Both);
            handlerSocket.Close();
        }

        public void RunOn(IPEndPoint endPoint)
        {
            _socket.Bind(endPoint);

            _socket.Listen(100);

            while (_socket.IsBound)
            {
                var handlerSocket = _socket.Accept();

                Task.Run(() => Listen(handlerSocket));
            }
        }
    }
}
