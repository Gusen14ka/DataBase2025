using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IServiceRepository
{
    Task<ServiceDto> AddAsync(ServiceDto service);
    Task<ServiceDto?> GetByIdAsync(int id);

    Task<List<ServiceDto>> GetAllAsync();
    Task UpdateAsync(ServiceDto service);
    Task HardDeleteAsync(int id);
    Task<List<ServiceDto>> FindByNameAsync(string nameQuery);
}