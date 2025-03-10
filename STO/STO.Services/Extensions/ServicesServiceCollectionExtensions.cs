using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STO.Infrastructure.Interfaces; // Интерфейсы репозиториев
using STO.Infrastructure.Extensions; // Вызовем метод регистрации DbContext из STO.Infrastructure
using STO.Infrastructure.Repositories; // Репозитории
using STO.Services.MappingProfiles; // Профили маппинга

namespace STO.Services.Extensions
{
    public static class ServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Добавляем инфраструктурные сервисы (DbContext, репозитории)
            services.AddInfrastructureServices(configuration);

            // Регистрируем репозитории через их интерфейсы
            services.AddScoped<ICarRepository, CarRepository>();

            // Регистрируем бизнес-логику (сервисы)
            services.AddScoped<CarService>();

            // Регистрируем профили маппинга
            services.AddAutoMapper(typeof(CarDetailedProfile));

            return services;
        }
    }
}