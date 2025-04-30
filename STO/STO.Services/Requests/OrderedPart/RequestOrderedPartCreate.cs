using System.ComponentModel.DataAnnotations;

namespace STO.Service.Requests.OrderedPart;

public class RequestOrderedPartCreate
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int PartId { get; set; }
    [Required]
    public int Quantity { get; set; }
}