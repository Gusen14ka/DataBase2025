using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IOrderedServiceRepository
{
    Task<OrderedServiceDto> AddAsync(OrderedServiceDto orderedService);
    Task<List<OrderedServiceDto>> GetAllAsync();
    Task<OrderedServiceDto?> GetByIdAsync(int id);
    Task UpdateAsync(OrderedServiceDto service);
    Task HardDeleteAsync(int id);
    Task<OrderedServiceDto> AddWithoutTrasactionAsync(OrderedServiceDto orderedService);
    Task<OrderedServiceDto?> GetByOrderIdAsync(int orderId);
}
