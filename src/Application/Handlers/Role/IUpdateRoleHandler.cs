using Application.Commands.Role;
using Application.Common;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Role
{
    public interface IUpdateRoleHandler: IRequestHandler<UpdateRoleCommand, RequestResult<RoleResult>>
    {
    }
}