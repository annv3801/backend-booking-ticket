using Application.DTO.Role.Responses;
using Application.Queries.Role;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Handlers.Role
{
    public interface IViewRoleHandler: IRequestHandler<ViewRoleQuery, RequestResult<ViewRoleResponse>>
    {
        
    }
}