using Meridian.Application.DTOs;
using Meridian.Application.Repositories;

namespace Meridian.Application.Services;

public class DashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IChecklistRepository _checklistRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public DashboardService(
        IEmployeeRepository employeeRepository,
        IChecklistRepository checklistRepository,
        IScheduleRepository scheduleRepository)
    {
        _employeeRepository = employeeRepository;
        _checklistRepository = checklistRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<DashboardDto?> GetDashboardAsync(string employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null) return null;

        PersonDto? manager = null;
        if (!string.IsNullOrEmpty(employee.ManagerId))
        {
            var managerEntity = await _employeeRepository.GetByIdAsync(employee.ManagerId);
            if (managerEntity is not null)
                manager = new PersonDto { Id = managerEntity.Id, FullName = $"{managerEntity.FirstName} {managerEntity.LastName}", Role = managerEntity.Role };
        }

        PersonDto? buddy = null;
        if (!string.IsNullOrEmpty(employee.BuddyId))
        {
            var buddyEntity = await _employeeRepository.GetByIdAsync(employee.BuddyId);
            if (buddyEntity is not null)
                buddy = new PersonDto { Id = buddyEntity.Id, FullName = $"{buddyEntity.FirstName} {buddyEntity.LastName}", Role = buddyEntity.Role };
        }

        var checklistItems = await _checklistRepository.GetByEmployeeIdAsync(employeeId);
        var checklistByStage = checklistItems
            .GroupBy(c => c.Stage)
            .ToDictionary(
                g => g.Key,
                g => g.Select(c => new ChecklistItemDto { Id = c.Id, Title = c.Title, Description = c.Description, Completed = c.Completed }).ToList()
            );

        var scheduleEvents = await _scheduleRepository.GetByEmployeeIdAsync(employeeId);
        var scheduleDtos = new List<ScheduleEventDto>();
        foreach (var ev in scheduleEvents.OrderBy(e => e.Date))
        {
            string? withName = null;
            if (!string.IsNullOrEmpty(ev.WithEmployeeId))
            {
                var withEntity = await _employeeRepository.GetByIdAsync(ev.WithEmployeeId);
                if (withEntity is not null) withName = $"{withEntity.FirstName} {withEntity.LastName}";
            }

            scheduleDtos.Add(new ScheduleEventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Type = ev.Type,
                Date = ev.Date,
                WithPersonName = withName
            });
        }

        return new DashboardDto
        {
            Employee = new EmployeeSummaryDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = employee.Role,
                Department = employee.Department,
                StartDate = employee.StartDate
            },
            Manager = manager,
            Buddy = buddy,
            ChecklistByStage = checklistByStage,
            Schedule = scheduleDtos
        };
    }
}
