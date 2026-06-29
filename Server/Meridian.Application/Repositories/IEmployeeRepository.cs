using Meridian.Application.Models;

namespace Meridian.Application.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(string id);
    Task<List<Employee>> GetAllAsync();
    Task CreateAsync(Employee employee);
}
