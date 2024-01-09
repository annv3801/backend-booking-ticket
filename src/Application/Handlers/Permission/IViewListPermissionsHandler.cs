using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using Domain.Common.Pagination.OffsetBased;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Permission
{
    public interface IViewListPermissionsHandler : IRequestHandler<ViewListPermissionsQuery, RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>>
    {
    }
}