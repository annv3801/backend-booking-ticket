using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DataTransferObjects.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;
using Microsoft.Extensions.Options;

namespace Infrastructure.Validators.Account;
public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator(IStringLocalizationService localizationService, IOptions<PasswordOptions> passOption)
    {
        RuleFor(x => x.CurrentPassword).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
        RuleFor(x => x.NewPassword).Cascade(CascadeMode.Stop)
            .Password(localizationService, passOption.Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
        RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
            .Password(localizationService, passOption.Value)
            .Equal(x => x.NewPassword).WithMessage(LocalizationString.PasswordValidation.FailedToConfirmPassword)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
    }
}
