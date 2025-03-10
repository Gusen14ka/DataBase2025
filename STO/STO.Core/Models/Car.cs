namespace STO.Core.Models;

public class Car
{
	public int Id { get; set; }
    public int ModelId { get; set; } //Внешний ключ
    public Model Model { get; set; } // Навигация
    public int Year { get; set; }
    public int VIN { get; set; }
    public bool IsDeleted { get; set; } = false;  // Мягкое удаление
    public int CustomerId { get; set; }  // Внешний ключ (ссылка на клиента)
    public Customer Customer { get; set; }  // Навигационное свойство (связь с клиентом)
    public DateTime StartService { get; set; }
    public DateTime? EndService { get; set; }
}
