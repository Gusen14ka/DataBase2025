using STO.Service.Interfaces;
using STO.Core.Models;
using STO.Infrastructure.Interfaces;
using AutoMapper;
using STO.Infrastructure.Dto;
using STO.Service.Responses.Part;

namespace STO.Service.Services;

public class PartService : IPartService
{
    private readonly IServicePartAssociationRepository _associationRepository;
    private readonly IPartRepository _partRepository;
    private readonly IPartModelCompatibilityRepository _compatibilityRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public PartService(
        IServicePartAssociationRepository associationRepository,
        IPartRepository partRepository,
        IPartModelCompatibilityRepository compatibilityRepository,
        ICarRepository carRepository,
        IMapper mapper)
    {
        _associationRepository = associationRepository;
        _partRepository = partRepository;
        _compatibilityRepository = compatibilityRepository;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    private async Task<List<PartDto>> GetAppropriatePartDtosToModelAndService(int serviceId, int carModelId)
    {
        // 1. Получаем список ServicePartAssociationDto и маппим его (в неполном виде), ассоциированных с данной услугой.
        var associationDtos = await _associationRepository.GetByServiceIdAsync(serviceId);

        if (associationDtos == null || !associationDtos.Any()) { return null; }

        var associations = associationDtos
            .Select(dto => _mapper.Map<ServicePartAssociation>(dto))
            .ToList();

        // 2. Получаем список PartModelCompatibilityDto и маппим его(в неполном виде), которые совместимы с моделью автомобиля.
        var compatibilityDtos = await _compatibilityRepository.GetByModelIdAsync(carModelId);

        if (compatibilityDtos == null || !associationDtos.Any()) { return null; }

        var compatibilities = compatibilityDtos
            .Select(dto => _mapper.Map<PartModelCompatibility>(dto))
            .ToList();

        // 3. Вычисляем пересечение: детали, которые есть и в ассоциациях, и в совместимых.
        var recommendedPartIds = associations
            .Select(spa => spa.PartId)
            .Intersect(compatibilities.Select(pmc => pmc.PartId))
            .ToList();

        // 4. Получаем детали по идентификаторам (маппим без навигации).
        var recommendedPartDtos = await _partRepository.GetPartsByIdsAsync(recommendedPartIds);

        return recommendedPartDtos;
    }
    public async Task<List<ResponsePartBrief>?> GetRecommendPartsAsync(int serviceId, int carModelId)
    {
        var recommendedPartDtos = await GetAppropriatePartDtosToModelAndService(serviceId, carModelId);
        var recommendedParts = recommendedPartDtos
            .Select(dto => _mapper.Map<Part>(dto))
            .ToList();
        var recommendedResponseParts = recommendedParts
            .Select(m => _mapper.Map<ResponsePartBrief>(m))
            .ToList();


        return recommendedResponseParts;
    }

    public async Task<List<ResponsePartBrief>> SearchByServiceIdCarIdAndNameAsync(int serviceId, int carId, string nameQuery,
        bool? isNew, int? quantity)
    {
        var requestedPartDtos = await _partRepository.GetByNoveltyQuantityAndName(isNew, quantity, nameQuery);
        var carDto = await _carRepository.GetByIdAsync(carId);
        if (carDto == null) {return new List<ResponsePartBrief>();}
        var car = _mapper.Map<Car>(carDto);
        var modelId = car.ModelId;
        var recommendedPartDtos = await GetAppropriatePartDtosToModelAndService(serviceId, modelId);
        requestedPartDtos = requestedPartDtos.Where(p => p != null).ToList();
        recommendedPartDtos = recommendedPartDtos.Where(p => p != null).ToList();
        var suitablePartDtos = requestedPartDtos.Where(p => recommendedPartDtos.Contains(p));
        var suitableParts = suitablePartDtos.Select(dto => _mapper.Map<Part>(dto));
        var response = suitableParts.Select(p => _mapper.Map<ResponsePartBrief>(p));
        return response.ToList();
    }
}