namespace Meridian.Application.Models;

public class ScheduleEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EmployeeId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "Meeting", "OfficeDay", "RemoteDay"
    public DateTime Date { get; set; }
    public string? WithEmployeeId { get; set; }
}
