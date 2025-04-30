using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class ModelDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
}
