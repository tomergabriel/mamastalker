using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using System;
using System.Threading.Tasks;

namespace Mamastalker.Common.FormsPresentation.ResponseHandlers
{
    public class ScreenshotOnTimerResponseHandler<TData> : IResponseHandler<TData>
    {
        private Action<byte[]> _listeningCallback;

        private async Task UpdateLoop()
        {
            while (true)
            {
                await Task.Delay(1000);

                SendScreenshot();
            }
        }

        private void SendScreenshot()
        {
            // var byteData;

            // _listeningCallback?.Invoke(byteData);
        }

        public void HandleData(TData data, Action<byte[]> reply)
        {
            _listeningCallback += (byteData) => reply?.Invoke(byteData);
        }
    }
}
