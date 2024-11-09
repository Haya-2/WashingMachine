using Microsoft.EntityFrameworkCore;
using WashinApi.Models;

namespace WashinApi.Data
{
    public static class SeedData
    {
        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    // Créer un scope pour résoudre des services avec une durée de vie 'scoped'
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<WashinContext>();

        //        // Activer la journalisation des données sensibles (utile pour le débogage)
        //        context.Database.EnsureCreated(); // Assure que la base de données est créée si elle ne l'est pas déjà
        //        context.ChangeTracker.LazyLoadingEnabled = false;
        //        if (!context.Users.Any())
        //        {
        //            // Ajout de 2 utilisateurs
        //            context.Users.AddRange(
        //                new User
        //                {
        //                    Login = "admin",
        //                    Password = "admin123",
        //                    Surname = "Admin",
        //                    Name = "Admin",
        //                    IsManager = true,
        //                    Id_Building = 1
        //                },
        //                new User
        //                {
        //                    Login = "resident1",
        //                    Password = "resident123",
        //                    Surname = "Resident",
        //                    Name = "John",
        //                    IsManager = false,
        //                    Id_Building = 1
        //                },
        //                new User
        //                {
        //                    Login = "resident1",
        //                    Password = "resident123",
        //                    Surname = "Resident",
        //                    Name = "John",
        //                    IsManager = false,
        //                    Id_Building = 1
        //                },
        //                new User
        //                {
        //                    Login = "resident1",
        //                    Password = "resident123",
        //                    Surname = "Resident",
        //                    Name = "John",
        //                    IsManager = false,
        //                    Id_Building = 2
        //                }
        //            );
        //        }
        //        // Ajout de 2 bâtiments
        //        if (!context.Buildings.Any())
        //        {
        //            context.Buildings.AddRange(
        //                new Building
        //                {
        //                    Name = "Building 1",
        //                },
        //                new Building
        //                {
        //                    Name = "Building 2",
        //                }
        //            );
        //        }

        //        // Ajout de 3 machines
        //        if (!context.Machines.Any())
        //        {
        //            context.Machines.AddRange(
        //                new Machine
        //                {
        //                    IsWorking = true,
        //                    userId = null,
        //                    Id_Building = 2 // Associe cette machine au bâtiment 1
        //                },
        //                new Machine
        //                {
        //                    IsWorking = true,
        //                    userId = null,
        //                    Id_Building = 1 // Associe cette machine au bâtiment 1
        //                },
        //                new Machine
        //                {
        //                    IsWorking = true,
        //                    userId = 2,
        //                    Id_Building = 1 // Associe cette machine au bâtiment 2
        //                }
        //            );
        //        }

        //        context.SaveChanges();
        //    }   }
    }
}

