using Mamastalker.Server.Logic.DataParsers.Abstract;
using Mamastalker.Server.Logic.ResponseHandlers.Abstract;

namespace Mamastalker.Server.Logic.ResponseHandlers
{
    public class SendBackResponseHandler<TData> : IResponseHandler<TData>
    {
        private readonly IDataParser<TData, byte[]> _dataParser;

        public SendBackResponseHandler(IDataParser<TData, byte[]> dataParser)
        {
            _dataParser = dataParser;
        }

        public byte[] HandleData(TData data)
        {
            return _dataParser.Parse(data);
        }
    }
}
