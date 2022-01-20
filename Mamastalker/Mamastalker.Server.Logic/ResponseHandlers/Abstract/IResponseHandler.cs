using System;

namespace PingPong.Server.Logic.ResponseHandlers.Abstract
{
    public interface IResponseHandler<TData>
    {
        void HandleData(TData data, Action<byte[]> reply);
    }
}
