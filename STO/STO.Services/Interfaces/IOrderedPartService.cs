using STO.Service.Responses.OrderedPart;
using STO.Service.Requests.OrderedPart;

namespace STO.Service.Interfaces;

public interface IOrderedPartService
{
    Task<ResponseOrderedPartBrief> AddOrderedPartAsync(RequestOrderedPartCreate request);
}
