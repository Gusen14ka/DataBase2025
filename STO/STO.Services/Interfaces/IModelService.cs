using STO.Service.Responses.Model;

namespace STO.Service.Interfaces;

public interface IModelService
{
    Task<List<ResponseModelBrief>> SearchByNameAsync(string nameQuery);
}