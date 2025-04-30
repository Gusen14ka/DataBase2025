namespace STO.Service.Responses.Timetable;

public class ResponseReminder
{
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string CustomerEmail { get; set; }
    public string CarModel { get; set; }
    public string CarBrand { get; set; }
    public string ServiceName { get; set; }
    public DateTime NextVisit { get; set; }
}
