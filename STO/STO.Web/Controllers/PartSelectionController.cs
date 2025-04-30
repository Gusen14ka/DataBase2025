using Microsoft.AspNetCore.Mvc;
using STO.Service.Interfaces;

namespace STO.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicePartsController : ControllerBase
{
    private readonly IPartService _selectionService;

    public ServicePartsController(IPartService selectionService)
    {
        _selectionService = selectionService;
    }

    
}