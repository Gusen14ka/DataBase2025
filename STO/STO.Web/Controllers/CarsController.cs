using Microsoft.AspNetCore.Mvc;
using STO.Service.Interfaces;
using STO.Service.Requests.Car;
using STO.Service.Responses.Car;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController(ICarService carService) : ControllerBase
{
    private readonly ICarService _carService = carService;

    // ✅ Получить все машины (GET api/cars)
    [HttpGet]
    public async Task<ActionResult<List<ResponseCarDetailed>>> GetCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }
    
    // ✅ Получить машину по ID (GET api/cars/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseCarDetailed>> GetCar( int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }
    
    // ✅ Добавить новую машину (POST api/cars)
    [HttpPost]
    public async Task<ActionResult<ResponseCarBrief>> PostCar([FromBody] RequestCarCreate request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var response = await _carService.AddCarAsync(request);
        if (response.Id == 0) return StatusCode(500);
        return Ok(response);
    }
    /*
    // ✅ Обновить данные машины (PUT api/car/1)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(int id, Car car)
    {
        if (id != car.Id) return BadRequest();
        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    */
    // ✅ Удалить машину (DELETE api/car/1)
    
}