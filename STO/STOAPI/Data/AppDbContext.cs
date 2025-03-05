using Microsoft.EntityFrameworkCore;
using STOAPI.Models;

namespace STOAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceToPart> ServiceToPart { get; set; }
        public DbSet<PartToModel> PartToModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка составного ключа для ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasKey(stp => new { stp.ServiceId, stp.PartId }); // Составной ключ

            // Настройка связи между Service и ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasOne(stp => stp.Service) // ServiceToPart связана с одним Service
                .WithMany(s => s.ServiceToPart) // Service может быть связан с многими ServiceToPart
                .HasForeignKey(stp => stp.ServiceId); // Внешний ключ в ServiceToPart

            // Настройка связи между Part и ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasOne(stp => stp.Part) // ServiceToPart связана с одним Part
                .WithMany(p => p.ServiceToPart) // Part может быть связан с многими ServiceToPart
                .HasForeignKey(stp => stp.PartId); // Внешний ключ в ServiceToPart

            // Настройка составного ключа для PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasKey(stp => new { stp.PartId, stp.ModelId }); // Составной ключ

            // Настройка связи между Part и PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasOne(stp => stp.Part) // PartToModel связана с одним Part
                .WithMany(s => s.PartToModel) // Part может быть связан с многими PartToModel
                .HasForeignKey(stp => stp.PartId); // Внешний ключ в PartToModel

            // Настройка связи между Model и PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasOne(stp => stp.Model) // PartToModel связана с одним Model
                .WithMany(p => p.PartToModel) // Model может быть связан с многими PartToModel
                .HasForeignKey(stp => stp.ModelId); // Внешний ключ в PartToModel
        }
    }
}