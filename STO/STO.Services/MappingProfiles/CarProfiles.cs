using AutoMapper;
using STO.Core.Models;
using STO.Service.Responses.Car;
using STO.Infrastructure.Dto;
using STO.Service.Requests.Car;

namespace STO.Service.MappingProfiles;
public class CarProfiles : Profile
{
    public CarProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Car, ResponseCarBrief>();

        CreateMap<RequestCarCreate, Car>()
            .ConstructUsing((src, context) => Car.FactoryCar(
                modelId: src.ModelId,
                year: src.Year,
                vin: src.Vin,
                customerId: src.CustomerId,
                startService: src.StartService
            ))
            .ForMember(dest => dest.StartService, opt => opt.Ignore());

        CreateMap<Car, ResponseCarDetailed>()
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model == null ? "": src.Model.Name)) // 🟢 Берём имя модели
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer == null ? "" : src.Customer.LastName)) // 🟢 Берём имя клиента
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

        CreateMap<Car, CarDto>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.StartService, opt => opt.MapFrom(src => src.StartService));

        CreateMap<CarDto, Car>()
            .ConstructUsing((dto, context) =>
            {
                Model? modelFromDto = null;
                Customer? customerFromDto = null;
                if (context.TryGetItems(out var items) &&
                    items != null)
                {
                    if (items.TryGetValue("Model", out var modelValue) &&
                        modelValue is Model typedModelValue)
                    {
                        modelFromDto = typedModelValue;
                    }
                    if (items.TryGetValue("Customer", out var customerValue) &&
                        customerValue is Customer typedCustomerValue)
                    {
                        customerFromDto = typedCustomerValue;
                    }
                }
                return Car.FactoryCar(
                    id: dto.Id,
                    modelId: dto.ModelId,
                    model: modelFromDto,
                    year: dto.Year,
                    vin: dto.Vin,
                    isDeleted: dto.IsDeleted,
                    customerId: dto.CustomerId,
                    customer: customerFromDto,
                    startService: dto.StartService,
                    endService: dto.EndService
                );
            });

        CreateMap<Car, ResponseCarWithModelAndBrand>()
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model == null ? "" : src.Model.Name))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model == null ? "" : src.Model.Brand));
    }
}