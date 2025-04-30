using STO.Service.Responses.Part;

namespace STO.Service.Interfaces;

public interface IPartService
{
    Task<List<ResponsePartBrief>?> GetRecommendPartsAsync(int serviceId, int carModelId);
    Task<List<ResponsePartBrief>> SearchByServiceIdCarIdAndNameAsync(int serviceId, int carId, string nameQuery, bool? isNew, int? quantity);
}
