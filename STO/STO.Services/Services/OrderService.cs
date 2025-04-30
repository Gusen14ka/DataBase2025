using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Service.Interfaces;
using STO.Service.Requests.Order;
using STO.Service.Responses.Order;
using STO.Infrastructure.Interfaces;
using STO.Service.Responses.Car;
using STO.Service.Responses.OrderedPart;

namespace STO.Service.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IPartRepository _partRepository;
    private readonly IOrderedServiceRepository _orderedServiceRepository;
    private readonly IOrderedPartRepository _orderedPartRepository;
    private readonly ITimetableRepository _tableRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IModelRepository _modelRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper,
        ICustomerRepository customerRepository,
        ICarRepository carRepository,
        IServiceRepository serviceRepository,
        IPartRepository partRepository,
        IOrderedServiceRepository orderedServiceRepository,
        IOrderedPartRepository orderedPartRepository,
        ITimetableRepository timetableRepository,
        IUnitOfWork unitOfWork,
        IModelRepository modelRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _carRepository = carRepository;
        _serviceRepository = serviceRepository;
        _partRepository = partRepository;
        _orderedServiceRepository = orderedServiceRepository;
        _orderedPartRepository = orderedPartRepository;
        _tableRepository = timetableRepository;
        _unitOfWork = unitOfWork;
        _modelRepository = modelRepository;
    }

    public async Task<ResponseOrderBrief> AddOrderAsync(RequestOrderCreate request)
    {
        var order = _mapper.Map<Order>(request);
        var orderDto = _mapper.Map<OrderDto>(order);
        var addedOrderDto = await _orderRepository.AddAsync(orderDto);
        var addedOrder = _mapper.Map<Order>(addedOrderDto);
        return _mapper.Map<ResponseOrderBrief>(addedOrder);
    }

    public async Task<decimal> CalculateOrderCostAsync(RequestOrderCost request)
    {
        var orderedServiceDto = await _serviceRepository.GetByIdAsync(request.ServiceId);
        var orderedService = _mapper.Map<Core.Models.Service>(orderedServiceDto);
        var serviceCost = orderedService?.Price ?? 0;
        var orderedPartDtos = await _partRepository.GetPartsByIdsAsync(request.PartIds);
        var partsCost = 0m;
        if (orderedPartDtos != null && orderedPartDtos.Any())
        {
            var orderedParts = orderedPartDtos
                .Select(dto => _mapper.Map<Part>(dto))
                .ToList();
            partsCost = orderedParts.Sum(p => p.Price);
        }
        return partsCost + serviceCost;
    }

    public async Task<ResponseOrderBrief?> SetOrderAsync(RequestOrderCreate request)
    {
        var order = _mapper.Map<Order>(request);
        Console.WriteLine(order.CreatedTime);
        var orderDto = _mapper.Map<OrderDto>(order);
        Console.WriteLine(orderDto.CreatedTime);
        var addedOrderDto = await _orderRepository.AddAsync(orderDto);
        if (addedOrderDto == null) { return null; }
        Console.WriteLine(addedOrderDto.CreatedTime);
        await _unitOfWork.BeginTransactionAsync();
        var addedOrder = _mapper.Map<Order>(addedOrderDto);
        var orderedServiceDto = new OrderedServiceDto
        {
            OrderId = addedOrder.Id,
            ServiceId = request.ServiceId,
            Quantity = 1
        };
        var addedOrderedServiceDto = await _orderedServiceRepository.AddWithoutTrasactionAsync(orderedServiceDto);
        if (addedOrderedServiceDto == null) { return null; }
        foreach (var item in request.Parts)
        {
            var orderedPartDto = new OrderedPartDto
            {
                OrderId = addedOrder.Id,
                PartId = item.PartId,
                Quantity = item.Quantity
            };
            var addedOrderedPartDto = await _orderedPartRepository.AddWithoutTrasactionAsync(orderedPartDto);
            if (addedOrderedPartDto == null) { return null;}
            await _partRepository.DecrementQuantityAsync(addedOrderedPartDto.PartId, addedOrderedPartDto.Quantity);
        }
        var serviceDto = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (serviceDto == null) { return null; }
        var service = _mapper.Map<Core.Models.Service>(serviceDto);
        if (service.NextVisit != null)
        {
            var timetableDto = new TimetableDto
            {
                ServiceId = request.ServiceId,
                CarId = request.CarId,
                NextVisit = DateTime.UtcNow.Date + service.NextVisit ?? DateTime.UtcNow.Date,
            };
            await _tableRepository.AddWithoutTrasactionAsync(timetableDto);
        }
        

        await _unitOfWork.CommitAsync();
        return _mapper.Map<ResponseOrderBrief>(addedOrder);
    }
    public async Task<List<ResponseFinancialReport>> CreateFinancialReport (DateTime startDate, DateTime endDate)
    {
        var orderDtos = await _orderRepository.FindOrdersBetweenDatesAsync(startDate, endDate);
        if (orderDtos == null) { return new List<ResponseFinancialReport>(); }
        var orders = orderDtos.Select(ord => _mapper.Map<Order>(ord)).ToList();
        var response = new List<ResponseFinancialReport>();
        foreach (Order order in orders)
        {
            var customerDto = await _customerRepository.GetByIdAsync(order.CustomerId);
            if (customerDto == null) { continue; }
            var customer = _mapper.Map<Customer>(customerDto);
            
            var carDto = await _carRepository.GetByIdAsync(order.CarId);
            if (carDto == null) { continue; }
            var modelDto = await _modelRepository.GetByIdAsync(carDto.ModelId);
            var model = _mapper.Map<Model>(modelDto);
            var car = _mapper.Map<Car>(carDto, opt =>
            {
                opt.Items["Model"] = model;
            });
            var carResponse = _mapper.Map<ResponseCarWithModelAndBrand>(car);

            var orderedServiceDto = await _orderedServiceRepository.GetByOrderIdAsync(order.Id);
            if (orderedServiceDto == null) { continue; }
            var orderedService = _mapper.Map<OrderedService>(orderedServiceDto);
            var serviceDto = await _serviceRepository.GetByIdAsync(orderedService.ServiceId);
            if (serviceDto == null) { continue; }
            var service = _mapper.Map<Core.Models.Service>(serviceDto);

            decimal fullPrice = service.Price;
            var partsResponse = new List<ResponseOrderedPartItemForFinancialReport>();
            var orderedPartDtos = await _orderedPartRepository.GetPartsByOrderIdAsync(order.Id);
            
            if (orderedPartDtos.Count != 0)
            {
                foreach (OrderedPartDto orderedPartDto in orderedPartDtos)
                {
                    var orderedPart = _mapper.Map<OrderedPart>(orderedPartDto);
                    var partDto = await _partRepository.GetByIdAsync(orderedPart.PartId);
                    if (partDto == null) { continue; }
                    var part = _mapper.Map<Part>(partDto);
                    var partRespone = new ResponseOrderedPartItemForFinancialReport
                    {
                        PartId = part.Id,
                        PartName = part.Name,
                        PartPrice = part.Price,
                        Quantity = orderedPart.Quantity,
                        IsNew = part.IsNew,
                        PartSumPrice = part.Price * orderedPart.Quantity
                    };
                    partsResponse.Add(partRespone);
                    fullPrice += part.Price * orderedPart.Quantity;
                }
            }
            var orderResponse = new ResponseFinancialReport
            {
                OrderId = order.Id,
                OrderDateTime = order.CreatedTime,
                FullOrderPrice = fullPrice,
                CustomerId = customer.Id,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Car = carResponse,
                ServiceId = service.Id,
                ServiceName = service.Name,
                ServicePrice = service.Price,
                OrderedParts = partsResponse
            };
            response.Add(orderResponse);
        }
        return response;

    }
}