namespace Meridian.Application.Models;

public class Employee
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string? ManagerId { get; set; }
    public string? BuddyId { get; set; }
}
