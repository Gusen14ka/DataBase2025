using AutoMapper;
using STO.DTO.Car;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;

namespace STO.Services;

public class CarService(ICarRepository carRepository, IMapper mapper)
{
    private readonly ICarRepository _carRepository = carRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<CarDetailedDto>> GetAllCarsAsync()
    {
        var cars = await _carRepository.GetAllAsync();
        return _mapper.Map<List<CarDetailedDto>>(cars); // 🟢 Конвертируем модели в DTO
    }
    public async Task<CarDetailedDto> GetCarByIdAsync(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        return _mapper.Map<CarDetailedDto>(car);
    }
    public async Task<CarMinimalisticDto> AddCarAsync(CarCreateDto carCreateDto)
    {
        var car = _mapper.Map<Car>(carCreateDto);
        car.StartService = car.StartService == DateTime.MinValue ? DateTime.UtcNow : car.StartService;
        await _carRepository.AddAsync(car);
        return _mapper.Map<CarMinimalisticDto>(car);
    }
}
