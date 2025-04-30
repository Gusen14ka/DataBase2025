using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class OrderedServiceDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
}
