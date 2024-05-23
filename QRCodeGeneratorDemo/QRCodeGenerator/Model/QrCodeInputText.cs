//using QRCodeDecoderLibrary;
using ZXing;
using ZXing.Common;
using QRCodeEncoderLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using System.Drawing.Imaging;

namespace QRCodeGenerator.Model
{
    public class QrCodeInputText
    {
        public string? ImageAddress { get; set; }
        public string? Text { get; set; }
        public string? CombinedPart { get; set; }
        public QrCodeInputText()
        {
            initialize();
        }

        public QRCodeEncoder _qRCodeEncoder { get; set; } = new QRCodeEncoder();

        public void initialize()
        {
            Bitmap img = _qRCodeEncoder.Encode("http://www.contenticore.com");
            //img.Save("C:\\Users\\TegaCode\\source\\repos\\QRCodeGeneratorDemo\\QRCodeGenerator\\wwwroot\\Img\\img.jpg", ImageFormat.Jpeg);
            img.Save("C:\\Users\\TegaCode\\source\\repos\\QRCodeGeneratorDemo\\QRCodeGenerator\\wwwroot\\Img\\img.jpg", ImageFormat.Jpeg);

            ImageAddress = "img.jpg";
        }


    }
}
