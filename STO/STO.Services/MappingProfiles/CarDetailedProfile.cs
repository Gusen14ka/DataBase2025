using AutoMapper;
using STO.DTO.Car;
using STO.Core.Models;

namespace STO.Services.MappingProfiles;
public class CarDetailedProfile : Profile
{
    public CarDetailedProfile()
    {
        CreateMap<Car, CarDetailedDto>()
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.Name)) // 🟢 Берём имя модели
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.LastName)); // 🟢 Берём имя клиента
    }
}