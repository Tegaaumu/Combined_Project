
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using ZXing.QrCode.Internal;

namespace QRCodeGenerator.Logic
{
    public static class QrInputLogic
    {

        public static string InitializeBarcodeBitmap(string? Text, string? ImageAddress, string webHostEnvironment, int Count= 0)
        {
            Count +=2;
            BarcodeWriter<Bitmap> barcodeWriter = new();

            EncodingOptions encodingOptions = new() { Width = 250, Height = 250, Margin = 0, PureBarcode = false, GS1Format = true};
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrection.L);
            
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = encodingOptions;
            if (Text == null)
            {
                Text = "Let love lead";
            }
            Bitmap bitmap = barcodeWriter.Write(Text);
            Bitmap logo;

            try
            {
                logo = new Bitmap(Path.Combine(webHostEnvironment, "Img", "90s.png"));
            }
            catch (NullReferenceException ex)
            {
                // handle the exception
                Console.WriteLine(ex.Message);
                logo = new Bitmap("default.png");
            }

            Graphics graphics = Graphics.FromImage(bitmap);

            int xPosition = ((bitmap.Width - logo.Width) / 3) * -1;
            int yPosition = ((bitmap.Height - logo.Height) / 3) * -1;

            // Calculate the dimensions of the area where the logo will be placed
            int targetWidth = (Math.Min(logo.Width, bitmap.Width - xPosition) / 2);
            int targetHeight = (Math.Min(logo.Height, bitmap.Height - yPosition) / 2);

            // Create a Rectangle representing the area where the logo will be drawn
            Rectangle targetRect = new Rectangle(xPosition, yPosition, targetWidth, targetHeight);

            // Draw the logo within the calculated area
            graphics.DrawImage(logo, targetRect);

            string james = $"son{Count}.png";
            
            bitmap.Save($"C:\\Users\\TegaCode\\source\\repos\\QRCodeGeneratorDemo\\QRCodeGenerator\\wwwroot\\Img\\{james}", ImageFormat.Png);

            return james;

        }
    }
}
