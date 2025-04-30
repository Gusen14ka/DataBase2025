using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<OrderDto> AddAsync(OrderDto order);
    Task<List<OrderDto>> GetAllAsync();
    Task<OrderDto?> GetByIdAsync(int id);
    Task UpdateAsync(OrderDto order);
    Task HardDeleteAsync(int id);
    Task<OrderDto> AddWithoutTrasactionAsync(OrderDto order);
    Task<List<OrderDto>> FindOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate);
}
