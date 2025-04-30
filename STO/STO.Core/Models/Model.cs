namespace STO.Core.Models;

public class Model
{
	public int Id { get; private set; }
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public ICollection<PartModelCompatibility>? PartModelCompatibilities { get; private set; }

    private Model(
        int id,
        string name,
        string brand,
        ICollection<PartModelCompatibility>? partModelCompatibilities)
    {
        Id = id;
        Name = name;
        Brand = brand;
        PartModelCompatibilities = partModelCompatibilities;
    }

    public static Model FactoryModel(
        int id,
        string name,
        string brand,
        ICollection<PartModelCompatibility>? partModelCompatibilities)
    {
        if (id <= 0)
            throw new ArgumentException("Некорректный Id.");
        if (name.Length > 100)
            throw new ArgumentException("Некорректное Название модели.");
        if (brand.Length > 100)
            throw new ArgumentException("Некорректное Название марки.");
        if (partModelCompatibilities != null)
        {
            foreach (var compatibility in partModelCompatibilities)
            {
                if (compatibility.ModelId != id)
                    throw new ArgumentException("Некорректный PartModelCompatibilities.");
            }
        }
        
        return new Model(
            id: id,
            name: name,
            brand: brand,
            partModelCompatibilities: partModelCompatibilities
        );
    }
}
