using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STO.Service.Interfaces;
using STO.Service.Responses.Timetable;
using System.ComponentModel.DataAnnotations;

namespace STO.Web.Pages.InstrumentWizard;

public class SendRemindersModel : PageModel
{
    private readonly ITimetableService _timetableService;

    public SendRemindersModel(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше 0")]
    public int? DaysBefore { get; set; }

    public List<ResponseReminder>? Reminders { get; private set; }

    public bool Sent => Reminders != null && Reminders.Any();

    public void OnGet() 
    {
        HttpContext.Session.Clear();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Reminders = await _timetableService.CreateRemindersByTimeSpan(TimeSpan.FromDays(DaysBefore!.Value));

        if (!Reminders.Any())
        {
            ModelState.AddModelError(string.Empty, "Напоминания не найдены для выбранного периода.");
        }

        return Page();
    }
}
