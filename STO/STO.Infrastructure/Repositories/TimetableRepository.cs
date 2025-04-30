using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STO.Infrastructure.Repositories;

public class TimetableRepository(AppDbContext context): ITimetableRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<TimetableDto>> GetAllAsync()
    {
        return await _context.Timetables.ToListAsync();
    }
    public async Task<TimetableDto?> GetByIdAsync(int id)
    {
        return await _context.Timetables
            .FirstOrDefaultAsync(dto => dto.Id == id);
    }
    public async Task<TimetableDto> AddAsync(TimetableDto dto)
    {
        await _context.Timetables.AddAsync(dto);
        await _context.SaveChangesAsync();
        return dto;

    }
    public async Task UpdateAsync(TimetableDto dto)
    {
        _context.Entry(dto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task HardDeleteAsync(int id)
    {
        var table = await _context.Timetables.FindAsync(id);
        if (table == null) return;
        _context.Timetables.Remove(table);
        await _context.SaveChangesAsync();
    }
    public async Task<List<TimetableDto>> GetAllBeforeDate(DateTime date)
    {
        return await _context.Timetables
            .Where(t => t.NextVisit < date)
            .ToListAsync();
    }
    public async Task<TimetableDto> AddWithoutTrasactionAsync(TimetableDto dto)
    {
        await _context.Timetables.AddAsync(dto);
        return dto;

    }

}
