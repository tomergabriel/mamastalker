using System;
using System.Net;
using System.Threading.Tasks;

namespace Mamastalker.Client.Logic.Clients.Abstract
{
    public interface IClient<TData>
    {
        Action<TData> OnReciveDataEvent { get; set; }

        void SendData(TData data);

        void Connect(IPEndPoint endPoint);
    }
}
