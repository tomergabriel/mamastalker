using Mamastalker.Client.Logic.OnDataHandlers.Abstract;
using Mamastalker.Common.Logic.DataConverters.Abstract;
using Mamastalker.Common.Presentation.Abstract;

namespace Mamastalker.Client.Presentation.OnDataHandlers
{
    public class OutputOnDataHandler<TData, TOut> : IOnDataHandler<TData>
    {
        private readonly IOutput<TOut> _output;

        private readonly IDataConverter<TData, TOut> _dataParser;

        public OutputOnDataHandler(IDataConverter<TData, TOut> dataParser, IOutput<TOut> output)
        {
            _dataParser = dataParser;
            _output = output;
        }

        public void OnDataEventHandler(TData data)
        {
            _output.Output(_dataParser.Parse(data));
        }
    }
}
