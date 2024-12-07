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
    internal class BuildingApiService
    {
        private readonly HttpClient _httpClient;

        public BuildingApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5299/");
        }

        // GET: api/building/1
        public async Task<string> GetBuildingAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/building/{buildingId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // GET: api/building/1/queue
        public async Task<List<Resident>> GetQueueAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/building/{buildingId}/queue");
            response.EnsureSuccessStatusCode();
            // Désérialiser le contenu JSON en List<User>
            var jsonString = await response.Content.ReadAsStringAsync();
            var queue = JsonSerializer.Deserialize<List<Resident>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });

            return queue;

        }

        // POST: api/building/1/addToQueue
        public async Task AddToQueueAsync(int buildingId, int userId)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/building/{buildingId}/addToQueue", userId);
            response.EnsureSuccessStatusCode();
        }

        // DELETE: api/building/1/removeFromQueue/5
        public async Task RemoveFromQueueAsync(int buildingId, int userId)
        {
            var response = await _httpClient.DeleteAsync($"api/building/{buildingId}/removeFromQueue/{userId}");
            response.EnsureSuccessStatusCode();
        }

        // GET: api/building/1/queuePosition/5
        public async Task<int> GetQueuePositionAsync(int buildingId, int userId)
        {
            var response = await _httpClient.GetAsync($"api/building/{buildingId}/queuePosition/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        // GET: api/building/1/managers
        public async Task<string> GetManagersAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/building/{buildingId}/managers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // GET: api/building/1/residents
        //public async Task<string> GetResidentsAsync(int buildingId)
        //{
        //    var response = await _httpClient.GetAsync($"api/building/{buildingId}/residents");
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsStringAsync();
        //}

        // GET: api/building/1/machines
        public async Task<string> GetMachinesAsync(int buildingId)
        {
            var response = await _httpClient.GetAsync($"api/building/{buildingId}/machines");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
