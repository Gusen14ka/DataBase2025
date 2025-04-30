namespace STO.Service.Responses.Order;

public class ResponseOrderBrief
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? FinishedTime { get; set; }
    public int Speedometer { get; set; }
    public bool IsFinished { get; set; }
}
