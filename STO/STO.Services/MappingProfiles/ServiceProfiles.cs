using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Service.Responses.Service;
using AutoMapper;

namespace STO.Service.MappingProfiles;

public class ServiceProfiles: Profile
{
    public ServiceProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<ServiceDto, Core.Models.Service>()
            .ConstructUsing((dto, context) =>
            {
                ICollection<ServicePartAssociation>? associations = null;
                if (context.TryGetItems(out var items) &&
                    items != null &&
                    items.TryGetValue("ServicePartAssociation", out var value) &&
                    value is ICollection<ServicePartAssociation> typedValue)
                {
                    associations = typedValue;
                }
                return Core.Models.Service.FactoryService(
                    id: dto.Id,
                    name: dto.Name,
                    price: dto.Price,
                    nextVisit: dto.NextVisit,
                    servicePartAssociations: associations
                );
            });

        CreateMap<Core.Models.Service, ResponseServiceDetailed>();
    }
}
