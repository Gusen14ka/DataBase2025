namespace STO.Service.Responses.OrderedPart;

public class ResponseOrderedPartItemForFinancialReport
{
    public int PartId { get; set; }
    public string PartName {  get; set; }
    public decimal PartPrice { get; set; }
    public int Quantity { get; set; }
    public bool IsNew { get; set; }
    public decimal PartSumPrice { get; set; }
}
