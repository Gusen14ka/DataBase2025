using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IPartRepository
{
    Task<List<PartDto>> GetPartsByIdsAsync(IEnumerable<int> partIds);
    Task<PartDto> AddAsync(PartDto part);
    Task<PartDto?> GetByIdAsync(int id);
    Task<List<PartDto>> GetAllAsync();
    Task UpdateAsync(PartDto part);
    Task HardDeleteAsync(int id);
    Task<List<PartDto>> GetByNoveltyQuantityAndName(bool? isNew, int? quantity, string nameQuery);
    Task DecrementQuantityAsync(int partId, int amount, CancellationToken cancellationToken = default);
}
