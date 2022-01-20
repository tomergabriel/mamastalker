using System.Net;
using System.Threading.Tasks;

namespace Mamastalker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });

            var endPoint = new IPEndPoint(ipAddress, int.Parse(args[0]));

            var bootstrapper = new Bootstrapper();

            var socketServer = bootstrapper.BootstrapSever();

            Task.Run(() => bootstrapper.OnDataHandler.StartUpdateLoop(int.Parse(args[1])));

            socketServer.RunOn(endPoint);
        }
    }
}
