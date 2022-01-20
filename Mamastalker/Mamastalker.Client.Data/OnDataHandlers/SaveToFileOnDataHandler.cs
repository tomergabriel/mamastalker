using Mamastalker.Client.Logic.OnDataHandlers.Abstract;

namespace Mamastalker.Client.Data.OnDataHandlers
{
    public class SaveToFileOnDataHandler<TData, TOut> : IOnDataHandler<TData>
    {
        public void OnDataEventHandler(TData data)
        {
            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // Create a Bitmap object based on a BMP file.
            myBitmap = new Bitmap("Shapes.bmp");

            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = Encoder.Quality;

            // Create an EncoderParameters object.

            // An EncoderParameters object has an array of EncoderParameter

            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            myBitmap.Save("Shapes025.jpg", myImageCodecInfo, myEncoderParameters);
        }
    }
}
