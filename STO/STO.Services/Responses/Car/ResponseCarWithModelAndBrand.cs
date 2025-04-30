namespace STO.Service.Responses.Car;

public class ResponseCarWithModelAndBrand
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public string BrandName { get; set; }
    public int Year { get; set; }
    public string Vin { get; set; }
}