using Application.DataTransferObjects.Permission.Responses;
using Application.Queries.Permission;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Permission
{
    public interface IViewPermissionHandler : IRequestHandler<ViewPermissionQuery, RequestResult<ViewPermissionResponse>>
    {
    }
}