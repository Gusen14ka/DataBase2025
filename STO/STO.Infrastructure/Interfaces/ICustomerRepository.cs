using STO.Infrastructure.Dto;

namespace STO.Infrastructure.Interfaces;

public interface ICustomerRepository
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<CustomerDto> AddAsync(CustomerDto customer);
    Task UpdateAsync(CustomerDto customer);
    Task HardDeleteAsync(int id);
    Task<List<CustomerDto>> GetByIdsAsync(List<int> ids);
    Task<CustomerDto?> GetByPhoneNumberAsync(string phoneNumber);
    Task<List<CustomerDto>> FindByPhoneAsync(string phoneQuery);
    Task<bool> MarkCustomerAsDeletedAsync(int customerId);
}