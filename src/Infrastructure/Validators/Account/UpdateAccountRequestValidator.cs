using Application.Common;
using Application.Common.Interfaces;
using Application.DataTransferObjects.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;

namespace Infrastructure.Validators.Account;
public class UpdateAccountRequestValidator: AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator(IStringLocalizationService localizationService)
    {
        RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).VietnamesePhoneNumber(localizationService);
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).Email(localizationService);
        RuleFor(x => x.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(LocalizationString.Common.EmptyField)
            .MinimumLength(Constants.FieldLength.TextMinLength)
            .WithMessage(LocalizationString.Common.MinLengthField)
            .MaximumLength(Constants.FieldLength.TextMaxLength)
            .WithMessage(LocalizationString.Common.MaxLengthField);
        RuleFor(x => x.AvatarPhoto)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(Constants.FieldLength.UrlMaxLength).WithMessage(LocalizationString.Common.MaxLengthField)
            .When(x => !string.IsNullOrEmpty(x.AvatarPhoto));
    }
}
