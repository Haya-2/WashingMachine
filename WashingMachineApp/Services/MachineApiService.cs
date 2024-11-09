using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Washin.App.Services
{
    internal class MachineApiService
    {
        private readonly HttpClient _httpClient;

        public MachineApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5299/");
        }

        // GET: api/machine/5
        public async Task<string> GetMachineAsync(int machineId)
        {
            var response = await _httpClient.GetAsync($"api/machine/{machineId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // PUT: api/machine/5/updateStatus
        public async Task UpdateMachineStatusAsync(int machineId, int? userId)
        {
            var jsonData = new { userId };
            var response = await _httpClient.PutAsJsonAsync($"api/machine/{machineId}/updateStatus", jsonData);
            response.EnsureSuccessStatusCode();
        }
    }
}
