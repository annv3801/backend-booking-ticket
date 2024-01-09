using Application.Commands.Permission;
using Application.Common;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Permission
{
    public interface IUpdatePermissionHandler: IRequestHandler<UpdatePermissionCommand, RequestResult<PermissionResult>>
    {
        
    }
}