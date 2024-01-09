using System.Diagnostics.CodeAnalysis;
using Application.Commands.Account;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Interface;
using AutoMapper;
using Domain.Common.Implementations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Profiles.Account.Resolvers
{
    [ExcludeFromCodeCoverage]
    public class SmsOtpValidEndResolver : IValueResolver<UpdateProfileAccountFirstLoginCommand, Domain.Entities.Identity.Account, DateTime>
    {
        private readonly ISmsService _smsService;
        private readonly SmsConfiguration _smsOptions;
        private readonly IDateTimeService _dateTime;

        public SmsOtpValidEndResolver(ISmsService smsService, IOptions<SmsConfiguration> smsOptions, IDateTimeService dateTime)
        {
            _smsService = smsService;
            _smsOptions = smsOptions.Value;
            _dateTime = dateTime;
        }

        public DateTime Resolve(UpdateProfileAccountFirstLoginCommand source, Domain.Entities.Identity.Account destination,
            DateTime destMember, ResolutionContext context)
        {
            return DateTime.UtcNow.AddSeconds(_smsOptions.Expiration);
        }
    }
}