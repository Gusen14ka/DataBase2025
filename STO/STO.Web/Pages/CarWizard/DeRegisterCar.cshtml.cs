using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Core.Models;
using STO.Service.Interfaces;
using STO.Service.Responses.Car;
using STO.Service.Responses.Customer;
using System.Linq;
using System.Threading.Tasks;

namespace STO.Web.Pages.CarWizard
{
    [IgnoreAntiforgeryToken]
    public class DeRegisterCarModel : PageModel
    {
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        public DeRegisterCarModel(ICarService carService,
            ICustomerService customerService)
        {
            _carService = carService;
            _customerService = customerService;
        }

        public int CustomerId { get; private set; }
        public ResponseCustomerBrief? CustomerBrief { get; private set; }

        // GET-запрос просто отрисовывает страницу
        public async Task<IActionResult> OnGet()
        {
            if (!HttpContext.Session.TryGetValue("SelectedCustomerId", out _))
            {
                return RedirectToPage("/CustomerWizard/SearchCustomer");
            }
            CustomerId = HttpContext.Session.GetInt32("SelectedCustomerId") ?? 0;
            CustomerBrief = await _customerService.GetCustomerByIdAsync(CustomerId);
            if (CustomerBrief == null) { return RedirectToPage("/CustomerWizard/SearchCustomer"); }
            return Page();
        }

        // AJAX-handler: теперь фильтруем и по VIN, и по CustomerId из Session
        public async Task<JsonResult> OnGetSearchAsync(string vin)
        {
            var custId = HttpContext.Session.GetInt32("SelectedCustomerId");
            if (custId == null || string.IsNullOrWhiteSpace(vin))
                return new JsonResult(Array.Empty<object>());

            var cars = await _carService.SearchByVinAndCustomerIdAsync(vin, custId.Value);
            if (cars == null || cars.Count == 0)
                return new JsonResult(Array.Empty<object>());
            var result = cars.Select(c => new {
                id = c.Id,
                modelId = c.ModelId,
                modelName = c.ModelName,
                brandName = c.BrandName,
                year = c.Year,
                vin = c.Vin
            }).ToList();

            return new JsonResult(result);
        }

        // AJAX-handler удаления остаётся без изменений
        public async Task<IActionResult> OnPostDeleteAsync(int carId)
        {
            var ok = await _carService.DeRegisterCarAsync(carId);
            return ok
                ? (IActionResult)new OkResult()
                : new BadRequestResult();
        }
    }
}
