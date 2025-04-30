using System.Reflection;

namespace STO.Core.Models;

public class ServicePartAssociation
{
	public int ServiceId { get; set; }
    public int PartId { get; set; }

    // Навигационные свойства
    public Service? Service { get; set; }
    public Part? Part { get; set; }

    private ServicePartAssociation(
        int serviceId,
        int partId,
        Service? service = null,
        Part? part = null)
    {
        ServiceId = serviceId;
        PartId = partId;
        Service = service;
        Part = part;
    }

    public static ServicePartAssociation FactoryServicePartAssociation(
        int serviceId,
        int partId,
        Service? service = null,
        Part? part = null)
    {
        if (serviceId <= 0)
            throw new ArgumentException("Некорректный ServiceId.");
        if (partId <= 0)
            throw new ArgumentException("Некорректный PartId.");
        if (service != null)
        {
            if (service.Id != serviceId)
                throw new ArgumentException("Некорректный Service.");
        }
        if (part != null)
        {
            if (part.Id != partId)
                throw new ArgumentException("Некорректный Part.");
        }

        return new ServicePartAssociation(
            serviceId: serviceId,
            partId: partId,
            service: service,
            part: part
        );
    }
}
