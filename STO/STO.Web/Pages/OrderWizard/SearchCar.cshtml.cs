using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Car;

namespace STO.Web.Pages.OrderWizard;

[IgnoreAntiforgeryToken]
public class SearchCarModel : PageModel
{
    private readonly ICarService _carService;

    public SearchCarModel(ICarService carService)
    {
        _carService = carService;
    }

    public int CustomerId { get; private set; }

    public async Task<JsonResult> OnGetSearchAsync(string vin)
    {
        var customerId = HttpContext.Session.GetInt32("SelectedCustomerId");
        if (customerId == null || string.IsNullOrWhiteSpace(vin))
            return new JsonResult(Array.Empty<ResponseCarBrief>());
        var result = await _carService.SearchByVinAndCustomerIdAsync(vin, customerId.Value);
        return new JsonResult(result);
    }

    public IActionResult OnPostSetSpeedo(int carId, int speedometer)
    {
        // сохраняем в сессии
        HttpContext.Session.SetInt32("SelectedCarId", carId);
        HttpContext.Session.SetInt32("SelectedSpeedometer", speedometer);
        return new OkResult();
    }
    public IActionResult OnGet()
    {
        if (!HttpContext.Session.TryGetValue("SelectedCustomerId", out _))
        {
            return RedirectToPage("/OrderWizard/SearchCustomer");
        }
        CustomerId = HttpContext.Session.GetInt32("SelectedCustomerId") ?? 0;
        return Page();
    }
}