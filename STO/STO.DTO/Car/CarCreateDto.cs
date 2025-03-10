namespace STO.DTO.Car;
public class CarCreateDto
{
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public int Year { get; set; }
    public int VIN { get; set; }
    public DateTime? StartService { get; set; }
}

