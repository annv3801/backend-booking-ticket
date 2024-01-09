using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DataTransferObjects.Account.Responses;
[ExcludeFromCodeCoverage]
public class ViewAccountResponse
{
    public long Id { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public string? UserName { get; set; }
    public string? AvatarPhoto { get; set; }
    public string? FullName { get; set; }
    public bool Gender { get; set; } = true;
    public AccountStatus Status { get; set; }
    public string PhoneNumber { get; set; }
    public string? Supplier { get; set; }
    public string? Name { get; set; }
}
