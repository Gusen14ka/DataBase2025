namespace STO.Core.Models;

public class Part
{
	public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public bool IsNew { get; private set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServicePartAssociation>? ServicePartAssociations { get; set; }
    public ICollection<PartModelCompatibility>? PartModelCompatibilities { get; set; }

    private Part(
        int id,
        string name,
        decimal price,
        int quantity,
        bool isNew,
        ICollection<ServicePartAssociation>? servicePartAssociations = null,
        ICollection<PartModelCompatibility>? partModelCompatibilities = null)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        IsNew = isNew;
        servicePartAssociations = ServicePartAssociations;
        partModelCompatibilities = PartModelCompatibilities;
    }

    public static Part FactoryPart(
        int id,
        string name,
        decimal price,
        int quantity,
        bool isNew,
        ICollection<ServicePartAssociation>? servicePartAssociations = null,
        ICollection<PartModelCompatibility>? partModelCompatibilities = null)
    {
        if (id <= 0)
            throw new ArgumentException("Некорректный Id.");
        if (name.Length > 100)
            throw new ArgumentException("Некорректное Название детали.");
        if (price <= 0)
            throw new ArgumentException("Некорректный Price.");
        if (quantity <= 0)
            throw new ArgumentException("Некорректный Quantity.");
        if (servicePartAssociations != null)
        {
            foreach (var association in servicePartAssociations)
            {
                if (association.PartId != id)
                    throw new ArgumentException("Некорректный ServicePartAssociations.");
            }
        }
        if (partModelCompatibilities != null)
        {
            foreach (var compatibility in partModelCompatibilities)
            {
                if (compatibility.PartId != id)
                    throw new ArgumentException("Некорректный PartModelCompatibilities.");
            }
        }

        return new Part(
            id: id,
            name: name,
            price: price,
            quantity: quantity,
            isNew: isNew,
            servicePartAssociations: servicePartAssociations,
            partModelCompatibilities: partModelCompatibilities
        );

    }
}
