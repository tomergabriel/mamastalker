namespace Mamastalker.Server.Logic.ResponseHandlers.Abstract
{
    public interface IResponseHandler<TData>
    {
        byte[] HandleData(TData data);
    }
}
