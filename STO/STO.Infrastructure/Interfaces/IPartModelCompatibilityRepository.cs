using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IPartModelCompatibilityRepository
{
    Task<List<PartModelCompatibilityDto>?> GetByModelIdAsync(int modelId);
    Task<PartModelCompatibilityDto> AddAsync(PartModelCompatibilityDto pmc);
    Task<List<PartModelCompatibilityDto>> GetAllAsync();
    Task UpdateAsync(PartModelCompatibilityDto pmc);
    Task DeleteAsync(int modelId, int partId);
    Task DeleteByModelIdAsync(int modelId);
    Task DeleteByPartIdAsync(int partId);
}