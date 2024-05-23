
using Microsoft.AspNetCore.Mvc;
using QRCodeGenerator.UrlShortener.Entities;

namespace QRCodeGenerator.UrlShortener.Services
{
    public interface IUrlShortenerServices
    {
        Task<IEnumerable<ShoetenerUrlRequest>> GetUrl();
        Task<ShortenedUrl> InputUrl(ShoetenerUrlRequest code);
        Task<ShortenedUrl> GetUrlId(string shortUrl);
    }
}
