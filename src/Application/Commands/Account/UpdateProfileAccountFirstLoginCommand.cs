using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Nobi.Core.Responses;

namespace Application.Commands.Account;
[ExcludeFromCodeCoverage]
public class UpdateProfileAccountFirstLoginCommand : IRequest<RequestResult<AccountResult>>
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string? AvatarPhoto { get; set; }
    [NotMapped]
    public IFormFile? AvatarPhotoFile { get; set; }
    public bool? Gender { get; set; }
}
