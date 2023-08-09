using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Account.Requests;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Infrastructure.Validators.Account;
public class SignInWithPhoneNumberValidator : AbstractValidator<SignInWithPhoneNumberRequest>
{
    public SignInWithPhoneNumberValidator(IStringLocalizationService localizationService,
        IOptions<ApplicationConfiguration> applicationConfiguration)
    {
        RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty()
            .WithMessage(localizationService[LocalizationString.Common.EmptyField])
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);

        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty()
            .WithMessage(localizationService[LocalizationString.Common.EmptyField])
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
    }
}
