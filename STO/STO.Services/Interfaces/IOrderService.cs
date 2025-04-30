using STO.Service.Requests.Order;
using STO.Service.Responses.Order;

namespace STO.Service.Interfaces;

public interface IOrderService
{
    Task<ResponseOrderBrief> AddOrderAsync(RequestOrderCreate request);
    Task<decimal> CalculateOrderCostAsync(RequestOrderCost request);

    Task<ResponseOrderBrief?> SetOrderAsync(RequestOrderCreate request);
    Task<List<ResponseFinancialReport>> CreateFinancialReport(DateTime startDate, DateTime endDate);
}
