using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class PartRepository(AppDbContext context) : IPartRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<PartDto>> GetAllAsync()
    {
        return await _context.Parts.ToListAsync();
    }

    public async Task<PartDto?> GetByIdAsync(int id)
    {
        return await _context.Parts
            .FirstOrDefaultAsync(part => part.Id == id);
    }

    public async Task UpdateAsync(PartDto part)
    {
        _context.Entry(part).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<PartDto>> GetPartsByIdsAsync(IEnumerable<int> partIds)
    {
        return await _context.Parts
            .Where(p => partIds.Contains(p.Id))
            .ToListAsync();
    }


    public async Task<List<PartDto>> GetByNoveltyQuantityAndName(bool? isNew, int? quantity, string nameQuery)
    {
        var query = _context.Parts.AsQueryable();

        // Регистронезависимый поиск для SQLite
        if (!string.IsNullOrWhiteSpace(nameQuery))
            query = query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{nameQuery.ToLower()}%"));

        // Фильтр по состоянию
        if (isNew.HasValue)
            query = query.Where(p => p.IsNew == isNew.Value);

        // Фильтр по количеству
        if (quantity.HasValue)
            query = query.Where(p => p.Quantity >= quantity.Value);

        return await query.ToListAsync();
    }

    public Task DecrementQuantityAsync(int partId, int amount, CancellationToken cancellationToken = default)
    {
        // Обратите внимание: мы не делаем _context.SaveChangesAsync() здесь,
        // и не трогаем ChangeTracker.
        // UPDATE выполняется прямо в базе, атомарно.
        return _context.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE Parts SET Quantity = Quantity - {amount} WHERE Id = {partId}",
            cancellationToken
        );
    }
    public async Task<PartDto> AddAsync(PartDto part)
    {
        await _context.Parts.AddAsync(part);
        await _context.SaveChangesAsync();
        return part;

    }
    public async Task HardDeleteAsync(int id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return;
        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();
    }
}
