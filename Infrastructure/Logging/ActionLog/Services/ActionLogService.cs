using Application.DataTransferObjects.ActionLog.Requests;
using Application.Logging.ActionLog.Services;

namespace Infrastructure.Logging.ActionLog.Services;
public class ActionLogService : IActionLogService
{
    public async Task LogSucceededEventAsync(CreateActionLogRequest createActionLogRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        Console.WriteLine("LogSucceededEventAsync");
    }

    public async Task LogFailedEventAsync(CreateActionLogRequest createActionLogRequest, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        Console.WriteLine("LogFailedEventAsync");
    }
}
