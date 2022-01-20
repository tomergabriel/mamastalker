namespace Mamastalker.Client.Logic.OnDataHandlers.Abstract
{
    public interface IOnDataHandler<TData>
    {
        void OnDataEventHandler(TData data);
    }
}
