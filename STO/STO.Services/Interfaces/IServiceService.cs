using STO.Service.Responses.Service;

namespace STO.Service.Interfaces;

public interface IServiceService
{
    Task<List<ResponseServiceDetailed>> SearchByNameAsync(string nameQuery);
    Task<ResponseServiceDetailed?> GetByIdAsync(int serviceId);
}