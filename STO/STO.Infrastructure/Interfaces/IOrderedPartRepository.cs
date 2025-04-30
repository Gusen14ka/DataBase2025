using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IOrderedPartRepository
{
    Task<OrderedPartDto> AddAsync(OrderedPartDto orderedPart);
    Task<OrderedPartDto> AddWithoutTrasactionAsync(OrderedPartDto orderedPart);
    Task UpdateAsync(OrderedPartDto orderedPart);
    Task HardDeleteAsync(int id);
    Task<List<OrderedPartDto>> GetAllAsync();
    Task<OrderedPartDto?> GetByIdAsync(int id);
    Task<List<OrderedPartDto>> GetPartsByOrderIdAsync(int orderId);
}
