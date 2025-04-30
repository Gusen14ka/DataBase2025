using Microsoft.AspNetCore.Mvc;
using STO.Service.Responses.OrderedPart;
using STO.Service.Requests.OrderedPart;
using STO.Service.Interfaces;
using STO.Service.Services;

namespace STO.Web.Controllers;

[Route("api/[controller]")]
[ApiController]

public class OrderedPartsController(IOrderedPartService orderedPartService) : ControllerBase
{
    private readonly IOrderedPartService _orderedPartService = orderedPartService;

    [HttpPost]
    public async Task<ActionResult<ResponseOrderedPartBrief>> PostOrderedPart(
        [FromBody] RequestOrderedPartCreate request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var response = await _orderedPartService.AddOrderedPartAsync(request);
        if (response.Id == 0) return StatusCode(500);
        return Ok(response);
    }
}