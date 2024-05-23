using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCodeGenerator.UrlShortener;
using QRCodeGenerator.UrlShortener.Entities;
using QRCodeGenerator.UrlShortener.Services;

namespace QRCodeGenerator.Controllers
{
    public class UrlShortener : Controller
    {
        [Inject]
        public IUrlShortenerServices UrlServices { get; set; }
        //public IEnumerable<Employee> Employees { get; set; } = Enumerable.Empty<Employee>();
        public IEnumerable<ShoetenerUrlRequest>? Urls{ get; set; }

        public UrlShortener(IUrlShortenerServices urlShortenerServices)
        {
            UrlServices = urlShortenerServices;
        }
        public async Task InitializeUrlsAsync()
        {
            if (UrlServices != null)
            {
                Urls = (await UrlServices.GetUrl()).ToList();
            }
            else
            {
                Console.WriteLine("There is no value in your controller");
            }
        }
        // GET: UrlShortener
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // Example usage
            if (UrlServices != null)
            {
                await InitializeUrlsAsync();
                var shortenedUrls = Urls.Select(urlRequest => new ShortenedUrl
                {
                    // Assuming ShoetenerUrlRequest has a property that can be assigned to ShortUrl
                    ShortUrl = urlRequest.Url
                }).ToList();

                return View(shortenedUrls);
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateUrl()
        {
            return View();
        }
        // POST: UrlShortener/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUrl(ShortenedUrl collection)
        {
            if (collection == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                ShoetenerUrlRequest GetSingleUrls = new ShoetenerUrlRequest();
                GetSingleUrls.Url = collection.LongUrl;
                var UrlCollection = await UrlServices.InputUrl(GetSingleUrls);

                //Write a code that will get all the propertys of the particular Url
                //var FinalUrlCollection = await UrlServices.GetUrlId(UrlCollection);

                return View(UrlCollection);
            }
            return View();
        }

    }
}
