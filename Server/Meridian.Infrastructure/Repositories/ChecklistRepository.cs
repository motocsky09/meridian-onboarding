using Meridian.Application.Models;
using Meridian.Application.Repositories;
using MongoDB.Driver;

namespace Meridian.Infrastructure.Repositories;

public class ChecklistRepository : IChecklistRepository
{
    private readonly MongoDbContext _context;

    public ChecklistRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChecklistItem>> GetByEmployeeIdAsync(string employeeId) =>
        await _context.ChecklistItems.Find(c => c.EmployeeId == employeeId).ToListAsync();
}
