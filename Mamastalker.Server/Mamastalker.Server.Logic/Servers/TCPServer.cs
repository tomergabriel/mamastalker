using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using Mamastalker.Server.Logic.Servers.Abstract;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mamastalker.Server.Logic.Servers
{
    public class TCPServer : IServer
    {
        private TcpListener _tcpListener;

        private readonly IResponseHandler<string> _onDataHandler;

        public TCPServer(IResponseHandler<string> onDataHandler)
        {
            _onDataHandler = onDataHandler;
        }

        ~TCPServer()
        {
            if (!(_tcpListener is null))
            {
                _tcpListener.Stop();
            }
        }

        private void Reply(byte[] message, TcpClient handlerSocket)
        {
            var finalizedMessage = new byte[message.Length + 1];

            message.CopyTo(finalizedMessage, 0);
            finalizedMessage[^1] = 4;

            var networkStream = handlerSocket.GetStream();
            networkStream.Write(finalizedMessage);
        }

        private string ListenLoop(TcpClient tcpClient)
        {
            var data = string.Empty;

            var networkStream = tcpClient.GetStream();

            while (true)
            {
                var bytes = new byte[1024];
                var bytesReceived = networkStream.Read(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                if (data.Contains((char)4))
                {
                    data = data.Substring(0, data.Length - 1);
                    break;
                }
            }

            return data;
        }

        private void Listen(TcpClient tcpClient)
        {
            while (tcpClient.Connected)
            {
                var data = ListenLoop(tcpClient);

                _onDataHandler.HandleData(data, (replyMessage) => Reply(replyMessage, tcpClient));
            }

            tcpClient.Close();
        }

        public void RunOn(IPEndPoint endPoint)
        {
            _tcpListener = new TcpListener(endPoint);

            _tcpListener.Start();

            while (true)
            {
                var tcpClient = _tcpListener.AcceptTcpClient();

                Task.Run(() => Listen(tcpClient));
            }
        }
    }
}
