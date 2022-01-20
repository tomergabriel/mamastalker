using Mamastalker.Server.Logic.DataConverters.Abstract;
using System.Text;

namespace Mamastalker.Server.Logic.DataConverters
{
    public class StringToByteArrayDataParser : IDataConverter<string, byte[]>
    {
        public byte[] Parse(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }
    }
}
