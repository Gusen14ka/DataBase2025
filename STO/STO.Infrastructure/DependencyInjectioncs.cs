using Microsoft.Extensions.DependencyInjection;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STO.Infrastructure.Repositories;

namespace STO.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Регистрация DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        // Регистрация репозиториев
        //services.AddScoped<IProductRepository, ProductRepository>();
    }
}
