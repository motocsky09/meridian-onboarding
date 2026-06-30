using Meridian.Application.Models;
using Meridian.Application.Repositories;
using MongoDB.Driver;

namespace Meridian.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly MongoDbContext _context;

    public ScheduleRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<ScheduleEvent>> GetByEmployeeIdAsync(string employeeId) =>
        await _context.ScheduleEvents.Find(s => s.EmployeeId == employeeId).ToListAsync();
}
