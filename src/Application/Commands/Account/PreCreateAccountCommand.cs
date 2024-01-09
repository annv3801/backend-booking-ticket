using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class PreCreateAccountCommand : IRequest<RequestResult<CreateAccountResponse>>
{
    public string? PhoneNumber { get; set; }
    public string Password { get; set; }
}
