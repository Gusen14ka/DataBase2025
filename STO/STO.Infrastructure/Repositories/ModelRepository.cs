using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class ModelRepository(AppDbContext context) : IModelRepository
{
    private readonly AppDbContext _context = context;
    public async Task<List<ModelDto>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Models
            .Where(c => ids.Contains(c.Id)) 
            .ToListAsync();
    }

    public async Task<ModelDto?> GetByIdAsync(int id)
    {
        return await _context.Models
            .FirstOrDefaultAsync(model => model.Id == id);
    }

    public async Task<List<ModelDto>> FindByNameAsync(string nameQuery)
    {
        var pattern = $"%{nameQuery.ToLower()}%";
        return await _context.Models
            .Where(m =>
                EF.Functions.Like(m.Name.ToLower(), pattern)
                || EF.Functions.Like(m.Brand.ToLower(), pattern)    // если хотите искать и по бренду
            )
            .ToListAsync();
    }
    public async Task<List<ModelDto>> GetAllAsync()
    {
        return await _context.Models.ToListAsync();
    }
    public async Task<ModelDto> AddAsync(ModelDto model)
    {
        await _context.Models.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }
    public async Task UpdateAsync(ModelDto model)
    {
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var model = await _context.Models.FindAsync(id);
        if (model == null) return;
        _context.Models.Remove(model);
        await _context.SaveChangesAsync();
    }
}