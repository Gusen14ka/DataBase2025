using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace STO.Infrastructure.Dto;

public class OrderDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? FinishedTime { get; set; }
    public int Speedometer { get; set; }
    public bool IsFinished { get; set; }
}
