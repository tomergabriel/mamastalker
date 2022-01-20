using Mamastalker.Server.Logic.DataParsers.Abstract;
using System.Text;

namespace Mamastalker.Server.Logic.DataParsers
{
    public class StringToByteArrayDataParser : IDataParser<string, byte[]>
    {
        public byte[] Parse(string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }
    }
}
