using Microsoft.EntityFrameworkCore;
using STO.Infrastructure.Data;
using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;

namespace STO.Infrastructure.Repositories;

public class OrderedServiceRepository(AppDbContext context) : IOrderedServiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<OrderedServiceDto> AddAsync(OrderedServiceDto orderedService)
    {
        await _context.OrderedServices.AddAsync(orderedService);
        await _context.SaveChangesAsync();
        return orderedService;
    }
    public async Task<OrderedServiceDto> AddWithoutTrasactionAsync(OrderedServiceDto orderedService)
    {
        await _context.OrderedServices.AddAsync(orderedService);
        return orderedService;
    }
    public async Task<OrderedServiceDto?> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderedServices
            .FirstOrDefaultAsync(serv => serv.OrderId == orderId);
    }
    public async Task<List<OrderedServiceDto>> GetAllAsync()
    {
        return await _context.OrderedServices.ToListAsync();
    }
    public async Task<OrderedServiceDto?> GetByIdAsync(int id)
    {
        return await _context.OrderedServices
            .FirstOrDefaultAsync(service => service.Id == id);
    }
    public async Task UpdateAsync(OrderedServiceDto service)
    {
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var service = await _context.OrderedServices.FindAsync(id);
        if (service == null) return;
        _context.OrderedServices.Remove(service);
        await _context.SaveChangesAsync();
    }
}
