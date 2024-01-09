using Application.Commands.Role;
using Application.Common;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Role
{
    public interface ICreateRoleHandler:IRequestHandler<CreateRoleCommand, RequestResult<RoleResult>>
    {
        
    }
}