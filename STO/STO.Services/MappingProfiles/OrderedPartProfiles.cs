using AutoMapper;
using STO.Service.Responses.OrderedPart;
using STO.Service.Requests.OrderedPart;
using STO.Core.Models;
using STO.Infrastructure.Dto;


namespace STO.Service.MappingProfiles;

public class OrderedPartProfiles : Profile
{
    public OrderedPartProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<RequestOrderedPartCreate, OrderedPart>()
            .ConstructUsing((src, context) => OrderedPart.FactoryOrderPart(
                id: src.Id,
                orderId: src.OrderId,
                partId: src.PartId,
                quantity: src.Quantity
            ));

        CreateMap<OrderedPart, OrderedPartDto>();

        CreateMap<OrderedPartDto, OrderedPart>()
            .ConstructUsing((src, context) => OrderedPart.FactoryOrderPart(
                id: src.Id,
                orderId: src.OrderId,
                partId: src.PartId,
                quantity: src.Quantity
            ));

        CreateMap<OrderedPart, ResponseOrderedPartBrief>();
    }
}