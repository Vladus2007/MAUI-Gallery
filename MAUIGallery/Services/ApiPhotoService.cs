using System.Text.Json;
using Gallery.Models;
using Gallery.Services.Interfaces;
using Microsoft.Extensions.Configuration;
namespace Gallery.Services
{
    public class ApiPhotoService : IPhotoService
    {
      
        
        private const string BaseUrl = "https://api.unsplash.com";

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiPhotoService(IConfiguration config)
        {
            var apiKey = config["UnsplashApiKey"];

            _httpClient = new HttpClient();


            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {apiKey}");
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Photo>> GetPhotosAsync(int page, int perPage = 30)
        {
            
            var url = $"{BaseUrl}/photos?page={page}&per_page={perPage}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                   
                    var photos = JsonSerializer.Deserialize<List<Photo>>(content, _serializerOptions);
                    return photos ?? new List<Photo>(); 
                }
                else
                {
                    
                    throw new HttpRequestException($"Failed to fetch photos. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in {nameof(GetPhotosAsync)}: {ex.Message}");
                throw; 
            }
        }
    }
}