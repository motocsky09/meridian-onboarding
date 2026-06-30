namespace Meridian.Application.DTOs;

public class DashboardDto
{
    public EmployeeSummaryDto Employee { get; set; } = null!;
    public PersonDto? Manager { get; set; }
    public PersonDto? Buddy { get; set; }
    public Dictionary<string, List<ChecklistItemDto>> ChecklistByStage { get; set; } = new();
    public List<ScheduleEventDto> Schedule { get; set; } = new();
}

public class EmployeeSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
}

public class PersonDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class ChecklistItemDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Completed { get; set; }
}

public class ScheduleEventDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? WithPersonName { get; set; }
}
