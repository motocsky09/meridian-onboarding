using Meridian.Application.Models;
using Meridian.Application.Repositories;
using MongoDB.Driver;

namespace Meridian.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MongoDbContext _context;

    public EmployeeRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(string id) =>
        await _context.Employees.Find(e => e.Id == id).FirstOrDefaultAsync();

    public async Task<List<Employee>> GetAllAsync() =>
        await _context.Employees.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Employee employee) =>
        await _context.Employees.InsertOneAsync(employee);
}
