using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Permission.Responses;
using Domain.Common.Pagination.OffsetBased;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Permission
{
    [ExcludeFromCodeCoverage]
    public class ViewListPermissionsQuery : OffsetPaginationRequest, IRequest<RequestResult<OffsetPaginationResponse<ViewPermissionResponse>>>
    {
    }
}