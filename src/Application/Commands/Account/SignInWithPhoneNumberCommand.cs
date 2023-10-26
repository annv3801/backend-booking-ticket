using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberCommand: IRequest<RequestResult<SignInWithPhoneNumberResponse>>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}