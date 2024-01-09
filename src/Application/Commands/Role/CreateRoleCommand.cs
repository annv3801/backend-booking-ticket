using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.DataTransferObjects.Role.Requests;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Role
{
    [ExcludeFromCodeCoverage]
    public class CreateRoleCommand : CreateRoleRequest, IRequest<RequestResult<RoleResult>>
    {
    }
}