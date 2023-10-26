using System.Diagnostics.CodeAnalysis;
using Domain.Common.Attributes;
using Domain.Common.Entities;
using Domain.Enums;

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class Account : Entity<long>
{
    public long Id { get; set; }
    [Sortable]  public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? AvatarPhoto { get; set; }
    public string? FullName { get; set; }
    public bool PasswordChangeRequired { get; set; } = false;
    public DateTime? PasswordValidUntilDate { get; set; }
    public string? PasswordHashTemporary { get; set; }
    public bool Gender { get; set; } = true;
    public AccountStatus Status { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public ICollection<AccountLogin>? AccountLogins { get; set; } = new List<AccountLogin>();
    public ICollection<AccountToken> AccountTokens { get; set; } = new List<AccountToken>();
    public string? Otp { get; set; } = "000000";
    public DateTime OtpValidEnd { get; set; }
    public int OtpCount { get; set; } = 0;
}
