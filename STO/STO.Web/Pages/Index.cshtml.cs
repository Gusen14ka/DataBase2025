using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace STO.Web.Pages
{
    public class IndexModel : PageModel
    {
        // Навигационные пункты, легко расширить
        public List<NavItem> NavItems { get; } = new()
        {
            new NavItem("Добавить клиента", "/CustomerWizard/CreateCustomer"),
            new NavItem("Добавить автомобиль", "/CarWizard/CreateCar"),
            new NavItem("Оформить заказ", "/OrderWizard/SearchCustomer"),
            new NavItem("Снять автомобиль с учёта", "/CarWizard/DeRegisterCar"),
            new NavItem("Снять клиента с учёта", "/CustomerWizard/DeRegisterCustomer"),
            new NavItem("Финансовый отчёт", "/InstrumentWizard/FinancialReport"),
            new NavItem("Отправить уведомления", "/InstrumentWizard/SendReminders")
        };

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }

    public record NavItem(string Title, string Page);
}
