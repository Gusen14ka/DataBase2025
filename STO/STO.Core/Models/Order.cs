namespace STO.Core.Models;

public class Order
{
	public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } // Навигация
    public int CarId { get; set; }
    public Car Car { get; set; } // Навигация
    public DateTime CreatedTime { get; set; }
    public int Speedometer { get; set; }
    public bool IsFinished { get; set; } = false;
}
