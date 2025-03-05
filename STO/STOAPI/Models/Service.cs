using System;
namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Service
{
	public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public DateTime NextVisit { get; set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServiceToPart> ServiceToPart { get; set; }
}
