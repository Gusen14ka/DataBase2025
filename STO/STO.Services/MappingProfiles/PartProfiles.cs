using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Service.Responses.Part;
using AutoMapper;

namespace STO.Service.MappingProfiles;

public class PartProfiles : Profile
{
    public PartProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Part, ResponsePartBrief>();

        CreateMap<PartDto, Part>()
            .ConstructUsing((dto, context) =>
            {
                ICollection<ServicePartAssociation>? associations = null;
                ICollection<PartModelCompatibility>? compatibilities = null;
                if (context.TryGetItems(out var items) &&
                    items != null)
                {
                    if (items.TryGetValue("ServicePartAssociation", out var associationObj) &&
                        associationObj is ICollection<ServicePartAssociation> typedAssociationObj)
                    {
                        associations = typedAssociationObj;
                    }
                    if (items.TryGetValue("PartModelCompatibility", out var compatibilityObj) &&
                        compatibilityObj is ICollection<PartModelCompatibility> typedcompatibilityObj)
                    {
                        compatibilities = typedcompatibilityObj;
                    }
                }
                return Part.FactoryPart(
                    id: dto.Id,
                    name: dto.Name,
                    price: dto.Price,
                    quantity: dto.Quantity,
                    isNew: dto.IsNew,
                    servicePartAssociations: associations,
                    partModelCompatibilities: compatibilities
                );
            });
    }
}
