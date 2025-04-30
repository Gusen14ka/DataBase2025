namespace STO.Service.Responses.OrderedPart;

public class ResponseOrderedPartBrief
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
}