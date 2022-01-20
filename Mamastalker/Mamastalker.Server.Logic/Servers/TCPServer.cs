using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using Mamastalker.Server.Logic.Servers.Abstract;
using System;
using System.IO;
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

        private void Reply(string message, TcpClient tcpClient)
        {
            var streamWriter = new StreamWriter(tcpClient.GetStream());

            Console.WriteLine("debug: send data");

            streamWriter.Write(message);
            streamWriter.Flush();
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

            Console.WriteLine("debug: recieve data");

            return data;
        }

        private void Listen(TcpClient tcpClient)
        {
            while (tcpClient.Connected)
            {
                var data = ListenLoop(tcpClient.GetStream());

                _onDataHandler.HandleData(data, (replyMessage) => Reply(replyMessage, tcpClient));
            }

            Console.WriteLine("debug: client disconnected");

            tcpClient.Close();
        }

        public void RunOn(IPEndPoint endPoint)
        {
            _tcpListener = new TcpListener(endPoint);

            _tcpListener.Start();

            while (true)
            {
                var tcpClient = _tcpListener.AcceptTcpClient();

                Console.WriteLine("debug: new client");

                Task.Run(() => Listen(tcpClient));
            }
        }
    }
}
