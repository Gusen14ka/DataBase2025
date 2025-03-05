namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ServiceToPart
{
	public int ServiceId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }

    // Навигационные свойства
    public Service Service { get; set; }
    public Part Part { get; set; }
}
