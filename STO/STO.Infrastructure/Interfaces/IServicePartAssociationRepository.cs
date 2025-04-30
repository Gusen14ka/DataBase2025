using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface IServicePartAssociationRepository
{
    Task<List<ServicePartAssociationDto>?> GetByServiceIdAsync(int serviceId);
    Task<ServicePartAssociationDto> AddAsync(ServicePartAssociationDto spa);
    Task<List<ServicePartAssociationDto>> GetAllAsync();
    Task UpdateAsync(ServicePartAssociationDto spa);
    Task DeleteAsync(int serviceId, int partId);
    Task DeleteByServiceIdAsync(int serviceId);
    Task DeleteByPartIdAsync(int partId);
}
