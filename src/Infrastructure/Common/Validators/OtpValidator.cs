using Application.Common;
using Application.Common.Interfaces;
using FluentValidation;

namespace Infrastructure.Common.Validators;
public static class OtpValidator
{
    public static IRuleBuilderOptions<T, string> Otp<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizationService localizationService)
    {
        return ruleBuilder
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MaximumLength(6).WithMessage(localizationService[LocalizationString.Common.MaxLengthField])
            .Matches("\\d{6}")
            .WithMessage(localizationService[LocalizationString.Common.IncorrectFormatField].Value);
    }
}
