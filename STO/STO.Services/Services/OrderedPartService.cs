using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Dto;
using STO.Service.Responses.OrderedPart;
using STO.Service.Requests.OrderedPart;
using STO.Service.Interfaces;


namespace STO.Service.Services;

public class OrderedPartService : IOrderedPartService
{
    private readonly IMapper _mapper;
    private readonly IOrderedPartRepository _orderedPartRepository;

    public OrderedPartService(
        IMapper mapper,
        IOrderedPartRepository orderedPartRepository)
    {
        _mapper = mapper;
        _orderedPartRepository = orderedPartRepository;
    }

    public async Task<ResponseOrderedPartBrief> AddOrderedPartAsync(RequestOrderedPartCreate request)
    {
        var orderedPart = _mapper.Map<OrderedPart>(request);
        var orderedPartDto = _mapper.Map<OrderedPartDto>(orderedPart);
        var addedOrderedPartDto = await _orderedPartRepository.AddAsync(orderedPartDto);
        var addedOrderedPart = _mapper.Map<OrderedPart>(addedOrderedPartDto);
        return _mapper.Map<ResponseOrderedPartBrief>(addedOrderedPart);
    }
}
