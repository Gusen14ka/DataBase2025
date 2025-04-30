using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STO.Service.Interfaces;
using STO.Service.Requests.Order;
using STO.Service.Responses.Order;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    /*
    // ✅ Получить все заказы (GET api/orders)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    // ✅ Получить заказ по ID (GET api/orders/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();
        return order;
    }
    */
    // ✅ Добавить новый заказ (POST api/orders)
    [HttpPost]
    public async Task<ActionResult<ResponseOrderBrief>> PostOrder([FromBody]RequestOrderCreate request)
    {
        var response = await _orderService.AddOrderAsync(request);
        if (response.Id == 0) return StatusCode(500);
        if (!ModelState.IsValid) return BadRequest();
        return Ok(response);
    }

    [HttpPost("calculate-cost")]
    public async Task<ActionResult<decimal>> CalculateOrderCost([FromBody] RequestOrderCost request)
    {
        var cost = await _orderService.CalculateOrderCostAsync(request);
        return Ok(cost);
    }
    /*
    // ✅ Обновить данные заказа (PUT api/order/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if (id != order.Id) return BadRequest();
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    */
}
