using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STO.Infrastructure.Interfaces; // Интерфейсы репозиториев
using STO.Infrastructure.Extensions; // Вызовем метод регистрации DbContext из STO.Infrastructure
using STO.Infrastructure.Repositories;
using STO.Service.MappingProfiles; 
using STO.Service.Services;
using STO.Service.Interfaces;

namespace STO.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Добавляем инфраструктурные сервисы (DbContext, репозитории)
            services.AddInfrastructureServices(configuration);

            // Регистрируем репозитории через их интерфейсы
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IOrderedPartRepository, OrderedPartRepository>();
            services.AddScoped<IOrderedServiceRepository, OrderedServiceRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPartModelCompatibilityRepository, PartModelCompatibilityRepository>();
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IServicePartAssociationRepository, ServicePartAssociationRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ITimetableRepository, TimetableRepository>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            // Регистрируем бизнес-логику (сервисы) также через их интерфейсы
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IOrderedPartService, OrderedPartService>();
            services.AddScoped<IOrderedServiceService, OrderedServiceService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ITimetableService, TimetableService>();

            /*// Регистрируем параметры для связанных сервисов
            services.AddScoped<CarServiceParams>(sp =>
                new CarServiceParams(
                    sp.GetRequiredService<IModelService>(),
                    sp.GetRequiredService<ICustomerService>()
                ));
            */

            // Регистрируем профили маппинга
            services.AddAutoMapper(typeof(CarProfiles));

            return services;
        }
    }
}