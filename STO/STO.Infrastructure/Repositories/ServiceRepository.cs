using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class ServiceRepository(AppDbContext context) : IServiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<ServiceDto>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        return await _context.Services
            .FirstOrDefaultAsync(service => service.Id == id);
    }

    public async Task<List<ServiceDto>> FindByNameAsync(string nameQuery)
    {
        return await _context.Services
            .Where(s => EF.Functions.Like(s.Name, $"%{nameQuery}%".ToLower()))
            .ToListAsync();
    }
    public async Task<ServiceDto> AddAsync(ServiceDto service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return service;

    }
    public async Task UpdateAsync(ServiceDto service)
    {
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return;
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }
}