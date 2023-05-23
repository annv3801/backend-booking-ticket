using Application.DataTransferObjects.ActionLog.Requests;

namespace Application.Logging.ActionLog.Services;
public interface IActionLogService
{
    Task LogSucceededEventAsync(CreateActionLogRequest createActionLogRequest, CancellationToken cancellationToken = default(CancellationToken));
}
