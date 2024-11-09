using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Washin.App.Services
{
    internal class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5299/");  
        }

        // GET: api/user/residents/1
        public async Task<string> GetResidentsAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/user/residents/{buildingId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // GET: api/user/residents/login/pwd
        public async Task<string> GetLoginAsync(string login, string pwd)
        {
            var response = await _httpClient.GetAsync($"api/user/residents/{login}/{pwd}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // POST: api/user/addToQueue
        public async Task AddToQueueAsync(int userId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/addToQueue", userId);
            response.EnsureSuccessStatusCode();
        }

        // DELETE: api/user/removeFromQueue/5
        public async Task RemoveFromQueueAsync(int userId)
        {
            var response = await _httpClient.DeleteAsync($"api/user/removeFromQueue/{userId}");
            response.EnsureSuccessStatusCode();
        }

    }
}
