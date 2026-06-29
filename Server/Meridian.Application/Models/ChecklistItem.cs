namespace Meridian.Application.Models;

public class ChecklistItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EmployeeId { get; set; } = string.Empty;
    public string Stage { get; set; } = string.Empty; // "Day1", "Week1", "Month1"
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}
