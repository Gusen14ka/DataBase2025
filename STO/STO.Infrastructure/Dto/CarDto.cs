using System.ComponentModel.DataAnnotations.Schema;

namespace STO.Infrastructure.Dto;

public class CarDto
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ModelId { get; set; } //Внешний ключ
    public int Year { get; set; }
    public string Vin { get; set; }
    public bool IsDeleted { get; set; }  // Мягкое удаление
    public int CustomerId { get; set; }  // Внешний ключ (ссылка на клиента)
    public DateTime StartService { get; set; }
    public DateTime? EndService { get; set; }

}
