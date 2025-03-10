namespace STO.Core.Models;

public class Service
{
	public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public DateTime? NextVisit { get; set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServicePartAssociation>? ServicePartAssociation { get; set; }
}
