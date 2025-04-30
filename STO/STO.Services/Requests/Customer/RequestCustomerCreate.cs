using System.ComponentModel.DataAnnotations;


namespace STO.Service.Requests.Customer;
public class RequestCustomerCreate
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
}
