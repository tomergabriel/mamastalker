using System;
using System.Net;
using System.Threading.Tasks;

namespace Mamastalker.Client
{
    class Program
    {
        static void Main()
        {
            // initialize

            var bootstrapper = new Bootstrapper();
            var socketClient = bootstrapper.BootstrapClient();

            Console.Write("Enter server ip address: ");
            var ipInput = Console.ReadLine().Split('.');
            var ipBytes = new byte[4];
            for (int i = 0; i < ipInput.Length; i++)
            {
                ipBytes[i] = byte.Parse(ipInput[i]);
            }

            var ipAddress = new IPAddress(ipBytes);

            Console.Write("Enter server port: ");
            var port = int.Parse(Console.ReadLine());
            var endPoint = new IPEndPoint(ipAddress, port);


            // connect and run
            socketClient.Connect(endPoint);
            socketClient.SendData("get");

            Console.WriteLine("Connected to server, press any key to exit...");
            Console.ReadKey();
        }
    }
}
