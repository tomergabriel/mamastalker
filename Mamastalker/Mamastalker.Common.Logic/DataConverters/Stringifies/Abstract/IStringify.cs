using Mamastalker.Common.Logic.DataConverters.Abstract;

namespace Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract
{
    public interface IStringify<TData> : IDataConverter<string, TData>
    {
        string Stringify(TData data);
    }
}