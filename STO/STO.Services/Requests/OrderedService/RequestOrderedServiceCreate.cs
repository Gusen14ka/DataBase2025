using System.ComponentModel.DataAnnotations;

namespace STO.Service.Requests.OrderedService;

public class RequestOrderedServiceCreate
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
