using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Requests.Customer;

namespace STO.Web.Pages.CustomerWizard
{
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        [BindProperty]
        public RequestCustomerCreate Input { get; set; }

        public bool ShowSuccess { get; private set; }
        public int CustomerId { get; private set; }

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult OnGet(bool created = false, int? customerId = null)
        {
            if (created && customerId.HasValue)
            {
                // Аналогия с SearchParts - сохранение в сессии
                HttpContext.Session.SetInt32("SelectedCustomerId", customerId.Value);
                ShowSuccess = true;
                CustomerId = customerId.Value;
            }
            else
            {
                HttpContext.Session.Clear();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var customer = await _customerService.AddCustomerAsync(Input);
            if (customer == null) return Page();

            // PRG-паттерн как в рабочем примере
            return RedirectToPage(new
            {
                created = true,
                customerId = customer.Id
            });
        }
    }
}