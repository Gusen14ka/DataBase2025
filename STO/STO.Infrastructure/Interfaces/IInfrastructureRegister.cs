using Microsoft.Extensions.DependencyInjection;

namespace STO.Infrastructure.Interfaces;
public interface IInfrastructureRegistrar
{
    void RegisterInfrastructure(IServiceCollection services, string connectionString);
}
