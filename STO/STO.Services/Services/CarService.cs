using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Dto;
using STO.Service.Requests.Car;
using STO.Service.Responses.Car;
using STO.Service.Interfaces;

namespace STO.Service.Services;

public class CarService: ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IModelRepository _modelRepository;
    private readonly IPartModelCompatibilityRepository _modelCompatibilityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarService(
        ICarRepository carRepository,
        IMapper mapper,
        ICustomerRepository customerRepository,
        IModelRepository modelRepository,
        IPartModelCompatibilityRepository modelCompatibilityRepository,
        IUnitOfWork unitOfWork)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _modelRepository = modelRepository;
        _modelCompatibilityRepository = modelCompatibilityRepository;
        _unitOfWork = unitOfWork;
    }

    private async Task<List<Car>> GetAllCarsIncludeDeletedAsync()
    {
        // Загружаем "сырые" Persistence DTO из репозитория
        var carDtos = await _carRepository.GetAllAsync();

        // Загружаем соответствующие модели и клиентов по ID
        var modelIds = carDtos.Select(c => c.ModelId).Distinct().ToList();
        var customerIds = carDtos.Select(c => c.CustomerId).Distinct().ToList();

        var modelDtos = await _modelRepository.GetByIdsAsync(modelIds);
        var customerDtos = await _customerRepository.GetByIdsAsync(customerIds);

        // Маппим Persistence DTO в бизнес-модели
        var carTasks = carDtos.Select(async dto =>
        {
        var modelDto = modelDtos.FirstOrDefault(m => m.Id == dto.ModelId);
        var partModelCompatibility = await _modelCompatibilityRepository.GetByModelIdAsync(modelDto.Id);
        var model = _mapper.Map<Model>(modelDto, opt => opt.Items["PartModelCompatibility"] = partModelCompatibility);
        var customerDto = customerDtos.FirstOrDefault(c => c.Id == dto.CustomerId);
        var customer = _mapper.Map<Customer>(customerDto);

        return _mapper.Map<Car>(dto, opt =>
        {
            opt.Items["Model"] = model;
            opt.Items["Customer"] = customer;
        });
        }).ToList();

        var cars = await Task.WhenAll(carTasks);
        return cars.ToList();
    }

    public async Task<List<ResponseCarDetailed>> GetAllCarsAsync()
    {
        var allCars = await GetAllCarsIncludeDeletedAsync();
        var responseList = allCars
            .Where(car => !car.IsDeleted)
            .Select(car => _mapper.Map<ResponseCarDetailed>(car))
            .ToList();
        return responseList;
    }

    public async Task<ResponseCarDetailed?> GetCarByIdAsync(int id)
    {
        var carDto = await _carRepository.GetByIdAsync(id);
        var car = _mapper.Map<Car>(carDto);
        return _mapper.Map<ResponseCarDetailed>(car);
    }
    public async Task<ResponseCarWithModelAndBrand?> GetCarWithModelAndBrandByIdAsync(int carId)
    {
        var carDto = await _carRepository.GetByIdAsync(carId);
        if (carDto == null) { return null; }
        var modelId = carDto.ModelId;
        var modelDto = await _modelRepository.GetByIdAsync(modelId);
        if (modelDto == null) { return null; }
        var model = _mapper.Map<Model>(modelDto);
        var car = _mapper.Map<Car>(carDto, opt =>
        {
            opt.Items["Model"] = model;
        });
        return _mapper.Map<ResponseCarWithModelAndBrand>(car);
    }
    public async Task<ResponseCarBrief> AddCarAsync(RequestCarCreate request)
    {
        var car = _mapper.Map<Car>(request);
        var carDto = _mapper.Map<CarDto>(car);
        var addedCarDto = await _carRepository.AddAsync(carDto);
        var addedCar = _mapper.Map<Car>(addedCarDto);
        return _mapper.Map<ResponseCarBrief>(addedCar);
    }
    public async Task<bool> DeRegisterCarAsync(int id)
    {

        return await _carRepository.MarkCarAsDeletedAsync(id);
    }
    public async Task<bool> DeRegisterCarAsyncCarsByCustomerIdAsync(int customerId)
    {
        return await _carRepository.MarkAsDeletedByCustomerIdAsync(customerId);
    }

    public async Task<List<ResponseCarWithModelAndBrand>> SearchByVinAndCustomerIdAsync(string vinQuery, int customerId)
    {
        var carDtos = await _carRepository.FindByVinAndCustomerIdAsync(vinQuery, customerId);
        var modelIds = carDtos.Select(c => c.ModelId).Distinct().ToList();
        var modelDtos = await _modelRepository.GetByIdsAsync(modelIds);
        var cars = carDtos.Select(dto =>
        {
            var modelDto = modelDtos.FirstOrDefault(m => m.Id == dto.ModelId);
            var model = _mapper.Map<Model>(modelDto);
            return _mapper.Map<Car>(dto, opt =>
            {
                opt.Items["Model"] = model;
            });
        }).ToList();
        var response = cars.Select(car => _mapper.Map<ResponseCarWithModelAndBrand>(car));
        return response.ToList();
    }
}
