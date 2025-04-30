using STO.Service.Responses.Car;
using STO.Service.Responses.OrderedPart;


namespace STO.Service.Responses.Order;

public class ResponseFinancialReport
{
    // Относится к заказу:
    public int OrderId { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal FullOrderPrice { get; set; }
    
    // Относится к клиенту:
    public int CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    // Относится к авто:
    public ResponseCarWithModelAndBrand Car { get; set; } = new ResponseCarWithModelAndBrand();

    //Относится к услуге
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public decimal ServicePrice { get; set; }

    //Относится к деталям
    public List<ResponseOrderedPartItemForFinancialReport> OrderedParts { get; set; } = new List<ResponseOrderedPartItemForFinancialReport>();

}
