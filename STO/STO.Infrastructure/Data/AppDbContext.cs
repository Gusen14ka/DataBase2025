using Microsoft.EntityFrameworkCore;
using STO.Core.Models;

namespace STO.Infrastructure.Data
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
        public DbSet<ServicePartAssociation> ServicePartAssociation { get; set; }
        public DbSet<PartModelCompatibility> PartModelCompatibility { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка составного ключа для ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasKey(stp => new { stp.ServiceId, stp.PartId }); // Составной ключ

            // Настройка связи между Service и ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasOne(stp => stp.Service) // ServicePartAssociation связана с одним Service
                .WithMany(s => s.ServicePartAssociation) // Service может быть связан с многими ServicePartAssociation
                .HasForeignKey(stp => stp.ServiceId); // Внешний ключ в ServicePartAssociation

            // Настройка связи между Part и ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasOne(stp => stp.Part) // ServicePartAssociation связана с одним Part
                .WithMany(p => p.ServicePartAssociation) // Part может быть связан с многими ServicePartAssociation
                .HasForeignKey(stp => stp.PartId); // Внешний ключ в ServicePartAssociation

            // Настройка составного ключа для PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasKey(stp => new { stp.PartId, stp.ModelId }); // Составной ключ

            // Настройка связи между Part и PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasOne(stp => stp.Part) // PartModelCompatibility связана с одним Part
                .WithMany(s => s.PartModelCompatibility) // Part может быть связан с многими PartModelCompatibility
                .HasForeignKey(stp => stp.PartId); // Внешний ключ в PartModelCompatibility

            // Настройка связи между Model и PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasOne(stp => stp.Model) // PartModelCompatibility связана с одним Model
                .WithMany(p => p.PartModelCompatibility) // Model может быть связан с многими PartModelCompatibility
                .HasForeignKey(stp => stp.ModelId); // Внешний ключ в PartModelCompatibility
        }
    }
}