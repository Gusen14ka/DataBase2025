namespace STO.DTO.Car;
public class CarDetailedDto
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public string ModelName { get; set; }
    public int Year { get; set; }
    public int VIN { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime StartService { get; set; }
    public DateTime? EndService { get; set; }
}