using AutoMapper;
using STO.Core.Models;
using STO.Service.Responses.Customer;
using STO.Infrastructure.Dto;
using STO.Service.Requests.Customer;

namespace STO.Service.MappingProfiles;
public class CustomerProfiles : Profile
{
    public CustomerProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Customer, ResponseCustomerDetailed>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

        CreateMap<RequestCustomerCreate, Customer>()
            .ConstructUsing((src, contex) =>
                Customer.FactoryCustomer(
                    firstName: src.FirstName,
                    lastName: src.LastName,
                    email: src.Email,
                    phoneNumber: src.PhoneNumber
                )
            );

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

        CreateMap<CustomerDto, Customer>()
            .ConstructUsing((dto, context) =>
                Customer.FactoryCustomer(
                    id: dto.Id,
                    firstName: dto.FirstName,
                    lastName: dto.LastName,
                    email: dto.Email,
                    phoneNumber: dto.PhoneNumber,
                    isDeleted: dto.IsDeleted
                )
            );

        CreateMap<Customer, ResponseCustomerBrief>();
    }
}
