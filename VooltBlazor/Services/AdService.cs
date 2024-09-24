using System.Net.Http.Json;
using VooltBlazor.Models;

namespace VooltBlazor.Services
{
    public class AdService : IAdService
    {
        private readonly HttpClient _httpClient;

        public AdService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(Ad ad)
        { 
            var response = await _httpClient.PostAsJsonAsync("api/ad", ad);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Ad>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Ad>>("api/ad");
        }
    }
}
