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

        private async Task Reply(string message, StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync(message);
            await streamWriter.FlushAsync();
        }

        private async Task Listen(TcpClient tcpClient)
        {
            using var networkStream = tcpClient.GetStream();
            using var streamReader = new StreamReader(networkStream);
            using var streamWriter = new StreamWriter(networkStream);

            while (tcpClient.Connected)
            {
                var data = await streamReader.ReadToEndAsync();

                _onDataHandler.HandleData(data, async (replyMessage) => await Reply(replyMessage, streamWriter));
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
