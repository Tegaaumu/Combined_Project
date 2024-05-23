using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCodeGenerator.UrlShortener.Entities;
using QRCodeGenerator.UrlShortener.Services;

namespace QRCodeGenerator.UrlShortener.Services
{
    public class UrlShortenerServices : IUrlShortenerServices
    {
        //Note if the code do not work it will be due to the HttpContext.
        private readonly HttpClient httpClient;

        public UrlShortenerServices (HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ShortenedUrl> InputUrl(ShoetenerUrlRequest code)
        {
            try
            {
                if (httpClient == null)
                {
                    throw new InvalidOperationException("httpClient is not initialized.");
                }

                var response = await httpClient.PostAsJsonAsync<ShoetenerUrlRequest>("api/UrlShortener", code);
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to get the ShortenedUrl object
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var Code = responseContent.Split("/").Last();

                    return await httpClient.GetFromJsonAsync<ShortenedUrl>($"api/UrlShortener/GetUrlId/{Code}");
                    //var itemFromApi = JsonConvert.DeserializeObject<ShortenedUrl>(responseContent);
                    //return responseContent;
                }
                return null!;

                //// Ensure the response is successful before deserializing
                //response.EnsureSuccessStatusCode();

                //// Deserialize the response content to an Employee object and return it
                //return await response.Content.ReadFromJsonAsync<ShoetenerUrlRequest>();
            }
            catch (HttpRequestException ex)
            {
                // Handle exception (log, display an error message, etc.)
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception or handle it as needed
            }
        }

        public async Task<IEnumerable<ShoetenerUrlRequest>> GetUrl()
        {
            return await httpClient.GetFromJsonAsync<ShoetenerUrlRequest[]>("api/UrlShortener");
        }

        public async Task<ShortenedUrl> GetUrlId(string code)
        {
            //This play is not working.
            try
            {
                return await httpClient.GetFromJsonAsync<ShortenedUrl>($"api/UrlShortener/GetUrlId/{code}");
            }
            catch (HttpRequestException ex)
            {
                // Handle exception (log, display an error message, etc.)
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception or handle it as needed
            }
        }
    }
}
