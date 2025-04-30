using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class TimetableDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CarId { get; set; }
    public int ServiceId { get; set; }
    public DateTime NextVisit { get; set; }
}
