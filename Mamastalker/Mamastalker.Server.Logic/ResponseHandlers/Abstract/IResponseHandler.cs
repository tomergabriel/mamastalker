using System;

namespace Mamastalker.Server.Logic.ResponseHandlers.Abstract
{
    public interface IResponseHandler<TData>
    {
        void HandleData(TData data, Action<string> reply);
    }
}
