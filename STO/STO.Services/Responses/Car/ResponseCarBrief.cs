namespace STO.Service.Responses.Car;
public class ResponseCarBrief 
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public int Year { get; set; }
    public string Vin { get; set; }
    public DateTime StartService { get; set; }
    public DateTime? EndService { get; set; }
}