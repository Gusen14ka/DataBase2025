namespace STO.Core.Models;

public class PartModelCompatibility
{
    public int PartId { get; set; }
    public int ModelId { get; set; }

    // Навигационные свойства
    public Part Part { get; set; }
    public Model Model { get; set; }
}
