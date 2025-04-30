using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Service.Requests.Order;
using STO.Service.Responses.Order;

namespace STO.Service.MappingProfiles;

public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Order, OrderDto>();

        CreateMap<OrderDto, Order>()
            .ConstructUsing((dto, contex) =>
            {
                Customer? customerFromDto = null;
                Car? carFromDto = null;
                if (contex.TryGetItems(out var items) && items != null)
                {
                    if (items.TryGetValue("Customer", out var customerValue) &&
                        customerValue is Customer typedCustomerValue)
                    {
                        customerFromDto = typedCustomerValue;
                    }
                    if (items.TryGetValue("Car", out var carValue) &&
                        carValue is Car typedCarValue)
                    {
                        carFromDto = typedCarValue;
                    }
                }
                return Order.FactoryOrder(
                    id: dto.Id,
                    customerId: dto.CustomerId,
                    carId: dto.CarId,
                    createdTime: dto.CreatedTime,
                    speedometer: dto.Speedometer,
                    isFinished: dto.IsFinished,
                    finishedTime: dto.FinishedTime,
                    customer: customerFromDto,
                    car: carFromDto
                );
            });

        CreateMap<RequestOrderCreate, Order>()
            .ConstructUsing((src, contex) =>
                Order.FactoryOrder(
                    customerId: src.CustomerId,
                    carId: src.CarId,
                    createdTime: src.CreatedTime,
                    speedometer: src.Speedometer,
                    isFinished: src.IsFinished ?? false
                )
            )
            .ForAllMembers(opt => opt.Ignore());

        CreateMap<Order, ResponseOrderDelailed>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer == null ? "" : src.Customer.LastName))
            .ForMember(dest => dest.CarModelName,
                opt => opt.MapFrom(src => src.Car == null ? "" : src.Car.Model == null ? "" : src.Car.Model.Name));

        CreateMap<Order, ResponseOrderBrief>();
    }
}

