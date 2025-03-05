using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STOAPI.Data;
using STOAPI.Models;

namespace STOAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderServicesController : ControllerBase
{
    private readonly AppDbContext _context;
    public OrderServicesController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Получить все заказанные услуги (GET api/orderServices)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderService>>> GetOrderServices()
    {
        return await _context.OrderServices.ToListAsync();
    }

    // ✅ Получить заказанную услугу по ID (GET api/orderServices/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderService>> GetOrderService(int id)
    {
        var orderService = await _context.OrderServices.FindAsync(id);
        if (orderService == null) return NotFound();
        return orderService;
    }

    // ✅ Добавить новую заказанную услугу (POST api/orderServices)
    [HttpPost]
    public async Task<ActionResult<OrderService>> PostOrderService(OrderService orderService)
    {
        _context.OrderServices.Add(orderService);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrderService), new { id = orderService.Id }, orderService);
    }

    // ✅ Обновить данные заказанной услуги (PUT api/orderService/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderService(int id, OrderService orderService)
    {
        if (id != orderService.Id) return BadRequest();
        _context.Entry(orderService).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


