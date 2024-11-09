using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WashinApi.Models;

namespace WashinApi.Data
{
    public class WashinContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Machine> Machines { get; set; }

        public WashinContext() { }

        public WashinContext(DbContextOptions<WashinContext> options)
           : base(options)
        { }

        // OnConfiguring définit la chaîne de connexion manuellement
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=WashinApp;User=root;Password=2020Epita!;",
                    new MySqlServerVersion(new Version(8, 0, 39)));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
