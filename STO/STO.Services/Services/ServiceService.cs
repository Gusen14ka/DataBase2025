using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using STO.Infrastructure.Dto;
using STO.Service.Interfaces;
using STO.Service.Responses.Service;

namespace STO.Service.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public ServiceService(
        IServiceRepository serviceRepository,
        IMapper mapper
    )
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseServiceDetailed>> SearchByNameAsync(string nameQuery)
    {
        var serviceDtos = await _serviceRepository.FindByNameAsync(nameQuery);
        var services = serviceDtos.Select(dto => _mapper.Map<Core.Models.Service>(dto));
        var response = services.Select(s => _mapper.Map<ResponseServiceDetailed>(s));
        return response.ToList();
    }

    public async Task<ResponseServiceDetailed?> GetByIdAsync(int serviceId)
    {
        var serviceDto = await _serviceRepository.GetByIdAsync(serviceId);
        if (serviceDto == null)
        {
            return null;
        }

        var service = _mapper.Map<Core.Models.Service>(serviceDto);
        return _mapper.Map<ResponseServiceDetailed>(service);
    }
}
