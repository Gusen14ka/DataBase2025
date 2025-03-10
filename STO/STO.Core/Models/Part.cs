namespace STO.Core.Models;

public class Part
{
	public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public bool IsNew { get; set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServicePartAssociation>? ServicePartAssociation { get; set; }
    public ICollection<PartModelCompatibility>? PartModelCompatibility { get; set; }
}
