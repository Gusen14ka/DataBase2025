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
            // ��������� ���������� ����� ��� ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasKey(stp => new { stp.ServiceId, stp.PartId }); // ��������� ����

            // ��������� ����� ����� Service � ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasOne(stp => stp.Service) // ServicePartAssociation ������� � ����� Service
                .WithMany(s => s.ServicePartAssociation) // Service ����� ���� ������ � ������� ServicePartAssociation
                .HasForeignKey(stp => stp.ServiceId); // ������� ���� � ServicePartAssociation

            // ��������� ����� ����� Part � ServicePartAssociation
            modelBuilder.Entity<ServicePartAssociation>()
                .HasOne(stp => stp.Part) // ServicePartAssociation ������� � ����� Part
                .WithMany(p => p.ServicePartAssociation) // Part ����� ���� ������ � ������� ServicePartAssociation
                .HasForeignKey(stp => stp.PartId); // ������� ���� � ServicePartAssociation

            // ��������� ���������� ����� ��� PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasKey(stp => new { stp.PartId, stp.ModelId }); // ��������� ����

            // ��������� ����� ����� Part � PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasOne(stp => stp.Part) // PartModelCompatibility ������� � ����� Part
                .WithMany(s => s.PartModelCompatibility) // Part ����� ���� ������ � ������� PartModelCompatibility
                .HasForeignKey(stp => stp.PartId); // ������� ���� � PartModelCompatibility

            // ��������� ����� ����� Model � PartModelCompatibility
            modelBuilder.Entity<PartModelCompatibility>()
                .HasOne(stp => stp.Model) // PartModelCompatibility ������� � ����� Model
                .WithMany(p => p.PartModelCompatibility) // Model ����� ���� ������ � ������� PartModelCompatibility
                .HasForeignKey(stp => stp.ModelId); // ������� ���� � PartModelCompatibility
        }
    }
}