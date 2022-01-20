using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System.Text.Json;

namespace Mamastalker.Common.Logic.DataConverters.Stringifies
{
    public class GenericJsonStringify<T> : IStringify<T>
    {
        public T Parse(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        public string Stringify(T data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}