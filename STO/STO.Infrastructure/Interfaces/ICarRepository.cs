using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface ICarRepository
{
    Task<List<CarDto>> GetAllAsync();
    Task<CarDto?> GetByIdAsync(int id);
    Task<CarDto> AddAsync(CarDto car);
    Task UpdateAsync(CarDto car);
    void UpdateWithoutTransaction(CarDto car);
    Task<bool> MarkCarAsDeletedAsync(int carId);
    Task<bool> MarkAsDeletedByCustomerIdAsync(int customerId);
    Task<List<CarDto>?> GetAllCarsByCustomerIdAsync(int customerId);
    Task<List<CarDto>> FindByVinAsync(string vinQuery);
    Task<List<CarDto>> FindByVinAndCustomerIdAsync(string vinQuery, int customerId);
    Task HardDeleteAsync(int id);
}