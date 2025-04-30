using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;
public interface IModelRepository
{
    Task<List<ModelDto>> GetByIdsAsync(List<int> ids);
    Task<ModelDto?> GetByIdAsync(int id);
    Task<List<ModelDto>> GetAllAsync();
    Task<ModelDto> AddAsync(ModelDto model);
    Task UpdateAsync(ModelDto model);
    Task HardDeleteAsync(int id);
    Task<List<ModelDto>> FindByNameAsync(string nameQuery);
}
