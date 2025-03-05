using System;
namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class OrderPart
{
	public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int PartId { get; set; }
    public Part Part { get; set; }
    public int Quantity { get; set; }
}
