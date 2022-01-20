using Mamastalker.Common.Logic.DataConverters.Abstract;
using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using System;

namespace Mamastalker.Server.Logic.ResponseHandlers
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
