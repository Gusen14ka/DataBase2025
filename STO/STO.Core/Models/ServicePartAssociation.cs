namespace STO.Core.Models;

public class ServicePartAssociation
{
	public int ServiceId { get; set; }
    public int PartId { get; set; }

    // Навигационные свойства
    public Service Service { get; set; }
    public Part Part { get; set; }
}
