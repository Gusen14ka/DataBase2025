using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STOAPI.Data;
using STOAPI.Models;

namespace STOAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly AppDbContext _context;
    public CarsController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Получить все машины (GET api/cars)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        return await _context.Cars.Where(c => !c.IsDeleted).ToListAsync();
    }

    // ✅ Получить машину по ID (GET api/cars/1)
    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCar(int id)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
        if (car == null) return NotFound();
        return car;
    }

    // ✅ Добавить новую машину (POST api/cars)
    [HttpPost]
    public async Task<ActionResult<Car>> PostCar(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
    }

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
}