using System;
namespace STOAPI.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Model
{
	public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public ICollection<PartToModel> PartToModel { get; set; }
}
