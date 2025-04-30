using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class PartDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsNew { get; set; }
}
