using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;
public class PartModelCompatibilityRepository(AppDbContext context): IPartModelCompatibilityRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<PartModelCompatibilityDto>?> GetByModelIdAsync(int modelId)
    {
        return await _context.PartModelCompatibilities
            .Where(pmc => pmc.ModelId == modelId) // Фильтруем по ModelId
            .ToListAsync(); // Загружаем из базы
    }
    public async Task<PartModelCompatibilityDto> AddAsync(PartModelCompatibilityDto pmc)
    {
        await _context.PartModelCompatibilities.AddAsync(pmc);
        await _context.SaveChangesAsync();
        return pmc;

    }
    public async Task<List<PartModelCompatibilityDto>> GetAllAsync()
    {
        return await _context.PartModelCompatibilities.ToListAsync();
    }
    public async Task UpdateAsync(PartModelCompatibilityDto pmc)
    {
        _context.Entry(pmc).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    // Удалить одну конкретную пару(ModelId, PartId)
    public async Task DeleteAsync(int modelId, int partId)
    {
        var entity = await _context.PartModelCompatibilities
                          .FindAsync(modelId, partId);
        if (entity == null) return;

        _context.PartModelCompatibilities.Remove(entity);
        await _context.SaveChangesAsync();
        return;
    }

    // Удалить все ассоциации для одной услуги
    public async Task DeleteByModelIdAsync(int modelId)
    {
        var list = _context.PartModelCompatibilities
                    .Where(x => x.ModelId == modelId);
        _context.PartModelCompatibilities.RemoveRange(list);
        await _context.SaveChangesAsync();
    }

    // Удалить все ассоциации для одной детали
    public async Task DeleteByPartIdAsync(int partId)
    {
        var list = _context.PartModelCompatibilities
                    .Where(pmc => pmc.PartId == partId);
        _context.PartModelCompatibilities.RemoveRange(list);
        await _context.SaveChangesAsync();
    }
}

