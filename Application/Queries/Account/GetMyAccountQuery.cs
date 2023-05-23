using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;

namespace Application.Queries.Account;
[ExcludeFromCodeCoverage]
public class GetMyAccountQuery : IRequest<Result<ViewAccountResponse>>
{
    public Guid UserId { get; set; }
}
