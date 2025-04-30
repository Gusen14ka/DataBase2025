using System.ComponentModel.DataAnnotations;

namespace STO.Service.Requests.OrderedPart;

public class OrderedPartItem
{
    [Required]
    public int PartId { get; set; }
    [Required]
    public int Quantity { get; set; }
}