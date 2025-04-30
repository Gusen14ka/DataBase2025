using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class CarRepository(AppDbContext context): ICarRepository
{
    private readonly AppDbContext _context = context;

    // Реализация выдачи списков всех машин
    public async Task<List<CarDto>> GetAllAsync()
    {
        return await _context.Cars.ToListAsync();
    }
    // Реализация выдачи машины по id
    public async Task<CarDto?> GetByIdAsync(int id)
    {
        return await _context.Cars
            .FirstOrDefaultAsync(car => car.Id == id);
    }
    // Реализация добавления новой машины
    public async Task<CarDto> AddAsync(CarDto car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return car;

    }
    // Реализация обновления данных
    public async Task UpdateAsync(CarDto car)
    {
        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public void UpdateWithoutTransaction(CarDto car)
    {
        _context.Entry(car).State = EntityState.Modified;
    }

    public async Task<bool> MarkCarAsDeletedAsync(int carId)
    {
        var carDto = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        if (carDto == null) { return false; }
        carDto.IsDeleted = true;
        carDto.EndService = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> MarkAsDeletedByCustomerIdAsync(int customerId)
    {
        // Загружаем все машины из списка, EF будет их отслеживать
        var cars = await _context.Cars
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();

        // Помечаем каждую как удалённую
        foreach (var car in cars)
        {
            car.IsDeleted = true;
            car.EndService = DateTime.UtcNow;
        }

        // Одним SaveChanges для всех
        await _context.SaveChangesAsync();
        return true;
    }
    // Реализация выдачи всех неудалённых машин по CustomerId
    public async Task<List<CarDto>?> GetAllCarsByCustomerIdAsync(int customerId)
    {
        var carDtos = await _context.Cars
            .Where(c => c.CustomerId == customerId && !c.IsDeleted)
            .ToListAsync();
        return carDtos.Any() ? carDtos : null;
    }

    public async Task<List<CarDto>> FindByVinAsync(string vinQuery)
    {
        return await _context.Cars
            .Where(c => EF.Functions.Like(c.Vin, $"%{vinQuery}%") && !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<CarDto>> FindByVinAndCustomerIdAsync(string vinQuery, int customerId)
    {
        return await _context.Cars
            .Where(c => EF.Functions.Like(c.Vin, $"%{vinQuery}%") && !c.IsDeleted && c.CustomerId == customerId)
            .ToListAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return;
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }
}
