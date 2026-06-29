using MongoDB.Driver;
using Meridian.Application.Models;

namespace Meridian.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Employee> Employees => _database.GetCollection<Employee>("employees");
    public IMongoCollection<ChecklistItem> ChecklistItems => _database.GetCollection<ChecklistItem>("checklistItems");
    public IMongoCollection<ScheduleEvent> ScheduleEvents => _database.GetCollection<ScheduleEvent>("scheduleEvents");
}
