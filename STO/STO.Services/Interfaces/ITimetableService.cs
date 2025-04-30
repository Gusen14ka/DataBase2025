using STO.Service.Responses.Timetable;

namespace STO.Service.Interfaces;

public interface ITimetableService
{
    Task<List<ResponseReminder>> CreateRemindersByTimeSpan(TimeSpan daysBefore);
}
