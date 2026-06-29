using Meridian.Application.Models;
using MongoDB.Driver;

namespace Meridian.Infrastructure.Seed;

public static class SeedData
{
    public static async Task SeedAsync(MongoDbContext context)
    {
        var existingCount = await context.Employees.CountDocumentsAsync(_ => true);
        if (existingCount > 0) return;

        var manager = new Employee
        {
            FirstName = "Diana",
            LastName = "Pop",
            Role = "Engineering Manager",
            Department = "Engineering",
            StartDate = DateTime.UtcNow.AddYears(-3)
        };

        var buddy = new Employee
        {
            FirstName = "Mihai",
            LastName = "Ionescu",
            Role = "Senior Developer",
            Department = "Engineering",
            StartDate = DateTime.UtcNow.AddYears(-2)
        };

        var hr = new Employee
        {
            FirstName = "Elena",
            LastName = "Vasilescu",
            Role = "HR Specialist",
            Department = "HR",
            StartDate = DateTime.UtcNow.AddYears(-4)
        };

        var newEmployee = new Employee
        {
            FirstName = "Andrei",
            LastName = "Motoc",
            Role = "Software Developer",
            Department = "Engineering",
            StartDate = DateTime.UtcNow.Date.AddDays(1),
            ManagerId = manager.Id,
            BuddyId = buddy.Id
        };

        await context.Employees.InsertManyAsync(new[] { manager, buddy, hr, newEmployee });

        var checklistItems = new List<ChecklistItem>
        {
            new() { EmployeeId = newEmployee.Id, Stage = "Day1", Title = "Complete laptop and account setup" },
            new() { EmployeeId = newEmployee.Id, Stage = "Day1", Title = "Meet your manager" },
            new() { EmployeeId = newEmployee.Id, Stage = "Day1", Title = "Join the team Slack channel" },
            new() { EmployeeId = newEmployee.Id, Stage = "Week1", Title = "Complete security and compliance training" },
            new() { EmployeeId = newEmployee.Id, Stage = "Week1", Title = "First 1:1 with your buddy" },
            new() { EmployeeId = newEmployee.Id, Stage = "Month1", Title = "Ship your first small task" }
        };
        await context.ChecklistItems.InsertManyAsync(checklistItems);

        var scheduleEvents = new List<ScheduleEvent>
        {
            new() { EmployeeId = newEmployee.Id, Title = "Welcome meeting", Type = "Meeting", Date = newEmployee.StartDate, WithEmployeeId = manager.Id },
            new() { EmployeeId = newEmployee.Id, Title = "HR orientation", Type = "Meeting", Date = newEmployee.StartDate, WithEmployeeId = hr.Id },
            new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate },
            new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate.AddDays(1) },
            new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate.AddDays(2) },
            new() { EmployeeId = newEmployee.Id, Title = "Remote day", Type = "RemoteDay", Date = newEmployee.StartDate.AddDays(3) },
            new() { EmployeeId = newEmployee.Id, Title = "Remote day", Type = "RemoteDay", Date = newEmployee.StartDate.AddDays(4) },
            new() { EmployeeId = newEmployee.Id, Title = "1:1 with buddy", Type = "Meeting", Date = newEmployee.StartDate.AddDays(1), WithEmployeeId = buddy.Id }
        };
        await context.ScheduleEvents.InsertManyAsync(scheduleEvents);
    }
}
