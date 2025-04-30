using Microsoft.AspNetCore.Mvc;
using STO.Service.Interfaces;
using STO.Service.Requests.Customer;
using STO.Service.Responses.Customer;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService; //изменить везде context
    
    // ✅ Получить всех клиентов (GET api/customers)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseCustomerDetailed>>> GetCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }
    
    // ✅ Получить клиента по ID (GET api/customers/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseCustomerDetailed>> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }
    
    // ✅ Добавить нового клиента (POST api/customers)
    [HttpPost]
    public async Task<ActionResult<ResponseCustomerDetailed>> PostCustomer([FromBody] RequestCustomerCreate request)
    {
        var response = await _customerService.AddCustomerAsync(request);
        if (response.Id == 0) return StatusCode(500);
        if (!ModelState.IsValid) return BadRequest();
        return Ok(response);
    }

    /*
    // ✅ Обновить данные клиента (PUT api/customer/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, RequestCustomerCreate customer)
    {

    }
    */
    // ✅ Удалить клиента (DELETE api/customer/1)
}
