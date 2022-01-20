using System;
using System.Net;

namespace Mamastalker.Client.Logic.Clients.Abstract
{
    public interface IClient<TData>
    {
        Action<TData> OnReciveDataEvent { get; set; }

        void SendData(TData data);

        void Connect(IPEndPoint endPoint);
    }
}
