using STO.Service.Requests.Customer;
using STO.Service.Responses.Customer;

namespace STO.Service.Interfaces;

public interface ICustomerService
{
    Task<List<ResponseCustomerDetailed>> GetAllCustomersAsync();
    Task<ResponseCustomerBrief?> GetCustomerByIdAsync(int id);
    Task<ResponseCustomerDetailed> AddCustomerAsync(RequestCustomerCreate request);
    Task<List<ResponseCustomerBrief>> SearchByPhoneAsync(string phoneQuery);
    Task<bool> DeRegisterCustomerAsync(int customerId);
}
