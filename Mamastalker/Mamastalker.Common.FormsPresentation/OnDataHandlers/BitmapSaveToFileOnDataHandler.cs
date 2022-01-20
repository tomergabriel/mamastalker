using Mamastalker.Client.Logic.OnDataHandlers.Abstract;
using Mamastalker.Common.Logic.DataConverters.Abstract;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Mamastalker.Client.Data.OnDataHandlers
{
    public class BitmapSaveToFileOnDataHandler<TData> : IOnDataHandler<TData>
    {
        private readonly IDataConverter<TData, Bitmap> _dataToBitmapDataConverter;

        public BitmapSaveToFileOnDataHandler(IDataConverter<TData, Bitmap> dataToBitmapDataConverter)
        {
            _dataToBitmapDataConverter = dataToBitmapDataConverter;
        }

        public void OnDataEventHandler(TData data)
        {
            try
            {
                var bitmap = _dataToBitmapDataConverter.Parse(data);

                var currnetTime = DateTime.Now;
                var fileName = $"screenshot {currnetTime:yyyy.MM.dd hh_mm_ss_FFF}.jpg";

                bitmap.Save(fileName, ImageFormat.Jpeg);

                Console.WriteLine("debug: saved screenshot!");
            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
