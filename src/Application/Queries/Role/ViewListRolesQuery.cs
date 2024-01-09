using System.Diagnostics.CodeAnalysis;
using Application.DTO.Role.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Role
{
    [ExcludeFromCodeCoverage]
    public class ViewListRolesQuery : OffsetPaginationRequest, IRequest<RequestResult<OffsetPaginationResponse<ViewRoleResponse>>>
    {
    }
}