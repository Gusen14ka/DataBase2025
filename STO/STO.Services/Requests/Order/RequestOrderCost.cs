using System.ComponentModel.DataAnnotations;

namespace STO.Service.Requests.Order;
public class RequestOrderCost
{
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public List<int> PartIds { get; set; } = new();
    [Required]
    public int CarId { get; set; }
}