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

    public async Task<bool> ToggleCompletedAsync(string itemId)
    {
        var item = await _context.ChecklistItems.Find(c => c.Id == itemId).FirstOrDefaultAsync();
        if (item is null) return false;

        var update = Builders<ChecklistItem>.Update.Set(c => c.Completed, !item.Completed);
        var result = await _context.ChecklistItems.UpdateOneAsync(c => c.Id == itemId, update);
        return result.ModifiedCount > 0;
    }
}
