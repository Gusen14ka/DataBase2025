using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Service;
using STO.Web.Extensions;


namespace STO.Web.Pages.OrderWizard;

[IgnoreAntiforgeryToken]
public class SearchServiceModel : PageModel
{
    private readonly IServiceService _serviceService;

    public SearchServiceModel(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public bool NeedSpeedo { get; private set; }

    public async Task<JsonResult> OnGetSearchAsync(string nameQuery)
    {
        if (string.IsNullOrWhiteSpace(nameQuery))
            return new JsonResult(Array.Empty<ResponseServiceDetailed>());

        var result = await _serviceService.SearchByNameAsync(nameQuery);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostSelectService([FromForm] int serviceId)
    {
        HttpContext.Session.SetInt32("SelectedServiceId", serviceId);

        var service = await _serviceService.GetByIdAsync(serviceId);
        if (service != null)
        {
            HttpContext.Session.SetDecimal("SelectedServiceCost", service.Price);
        }

        return new OkResult();
    }

    public IActionResult OnPostSetSpeedo(int speedometer)
    {
        HttpContext.Session.SetInt32("SelectedSpeedometer", speedometer);
        return new OkResult();
    }

    public IActionResult OnGet()
    {
        if (!HttpContext.Session.TryGetValue("SelectedCarId", out _) ||
            !HttpContext.Session.TryGetValue("SelectedCustomerId", out _))
        {
            return RedirectToPage("/OrderWizard/SearchCar");
        }

        NeedSpeedo = !HttpContext.Session.TryGetValue("SelectedSpeedometer", out _);
        return Page();
    }
}