using Application.Common;
using Application.Common.Interfaces;
using Application.Repositories.Account;
using Domain.Extensions;
using FluentValidation;

namespace Infrastructure.Validators.Account;
public class CreateAccountEntityValidator : AbstractValidator<Domain.Entities.Identity.Account>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountEntityValidator(IStringLocalizationService localizationService, IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
        // Phone number must be unique
        RuleFor(u => u.PhoneNumber).MustAsync(IsUniquePhoneNumber)
            .WithMessage(localizationService[LocalizationString.Common.DuplicatedField].Value);
    }

    private async Task<bool> IsUniquePhoneNumber(string? phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (phoneNumber.IsMissing()) return true;
        var existedAccount = await _accountRepository.GetAccountByPhoneNumberAsync(phoneNumber, cancellationToken);
        return existedAccount == null;
    }
}
