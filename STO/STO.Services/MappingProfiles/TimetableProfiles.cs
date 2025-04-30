using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;

namespace STO.Service.MappingProfiles;

public class TimetableProfiles: Profile
{
    public TimetableProfiles() 
    {
        AllowNullDestinationValues = true;
        AllowNullCollections = true;

        CreateMap<Timetable, TimetableDto>();

        CreateMap<TimetableDto, Timetable>()
            .ConstructUsing((dto, context) =>
            {
                Core.Models.Service? serviceFromDto = null;
                Car? carFromDto = null;
                if (context.TryGetItems(out var items) &&
                items != null)
                {
                    if (items.TryGetValue("Service", out var serviceValue) &&
                    serviceValue is Core.Models.Service typedServiceValue)
                    {
                        serviceFromDto = typedServiceValue;
                    }
                    if (items.TryGetValue("Car", out var CarValue) &&
                    CarValue is Car typedCarValue)
                    {
                       carFromDto = typedCarValue;
                    }
                }
                return Timetable.FactoryTimetable(
                    id: dto.Id,
                    carId: dto.CarId,
                    car: carFromDto,
                    serviceId: dto.ServiceId,
                    service: serviceFromDto,
                    nextVisit: dto.NextVisit
                    );

            });
    }
}
