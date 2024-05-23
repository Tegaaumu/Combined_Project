//using QRCodeDecoderLibrary;
using ZXing;
using ZXing.Windows.Compatibility;
using QRCodeEncoderLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Components.RenderTree;
using ZXing.Rendering;
using ZXing.Common;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using QRCodeGenerator.Logic;

namespace QRCodeGenerator.Model
{
    public class QrCodeInput
    {
        public string? ImageAddress { get; set; }
        public string? CombinedPart { get; set; }
        public string? Text { get; set; }
        public int Count { get; set; }


        //static string ConvertBitmapToBase64(Bitmap bitmap)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bitmap.Save(stream, ImageFormat.Png);
        //        byte[] imageBytes = stream.ToArray();
        //        return Convert.ToBase64String(imageBytes);
        //    }
        //}
    }
}
