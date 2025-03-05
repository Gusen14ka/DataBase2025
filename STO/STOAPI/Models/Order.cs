using System;
namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Order
{
	public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int CarId { get; set; }
    public Car Car { get; set; }
    public DateTime CreatedTime { get; set; }
    public int Speedometer { get; set; }
    public bool IsFinished { get; set; }
}
