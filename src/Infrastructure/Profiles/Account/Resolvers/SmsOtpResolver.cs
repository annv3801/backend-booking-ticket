using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common.Interfaces;
using AutoMapper;

namespace Infrastructure.Profiles.Account.Resolvers
{
    [ExcludeFromCodeCoverage]
    public class SmsOtpResolver : IValueResolver<CreateAccountCommand, Domain.Entities.Identity.Account, string?>
    {
        private readonly ISmsService _smsService;

        public SmsOtpResolver(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public string Resolve(CreateAccountCommand source, Domain.Entities.Identity.Account destination,
            string? destMember, ResolutionContext context)
        {
            return _smsService.GenerateOtpAsync(CancellationToken.None).Result.Data.Otp;
        }
    }
}