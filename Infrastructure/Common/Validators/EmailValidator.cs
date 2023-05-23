using Application.Common;
using Application.Common.Interfaces;
using FluentValidation;

namespace Infrastructure.Common.Validators;
public static class EmailValidator
{
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizationService localizationService)
    {
        return ruleBuilder.NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .EmailAddress().WithMessage(localizationService[LocalizationString.Common.IncorrectFormatField].Value);
    }
}
