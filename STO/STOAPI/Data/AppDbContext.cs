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
            // ��������� ���������� ����� ��� ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasKey(stp => new { stp.ServiceId, stp.PartId }); // ��������� ����

            // ��������� ����� ����� Service � ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasOne(stp => stp.Service) // ServiceToPart ������� � ����� Service
                .WithMany(s => s.ServiceToPart) // Service ����� ���� ������ � ������� ServiceToPart
                .HasForeignKey(stp => stp.ServiceId); // ������� ���� � ServiceToPart

            // ��������� ����� ����� Part � ServiceToPart
            modelBuilder.Entity<ServiceToPart>()
                .HasOne(stp => stp.Part) // ServiceToPart ������� � ����� Part
                .WithMany(p => p.ServiceToPart) // Part ����� ���� ������ � ������� ServiceToPart
                .HasForeignKey(stp => stp.PartId); // ������� ���� � ServiceToPart

            // ��������� ���������� ����� ��� PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasKey(stp => new { stp.PartId, stp.ModelId }); // ��������� ����

            // ��������� ����� ����� Part � PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasOne(stp => stp.Part) // PartToModel ������� � ����� Part
                .WithMany(s => s.PartToModel) // Part ����� ���� ������ � ������� PartToModel
                .HasForeignKey(stp => stp.PartId); // ������� ���� � PartToModel

            // ��������� ����� ����� Model � PartToModel
            modelBuilder.Entity<PartToModel>()
                .HasOne(stp => stp.Model) // PartToModel ������� � ����� Model
                .WithMany(p => p.PartToModel) // Model ����� ���� ������ � ������� PartToModel
                .HasForeignKey(stp => stp.ModelId); // ������� ���� � PartToModel
        }
    }
}