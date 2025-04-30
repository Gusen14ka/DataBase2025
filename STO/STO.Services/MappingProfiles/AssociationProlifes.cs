using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;

namespace STO.Service.MappingProfiles;

public class AssociationProfiles : Profile
{
    public AssociationProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<ServicePartAssociationDto, ServicePartAssociation>()
            .ConstructUsing((dto, context) =>
            {
                Part? partFromDto = null;
                Core.Models.Service? serviceFromDto = null;
                if (context.TryGetItems(out var items) &&
                    items != null)
                {
                    if (items.TryGetValue("Part", out var partObj) &&
                        partObj is Part typedPartObj)
                    {
                        partFromDto = typedPartObj;
                    }

                    if (items.TryGetValue("Service", out var serviceObj) &&
                        serviceObj is Core.Models.Service typedServiceObj)
                    {
                        serviceFromDto = typedServiceObj;
                    }
                }

                return ServicePartAssociation.FactoryServicePartAssociation(
                    serviceId: dto.ServiceId,
                    partId: dto.PartId,
                    service: serviceFromDto,
                    part: partFromDto
                );
            });
    }
}
