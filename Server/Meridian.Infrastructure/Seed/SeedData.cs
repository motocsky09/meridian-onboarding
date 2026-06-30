using Meridian.Application.Models;
using MongoDB.Driver;

namespace Meridian.Infrastructure.Seed;

public static class SeedData
{
    public static async Task SeedAsync(MongoDbContext context)
    {
        var existingCount = await context.Employees.CountDocumentsAsync(_ => true);
        if (existingCount > 0) return;

        // --- Department leads ---
        var engManager = new Employee { FirstName = "Diana", LastName = "Pop", Role = "Engineering Manager", Department = "Engineering", StartDate = DateTime.UtcNow.AddYears(-3) };
        var salesManager = new Employee { FirstName = "Bogdan", LastName = "Radu", Role = "Sales Manager", Department = "Sales", StartDate = DateTime.UtcNow.AddYears(-4) };
        var marketingManager = new Employee { FirstName = "Ioana", LastName = "Marin", Role = "Marketing Manager", Department = "Marketing", StartDate = DateTime.UtcNow.AddYears(-2) };
        var financeManager = new Employee { FirstName = "Cristian", LastName = "Dumitrescu", Role = "Finance Manager", Department = "Finance", StartDate = DateTime.UtcNow.AddYears(-5) };
        var hr = new Employee { FirstName = "Elena", LastName = "Vasilescu", Role = "HR Specialist", Department = "HR", StartDate = DateTime.UtcNow.AddYears(-4) };

        // --- Existing team members (Engineering) ---
        var buddy = new Employee { FirstName = "Mihai", LastName = "Ionescu", Role = "Senior Developer", Department = "Engineering", StartDate = DateTime.UtcNow.AddYears(-2), ManagerId = engManager.Id };
        var eng2 = new Employee { FirstName = "Alexandra", LastName = "Stoica", Role = "Frontend Developer", Department = "Engineering", StartDate = DateTime.UtcNow.AddMonths(-18), ManagerId = engManager.Id };
        var eng3 = new Employee { FirstName = "Radu", LastName = "Constantin", Role = "Backend Developer", Department = "Engineering", StartDate = DateTime.UtcNow.AddMonths(-10), ManagerId = engManager.Id };
        var eng4 = new Employee { FirstName = "Larisa", LastName = "Nistor", Role = "QA Engineer", Department = "Engineering", StartDate = DateTime.UtcNow.AddMonths(-14), ManagerId = engManager.Id };

        // --- Existing team members (Sales) ---
        var sales2 = new Employee { FirstName = "Vlad", LastName = "Tudor", Role = "Account Executive", Department = "Sales", StartDate = DateTime.UtcNow.AddYears(-2), ManagerId = salesManager.Id };
        var sales3 = new Employee { FirstName = "Simona", LastName = "Barbu", Role = "Sales Representative", Department = "Sales", StartDate = DateTime.UtcNow.AddMonths(-8), ManagerId = salesManager.Id };

        // --- Existing team members (Marketing) ---
        var mkt2 = new Employee { FirstName = "Cosmin", LastName = "Florea", Role = "Content Strategist", Department = "Marketing", StartDate = DateTime.UtcNow.AddMonths(-16), ManagerId = marketingManager.Id };
        var mkt3 = new Employee { FirstName = "Raluca", LastName = "Gheorghe", Role = "Social Media Specialist", Department = "Marketing", StartDate = DateTime.UtcNow.AddMonths(-6), ManagerId = marketingManager.Id };

        // --- Existing team members (Finance) ---
        var fin2 = new Employee { FirstName = "Andreea", LastName = "Lupu", Role = "Financial Analyst", Department = "Finance", StartDate = DateTime.UtcNow.AddYears(-1), ManagerId = financeManager.Id };

        var establishedEmployees = new List<Employee>
        {
            engManager, salesManager, marketingManager, financeManager, hr,
            buddy, eng2, eng3, eng4,
            sales2, sales3,
            mkt2, mkt3,
            fin2
        };

        // --- New hires (consistent with "2-3 hires per month") ---
        var newEmployee1 = new Employee
        {
            FirstName = "Andrei",
            LastName = "Motoc",
            Role = "Software Developer",
            Department = "Engineering",
            StartDate = DateTime.UtcNow.Date.AddDays(1),
            ManagerId = engManager.Id,
            BuddyId = buddy.Id
        };

        var newEmployee2 = new Employee
        {
            FirstName = "Paula",
            LastName = "Anton",
            Role = "Sales Representative",
            Department = "Sales",
            StartDate = DateTime.UtcNow.Date.AddDays(1),
            ManagerId = salesManager.Id,
            BuddyId = sales2.Id
        };

        var newEmployee3 = new Employee
        {
            FirstName = "Daniel",
            LastName = "Cojocaru",
            Role = "Marketing Coordinator",
            Department = "Marketing",
            StartDate = DateTime.UtcNow.Date.AddDays(1),
            ManagerId = marketingManager.Id,
            BuddyId = mkt2.Id
        };

        var newEmployees = new List<Employee> { newEmployee1, newEmployee2, newEmployee3 };

        await context.Employees.InsertManyAsync(establishedEmployees.Concat(newEmployees));

        // --- Checklist + schedule for each new hire ---
        foreach (var newEmployee in newEmployees)
        {
            var managerId = newEmployee.ManagerId!;
            var buddyId = newEmployee.BuddyId!;

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
                new() { EmployeeId = newEmployee.Id, Title = "Welcome meeting", Type = "Meeting", Date = newEmployee.StartDate, WithEmployeeId = managerId },
                new() { EmployeeId = newEmployee.Id, Title = "HR orientation", Type = "Meeting", Date = newEmployee.StartDate, WithEmployeeId = hr.Id },
                new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate },
                new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate.AddDays(1) },
                new() { EmployeeId = newEmployee.Id, Title = "Office day", Type = "OfficeDay", Date = newEmployee.StartDate.AddDays(2) },
                new() { EmployeeId = newEmployee.Id, Title = "Remote day", Type = "RemoteDay", Date = newEmployee.StartDate.AddDays(3) },
                new() { EmployeeId = newEmployee.Id, Title = "Remote day", Type = "RemoteDay", Date = newEmployee.StartDate.AddDays(4) },
                new() { EmployeeId = newEmployee.Id, Title = "1:1 with buddy", Type = "Meeting", Date = newEmployee.StartDate.AddDays(1), WithEmployeeId = buddyId }
            };
            await context.ScheduleEvents.InsertManyAsync(scheduleEvents);
        }
    }
}
