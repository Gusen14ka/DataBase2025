using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CarDto> Cars { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<CustomerDto> Customers { get; set; }
        public DbSet<ModelDto> Models { get; set; }
        public DbSet<OrderedPartDto> OrderedParts { get; set; }
        public DbSet<OrderedServiceDto> OrderedServices { get; set; }
        public DbSet<PartDto> Parts { get; set; }
        public DbSet<ServiceDto> Services { get; set; }
        public DbSet<ServicePartAssociationDto> ServicePartAssociations { get; set; }
        public DbSet<PartModelCompatibilityDto> PartModelCompatibilities { get; set; }
        public DbSet<TimetableDto> Timetables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конвертер для TimeSpan?
            var timeSpanConverter = new ValueConverter<TimeSpan?, string>(
                v => v.HasValue ? v.Value.ToString() : null,
                v => string.IsNullOrEmpty(v) ? (TimeSpan?)null : TimeSpan.Parse(v)
            );

            

            // Конфигурация CarDto
            modelBuilder.Entity<CarDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ModelId).IsRequired();
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Vin).IsRequired();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.StartService).IsRequired();
                entity.Property(e => e.EndService).IsRequired(false);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                entity.HasOne<ModelDto>()
                    .WithMany()
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<CustomerDto>()
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация CustomerDto
            modelBuilder.Entity<CustomerDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Конфигурация ModelDto
            modelBuilder.Entity<ModelDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Конфигурация OrderDto
            modelBuilder.Entity<OrderDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.CarId).IsRequired();
                entity.Property(e => e.CreatedTime).IsRequired();
                entity.Property(e => e.Speedometer).IsRequired();
                entity.Property(e => e.IsFinished).HasDefaultValue(false);
                entity.Property(e => e.FinishedTime).IsRequired(false);

                entity.HasOne<CustomerDto>()
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<CarDto>()
                    .WithMany()
                    .HasForeignKey(e => e.CarId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация OrderPartDto
            modelBuilder.Entity<OrderedPartDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderId).IsRequired();
                entity.Property(e => e.PartId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne<OrderDto>()
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<PartDto>()
                    .WithMany()
                    .HasForeignKey(e => e.PartId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Конфигурация OrderServiceDto
            modelBuilder.Entity<OrderedServiceDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderId).IsRequired();
                entity.Property(e => e.ServiceId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne<OrderDto>()
                    .WithMany()
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<ServiceDto>()
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация PartDto
            modelBuilder.Entity<PartDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.IsNew).IsRequired();
            });

            // Конфигурация ServiceDto
            modelBuilder.Entity<ServiceDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("NOCASE");
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.NextVisit).HasConversion(timeSpanConverter).IsRequired(false);
            });

            // Конфигурация связывающей таблицы PartModelCompatibilityDto
            modelBuilder.Entity<PartModelCompatibilityDto>(entity =>
            {
                // Составной ключ: комбинация ServiceId и PartId
                entity.HasKey(e => new { e.PartId, e.ModelId });

                // Настройка внешних ключей
                // Здесь нет навигационных свойств, поэтому используем WithMany() без параметров.
                entity.HasOne<PartDto>()
                    .WithMany()
                    .HasForeignKey(e => e.PartId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<ModelDto>()
                    .WithMany()
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация связывающей таблицы ServicePartAssociationDto
            modelBuilder.Entity<ServicePartAssociationDto>(entity =>
            {
                // Составной ключ: комбинация ServiceId и PartId
                entity.HasKey(e => new { e.ServiceId, e.PartId });

                // Настройка внешних ключей
                // Здесь нет навигационных свойств, поэтому используем WithMany() без параметров.
                entity.HasOne<ServiceDto>()
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<PartDto>()
                    .WithMany()
                    .HasForeignKey(e => e.PartId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Конфигурация связывающей таблицы TimetableDto
            modelBuilder.Entity<TimetableDto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NextVisit).IsRequired();

                // Настройка внешних ключей
                // Здесь нет навигационных свойств, поэтому используем WithMany() без параметров.
                entity.HasOne<CarDto>()
                    .WithMany()
                    .HasForeignKey(e => e.CarId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<ServiceDto>()
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}