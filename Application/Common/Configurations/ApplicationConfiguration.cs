using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Configurations;
[ExcludeFromCodeCoverage]
public class ApplicationConfiguration
{
    public int MaximumAccountActivationFailedCount { get; set; } = 10;
    public int MaximumOtpPerDay { get; set; } = 10;
    public bool EnableRegistrationWithOtp { get; set; } = true;
    public bool EnableRecaptchaWithGuest { get; set; } = true;
    public int LockoutDurationInMin { get; set; } = 30;
    public int LockoutLimit { get; set; } = 5;
}
