using STO.Service.Responses.OrderedService;
using STO.Service.Requests.OrderedService;

namespace STO.Service.Interfaces;

public interface IOrderedServiceService
{
    Task<ResponseOrderedServiceBrief> AddOrderedServiceAsync(RequestOrderedServiceCreate request);
}
