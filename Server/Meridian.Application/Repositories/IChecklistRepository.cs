using Meridian.Application.Models;

namespace Meridian.Application.Repositories;

public interface IChecklistRepository
{
    Task<List<ChecklistItem>> GetByEmployeeIdAsync(string employeeId);
}
