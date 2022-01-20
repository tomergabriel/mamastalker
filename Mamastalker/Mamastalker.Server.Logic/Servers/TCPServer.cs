using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using Mamastalker.Server.Logic.Servers.Abstract;
using System.IO;
using System.Net;
using System.Net.Sockets;
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

        private void Reply(byte[] message, StreamWriter streamWriter)
        {
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        private void Listen(TcpClient tcpClient)
        {
            using var networkStream = tcpClient.GetStream();
            using var streamReader = new StreamReader(networkStream);
            using var streamWriter = new StreamWriter(networkStream);

            while (tcpClient.Connected)
            {
                var data = streamReader.ReadToEnd();

                _onDataHandler.HandleData(data, (replyMessage) => Reply(replyMessage, streamWriter));
            }

            tcpClient.Close();
        }

        public void RunOn(IPEndPoint endPoint)
        {
            _tcpListener = new TcpListener(endPoint);

            _tcpListener.Start();

            while (true)
            {
                using var tcpClient = _tcpListener.AcceptTcpClient();

                Task.Run(() => Listen(tcpClient));
            }
        }
    }
}
