using Microsoft.Extensions.DependencyInjection;

namespace STO.Services.Interfaces;

public interface IInfrastructureRegistrar
{
    void RegisterInfrastructure(IServiceCollection services, string connectionString);
}
