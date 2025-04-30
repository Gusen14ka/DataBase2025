using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class OrderedPartDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
}
