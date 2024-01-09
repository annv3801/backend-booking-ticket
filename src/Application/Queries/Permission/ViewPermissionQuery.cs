using System.Diagnostics.CodeAnalysis;
using Application.DataTransferObjects.Permission.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Permission
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionQuery : IRequest<RequestResult<ViewPermissionResponse>>
    {
        public long PermissionId { get; set; }
    }
}