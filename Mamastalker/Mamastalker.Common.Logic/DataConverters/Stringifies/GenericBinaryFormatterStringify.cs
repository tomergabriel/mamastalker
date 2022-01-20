using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mamastalker.Common.Logic.DataConverters.Stringifies
{
    public class GenericBinaryFormatterStringify<T> : IStringify<T>
    {
        private readonly IStringify<byte[]> _byteArrayStringify;

        private readonly BinaryFormatter _binaryFormatter;

        public GenericBinaryFormatterStringify(BinaryFormatter binaryFormatter,
                                               IStringify<byte[]> byteArrayStringify)
        {
            _binaryFormatter = binaryFormatter;
            _byteArrayStringify = byteArrayStringify;
        }

        public T Parse(string data)
        {
            using var memoryStream = new MemoryStream(_byteArrayStringify.Parse(data));

            return (T)_binaryFormatter.Deserialize(memoryStream);
        }

        public string Stringify(T data)
        {
            using var memoryStream = new MemoryStream();

            _binaryFormatter.Serialize(memoryStream, data);

            return _byteArrayStringify.Stringify(memoryStream.ToArray());
        }
    }
}