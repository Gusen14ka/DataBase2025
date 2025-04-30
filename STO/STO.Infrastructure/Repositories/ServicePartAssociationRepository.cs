using Microsoft.EntityFrameworkCore;
using STO.Infrastructure.Data;
using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;

namespace STO.Infrastructure.Repositories;

public class ServicePartAssociationRepository(AppDbContext context) : IServicePartAssociationRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<ServicePartAssociationDto>?> GetByServiceIdAsync(int serviceId)
    {
        return await _context.ServicePartAssociations
            .Where(spa => spa.ServiceId == serviceId)
            .ToListAsync();
    }
    public async Task<ServicePartAssociationDto> AddAsync(ServicePartAssociationDto spa)
    {
        await _context.ServicePartAssociations.AddAsync(spa);
        await _context.SaveChangesAsync();
        return spa;

    }
    public async Task<List<ServicePartAssociationDto>> GetAllAsync()
    {
        return await _context.ServicePartAssociations.ToListAsync();
    }
    public async Task UpdateAsync(ServicePartAssociationDto spa)
    {
        _context.Entry(spa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    // Удалить одну конкретную пару(ServiceId, PartId)
    public async Task DeleteAsync(int serviceId, int partId)
    {
        var entity = await _context.ServicePartAssociations
                          .FindAsync(serviceId, partId);
        if (entity == null) return;

        _context.ServicePartAssociations.Remove(entity);
        await _context.SaveChangesAsync();
    }

    // Удалить все ассоциации для одной услуги
    public async Task DeleteByServiceIdAsync(int serviceId)
    {
        var list = _context.ServicePartAssociations
                    .Where(spa => spa.ServiceId == serviceId);
        _context.ServicePartAssociations.RemoveRange(list);
        await _context.SaveChangesAsync();
    }

    // Удалить все ассоциации для одной детали
    public async Task DeleteByPartIdAsync(int partId)
    {
        var list = _context.ServicePartAssociations
                    .Where(x => x.PartId == partId);
        _context.ServicePartAssociations.RemoveRange(list);
        await _context.SaveChangesAsync();
    }
}
