using STO.Infrastructure.Data;
using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class OrderedPartRepository(AppDbContext context) : IOrderedPartRepository
{
    private readonly AppDbContext _context = context;

    public async Task<OrderedPartDto> AddAsync(OrderedPartDto orderedPart)
    {
        await _context.OrderedParts.AddAsync(orderedPart);
        await _context.SaveChangesAsync();
        return orderedPart;
    }

    public async Task<OrderedPartDto> AddWithoutTrasactionAsync(OrderedPartDto orderedPart)
    {
        await _context.OrderedParts.AddAsync(orderedPart);
        return orderedPart;
    }

    public async Task UpdateAsync(OrderedPartDto orderedPart)
    {
        _context.Entry(orderedPart).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderedPartDto>> GetPartsByOrderIdAsync(int orderId)
    {
        return await _context.OrderedParts
            .Where(p => p.OrderId == orderId)
            .ToListAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var part = await _context.OrderedParts.FindAsync(id);
        if (part == null) return;
        _context.OrderedParts.Remove(part);
        await _context.SaveChangesAsync();
    }
    public async Task<List<OrderedPartDto>> GetAllAsync()
    {
        return await _context.OrderedParts.ToListAsync();
    }
    public async Task<OrderedPartDto?> GetByIdAsync(int id)
    {
        return await _context.OrderedParts
            .FirstOrDefaultAsync(part => part.Id == id);
    }
}