using System.Diagnostics.CodeAnalysis;
using Application.Common;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Role
{
    [ExcludeFromCodeCoverage]
    public class ActivateRoleCommand : IRequest<RequestResult<RoleResult>>
    {
        public long Id { get; set; }
    }
}