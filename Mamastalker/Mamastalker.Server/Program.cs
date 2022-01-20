using System.Net;

namespace Mamastalker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });

            var endPoint = new IPEndPoint(ipAddress, int.Parse(args[0]));

            var bootstrapper = new Bootstrapper();

            var socketServer = bootstrapper.BootstrapSocketSever();

            socketServer.RunOn(endPoint);
        }
    }
}
