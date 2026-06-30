using Meridian.Application.Models;

namespace Meridian.Application.Repositories;

public interface IScheduleRepository
{
    Task<List<ScheduleEvent>> GetByEmployeeIdAsync(string employeeId);
}
