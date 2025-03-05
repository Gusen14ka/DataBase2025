namespace STOAPI.Models;

public class PartToModel
{
    public int PartId { get; set; }
    public int ModelId { get; set; }
    public int Quantity { get; set; }

    // Навигационные свойства
    public Part Part { get; set; }
    public Model Model { get; set; }
}
