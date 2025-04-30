using AutoMapper;
using STO.Infrastructure.Dto;
using STO.Core.Models;

namespace STO.Service.MappingProfiles;
public class CompatibilityProfiles : Profile
{
    public CompatibilityProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<PartModelCompatibilityDto, PartModelCompatibility>()
            .ConstructUsing((dto, context) =>
            {
                Part? partFromDto = null;
                Model? modelFromDto = null;
                if (context.TryGetItems(out var items) &&
                    items != null)
                {
                    if (items.TryGetValue("Part", out var partObj) &&
                        partObj is Part typedPartObj)
                    {
                        partFromDto = typedPartObj;
                    }
                    if (items.TryGetValue("Model", out var modelObj) &&
                        modelObj is Model typedModelObj)
                    {
                        modelFromDto = typedModelObj;
                    }
                }
                return PartModelCompatibility.FactoryPartModelCompatibility(
                    partId: dto.PartId,
                    modelId: dto.ModelId,
                    part: partFromDto,
                    model: modelFromDto
                );
            });

    }
}