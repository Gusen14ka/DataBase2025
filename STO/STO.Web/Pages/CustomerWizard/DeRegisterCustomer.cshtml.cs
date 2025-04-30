using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Customer;

namespace STO.Web.Pages.CustomerWizard
{
    [IgnoreAntiforgeryToken]
    public class DeRegisterCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public DeRegisterCustomerModel(ICustomerService customerService)
            => _customerService = customerService;

        public ResponseCustomerBrief? CustomerBrief { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var cusetomerId = HttpContext.Session.GetInt32("SelectedCustomerId");
            if (cusetomerId == null)
                return RedirectToPage("/CustomerWizard/SearchCustomer");

            CustomerBrief = await _customerService.GetCustomerByIdAsync(cusetomerId.Value);
            if (CustomerBrief == null)
                return RedirectToPage("/CustomerWizard/SearchCustomer");

            return Page();
        }

        // AJAX-handler для снятия клиента
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var cusetomerId = HttpContext.Session.GetInt32("SelectedCustomerId");
            if (cusetomerId == null)
                return BadRequest();

            var ok = await _customerService.DeRegisterCustomerAsync(cusetomerId.Value);
            return ok ? (IActionResult)new OkResult() : new BadRequestResult();
        }
    }
}
