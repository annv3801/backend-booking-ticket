using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberCommand: IRequest<Result<SignInWithPhoneNumberResponse>>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}