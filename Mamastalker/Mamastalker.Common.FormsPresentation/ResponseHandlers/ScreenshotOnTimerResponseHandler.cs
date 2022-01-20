using Mamastalker.Common.Logic.DataConverters.Stringifies.Abstract;
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
        private readonly IStringify<Bitmap> _bitmapStringify;

        private Action<string> _listeningCallback;

        public bool Running { get; set; }

        public ScreenshotOnTimerResponseHandler(IStringify<Bitmap> bitmapStringify)
        {
            _bitmapStringify = bitmapStringify;
            Running = false;
        }

        private async Task UpdateLoop(int refreshInterval)
        {
            while (Running)
            {
                await Task.Delay(refreshInterval);

                SendScreenshot();
            }
        }

        private void SendScreenshot()
        {
            try
            {
                var screen = Screen.PrimaryScreen;

                var bitmap = new Bitmap(screen.Bounds.Width,
                                        screen.Bounds.Height,
                                        PixelFormat.Format32bppArgb);

                var bitmapGraphics = Graphics.FromImage(bitmap);

                bitmapGraphics.CopyFromScreen(screen.Bounds.X,
                                        screen.Bounds.Y,
                                        0,
                                        0,
                                        screen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

                var stringifiedBitmap = _bitmapStringify.Stringify(bitmap);

                Console.WriteLine("debug: took screenshot, sending...");

                _listeningCallback?.Invoke(stringifiedBitmap);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task StartUpdateLoop(int refreshInterval)
        {
            if (Running)
            {
                return;
            }

            Running = true;

            await UpdateLoop(refreshInterval);
        }

        public void HandleData(TData data, Action<string> reply)
        {
            _listeningCallback += (data) => reply?.Invoke(data);
        }
    }
}
