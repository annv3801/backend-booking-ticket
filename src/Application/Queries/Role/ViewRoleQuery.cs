using System.Diagnostics.CodeAnalysis;
using Application.DTO.Role.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Role
{
    [ExcludeFromCodeCoverage]
    public class ViewRoleQuery : IRequest<RequestResult<ViewRoleResponse>>
    {
        public long Id { get; set; }
    }
}