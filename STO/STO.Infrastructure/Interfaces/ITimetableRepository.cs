using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface ITimetableRepository
{
    Task<TimetableDto?> GetByIdAsync(int id);
    Task<List<TimetableDto>> GetAllAsync();
    Task<TimetableDto> AddAsync(TimetableDto dto);
    Task UpdateAsync(TimetableDto dto);
    Task HardDeleteAsync(int id);
    Task<List<TimetableDto>> GetAllBeforeDate(DateTime date);
    Task<TimetableDto> AddWithoutTrasactionAsync(TimetableDto dto);
}
