using PingPong.Common.Logic.DataConverters.Abstract;
using PingPong.Server.Logic.ResponseHandlers.Abstract;
using System;

namespace PingPong.Server.Logic.ResponseHandlers
{
    public class SendBackResponseHandler<TData> : IResponseHandler<TData>
    {
        private readonly IDataConverter<TData, byte[]> _dataConverter;

        public SendBackResponseHandler(IDataConverter<TData, byte[]> dataConverter)
        {
            _dataConverter = dataConverter;
        }

        public void HandleData(TData data, Action<byte[]> reply)
        {
            reply?.Invoke(_dataConverter.Parse(data));
        }
    }
}
