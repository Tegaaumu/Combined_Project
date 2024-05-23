using Microsoft.AspNetCore.Mvc;
using QRCodeGenerator.ControllerShortennerMethod;
using QRCodeGenerator.Logic;
using QRCodeGenerator.Model;
using QRCodeGenerator.ModelEdit;
using QRCodeGenerator.Models;
using System.Diagnostics;

namespace QRCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //To get the file part of your project.
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            QrCodeInput qrCodeInputText = new QrCodeInput();
            qrCodeInputText.ImageAddress = QrInputLogic.InitializeBarcodeBitmap(qrCodeInputText.Text, qrCodeInputText.ImageAddress, _webHostEnvironment.WebRootPath);
            string ImageFilePart = null!;
            ImageAddress.ImageDetails(_webHostEnvironment.WebRootPath, qrCodeInputText.ImageAddress, ref ImageFilePart!);
            @ViewData["Title"] = "Generate barcode Throug Text";
            qrCodeInputText.CombinedPart = ImageFilePart;
            return View(qrCodeInputText);
        }

        [HttpPost]
        public IActionResult Index(QrCodeInput? model)
        {
             if (model != null)
            {
                QrCodeInput qrCodeInputText = new QrCodeInput();
                qrCodeInputText.ImageAddress = QrInputLogic.InitializeBarcodeBitmap(model.Text, "null", _webHostEnvironment.WebRootPath,  qrCodeInputText.Count);
                string ImageFilePart = null!;
                ImageAddress.ImageDetails(_webHostEnvironment.WebRootPath, qrCodeInputText.ImageAddress, ref ImageFilePart);
                @ViewData["Title"] = "Generate barcode Throug Text";
                qrCodeInputText.CombinedPart = ImageFilePart;
                return View(qrCodeInputText);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            QrCodeInputText qrCodeInputText = new QrCodeInputText();
            string imagePart = "C:\\Users\\TegaCode\\source\\repos\\QRCodeGeneratorDemo\\QRCodeGenerator\\wwwroot\\Img\\img.jpg";
            string ImageFilePart = Path.GetRelativePath(_webHostEnvironment.WebRootPath, imagePart);
            ImageFilePart = "\\" +ImageFilePart;

            //string ImageFilePart = Path.Combine(_webHostEnvironment.ContentRootPath, "Img", qrCodeInputText.ImageAddress);
            qrCodeInputText.CombinedPart = ImageFilePart;
            @ViewData["Title"] = "Testing the Image Barcode";
            return View(qrCodeInputText);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
