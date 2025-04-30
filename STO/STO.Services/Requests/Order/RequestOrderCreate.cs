using System.ComponentModel.DataAnnotations;
using STO.Service.Requests.OrderedPart;

namespace STO.Service.Requests.Order;

public class RequestOrderCreate
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int CarId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int Speedometer { get; set; }
    public DateTime? CreatedTime { get; set; }
    public bool? IsFinished { get; set; }
    public List<OrderedPartItem> Parts { get; set; } = new();
}
