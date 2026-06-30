using Meridian.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Meridian.Api.Controllers;

[ApiController]
[Route("api/checklist")]
public class ChecklistController : ControllerBase
{
    private readonly ChecklistService _checklistService;

    public ChecklistController(ChecklistService checklistService)
    {
        _checklistService = checklistService;
    }

    [HttpPatch("{itemId}/toggle")]
    public async Task<IActionResult> ToggleCompleted(string itemId)
    {
        var success = await _checklistService.ToggleCompletedAsync(itemId);
        return success ? NoContent() : NotFound();
    }
}
