using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STO.Service.Interfaces;
using STO.Service.Requests.Car;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STO.Web.Pages.CarWizard
{
    public class CreateCarModel : PageModel
    {
        private readonly ICarService _carService;
        private readonly IModelService _modelService;

        public CreateCarModel(ICarService carService, IModelService modelService)
        {
            _carService = carService;
            _modelService = modelService;
        }

        [BindProperty]
        public RequestCarCreate Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CustomerId { get; set; }

        public bool ShowSuccess { get; private set; }
        public int? NewCarId { get; private set; }

        public IActionResult OnGet(bool created = false, int? carId = null)
        {
            // Если нет выбранного customer в сессии — отправляем на поиск клиента
            var sessionCustomer = HttpContext.Session.GetInt32("SelectedCustomerId");
            if (sessionCustomer == null)
            {
                return RedirectToPage("/CustomerWizard/SearchCustomer");
            }

            CustomerId = sessionCustomer;  // запомним для дальнейшей работы

            // если вернулись после создания машины — покажем модалку и сохраним в сессии
            if (created && carId.HasValue)
            {
                ShowSuccess = true;
                NewCarId = carId.Value;

                HttpContext.Session.SetInt32("SelectedCarId", carId.Value);
                HttpContext.Session.SetInt32("SelectedCustomerId", CustomerId.Value);
            }

            // Инициализируем Input на всякий случай
            Input = new RequestCarCreate
            {
                CustomerId = CustomerId.Value
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.CustomerId == 0)
                ModelState.AddModelError(string.Empty, "Нужно выбрать клиента.");

            if (!ModelState.IsValid)
                return Page();

            var created = await _carService.AddCarAsync(Input);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Не удалось создать машину.");
                return Page();
            }

            return RedirectToPage(
                new { created = true, carId = created.Id, customerId = Input.CustomerId });
        }

        // handler для AJAX-поиска моделей
        public async Task<IActionResult> OnGetSearchModels(string nameQuery)
        {
            if (string.IsNullOrWhiteSpace(nameQuery))
                return new JsonResult(new List<object>());
            var models = await _modelService.SearchByNameAsync(nameQuery);
            var result = models
                        .Select(m => new {
                            id = m.Id,
                            name = m.Name,
                            brand = m.Brand   // предполагаем, что DTO имеет BrandName
                        })
                        .ToList();
            return new JsonResult(result);
        }
    }
}
