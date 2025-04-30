using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Car;
using STO.Service.Responses.Customer;
using STO.Service.Responses.Part;
using STO.Service.Responses.Service;
using STO.Service.Requests.Order;
using System.Globalization;

namespace STO.Web.Pages.OrderWizard;

[IgnoreAntiforgeryToken]
public class SearchPartsModel : PageModel
{
    private readonly IPartService _partService;
    private readonly ICustomerService _customerService;
    private readonly ICarService _carService;
    private readonly IServiceService _serviceService;
    private readonly IOrderService _orderService;

    public SearchPartsModel(
        ICustomerService customerService,
        ICarService carService,
        IServiceService serviceService,
        IPartService partService,
        IOrderService orderService)
    {
        _customerService = customerService;
        _carService = carService;
        _serviceService = serviceService;
        _partService = partService;
        _orderService = orderService;
    }

    public decimal ServiceCost { get; set; }
    public int CustomerId { get; private set; }
    public int CarId { get; private set; }
    public int ServiceId { get; private set; }
    public int Speedometer { get; private set; }


    public ResponseCustomerBrief? Customer { get; private set; }
    public ResponseCarWithModelAndBrand? Car { get; private set; }
    public ResponseServiceDetailed? Service { get; private set; }

    // Ётот метод вызываетс€ AJAX-запросом при вводе фильтров
    public async Task<JsonResult> OnGetSearchPartsAsync(string nameQuery, bool? isNew, int? quantity)
    {
        // ѕровер€ем наличие об€зательных данных: выбранной услуги и машины
        var serviceId = HttpContext.Session.GetInt32("SelectedServiceId");
        var carId = HttpContext.Session.GetInt32("SelectedCarId");
        if (serviceId == null || carId == null || string.IsNullOrWhiteSpace(nameQuery))
        {
            return new JsonResult(Array.Empty<ResponsePartBrief>());
        }

        var result = await _partService.SearchByServiceIdCarIdAndNameAsync(
            serviceId.Value,
            carId.Value,
            nameQuery,
            isNew,
            quantity
        );

        return new JsonResult(result);
    }


    // ≈сли к этой странице попадают напр€мую через GET без поисковых параметров, провер€ем зависимые шаги:
    public async Task<IActionResult> OnGetAsync()
    {
        if (!HttpContext.Session.TryGetValue("SelectedCustomerId", out _) ||
            !HttpContext.Session.TryGetValue("SelectedCarId", out _) ||
            !HttpContext.Session.TryGetValue("SelectedServiceId", out _))
        {
            return RedirectToPage("/OrderWizard/SearchService");
        }

        var costString = HttpContext.Session.GetString("SelectedServiceCost");
        ServiceCost = decimal.TryParse(costString, NumberStyles.Any, CultureInfo.InvariantCulture, out var cost) ? cost : 0;

        CustomerId = HttpContext.Session.GetInt32("SelectedCustomerId")!.Value;
        CarId = HttpContext.Session.GetInt32("SelectedCarId")!.Value;
        ServiceId = HttpContext.Session.GetInt32("SelectedServiceId")!.Value;
        Speedometer = HttpContext.Session.GetInt32("SelectedSpeedometer") ?? 1;

        Customer = await _customerService.GetCustomerByIdAsync(CustomerId);
        Car = await _carService.GetCarWithModelAndBrandByIdAsync(CarId);
        Service = await _serviceService.GetByIdAsync(ServiceId);

        return Page();
    }
    
    public async Task<IActionResult> OnPostCreateOrderAsync([FromBody] RequestOrderCreate req)
    {
        var response = await _orderService.SetOrderAsync(req);
        if (response == null)
        {
            return StatusCode(500);
        }
        return new JsonResult(new { success = true });
    }
    
}