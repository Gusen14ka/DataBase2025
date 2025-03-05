using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STOAPI.Data;
using STOAPI.Models;

namespace STOAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderPartsController : ControllerBase
{
    private readonly AppDbContext _context;
    public OrderPartsController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Получить все заказанный детали (GET api/orderParts)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderPart>>> GetOrderParts()
    {
        return await _context.OrderParts.ToListAsync();
    }

    // ✅ Получить заказанную деталь по ID (GET api/orderParts/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderPart>> GetOrderPart(int id)
    {
        var orderPart = await _context.OrderParts.FindAsync(id);
        if (orderPart == null) return NotFound();
        return orderPart;
    }

    // ✅ Добавить новую заказанную деталь (POST api/orderParts)
    [HttpPost]
    public async Task<ActionResult<OrderPart>> PostOrderPart(OrderPart orderPart)
    {
        _context.OrderParts.Add(orderPart);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrderPart), new { id = orderPart.Id }, orderPart);
    }

    // ✅ Обновить данные заказанной детали (PUT api/orderPart/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderPart(int id, OrderPart orderPart)
    {
        if (id != orderPart.Id) return BadRequest();
        _context.Entry(orderPart).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

