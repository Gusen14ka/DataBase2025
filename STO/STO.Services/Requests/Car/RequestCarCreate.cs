using System.ComponentModel.DataAnnotations;

namespace STO.Service.Requests.Car;
public class RequestCarCreate
{
    [Required]
    public int ModelId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string Vin { get; set; }
    public DateTime? StartService { get; set; }
}

