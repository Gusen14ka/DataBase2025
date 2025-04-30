using AutoMapper;
using STO.Core.Models;
using STO.Infrastructure.Dto;
using STO.Infrastructure.Interfaces;
using STO.Service.Interfaces;
using STO.Service.Responses.Model;

namespace STO.Service.Services;

public class ModelService(IModelRepository modelRepository,
    IMapper mapper): IModelService
{
    private readonly IModelRepository _modelRepository = modelRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ResponseModelBrief>> SearchByNameAsync(string nameQuery)
    {
        var modelDtos = await _modelRepository.FindByNameAsync(nameQuery);
        var models = modelDtos.Select(dto => _mapper.Map<Model>(dto));
        var response = models.Select(m => _mapper.Map<ResponseModelBrief>(m));
        return response.ToList();
    }
}
