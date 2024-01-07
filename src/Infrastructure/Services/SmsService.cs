using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common.Interface;
using Microsoft.Extensions.Options;
using Nobi.Core.Responses;

#pragma warning disable 8604

namespace Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class SmsService : ISmsService
    {
        private readonly ILoggerService _loggerService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptions<SmsConfiguration> _smsOption;
        private readonly IStringLocalizationService _localizationService;


        public SmsService(ILoggerService loggerService, IHttpClientFactory clientFactory,
            IOptions<SmsConfiguration> smsOption, IStringLocalizationService localizationService)
        {
            _loggerService = loggerService;
            _clientFactory = clientFactory;
            _smsOption = smsOption;
            _localizationService = localizationService;
        }

        ///<inheritdoc cref="ISmsService.SendOtpAsync"/>
        public async Task<RequestResult<SmsResult>> SendOtpAsync(string? phoneNumber, string? otp,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var message = _localizationService[_smsOption.Value.OtpMessage, otp ?? string.Empty,
                    _smsOption.Value.Expiration, _smsOption.Value.WebsiteUrl].Value;
                var client = _clientFactory.CreateClient();
                var payload = new StringContent(JsonSerializer.Serialize(new
                {
                    Phone = phoneNumber,
                    // ReSharper disable once RedundantAnonymousTypePropertyName
                    ApiKey = _smsOption.Value.ApiKey,
                    SecretKey = _smsOption.Value.ApiSecret,
                    Content = message,
                    Brandname = _smsOption.Value.BranchName,
                    // ReSharper disable once RedundantAnonymousTypePropertyName
                    SmsType = _smsOption.Value.SmsType
                }, Constants.JsonSerializerOptions), Encoding.UTF8, Constants.MimeTypes.Application.Json);
                Console.WriteLine(payload);
                using var response = await client.PostAsync(_smsOption.Value.OtpUrl, payload, cancellationToken);
                response.EnsureSuccessStatusCode();
                // Todo: May need to dispatch an sent OTP event
                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                _loggerService.LogTrace("Sent message to {0} with otp: {1} --> {2}", phoneNumber, otp, responseString);
                return RequestResult<SmsResult>.Succeed();
                
            }
            catch (Exception e)
            {
                _loggerService.LogError(e, e.ToString());
                throw;
            }
        }
        
        ///<inheritdoc cref="ISmsService.GenerateOtpAsync"/>
        public async Task<RequestResult<SmsResult>> GenerateOtpAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var random = new Random();
                await Task.CompletedTask;
                return RequestResult<SmsResult>.Succeed(data: new SmsResult() {Otp = random.Next(100000, 999999).ToString()});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}