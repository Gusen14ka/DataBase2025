using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class CarRepository(AppDbContext Context) : ICarRepository
{
    private readonly AppDbContext _context = Context;

    // Реализация выдачи списках всех машин
    public async Task<List<Car>> GetAllAsync()
    {
        return await _context.Cars
            .Where(c => !c.IsDeleted) // 🟢 Фильтруем удалённые машины
            .Include(c => c.Model)    // Загружаем связанные модели
            .Include(c => c.Customer) // Загружаем владельца
            .ToListAsync();
    }
    // Реализация выдачи машины по id
    public async Task<Car?> GetByIdAsync(int id)
    {
        return await _context.Cars
            .FirstOrDefaultAsync(car => car.Id == id && !car.IsDeleted);
    }
    // Реализация добавления новой машины
    public async Task AddAsync(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }
}
