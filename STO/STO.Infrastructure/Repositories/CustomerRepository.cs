using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    private readonly AppDbContext _context = context;

    // Реализация выдачи всех клиентов
    public async Task<List<CustomerDto>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    // Реализация выдачи клиента по id
    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(customer => customer.Id == id && !customer.IsDeleted);
    }

    public async Task<CustomerDto?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(customer => customer.PhoneNumber == phoneNumber);
    }

    public async Task<List<CustomerDto>> FindByPhoneAsync(string phoneQuery)
    {
        // Пример с использованием EF Core: ищем совпадения по вхождению строки
        return await _context.Customers
            .Where(c => EF.Functions.Like(c.PhoneNumber, $"%{phoneQuery}%") && !c.IsDeleted)
            .ToListAsync();
    }
    public async Task UpdateAsync(CustomerDto customer)
    {
        var existingCustomer = _context.Customers.Local
            .FirstOrDefault(c => c.Id == customer.Id);

        if (existingCustomer != null)
        {
            // Если сущность уже отслеживается, обновите её свойства
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
        }
        else
        {
            // Если сущность не отслеживается, добавьте её
            _context.Customers.Update(customer);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CustomerDto>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Customers
            .Where(c => ids.Contains(c.Id)) 
            .ToListAsync();
    }

    public async Task<CustomerDto> AddAsync(CustomerDto customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
    public async Task<bool> MarkCustomerAsDeletedAsync(int customerId)
    {
        var customerDto = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        if (customerDto == null) { return false; }
        customerDto.IsDeleted = true; // или что у тебя там делает MarkAsDeleted
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task HardDeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return;
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

}