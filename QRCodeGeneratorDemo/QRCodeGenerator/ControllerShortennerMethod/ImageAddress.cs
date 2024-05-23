using Microsoft.AspNetCore.Hosting;
using QRCodeGenerator.Model;

namespace QRCodeGenerator.ControllerShortennerMethod
{
    public static class ImageAddress
    {
        public static void ImageDetails(string WebEnv, string ImageAddress, ref string ImageFilePart)
        {
            string imagePart = $"C:\\Users\\TegaCode\\source\\repos\\QRCodeGeneratorDemo\\QRCodeGenerator\\wwwroot\\Img\\{ImageAddress}";

            ImageFilePart = Path.GetRelativePath(WebEnv, imagePart);
        }
    }
}
