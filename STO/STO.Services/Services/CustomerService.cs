using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Service.Interfaces;
using STO.Service.Requests.Customer;
using STO.Service.Responses.Customer;

namespace STO.Service.Services;

public class CustomerService(ICustomerRepository customerRepository,
    IMapper mapper,
    ICarRepository carRepository): ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ICarRepository _carRepository = carRepository;

    private async Task<List<Customer>> GetAllCustomersIncludeDeletedAsync()
    {
        var customerDtos = await _customerRepository.GetAllAsync();
        return customerDtos.Select(dto => _mapper.Map<Customer>(dto)).ToList();
    }

    public async Task<List<ResponseCustomerDetailed>> GetAllCustomersAsync()
    {
        var allCustomers = await GetAllCustomersIncludeDeletedAsync();
        var responseList = allCustomers
            .Where(customer => !customer.IsDeleted)
            .Select(customer => _mapper.Map<ResponseCustomerDetailed>(customer))
            .ToList();
        return responseList;
    }
    public async Task<ResponseCustomerBrief?> GetCustomerByIdAsync(int id)
    {
        var customerDto = await _customerRepository.GetByIdAsync(id);
        var customer = _mapper.Map<Customer>(customerDto);
        return _mapper.Map<ResponseCustomerBrief>(customer);
    }

    public async Task<ResponseCustomerDetailed> AddCustomerAsync(RequestCustomerCreate request)
    {
        var customer = _mapper.Map<Customer>(request);
        var customerDto = _mapper.Map<CustomerDto>(customer);
        var addedCustomerDto = await _customerRepository.AddAsync(customerDto);
        var addedCustomer = _mapper.Map<Customer>(addedCustomerDto);
        return _mapper.Map<ResponseCustomerDetailed>(addedCustomer);
    }

    public async Task<List<ResponseCustomerBrief>> SearchByPhoneAsync(string phoneQuery)
    {
        var customerDtos = await _customerRepository.FindByPhoneAsync(phoneQuery);
        var customers = customerDtos.Select(dto => _mapper.Map<Customer>(dto));
        var response = customers.Select(cus => _mapper.Map<ResponseCustomerBrief>(cus));
        return response.ToList();
    }

    public async Task<bool> DeRegisterCustomerAsync(int customerId)
    {
        var result = await _carRepository.MarkAsDeletedByCustomerIdAsync(customerId);
        if (!result) { return result; }
        return await _customerRepository.MarkCustomerAsDeletedAsync(customerId);
    }
}
