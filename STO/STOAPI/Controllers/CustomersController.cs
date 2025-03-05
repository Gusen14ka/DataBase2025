using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STOAPI.Data;
using STOAPI.Models;

namespace STOAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;
    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Получить всех клиентов (GET api/customers)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.Where(c => !c.IsDeleted).ToListAsync();
    }

    // ✅ Получить клиента по ID (GET api/customers/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null) return NotFound();
        return customer;
    }

    // ✅ Добавить нового клиента (POST api/customers)
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    // ✅ Обновить данные клиента (PUT api/customer/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.Id) return BadRequest();
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ✅ Удалить клиента (DELETE api/customer/1)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();
        customer.IsDeleted = true;

        var cars = await _context.Cars.Where(car => car.CustomerId == id).ToListAsync();
        foreach(var car in cars){
            car.IsDeleted = true;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}
