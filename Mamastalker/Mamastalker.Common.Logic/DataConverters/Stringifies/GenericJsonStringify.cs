using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System.Text.Json;

namespace Mamastalker.Common.Logic.DataConverters.Stringifies
{
    public class GenericJsonStringify<T> : IStringify<T>
    {
        public T Parse(string data)
        {
            var parsedData = JsonSerializer.Deserialize<T>(data);

            return parsedData;
        }

        public string Stringify(T data)
        {
            var serialized = JsonSerializer.Serialize(data);

            return serialized;
        }
    }
}