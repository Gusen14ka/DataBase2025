namespace STO.Service.Responses.Car;
public class ResponseCarDetailed
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public int Year { get; set; }
    public string Vin { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime StartService { get; set; }
    public DateTime? EndService { get; set; }
    public bool IsDeleted { get; set; }
}