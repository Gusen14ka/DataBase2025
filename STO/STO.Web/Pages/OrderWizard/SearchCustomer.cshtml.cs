using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Customer;


namespace STO.Web.Pages.OrderWizard;

[IgnoreAntiforgeryToken]
public class SearchCustomerModel : PageModel
{
    private readonly ICustomerService _customerService;

    public SearchCustomerModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [BindProperty]
    public string PhoneNumber { get; set; } = "";

    public List<ResponseCustomerBrief>? Customers { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber))
        {
            ModelState.AddModelError("PhoneNumber", "Введите номер телефона");
            return Page();
        }

        // Получаем клиентов из сервиса, который возвращает DTO из STO.Service.Responses
        Customers = await _customerService.SearchByPhoneAsync(PhoneNumber);
        return Page();
    }

    public async Task<JsonResult> OnGetSearchCustomerAjaxAsync(string phoneQuery)
    {
        var customers = await _customerService.SearchByPhoneAsync(phoneQuery);
        return new JsonResult(customers);
    }

    public IActionResult OnPostSelectCustomer([FromForm] int customerId)
    {
        HttpContext.Session.SetInt32("SelectedCustomerId", customerId);
        return new OkResult();
    }

}