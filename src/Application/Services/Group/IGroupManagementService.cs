using Application.DataTransferObjects.Group.Requests;
using Application.DataTransferObjects.Group.Responses;
using Domain.Common.Pagination.OffsetBased;
using Nobi.Core.Responses;

namespace Application.Services.Group;

public interface IGroupManagementService
{
    Task<RequestResult<bool>> CreateGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken);
    Task<RequestResult<bool>> DeleteGroupAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<GroupResponse>> GetGroupAsync(long id, CancellationToken cancellationToken);
    Task<RequestResult<OffsetPaginationResponse<GroupResponse>>> GetListGroupsAsync(OffsetPaginationRequest request, string type, CancellationToken cancellationToken);
}