using Meridian.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Meridian.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EmployeeService>();
        services.AddScoped<DashboardService>();
        return services;
    }
}
