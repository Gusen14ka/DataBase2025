using Microsoft.AspNetCore.Mvc;
using STO.Services;
using STO.DTO.Car;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController(CarService carService) : ControllerBase
{
    private readonly CarService _carService = carService;

    // ✅ Получить все машины (GET api/cars)
    [HttpGet]
    public async Task<ActionResult<List<CarDetailedDto>>> GetCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }
    
    // ✅ Получить машину по ID (GET api/cars/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<CarDetailedDto>> GetCar(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }
    
    // ✅ Добавить новую машину (POST api/cars)
    [HttpPost]
    public async Task<ActionResult<CarMinimalisticDto>> PostCar([FromBody] CarCreateDto сarCreateDto)
    {
        var car = await _carService.AddCarAsync(сarCreateDto);
        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
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

    // ✅ Удалить машину (DELETE api/car/1)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return NotFound();
        car.IsDeleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    */
}