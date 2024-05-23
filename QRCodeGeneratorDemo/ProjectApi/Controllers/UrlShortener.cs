using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Entities;
using ProjectApi.Models;
using ProjectApi.Services;
using System;
using System.Net.Http;
using System.Web;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortener : ControllerBase
    {
        private readonly UrlShorteningService _urlShorteningService;
        private readonly ApplicationDbContext? _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlShortener(UrlShorteningService urlShorteningService, ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _urlShorteningService = urlShorteningService;
            _dbContext = dbContext;

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [HttpGet]
        // GET: UrlShortener
        public async Task<IActionResult> Get()
        {
            var items = _dbContext!.ShortenedUrls.Select(s => new ShoetenerUrlRequest
            {
                Url = s.ShortUrl
            }).ToList();
            if (items.Any() == false || items.Count == 0)
            {
                return Ok(items);
            }


            return Ok(items);

        }


        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] ShoetenerUrlRequest request)
        {
            var _httpContext = _httpContextAccessor.HttpContext;
            if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
            {
                return BadRequest("The specified URL is invalid.");
            }
            var code = await _urlShorteningService.GenerateUniqueCode();
            string controllerName = this.ControllerContext.ActionDescriptor.ControllerName;
            var shortenedUrl = new ShortenedUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = request.Url,
                Code = code,
                ShortUrl = $"{_httpContext!.Request.Scheme}://{_httpContext.Request.Host}/api/{controllerName}/{code}",
                CreatedOnUtc = DateTime.Now
            };

            _dbContext!.ShortenedUrls.Add(shortenedUrl);

            await _dbContext.SaveChangesAsync();

            return Ok(shortenedUrl.ShortUrl);

        }

        [HttpGet("GetUrlId/{code}")]
        //public async Task<IActionResult> ShortenUrl(string code, ApplicationDbContext applicationDbContext)
        public async Task<ActionResult<ShortenedUrl>> Mark(string code)
        {
            code = _urlShorteningService.DisplayAttributeShortCode(code);
            var shortenedUrl = _dbContext.ShortenedUrls.FirstOrDefault(s => s.Code == code);
            if (shortenedUrl == null)
            {
                return NotFound();
            }
            return Ok(shortenedUrl);

        }

        //Check Why is it that some of the code will redirct while others will not
        [HttpGet("{code}")]
        //public async Task<IActionResult> ShortenUrl(string code, ApplicationDbContext applicationDbContext)
        public async Task<ActionResult<ShortenedUrl>> ShortenUrl(string code)
        {
            code = _urlShorteningService.DisplayAttributeShortCode(code);
            var shortenedUrl = _dbContext.ShortenedUrls.FirstOrDefault(s => s.Code == code);
            if (shortenedUrl == null)
            {
                return NotFound();
            }
            return Redirect(shortenedUrl.LongUrl);

        }
    }
}
