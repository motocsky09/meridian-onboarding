using Meridian.Application.Repositories;

namespace Meridian.Application.Services;

public class ChecklistService
{
    private readonly IChecklistRepository _repository;

    public ChecklistService(IChecklistRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ToggleCompletedAsync(string itemId) => await _repository.ToggleCompletedAsync(itemId);
}
