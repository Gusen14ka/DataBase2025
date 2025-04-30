namespace STO.Core.Models;

public class Service
{
	public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public TimeSpan? NextVisit { get; private set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServicePartAssociation>? ServicePartAssociations { get; private set; }

    private Service(
        int id,
        string name,
        decimal price,
        TimeSpan? nextVisit = null,
        ICollection<ServicePartAssociation>? servicePartAssociations = null)
    {
        Id = id;
        Name = name;
        Price = price;
        NextVisit = nextVisit;
        ServicePartAssociations = servicePartAssociations;
    }

    public static Service FactoryService(
        int id,
        string name,
        decimal price,
        TimeSpan? nextVisit = null,
        ICollection<ServicePartAssociation>? servicePartAssociations = null)
    {
        if (id <= 0)
            throw new ArgumentException("Некорректный Id.");
        if (name.Length > 100)
            throw new ArgumentException("Некорректное Название модели.");
        if (price <= 0)
            throw new ArgumentException("Некорректный Price.");
        if (nextVisit != null && nextVisit <= TimeSpan.Zero)
            throw new ArgumentException("Некорректный NextVisit.");
        if (servicePartAssociations != null)
        {
            foreach (var association in servicePartAssociations)
            {
                if (association.ServiceId != id)
                    throw new ArgumentException("Некорректный ServicePartAssociations.");
            }
        }

        return new Service(
            id: id,
            name: name,
            price: price,
            nextVisit: nextVisit,
            servicePartAssociations: servicePartAssociations
        );
    }
}
