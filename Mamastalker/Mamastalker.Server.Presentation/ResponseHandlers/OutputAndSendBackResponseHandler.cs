using Mamastalker.Common.Logic.DataConverters.Abstract;
using Mamastalker.Common.Presentation.Abstract;
using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using System;

namespace Mamastalker.Server.Presentation.ResponseHandlers
{
    public class OutputAndSendBackResponseHandler<TData, TOut> : IResponseHandler<TData>
    {
        private readonly IOutput<TOut> _output;

        private readonly IDataConverter<TData, TOut> _dataToOutDataConverter;

        private readonly IDataConverter<TData, byte[]> _dataToByteArrayDataConverter;

        public OutputAndSendBackResponseHandler(IOutput<TOut> output,
                                                IDataConverter<TData, TOut> dataToOutDataConverter,
                                                IDataConverter<TData, byte[]> dataToByteArrayDataConverter)
        {
            _output = output;
            _dataToOutDataConverter = dataToOutDataConverter;
            _dataToByteArrayDataConverter = dataToByteArrayDataConverter;
        }

        public void HandleData(TData data, Action<byte[]> reply)
        {
            var outData = _dataToOutDataConverter.Parse(data);

            _output.Output(outData);

            var dataBytes = _dataToByteArrayDataConverter.Parse(data);

            reply?.Invoke(dataBytes);
        }
    }
}
