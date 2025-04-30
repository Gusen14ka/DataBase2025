using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class ServiceDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public TimeSpan? NextVisit { get; set; }
}
