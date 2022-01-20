using PingPong.Common.Logic.DataConverters.Abstract;
using PingPong.Common.Presentation.Abstract;
using PingPong.Server.Logic.ResponseHandlers.Abstract;
using System;

namespace PingPong.Server.Presentation.ResponseHandlers
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
