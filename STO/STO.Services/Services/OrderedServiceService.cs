using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Dto;
using STO.Service.Requests.OrderedService;
using STO.Service.Responses.OrderedService;
using STO.Service.Interfaces;

namespace STO.Service.Services;

public class OrderedServiceService : IOrderedServiceService
{
    private readonly IOrderedServiceRepository _orderedServiceRepository;
    private readonly IMapper _mapper;

    public OrderedServiceService(
        IOrderedServiceRepository orderedServiceRepository,
        IMapper mapper)
    {
        _orderedServiceRepository = orderedServiceRepository;
        _mapper = mapper;
    }

    public async Task<ResponseOrderedServiceBrief> AddOrderedServiceAsync(RequestOrderedServiceCreate request)
    {
        var orderedService = _mapper.Map<OrderedService>(request);
        var orderedServiceDto = _mapper.Map<OrderedServiceDto>(orderedService);
        var addedOrderedServiceDto = await _orderedServiceRepository.AddAsync(orderedServiceDto);
        var addedOrderedService = _mapper.Map<OrderedService>(addedOrderedServiceDto);
        return _mapper.Map<ResponseOrderedServiceBrief>(addedOrderedService);
    }
}