using PingPong.Common.Logic.DataConverters.Abstract;

namespace PingPong.Common.Logic.DataConverters.Stringifies.Abstract
{
    public interface IStringify<TData> : IDataConverter<string, TData>
    {
        string Stringify(TData data);
    }
}