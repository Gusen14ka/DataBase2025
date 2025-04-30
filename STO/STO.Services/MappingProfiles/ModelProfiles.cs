using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Service.Responses.Model;

namespace STO.Service.MappingProfiles;
public class ModelProfiles : Profile
{
    public ModelProfiles()
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Model, ModelDto>();

        CreateMap<ModelDto, Model>()
            .ConstructUsing((dto, context) =>
            {
                ICollection<PartModelCompatibility>? partCompatibilities = null;
                if (context.TryGetItems(out var items) &&
                    items != null &&
                    items.TryGetValue("PartModelCompatibility", out var value) &&
                    value is ICollection<PartModelCompatibility> typedValue)
                {
                    partCompatibilities = typedValue;
                }

                // Вызов фабричного метода
                return Model.FactoryModel(
                    id: dto.Id,
                    name: dto.Name,
                    brand: dto.Brand,
                    partModelCompatibilities: partCompatibilities ?? null
                );
            });
        CreateMap<Model, ResponseModelBrief>();
    }
}
