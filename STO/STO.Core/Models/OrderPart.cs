namespace STO.Core.Models;

public class OrderPart
{
	public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } // Навигация
    public int PartId { get; set; }
    public Part Part { get; set; } // Навигация
    public int Quantity { get; set; }
}
