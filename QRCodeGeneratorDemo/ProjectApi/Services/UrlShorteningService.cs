using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ProjectApi.Services
{
    public class UrlShorteningService
    {
        public const int NumberOfChersIinShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxya0123456789";

        private readonly Random _random = new();
        private readonly ApplicationDbContext _dbContext;
        public UrlShorteningService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string DisplayAttributeShortCode(string code)
        {
            if (code.Contains("https") || code.Contains("http"))
            {
                string decodedUrl = HttpUtility.UrlDecode(code);
                code = decodedUrl.Split("/").Last();
            }
            return code;
        }
        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfChersIinShortLink];

            while (true)
            {
                for (int i = 0; i < NumberOfChersIinShortLink; i++)
                {
                    var randomIndex = _random.Next(Alphabet.Length - 1);

                    codeChars[i] = Alphabet[randomIndex];
                }
                var code = new string(codeChars);

                if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
