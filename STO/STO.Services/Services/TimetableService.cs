using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Dto;
using STO.Service.Responses.Timetable;
using STO.Service.Interfaces;

namespace STO.Service.Services;

public class TimetableService: ITimetableService
{
    private readonly IMapper _mapper;
    private readonly ITimetableRepository _timetableRepository;
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IModelRepository _modelRepository;

    public TimetableService(
        IMapper mapper,
        ITimetableRepository timetableRepository,
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        IServiceRepository serviceRepository,
        IModelRepository modelRepository)
    {
        _mapper = mapper;
        _timetableRepository = timetableRepository;
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _serviceRepository = serviceRepository;
        _modelRepository = modelRepository;
    }

    public async Task<List<ResponseReminder>> CreateRemindersByTimeSpan(TimeSpan daysBefore)
    {
        DateTime cutoff = DateTime.Today + daysBefore;
        var timetableDtos = await _timetableRepository.GetAllBeforeDate(cutoff);
        if (timetableDtos.Count == 0)
            return new();

        var timetableTasks = timetableDtos.Select(async dto =>
        {
            var carDto = await _carRepository.GetByIdAsync(dto.CarId);
            if (carDto == null) return null;

            var modelDto = await _modelRepository.GetByIdAsync(carDto.ModelId);
            if (modelDto == null) return null;

            var serviceDto = await _serviceRepository.GetByIdAsync(dto.ServiceId);
            if (serviceDto == null) return null;

            var customerDto = await _customerRepository.GetByIdAsync(carDto.CustomerId);
            if (customerDto == null) return null;

            // создаём доменные сущности
            var model = _mapper.Map<Model>(modelDto);
            var car = _mapper.Map<Car>(carDto, opt => opt.Items["Model"] = model);
            var service = _mapper.Map<Core.Models.Service>(serviceDto);
            var customer = _mapper.Map<Customer>(customerDto);

            var timetable = _mapper.Map<Timetable>(dto, opt =>
            {
                opt.Items["Car"] = car;
                opt.Items["Service"] = service;
            });

            return new ResponseReminder
            {
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                CustomerPhoneNumber = customer.PhoneNumber,
                CustomerEmail = customer.Email,
                CarModel = timetable.Car!.Model!.Name,
                CarBrand = timetable.Car!.Model!.Brand,
                ServiceName = timetable.Service!.Name,
                NextVisit = timetable.NextVisit
            };
        });

        var remindersArray = await Task.WhenAll(timetableTasks);

        // ЕСЛИ ХОТЯ БЫ ОДНО ОБРАЩЕНИЕ ДАЛО NULL => вернём пустой список
        if (remindersArray.Any(r => r == null))
            return new List<ResponseReminder>();

        return remindersArray.ToList()!;
    }

}
