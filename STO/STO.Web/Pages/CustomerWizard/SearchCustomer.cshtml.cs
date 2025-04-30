using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Customer;

namespace STO.Web.Pages.CustomerWizard
{
    [IgnoreAntiforgeryToken]
    public class SearchCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public SearchCustomerModel(ICustomerService customerService)
            => _customerService = customerService;

        // GET-запрос: очищаем старую сессию, т.к. это вход в CarWizard
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        // AJAX-хендлер для поиска
        public async Task<JsonResult> OnGetSearchCustomerAjaxAsync(string phoneQuery)
        {
            if (string.IsNullOrWhiteSpace(phoneQuery))
                return new JsonResult(Array.Empty<ResponseCustomerBrief>());

            var list = await _customerService.SearchByPhoneAsync(phoneQuery);
            return new JsonResult(list);
        }

        // по клику на клиента сохраняем в сессию
        public IActionResult OnPostSelectCustomer([FromForm] int customerId)
        {
            HttpContext.Session.SetInt32("SelectedCustomerId", customerId);
            return new OkResult();
        }
    }
}
