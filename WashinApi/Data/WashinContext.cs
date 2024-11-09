using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using WashinApi.Models;

namespace WashinApi.Data
{
    public class WashinContext : DbContext
    {
        public List<Building> Buildings { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<Machine> Machines { get; set; } = new();

        public void JsonDbContext()
        {
            LoadData();
        }

        public void LoadData()
        {
            // Charger les bâtiments depuis le fichier JSON
            var buildingsData = File.ReadAllText("Data/buildings.json");
            Buildings = System.Text.Json.JsonSerializer.Deserialize<List<Building>>(buildingsData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignorer la casse
            }) ?? new List<Building>();

            // Charger les utilisateurs depuis le fichier JSON
            var usersData = File.ReadAllText("Data/users.json");
            Users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignorer la casse
            }) ?? new List<User>();

            // Charger les machines depuis le fichier JSON
            var machinesData = File.ReadAllText("Data/machines.json");
            Machines = System.Text.Json.JsonSerializer.Deserialize<List<Machine>>(machinesData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignorer la casse
            }) ?? new List<Machine>();
        }

        public void SaveData()
        {
            File.WriteAllText("Data/buildings.json", System.Text.Json.JsonSerializer.Serialize(Buildings));
            File.WriteAllText("Data/users.json", System.Text.Json.JsonSerializer.Serialize(Users));
            File.WriteAllText("Data/machines.json", System.Text.Json.JsonSerializer.Serialize(Machines));
        }
    }

}