using STO.Core.Models;

namespace STO.Infrastructure.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task AddAsync(Car car);
    //Task UpdateAsync(Car car);
}