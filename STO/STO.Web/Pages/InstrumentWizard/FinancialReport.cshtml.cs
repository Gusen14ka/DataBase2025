using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Order;
using System.ComponentModel.DataAnnotations;

namespace STO.Web.Pages.InstrumentWizard;

public class FinancialReportModel : PageModel
{
    private readonly IOrderService _orderService;
    public FinancialReportModel(IOrderService orderService)
        => _orderService = orderService;

    [BindProperty, DataType(DataType.Date), Required]
    public DateTime? StartDate { get; set; }

    [BindProperty, DataType(DataType.Date), Required]
    public DateTime? EndDate { get; set; }

    // Сюда попадёт результат сервиса
    public List<ResponseFinancialReport>? Report { get; set; }

    public void OnGet()
    {
        // при GET просто показываем форму, Report = null
        HttpContext.Session.Clear();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Гарантируем, что даты не null
        var from = StartDate!.Value;
        var to = EndDate!.Value;

        // Вызываем сервис, получаем список DTO
        Report = await _orderService.CreateFinancialReport(from, to);

        return Page();
    }
}
