namespace STO.Core.Models;

public class OrderService
{
	public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } // Навигация
    public int ServiceId { get; set; }
    public Service Service { get; set; } // Навигация
    public int Quantity { get; set; }
}
