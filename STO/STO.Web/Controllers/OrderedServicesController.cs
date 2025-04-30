using Microsoft.AspNetCore.Mvc;
using STO.Service.Responses.OrderedService;
using STO.Service.Requests.OrderedService;
using STO.Service.Interfaces;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderedServicesController(IOrderedServiceService orderedServiceService) : ControllerBase
{
    private readonly  IOrderedServiceService _orderedServiceService = orderedServiceService;
    /*
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
    */
    
    // ✅ Добавить новую заказанную услугу (POST api/orderServices)
    [HttpPost]
    public async Task<ActionResult<ResponseOrderedServiceBrief>> PostOrderedService(
        [FromBody] RequestOrderedServiceCreate request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var response = await _orderedServiceService.AddOrderedServiceAsync(request);
        if (response.Id == 0) return StatusCode(500);
        return Ok(response);
    }
    /*
    // ✅ Обновить данные заказанной услуги (PUT api/orderService/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderService(int id, OrderService orderService)
    {
        if (id != orderService.Id) return BadRequest();
        _context.Entry(orderService).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    */
}


