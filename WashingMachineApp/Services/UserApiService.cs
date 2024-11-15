using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        public async Task<List<Resident>> GetResidentsAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/User/residents/{buildingId}");
            response.EnsureSuccessStatusCode();

            // Désérialiser le contenu JSON en List<User>
            var jsonString = await response.Content.ReadAsStringAsync();
            var residents = JsonSerializer.Deserialize<List<Resident>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });

            return residents;
        }

        // GET: api/user/residents/login/pwd
        public async Task<Resident?> GetLoginAsync(string login, string pwd)
        {
            var response = await _httpClient.GetAsync($"api/user/residents/{login}/{pwd}");
            response.EnsureSuccessStatusCode();
            
            var userJson = await response.Content.ReadAsStringAsync();
            if (userJson == null)
            {
                //MessageBox.Show("Invalid login or password.");
                return null;
            }
                var user = JsonSerializer.Deserialize<Resident>(userJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return user;
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
