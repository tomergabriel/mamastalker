using System.Net;

namespace Mamastalker.Server.Logic.Servers.Abstract
{
    public interface IServer
    {
        void RunOn(IPEndPoint endPoint);
    }
}
