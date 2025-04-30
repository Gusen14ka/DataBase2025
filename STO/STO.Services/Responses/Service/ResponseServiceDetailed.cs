namespace STO.Service.Responses.Service;

public class ResponseServiceDetailed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public TimeSpan? NextVisit { get; set; }
}