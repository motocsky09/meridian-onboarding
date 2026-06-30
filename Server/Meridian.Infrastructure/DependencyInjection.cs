using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meridian.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoSettings>()
            ?? throw new InvalidOperationException("MongoSettings section is missing from configuration.");

        services.AddSingleton(mongoSettings);
        services.AddSingleton<MongoDbContext>();
        services.AddScoped<Meridian.Application.Repositories.IEmployeeRepository, Repositories.EmployeeRepository>();
        services.AddScoped<Meridian.Application.Repositories.IChecklistRepository, Repositories.ChecklistRepository>();
        services.AddScoped<Meridian.Application.Repositories.IScheduleRepository, Repositories.ScheduleRepository>();

        return services;
    }
}
