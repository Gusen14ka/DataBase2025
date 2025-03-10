namespace STO.DTO.Car;
public class CarMinimalisticDto
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public int Year { get; set; }
    public int VIN { get; set; }
    public DateTime? StartService { get; set; }
    public DateTime? EndService { get; set; }
}