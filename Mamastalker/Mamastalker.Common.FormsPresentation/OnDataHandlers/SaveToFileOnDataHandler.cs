using Mamastalker.Client.Logic.OnDataHandlers.Abstract;
using Mamastalker.Common.Logic.DataConverters.Abstract;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Mamastalker.Client.Data.OnDataHandlers
{
    public class SaveToFileOnDataHandler<TData> : IOnDataHandler<TData>
    {
        private readonly string _folderName;

        private readonly IDataConverter<TData, Bitmap> _dataToBitmapDataConverter;

        public SaveToFileOnDataHandler(string folderName,
                                       IDataConverter<TData, Bitmap> dataToBitmapDataConverter)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentException($"'{nameof(folderName)}' cannot be null or empty.", nameof(folderName));
            }

            _folderName = folderName;
            _dataToBitmapDataConverter = dataToBitmapDataConverter;
        }

        public void OnDataEventHandler(TData data)
        {
            var bitmap = _dataToBitmapDataConverter.Parse(data);

            var imageCodecInfo = ImageCodecInfo.GetImageEncoders()
                                                 .Where(encoder => encoder.MimeType == "image/jpeg")
                                                 .First();

            var currnetTime = DateTime.Now;
            var encoderParameters = new EncoderParameters(0);
            bitmap.Save($"{Environment.CurrentDirectory}\\{_folderName}\\{currnetTime:yyyy.MM.dd hh_mm_ss_FFF}.jpg", imageCodecInfo, encoderParameters);
        }
    }
}
