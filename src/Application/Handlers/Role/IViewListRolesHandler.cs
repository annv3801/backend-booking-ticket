using Application.DTO.Role.Responses;
using Application.Queries.Role;
using Domain.Common.Pagination.OffsetBased;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Role
{
    public interface IViewListRolesHandler: IRequestHandler<ViewListRolesQuery, RequestResult<OffsetPaginationResponse<ViewRoleResponse>>>
    {
        
    }
}