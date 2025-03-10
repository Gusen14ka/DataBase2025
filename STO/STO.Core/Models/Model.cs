namespace STO.Core.Models;

public class Model
{
	public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public ICollection<PartModelCompatibility>? PartModelCompatibility { get; set; }
}
