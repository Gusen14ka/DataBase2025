using AutoMapper;
using STO.Service.Responses.OrderedService;
using STO.Service.Requests.OrderedService;
using STO.Core.Models;
using STO.Infrastructure.Dto;

namespace STO.Service.MappingProfiles;

public class OrderedServiceProfiles : Profile
{
    public OrderedServiceProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<RequestOrderedServiceCreate, OrderedService>()
            .ConstructUsing((src, context) => OrderedService.FactoryOrderService(
                id: src.Id,
                orderId: src.OrderId,
                serviceId: src.ServiceId,
                quantity: src.Quantity
            ));

        CreateMap<OrderedService, OrderedServiceDto>();

        CreateMap<OrderedServiceDto, OrderedService>()
            .ConstructUsing((src, context) => OrderedService.FactoryOrderService(
                id: src.Id,
                orderId: src.OrderId,
                serviceId: src.ServiceId,
                quantity: src.Quantity
            ));

        CreateMap<OrderedService, ResponseOrderedServiceBrief>();
    }
}
