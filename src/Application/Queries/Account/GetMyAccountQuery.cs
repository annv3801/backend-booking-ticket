using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class GetMyAccountQuery : IRequest<RequestResult<ViewAccountResponse>>
{
    public Guid UserId { get; set; }
}
