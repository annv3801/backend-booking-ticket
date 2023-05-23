using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common.Configurations;
using AutoMapper;
using Domain.Enums;
using Microsoft.Extensions.Options;

namespace Infrastructure.Profiles.Resolvers;
[ExcludeFromCodeCoverage]
public class AccountDefaultStatusResolver : IValueResolver<CreateAccountCommand, Domain.Entities.Identity.Account, AccountStatus>
{
    private readonly IOptions<ApplicationConfiguration> _appOptions;

    public AccountDefaultStatusResolver(IOptions<ApplicationConfiguration> appOptions)
    {
        _appOptions = appOptions;
    }

    public AccountStatus Resolve(CreateAccountCommand source, Domain.Entities.Identity.Account destination, AccountStatus destMember, ResolutionContext context)
    {
        return _appOptions.Value.EnableRegistrationWithOtp
            ? AccountStatus.Active
            : AccountStatus.Active;
    }
}
