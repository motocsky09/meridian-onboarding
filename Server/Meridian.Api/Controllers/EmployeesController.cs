using Meridian.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meridian.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    private readonly DashboardService _dashboardService;

    public EmployeesController(EmployeeService employeeService, DashboardService dashboardService)
    {
        _employeeService = employeeService;
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return employee is null ? NotFound() : Ok(employee);
    }

    [HttpGet("{id}/dashboard")]
    public async Task<IActionResult> GetDashboard(string id)
    {
        var dashboard = await _dashboardService.GetDashboardAsync(id);
        return dashboard is null ? NotFound() : Ok(dashboard);
    }
}
