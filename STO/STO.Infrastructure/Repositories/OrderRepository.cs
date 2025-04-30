using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context): IOrderRepository
{
    private readonly AppDbContext _context = context;

    // Реализация добавление нового заказа
    public async Task<OrderDto> AddAsync(OrderDto order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<OrderDto> AddWithoutTrasactionAsync(OrderDto order)
    {
        await _context.Orders.AddAsync(order);
        return order;
    }

    public async Task<List<OrderDto>> FindOrdersBetweenDatesAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Orders
            .Where(order => order.CreatedTime.Date >= startDate && order.CreatedTime.Date <= endDate)
            .ToListAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return;
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(OrderDto order)
    {
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task<List<OrderDto>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }
    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(order => order.Id == id);
    }
}