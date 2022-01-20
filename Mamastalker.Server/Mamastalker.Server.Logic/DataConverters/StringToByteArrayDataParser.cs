using Mamastalker.Server.Logic.DataConverter.Abstract;
using System.Text;

namespace Mamastalker.Server.Logic.DataConverter
{
    public class StringToByteArrayDataParser : IDataConverter<string, byte[]>
    {
        public byte[] Parse(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }
    }
}
