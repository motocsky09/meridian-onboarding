using Meridian.Application.Models;
using Meridian.Application.Repositories;

namespace Meridian.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Employee?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task<List<Employee>> GetAllAsync() => await _repository.GetAllAsync();
}
