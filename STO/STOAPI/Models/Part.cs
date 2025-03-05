using System;
namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Part
{
	public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public bool IsNew { get; set; }

    // Навигационное свойство для связи многие-ко-многим
    public ICollection<ServiceToPart> ServiceToPart { get; set; }
    public ICollection<PartToModel> PartToModel { get; set; }
}
